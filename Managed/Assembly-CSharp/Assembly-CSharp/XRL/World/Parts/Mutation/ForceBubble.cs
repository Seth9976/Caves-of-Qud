using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E9 RID: 5097
	[Serializable]
	public class ForceBubble : BaseMutation
	{
		// Token: 0x0600C520 RID: 50464 RVA: 0x0047C2D7 File Offset: 0x0047A4D7
		public ForceBubble()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C521 RID: 50465 RVA: 0x0047C308 File Offset: 0x0047A508
		public void Validate()
		{
			if (this.CurrentField.Count <= 0)
			{
				return;
			}
			ForceBubble.RemovalList.Clear();
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				if (!GameObject.Validate(keyValuePair.Value))
				{
					ForceBubble.RemovalList.Add(keyValuePair.Key);
				}
			}
			foreach (string text in ForceBubble.RemovalList)
			{
				this.CurrentField.Remove(text);
			}
			if (this.CurrentField.Count == 0)
			{
				this.DestroyBubble(true);
			}
		}

		// Token: 0x0600C522 RID: 50466 RVA: 0x0047C3E8 File Offset: 0x0047A5E8
		public bool IsActive()
		{
			if (this.CurrentField.Count > 0)
			{
				this.Validate();
				return this.CurrentField.Count > 0;
			}
			return false;
		}

		// Token: 0x0600C523 RID: 50467 RVA: 0x0047C410 File Offset: 0x0047A610
		public bool IsSuspended()
		{
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			this.Validate();
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator = this.CurrentField.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CurrentCell != null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600C524 RID: 50468 RVA: 0x0047C494 File Offset: 0x0047A694
		public bool IsAnySuspended()
		{
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			this.Validate();
			if (this.CurrentField.Count <= 0)
			{
				return false;
			}
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator = this.CurrentField.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CurrentCell == null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600C525 RID: 50469 RVA: 0x0047C518 File Offset: 0x0047A718
		public void DestroyBubble(bool Validated = false)
		{
			if (!Validated)
			{
				this.Validate();
			}
			base.MyActivatedAbility(this.ActivatedAbilityID, null).ToggleState = false;
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				gameObject.Obliterate(null, false, null);
			}
			this.CurrentField.Clear();
			if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("The {{B|force bubble}} snaps off.", null, true);
				return;
			}
			if (IComponent<GameObject>.Visible(this.ParentObject))
			{
				IComponent<GameObject>.AddPlayerMessage("The {{B|force bubble}} around " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " snaps off.", null, true);
			}
		}

		// Token: 0x0600C526 RID: 50470 RVA: 0x0047C600 File Offset: 0x0047A800
		public int GetPushForce()
		{
			return 5000 + base.Level * 500;
		}

		// Token: 0x0600C527 RID: 50471 RVA: 0x0047C614 File Offset: 0x0047A814
		public int CreateBubble()
		{
			this.Validate();
			int num = 0;
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return num;
			}
			if (currentCell.ParentZone.IsWorldMap())
			{
				return num;
			}
			string[] directionList = Directions.DirectionList;
			int i = 0;
			while (i < directionList.Length)
			{
				string text = directionList[i];
				Cell cellFromDirection = currentCell.GetCellFromDirection(text, false);
				if (!this.CurrentField.ContainsKey(text))
				{
					goto IL_008A;
				}
				GameObject gameObject = this.CurrentField[text];
				if (gameObject.CurrentCell != cellFromDirection)
				{
					gameObject.Obliterate(null, false, null);
					this.CurrentField.Remove(text);
					goto IL_008A;
				}
				IL_026B:
				i++;
				continue;
				IL_008A:
				if (cellFromDirection != null && this.IsRealityDistortionBased && !IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cellFromDirection, this.ParentObject, null, this, 0))
				{
					goto IL_026B;
				}
				GameObject gameObject2 = GameObject.Create(this.Blueprint);
				Forcefield part = gameObject2.GetPart<Forcefield>();
				if (part != null)
				{
					part.Creator = this.ParentObject;
					part.MovesWithOwner = true;
					part.RejectOwner = false;
				}
				ExistenceSupport existenceSupport = gameObject2.RequirePart<ExistenceSupport>(false);
				existenceSupport.SupportedBy = this.ParentObject;
				existenceSupport.ValidateEveryTurn = true;
				Phase.carryOver(this.ParentObject, gameObject2);
				this.CurrentField.Add(text, gameObject2);
				if (cellFromDirection != null)
				{
					cellFromDirection.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (cellFromDirection == null || gameObject2.CurrentCell != cellFromDirection)
				{
					goto IL_026B;
				}
				num++;
				if (!The.Game.InForceFieldPush)
				{
					The.Game.InForceFieldPush = true;
					try
					{
						foreach (GameObject gameObject3 in cellFromDirection.GetObjectsWithPartReadonly("Physics"))
						{
							if (gameObject3 != gameObject2 && !gameObject3.HasPart<Forcefield>() && !gameObject3.HasPart<HologramMaterial>() && gameObject3.ConsiderSolidFor(gameObject2, false) && gameObject2.ConsiderSolidFor(gameObject3, false))
							{
								gameObject3.Push(text, this.GetPushForce(), 4, false, true);
							}
						}
						foreach (GameObject gameObject4 in cellFromDirection.GetObjectsWithPartReadonly("Combat"))
						{
							if (gameObject4 != gameObject2 && !gameObject4.HasPart<Forcefield>() && !gameObject4.HasPart<HologramMaterial>() && (part == null || !part.CanPass(gameObject4)) && gameObject4.PhaseMatches(gameObject2))
							{
								gameObject4.Push(text, this.GetPushForce(), 4, false, true);
							}
						}
					}
					finally
					{
						The.Game.InForceFieldPush = false;
					}
					goto IL_026B;
				}
				goto IL_026B;
			}
			IComponent<GameObject>.EmitMessage(this.ParentObject, "A {{B|force bubble}} pops into being around " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, false, false, this.ParentObject, null);
			return num;
		}

		// Token: 0x0600C528 RID: 50472 RVA: 0x0047C914 File Offset: 0x0047AB14
		public void SuspendBubble()
		{
			this.Validate();
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				gameObject.RemoveFromContext(null);
			}
		}

		// Token: 0x0600C529 RID: 50473 RVA: 0x0047C970 File Offset: 0x0047AB70
		public void DesuspendBubble(bool Validated = false)
		{
			if (!Validated)
			{
				this.Validate();
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				this.DestroyBubble(false);
				return;
			}
			if (currentCell.ParentZone != null && currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			ForceBubble.RemovalList.Clear();
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				string key = keyValuePair.Key;
				GameObject value = keyValuePair.Value;
				if (value.CurrentCell == null)
				{
					Cell cellFromDirection = currentCell.GetCellFromDirection(key, false);
					if (cellFromDirection != null)
					{
						cellFromDirection.AddObject(value, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						Forcefield part = value.GetPart<Forcefield>();
						if (value.CurrentCell == cellFromDirection)
						{
							if (The.Game.InForceFieldPush)
							{
								continue;
							}
							The.Game.InForceFieldPush = true;
							try
							{
								foreach (GameObject gameObject in cellFromDirection.GetObjectsWithPartReadonly("Physics"))
								{
									if (gameObject != value && !gameObject.HasPart<Forcefield>() && !gameObject.HasPart<HologramMaterial>() && gameObject.ConsiderSolidFor(value, false) && value.ConsiderSolidFor(gameObject, false))
									{
										gameObject.Push(key, this.GetPushForce(), 4, false, true);
									}
								}
								using (List<GameObject>.Enumerator enumerator2 = cellFromDirection.GetObjectsWithPartReadonly("Combat").GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										GameObject gameObject2 = enumerator2.Current;
										if (gameObject2 != value && !gameObject2.HasPart<Forcefield>() && !gameObject2.HasPart<HologramMaterial>() && (part == null || !part.CanPass(gameObject2)) && gameObject2.PhaseMatches(value))
										{
											gameObject2.Push(key, this.GetPushForce(), 4, false, true);
										}
									}
									continue;
								}
							}
							finally
							{
								The.Game.InForceFieldPush = false;
							}
						}
						value.Obliterate(null, false, null);
						ForceBubble.RemovalList.Add(key);
					}
				}
			}
			foreach (string text in ForceBubble.RemovalList)
			{
				this.CurrentField.Remove(text);
			}
		}

		// Token: 0x0600C52A RID: 50474 RVA: 0x0047CC3C File Offset: 0x0047AE3C
		public void MaintainBubble()
		{
			Phased phased;
			Omniphase omniphase;
			Phase.syncPrep(this.ParentObject, out phased, out omniphase);
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				Phase.sync(this.ParentObject, gameObject, phased, omniphase);
			}
		}

		// Token: 0x0600C52B RID: 50475 RVA: 0x0047CCAC File Offset: 0x0047AEAC
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.CurrentField.Count);
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				Writer.Write(keyValuePair.Key);
				Writer.WriteGameObject(keyValuePair.Value);
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x0600C52C RID: 50476 RVA: 0x0047CD2C File Offset: 0x0047AF2C
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.CurrentField.Clear();
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				GameObject gameObject = Reader.ReadGameObject("forcebubble");
				this.CurrentField.Add(text, gameObject);
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600C52D RID: 50477 RVA: 0x0047CD80 File Offset: 0x0047AF80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetPartyLeaderFollowDistanceEvent>.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x0600C52E RID: 50478 RVA: 0x0047CDD4 File Offset: 0x0047AFD4
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null) && (!this.IsRealityDistortionBased || base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0)) && !this.ParentObject.CurrentCell.AnyAdjacentCell((Cell c) => this.ParentObject.IsAlliedTowards(c.GetCombatObject()), true))
			{
				E.Add(ForceBubble.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C52F RID: 50479 RVA: 0x0047CE58 File Offset: 0x0047B058
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == ForceBubble.COMMAND_NAME)
			{
				if (this.IsActive())
				{
					this.DestroyBubble(false);
				}
				else
				{
					if (this.IsRealityDistortionBased && !this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this), E))
					{
						return false;
					}
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 100, null, null, false);
					base.MyActivatedAbility(this.ActivatedAbilityID, null).ToggleState = true;
					this.Duration = 9 + base.Level * 3 + 1;
					this.CreateBubble();
					base.UseEnergy(1000, "Mental Mutation ForceBubble");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C530 RID: 50480 RVA: 0x0047CF17 File Offset: 0x0047B117
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.DesuspendBubble(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C531 RID: 50481 RVA: 0x0047CF27 File Offset: 0x0047B127
		public override bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			if (this.IsActive())
			{
				E.MinDistance(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C532 RID: 50482 RVA: 0x0047CF40 File Offset: 0x0047B140
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			if (this.CurrentField.ContainsValue(E.Object))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null && currentCell.DistanceTo(E.Object) <= 1)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C533 RID: 50483 RVA: 0x0047CF87 File Offset: 0x0047B187
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.IsActive())
			{
				this.DesuspendBubble(true);
				this.Duration--;
				if (this.Duration <= 0)
				{
					this.DestroyBubble(false);
				}
				else
				{
					this.MaintainBubble();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C534 RID: 50484 RVA: 0x0047CFC5 File Offset: 0x0047B1C5
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.DestroyBubble(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C535 RID: 50485 RVA: 0x0047CFD5 File Offset: 0x0047B1D5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			Registrar.Register("MoveFailed");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C536 RID: 50486 RVA: 0x0047CFF5 File Offset: 0x0047B1F5
		public override string GetDescription()
		{
			return "You generate a forcefield around yourself.";
		}

		// Token: 0x0600C537 RID: 50487 RVA: 0x0047CFFC File Offset: 0x0047B1FC
		public override string GetLevelText(int Level)
		{
			return "Creates a 3x3 forcefield centered on yourself\n" + "Duration: {{rules|" + this.GetDuration(Level).ToString() + "}} rounds\n" + "Cooldown: 100 rounds\n" + "You may fire missile weapons through the forcefield.";
		}

		// Token: 0x0600C538 RID: 50488 RVA: 0x0047D040 File Offset: 0x0047B240
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Duration", this.GetDuration(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C539 RID: 50489 RVA: 0x0047D08E File Offset: 0x0047B28E
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				this.SuspendBubble();
			}
			else if (E.ID == "MoveFailed")
			{
				this.DesuspendBubble(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C53A RID: 50490 RVA: 0x0047D0CA File Offset: 0x0047B2CA
		public int GetCooldown(int Level)
		{
			return 100;
		}

		// Token: 0x0600C53B RID: 50491 RVA: 0x0047D0CE File Offset: 0x0047B2CE
		public int GetDuration(int Level)
		{
			return 9 + Level * 3 + 1;
		}

		// Token: 0x0600C53C RID: 50492 RVA: 0x0047D0D8 File Offset: 0x0047B2D8
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C53D RID: 50493 RVA: 0x0047D0E4 File Offset: 0x0047B2E4
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Force Bubble", ForceBubble.COMMAND_NAME, "Mental Mutations", null, "\t", null, true, false, true, false, this.IsRealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C53E RID: 50494 RVA: 0x0047D132 File Offset: 0x0047B332
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DF1 RID: 19953
		public static readonly string COMMAND_NAME = "CommandForceBubble";

		// Token: 0x04004DF2 RID: 19954
		public string Blueprint = "Forcefield";

		// Token: 0x04004DF3 RID: 19955
		public bool IsRealityDistortionBased = true;

		// Token: 0x04004DF4 RID: 19956
		public int Duration;

		// Token: 0x04004DF5 RID: 19957
		[NonSerialized]
		public Dictionary<string, GameObject> CurrentField = new Dictionary<string, GameObject>(8);

		// Token: 0x04004DF6 RID: 19958
		[NonSerialized]
		public static List<string> RemovalList = new List<string>();
	}
}
