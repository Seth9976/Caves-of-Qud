using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: the effective PushLevel and PushDistance are
	/// increased by the standard power load bonus, i.e. 2 for the standard
	/// overload power load of 400, which means an additional 1000 lbs. of
	/// push force and two cells of push distance, and reality stabilization
	/// penetration is increased by ((power load - 100) / 10), i.e. 30 for
	/// the standard overload power load of 400 (affecting only the capacity
	/// of the device to activate within reality stabilization, not the
	/// ability of emitted force fields to resist reality stabilization).
	/// </remarks>
	// Token: 0x02001030 RID: 4144
	[Serializable]
	public class ForceEmitter : IPoweredPart
	{
		// Token: 0x0600A4F5 RID: 42229 RVA: 0x003C4C0C File Offset: 0x003C2E0C
		public ForceEmitter()
		{
			this.ChargeUse = 500;
			this.IsPowerLoadSensitive = true;
			this.IsRealityDistortionBased = true;
			this.WorksOnHolder = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A4F6 RID: 42230 RVA: 0x003C4C73 File Offset: 0x003C2E73
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A4F7 RID: 42231 RVA: 0x003C4C78 File Offset: 0x003C2E78
		public string MyCommandName()
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				return ForceEmitter.COMMAND_NAME;
			}
			ActivatedAbilityEntry activatedAbility = (this.ParentObject.Equipped ?? this.ParentObject).GetActivatedAbility(this.ActivatedAbilityID);
			return ((activatedAbility != null) ? activatedAbility.Command : null) ?? ForceEmitter.COMMAND_NAME;
		}

		// Token: 0x0600A4F8 RID: 42232 RVA: 0x003C4CD4 File Offset: 0x003C2ED4
		public void Validate()
		{
			if (this.CurrentField.Count > 0)
			{
				ForceEmitter.ToRemove.Clear();
				foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
				{
					if (keyValuePair.Value == null || keyValuePair.Value.IsInvalid())
					{
						ForceEmitter.ToRemove.Add(keyValuePair.Key);
					}
				}
				foreach (string text in ForceEmitter.ToRemove)
				{
					this.CurrentField.Remove(text);
				}
				if (this.CurrentField.Count <= 0)
				{
					this.SyncActivatedAbilityName(null);
				}
			}
		}

		// Token: 0x0600A4F9 RID: 42233 RVA: 0x003C4DC0 File Offset: 0x003C2FC0
		public bool IsActive()
		{
			if (this.CurrentField.Count > 0)
			{
				this.Validate();
				return this.CurrentField.Count > 0;
			}
			return false;
		}

		// Token: 0x0600A4FA RID: 42234 RVA: 0x003C4DE8 File Offset: 0x003C2FE8
		public bool IsSuspended()
		{
			if (this.CurrentField.Count > 0)
			{
				this.Validate();
				if (this.CurrentField.Count > 0)
				{
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
			}
			return false;
		}

		// Token: 0x0600A4FB RID: 42235 RVA: 0x003C4E6C File Offset: 0x003C306C
		public bool IsAnySuspended()
		{
			if (this.CurrentField.Count > 0)
			{
				this.Validate();
				if (this.CurrentField.Count > 0)
				{
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
			}
			return false;
		}

		// Token: 0x0600A4FC RID: 42236 RVA: 0x003C4EEC File Offset: 0x003C30EC
		public void DestroyBubble()
		{
			this.Validate();
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				gameObject.Obliterate(null, false, null);
			}
			this.CurrentField.Clear();
			this.SyncActivatedAbilityName(null);
		}

		// Token: 0x0600A4FD RID: 42237 RVA: 0x003C4F60 File Offset: 0x003C3160
		public int CreateBubble(bool Renew = false)
		{
			this.Validate();
			int num = 0;
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return num;
			}
			Cell currentCell = activePartFirstSubject.CurrentCell;
			if (currentCell == null)
			{
				return num;
			}
			if (currentCell.ParentZone.IsWorldMap())
			{
				return num;
			}
			if (Renew && this.RenewChance <= 0)
			{
				return num;
			}
			int num2 = this.MyPowerLoadLevel();
			int pushForce = this.GetPushForce(num2);
			int pushDistance = this.GetPushDistance(num2);
			foreach (string text in Directions.DirectionList)
			{
				Cell cellFromDirection = currentCell.GetCellFromDirection(text, false);
				if (cellFromDirection != null)
				{
					if (this.CurrentField.ContainsKey(text))
					{
						GameObject gameObject = this.CurrentField[text];
						if (gameObject.CurrentCell == cellFromDirection)
						{
							goto IL_027D;
						}
						gameObject.Obliterate(null, false, null);
						this.CurrentField.Remove(text);
					}
					if ((!this.IsRealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cellFromDirection, activePartFirstSubject, this.ParentObject, null, 0)) && (!Renew || this.RenewChance.in100()))
					{
						GameObject gameObject2 = GameObject.Create(this.Blueprint);
						Forcefield part = gameObject2.GetPart<Forcefield>();
						if (part != null)
						{
							part.Creator = activePartFirstSubject;
							part.MovesWithOwner = true;
							part.RejectOwner = false;
						}
						ExistenceSupport existenceSupport = gameObject2.RequirePart<ExistenceSupport>(false);
						existenceSupport.SupportedBy = this.ParentObject;
						existenceSupport.ValidateEveryTurn = true;
						Phase.carryOver(activePartFirstSubject, gameObject2);
						cellFromDirection.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (gameObject2.CurrentCell == cellFromDirection)
						{
							this.CurrentField[text] = gameObject2;
							num++;
							foreach (GameObject gameObject3 in cellFromDirection.GetObjectsWithPartReadonly("Physics"))
							{
								if (gameObject3 != gameObject2 && !gameObject3.HasPart<Forcefield>() && !gameObject3.HasPart<HologramMaterial>() && gameObject3.ConsiderSolidFor(gameObject2, false) && gameObject2.ConsiderSolidFor(gameObject3, false))
								{
									gameObject3.Push(text, pushForce, pushDistance, false, true);
								}
							}
							using (List<GameObject>.Enumerator enumerator = cellFromDirection.GetObjectsWithPartReadonly("Combat").GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									GameObject gameObject4 = enumerator.Current;
									if (gameObject4 != gameObject2 && !gameObject4.HasPart<Forcefield>() && !gameObject4.HasPart<HologramMaterial>() && (part == null || !part.CanPass(gameObject4)) && gameObject4.PhaseMatches(gameObject2))
									{
										gameObject4.Push(text, pushForce, pushDistance, false, true);
									}
								}
								goto IL_027D;
							}
						}
						gameObject2.Obliterate(null, false, null);
					}
				}
				IL_027D:;
			}
			return num;
		}

		// Token: 0x0600A4FE RID: 42238 RVA: 0x003C5218 File Offset: 0x003C3418
		public int GetPushForce(int PowerLoad)
		{
			return 5000 + (this.PushLevel + this.MyPowerLoadBonus(PowerLoad, 100, 150)) * 500;
		}

		// Token: 0x0600A4FF RID: 42239 RVA: 0x003C523B File Offset: 0x003C343B
		public int GetPushDistance(int PowerLoad)
		{
			return this.PushDistance + this.MyPowerLoadBonus(PowerLoad, 100, 150);
		}

		// Token: 0x0600A500 RID: 42240 RVA: 0x003C5254 File Offset: 0x003C3454
		public void SuspendBubble()
		{
			this.Validate();
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				gameObject.RemoveFromContext(null);
			}
		}

		// Token: 0x0600A501 RID: 42241 RVA: 0x003C52B0 File Offset: 0x003C34B0
		public void DesuspendBubble(bool Validated = false)
		{
			if (!Validated)
			{
				this.Validate();
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			Cell cell = ((activePartFirstSubject != null) ? activePartFirstSubject.CurrentCell : null);
			if (cell == null || cell.ParentZone == null || cell.OnWorldMap())
			{
				this.DestroyBubble();
				return;
			}
			ForceEmitter.ToRemove.Clear();
			int num = this.MyPowerLoadLevel();
			int pushForce = this.GetPushForce(num);
			int pushDistance = this.GetPushDistance(num);
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.CurrentField)
			{
				string key = keyValuePair.Key;
				GameObject value = keyValuePair.Value;
				if (value.CurrentCell == null)
				{
					Cell cellFromDirection = cell.GetCellFromDirection(key, false);
					if (cellFromDirection == null)
					{
						value.Obliterate(null, false, null);
						ForceEmitter.ToRemove.Add(key);
					}
					else
					{
						cellFromDirection.AddObject(value, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						Forcefield part = value.GetPart<Forcefield>();
						if (value.CurrentCell == cellFromDirection)
						{
							foreach (GameObject gameObject in cellFromDirection.GetObjectsWithPartReadonly("Physics"))
							{
								if (gameObject != value && !gameObject.HasPart<Forcefield>() && !gameObject.HasPart<HologramMaterial>() && gameObject.ConsiderSolidFor(value, false) && value.ConsiderSolidFor(gameObject, false))
								{
									gameObject.Push(key, pushForce, pushDistance, false, true);
								}
							}
							using (List<GameObject>.Enumerator enumerator2 = cellFromDirection.GetObjectsWithPartReadonly("Combat").GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									GameObject gameObject2 = enumerator2.Current;
									if (gameObject2 != value && !gameObject2.HasPart<Forcefield>() && !gameObject2.HasPart<HologramMaterial>() && (part == null || !part.CanPass(gameObject2)) && gameObject2.PhaseMatches(value))
									{
										gameObject2.Push(key, pushForce, pushDistance, false, true);
									}
								}
								continue;
							}
						}
						value.Obliterate(null, false, null);
						ForceEmitter.ToRemove.Add(key);
					}
				}
			}
			foreach (string text in ForceEmitter.ToRemove)
			{
				this.CurrentField.Remove(text);
			}
		}

		// Token: 0x0600A502 RID: 42242 RVA: 0x003C556C File Offset: 0x003C376C
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

		// Token: 0x0600A503 RID: 42243 RVA: 0x003C55EC File Offset: 0x003C37EC
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.CurrentField.Clear();
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				GameObject gameObject = Reader.ReadGameObject("forcebubble");
				this.CurrentField[text] = gameObject;
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600A504 RID: 42244 RVA: 0x003C563F File Offset: 0x003C383F
		public void CollectStats(Templates.StatCollector stats)
		{
		}

		// Token: 0x0600A505 RID: 42245 RVA: 0x003C5644 File Offset: 0x003C3844
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPartyLeaderFollowDistanceEvent>.ID || ID == PooledEvent<GetRealityStabilizationPenetrationEvent>.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == OnDestroyObjectEvent.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A506 RID: 42246 RVA: 0x003C56E0 File Offset: 0x003C38E0
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A507 RID: 42247 RVA: 0x003C570D File Offset: 0x003C390D
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject && E.Complete)
			{
				this.SetUpActivatedAbility(this.ParentObject.Equipped);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A508 RID: 42248 RVA: 0x003C5740 File Offset: 0x003C3940
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (!this.IsActive() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.ForceFieldAdvisable() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && !this.ParentObject.CurrentCell.AnyAdjacentCell(new Predicate<Cell>(this.ContainsAlly), true))
			{
				E.Add(this.MyCommandName(), 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x003C57D1 File Offset: 0x003C39D1
		public override bool HandleEvent(GetPartyLeaderFollowDistanceEvent E)
		{
			if (this.IsActive())
			{
				E.MinDistance(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A50A RID: 42250 RVA: 0x003C57E9 File Offset: 0x003C39E9
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.WorksOnSelf)
			{
				this.SetUpActivatedAbility(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A50B RID: 42251 RVA: 0x003C5808 File Offset: 0x003C3A08
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.DestroyBubble();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A50C RID: 42252 RVA: 0x003C584C File Offset: 0x003C3A4C
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.DestroyBubble();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A50D RID: 42253 RVA: 0x003C5890 File Offset: 0x003C3A90
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

		// Token: 0x0600A50E RID: 42254 RVA: 0x003C58D8 File Offset: 0x003C3AD8
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.IsActive())
			{
				if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.DestroyBubble();
				}
				else if (this.RenewChance > 0 && this.CurrentField.Count < 8)
				{
					this.CreateBubble(true);
				}
				else
				{
					this.MaintainBubble();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A50F RID: 42255 RVA: 0x003C594B File Offset: 0x003C3B4B
		public override bool HandleEvent(GetRealityStabilizationPenetrationEvent E)
		{
			E.Penetration += this.MyPowerLoadBonus(int.MinValue, 100, 10);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A510 RID: 42256 RVA: 0x003C5970 File Offset: 0x003C3B70
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "BeginMove");
			E.Actor.RegisterPartEvent(this, "EffectApplied");
			E.Actor.RegisterPartEvent(this, "EnteredCell");
			E.Actor.RegisterPartEvent(this, "MoveFailed");
			if (this.ParentObject.Understood())
			{
				this.SetUpActivatedAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A511 RID: 42257 RVA: 0x003C59E4 File Offset: 0x003C3BE4
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "BeginMove");
			E.Actor.UnregisterPartEvent(this, "EffectApplied");
			E.Actor.UnregisterPartEvent(this, "EnteredCell");
			E.Actor.UnregisterPartEvent(this, "MoveFailed");
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			this.DestroyBubble();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A512 RID: 42258 RVA: 0x003C5A54 File Offset: 0x003C3C54
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null && activePartFirstSubject.IsPlayer())
				{
					if (this.IsActive())
					{
						E.AddAction("Deactivate", "deactivate", this.MyCommandName(), null, 'a', false, 10, 0, false, false, false, false, true, null, false);
					}
					else
					{
						E.AddAction("Activate", "activate", this.MyCommandName(), null, 'a', false, 10, 0, false, false, false, false, true, null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A513 RID: 42259 RVA: 0x003C5ADC File Offset: 0x003C3CDC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == this.MyCommandName() && this.ActivateForceEmitter(E))
			{
				E.RequestInterfaceExit();
				E.Actor.UseEnergy(1000, "Item Force Bracelet", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A514 RID: 42260 RVA: 0x003C5B34 File Offset: 0x003C3D34
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.MyCommandName() && this.ActivateForceEmitter(E))
			{
				E.RequestInterfaceExit();
				E.Actor.UseEnergy(1000, "Item Force Bracelet", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A515 RID: 42261 RVA: 0x003C5B8B File Offset: 0x003C3D8B
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.DestroyBubble();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A516 RID: 42262 RVA: 0x003C5B9A File Offset: 0x003C3D9A
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A517 RID: 42263 RVA: 0x003C5B9D File Offset: 0x003C3D9D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnterCell");
			if (this.WorksOnSelf)
			{
				Registrar.Register("BeginMove");
				Registrar.Register("EnteredCell");
				Registrar.Register("MoveFailed");
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A518 RID: 42264 RVA: 0x003C5BDC File Offset: 0x003C3DDC
		private bool ContainsAlly(Cell Cell)
		{
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				if (Cell.Objects[i].IsCombatObject() && this.ParentObject.IsAlliedTowards(Cell.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600A519 RID: 42265 RVA: 0x003C5C30 File Offset: 0x003C3E30
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				this.SuspendBubble();
			}
			else if (E.ID == "EnteredCell" || E.ID == "MoveFailed")
			{
				this.DesuspendBubble(false);
			}
			else if (E.ID == "EffectApplied")
			{
				if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.DestroyBubble();
				}
			}
			else if (E.ID == "EnterCell")
			{
				if (this.StartActive && !this.IsActive())
				{
					this.ActivateForceEmitter(null);
				}
				this.ParentObject.UnregisterPartEvent(this, "EnterCell");
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A51A RID: 42266 RVA: 0x003C5D0C File Offset: 0x003C3F0C
		private bool ForceFieldAdvisable()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			return activePartFirstSubject != null && IComponent<GameObject>.CheckRealityDistortionAdvisability(activePartFirstSubject, null, activePartFirstSubject, this.ParentObject, null, null, 0);
		}

		// Token: 0x0600A51B RID: 42267 RVA: 0x003C5D40 File Offset: 0x003C3F40
		public bool ActivateForceEmitter(IEvent E = null)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return false;
			}
			Cell currentCell = activePartFirstSubject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (!currentCell.BroadcastEvent(Event.New("InitiateForceBubble", "Object", activePartFirstSubject, "Device", this.ParentObject, "Operator", activePartFirstSubject), E))
			{
				return false;
			}
			if (!currentCell.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", activePartFirstSubject, "Device", this.ParentObject, "Operator", activePartFirstSubject), E))
			{
				return false;
			}
			if (this.IsActive())
			{
				if (this.IsSuspended())
				{
					activePartFirstSubject.Fail(this.ParentObject.Does("vibrate", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " slightly.");
				}
				else if (activePartFirstSubject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("The {{B|force bubble}} snaps off.", null, true);
				}
				else if (IComponent<GameObject>.Visible(activePartFirstSubject))
				{
					IComponent<GameObject>.AddPlayerMessage("The {{B|force bubble}} around " + activePartFirstSubject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " snaps off.", null, true);
				}
				this.DestroyBubble();
				return true;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus == ActivePartStatus.Unpowered)
			{
				activePartFirstSubject.Fail(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " have enough charge to sustain the field!");
			}
			else
			{
				if (activePartStatus == ActivePartStatus.Operational && this.CreateBubble(false) > 0)
				{
					activePartFirstSubject.PlayWorldSound("Sounds/Interact/sfx_interact_forceProject_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					if (activePartFirstSubject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("{{G|A {{B|force bubble}} pops into being around you.}}", null, true);
					}
					else if (IComponent<GameObject>.Visible(activePartFirstSubject))
					{
						IComponent<GameObject>.AddPlayerMessage("A {{B|force bubble}} pops into being around " + activePartFirstSubject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
					}
					this.SyncActivatedAbilityName(activePartFirstSubject);
					return true;
				}
				activePartFirstSubject.Fail("Nothing happens.");
			}
			return false;
		}

		// Token: 0x0600A51C RID: 42268 RVA: 0x003C5F98 File Offset: 0x003C4198
		public void SetUpActivatedAbility(GameObject Actor)
		{
			if (Actor != null)
			{
				string activatedAbilityName = this.GetActivatedAbilityName(Actor);
				string text;
				this.ActivatedAbilityID = Actor.AddDynamicCommand(out text, ForceEmitter.COMMAND_NAME, activatedAbilityName, (Actor == this.ParentObject) ? "Maneuvers" : "Items", null, "è", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null);
			}
		}

		// Token: 0x0600A51D RID: 42269 RVA: 0x003C5FF4 File Offset: 0x003C41F4
		public string GetActivatedAbilityName(GameObject Actor = null)
		{
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped ?? this.ParentObject;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.IsActive() ? "Deactivate" : "Activate").Append(' ').Append((Actor == null || Actor == this.ParentObject) ? "Force Emitter" : Grammar.MakeTitleCase(this.ParentObject.BaseDisplayNameStripped));
			return stringBuilder.ToString();
		}

		// Token: 0x0600A51E RID: 42270 RVA: 0x003C6070 File Offset: 0x003C4270
		public void SyncActivatedAbilityName(GameObject Actor = null)
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				return;
			}
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped ?? this.ParentObject;
			}
			Actor.SetActivatedAbilityDisplayName(this.ActivatedAbilityID, this.GetActivatedAbilityName(Actor));
		}

		// Token: 0x0600A51F RID: 42271 RVA: 0x003C60C0 File Offset: 0x003C42C0
		public void MaintainBubble()
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject == null)
			{
				return;
			}
			Phased phased;
			Omniphase omniphase;
			Phase.syncPrep(activePartFirstSubject, out phased, out omniphase);
			foreach (GameObject gameObject in this.CurrentField.Values)
			{
				Phase.sync(activePartFirstSubject, gameObject, phased, omniphase);
			}
		}

		// Token: 0x04004444 RID: 17476
		public static readonly string COMMAND_NAME = "ToggleForceEmitter";

		// Token: 0x04004445 RID: 17477
		public string Blueprint = "Forcefield";

		// Token: 0x04004446 RID: 17478
		public int PushLevel = 5;

		// Token: 0x04004447 RID: 17479
		public int PushDistance = 4;

		// Token: 0x04004448 RID: 17480
		public bool StartActive;

		// Token: 0x04004449 RID: 17481
		public int RenewChance = 10;

		// Token: 0x0400444A RID: 17482
		public Guid ActivatedAbilityID;

		// Token: 0x0400444B RID: 17483
		[NonSerialized]
		public Dictionary<string, GameObject> CurrentField = new Dictionary<string, GameObject>(8);

		// Token: 0x0400444C RID: 17484
		[NonSerialized]
		public static List<string> ToRemove = new List<string>();
	}
}
