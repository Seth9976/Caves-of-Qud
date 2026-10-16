using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, save difficulty to resist being distracted
	/// by the hologram is increased by the standard power load bonus, i.e.
	/// 2 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001013 RID: 4115
	[Serializable]
	public class DecoyHologramEmitter : IPoweredPart
	{
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600A3EB RID: 41963 RVA: 0x003BEA49 File Offset: 0x003BCC49
		// (set) Token: 0x0600A3EC RID: 41964 RVA: 0x003BEA5C File Offset: 0x003BCC5C
		public bool Stationary
		{
			get
			{
				return this.Flags.HasBit(this.FLAG_STATIONARY);
			}
			set
			{
				(ref this.Flags).SetBit(this.FLAG_STATIONARY, value);
			}
		}

		// Token: 0x0600A3ED RID: 41965 RVA: 0x003BEA70 File Offset: 0x003BCC70
		public DecoyHologramEmitter()
		{
			this.ChargeUse = 2;
			this.WorksOnHolder = true;
			this.WorksOnWearer = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A3EE RID: 41966 RVA: 0x003BEAC8 File Offset: 0x003BCCC8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A3EF RID: 41967 RVA: 0x003BEACB File Offset: 0x003BCCCB
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.WriteGameObjectList(this.Holograms);
		}

		// Token: 0x0600A3F0 RID: 41968 RVA: 0x003BEAE1 File Offset: 0x003BCCE1
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			Reader.ReadGameObjectList(this.Holograms, null);
		}

		// Token: 0x0600A3F1 RID: 41969 RVA: 0x003BEAF8 File Offset: 0x003BCCF8
		public void CollectStats(Templates.StatCollector stats)
		{
			if (this.MaxDecoys == 1)
			{
				stats.Set("DecoyNumber", "a", false, 0);
				stats.Set("Decoys", "decoy", false, 0);
			}
			else
			{
				stats.Set("DecoyNumber", this.MaxDecoys, false, 0);
				stats.Set("Decoys", "decoys", false, 0);
			}
			if (this.ChargeUse * this.MaxDecoys >= 100)
			{
				stats.Set("ChargeDraw", "Medium", false, 0);
			}
			else
			{
				stats.Set("ChargeDraw", "Low", false, 0);
			}
			stats.Set("Range", this.Range, false, 0);
		}

		// Token: 0x0600A3F2 RID: 41970 RVA: 0x003BEBA4 File Offset: 0x003BCDA4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<ExamineSuccessEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == OnDestroyObjectEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A3F3 RID: 41971 RVA: 0x003BEC18 File Offset: 0x003BCE18
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "ToggleHologramEmitter" && GameObject.Validate(E.Actor))
			{
				if (this.HologramActive)
				{
					base.PlayWorldSound("Sounds/Interact/sfx_interact_hologramBracelet_deactivate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					this.DestroyHolograms(null, null, false);
					E.Actor.UseEnergy(1000, "Item Deactivation", null, null, false);
					E.RequestInterfaceExit();
				}
				else
				{
					this.ActivateHologramBracelet(E.Actor, E);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3F4 RID: 41972 RVA: 0x003BECC3 File Offset: 0x003BCEC3
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3F5 RID: 41973 RVA: 0x003BECF0 File Offset: 0x003BCEF0
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Object == this.ParentObject && E.Complete)
			{
				this.SetUpActivatedAbility(this.ParentObject.Equipped);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3F6 RID: 41974 RVA: 0x003BED20 File Offset: 0x003BCF20
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (!this.HologramActive && !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && (E.Distance > 1 || 20.in100()))
			{
				E.Add("ActivateHologramBracelet", 2, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3F7 RID: 41975 RVA: 0x003BED8C File Offset: 0x003BCF8C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.WorksOnSelf && this.ParentObject.IsCombatObject())
			{
				this.SetUpActivatedAbility(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3F8 RID: 41976 RVA: 0x003BEDB8 File Offset: 0x003BCFB8
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (!this.HologramActive || !this.Holograms.Contains(E.Object) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A3F9 RID: 41977 RVA: 0x003BEE14 File Offset: 0x003BD014
		public override bool HandleEvent(EquippedEvent E)
		{
			this.DestroyHolograms(null, E.Actor, true);
			E.Actor.RegisterPartEvent(this, "AfterMoved");
			E.Actor.RegisterPartEvent(this, "BeginTakeAction");
			if (this.ParentObject.Understood())
			{
				this.SetUpActivatedAbility(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3FA RID: 41978 RVA: 0x003BEE74 File Offset: 0x003BD074
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "AfterMoved");
			E.Actor.UnregisterPartEvent(this, "BeginTakeAction");
			E.Actor.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			if (this.HologramActive)
			{
				this.DestroyHolograms(null, E.Actor, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3FB RID: 41979 RVA: 0x003BEED2 File Offset: 0x003BD0D2
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.DestroyHolograms(null, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3FC RID: 41980 RVA: 0x003BEEE4 File Offset: 0x003BD0E4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.HologramActive)
			{
				if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) != ActivePartStatus.NeedsSubject)
				{
					E.AddAction("Deactivate", "deactivate", "DeactivateHologramBracelet", null, 'a', false, 10, 0, false, false, false, false, true, null, false);
				}
			}
			else if (base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) != ActivePartStatus.NeedsSubject)
			{
				E.AddAction("Activate", "activate", "ActivateHologramBracelet", null, 'a', false, 10, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3FD RID: 41981 RVA: 0x003BEF9C File Offset: 0x003BD19C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateHologramBracelet")
			{
				this.ActivateHologramBracelet(E.Actor, E);
			}
			else if (E.Command == "DeactivateHologramBracelet" && this.HologramActive)
			{
				base.PlayWorldSound("Sounds/Interact/sfx_interact_hologramBracelet_deactivate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				this.DestroyHolograms(null, E.Actor, true);
				E.Actor.UseEnergy(1000, "Item Deactivation", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3FE RID: 41982 RVA: 0x003BF04C File Offset: 0x003BD24C
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A3FF RID: 41983 RVA: 0x003BF04F File Offset: 0x003BD24F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("BootSequenceInitialized");
			Registrar.Register("EffectApplied");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A400 RID: 41984 RVA: 0x003BF07C File Offset: 0x003BD27C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.HologramActive)
				{
					int num = (int)Math.Ceiling((double)this.ChargeUse * (1.0 * (double)this.Holograms.Count / (double)this.MaxDecoys));
					if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, new int?(num), false, 0L, null))
					{
						this.DestroyHolograms(null, null, false);
					}
				}
			}
			else if (E.ID == "AfterMoved")
			{
				if (this.HologramActive && !this.Stationary)
				{
					for (int i = 0; i < this.Holograms.Count; i++)
					{
						GameObject gameObject = this.Holograms[i];
						if (gameObject.IsInvalid())
						{
							this.DestroyHolograms(gameObject, null, false);
						}
						else
						{
							gameObject.Move(Directions.GetOppositeDirection(E.GetStringParameter("Direction", null)), true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
						}
					}
				}
			}
			else if (E.ID == "BootSequenceInitialized")
			{
				this.DestroyHolograms(null, null, false);
			}
			else if (E.ID == "EffectApplied" && this.HologramActive && base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.DestroyHolograms(null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A401 RID: 41985 RVA: 0x003BF214 File Offset: 0x003BD414
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.ParentObject.OnWorldMap();
		}

		// Token: 0x0600A402 RID: 41986 RVA: 0x003BF226 File Offset: 0x003BD426
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "InvalidContext";
		}

		// Token: 0x0600A403 RID: 41987 RVA: 0x003BF230 File Offset: 0x003BD430
		public static GameObject CreateHologramOf(GameObject Object)
		{
			GameObject gameObject = GameObject.Create("Hologram Distraction");
			gameObject.Render.Tile = Object.Render.Tile;
			gameObject.Render.RenderString = Object.Render.RenderString;
			gameObject.Render.DisplayName = Object.Render.DisplayName;
			gameObject.GetPart<Description>().Short = "Light stammers in parallax to form the image of an object. " + Object.GetPart<Description>().Short;
			gameObject.SetStringProperty("HologramOf", Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false), false);
			if (Object.HasProperName)
			{
				gameObject.SetIntProperty("ProperNoun", 1, false);
			}
			return gameObject;
		}

		// Token: 0x0600A404 RID: 41988 RVA: 0x003BF2F4 File Offset: 0x003BD4F4
		public void PlaceHologram(Cell C, GameObject Who, int Load, int ID)
		{
			GameObject gameObject;
			if (this.Blueprint.IsNullOrEmpty())
			{
				gameObject = DecoyHologramEmitter.CreateHologramOf(Who);
			}
			else
			{
				gameObject = GameObject.Create(this.Blueprint);
			}
			gameObject.RequirePart<ExistenceSupport>(false).SupportedBy = this.ParentObject;
			Distraction distraction;
			if (gameObject.TryGetPart<Distraction>(out distraction))
			{
				distraction.ID = ID;
				distraction.Original = Who;
				distraction.Source = this.ParentObject;
				distraction.SaveTarget = this.Difficulty + IComponent<GameObject>.PowerLoadBonus(Load, 100, 150);
			}
			C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.Holograms.Add(gameObject);
			base.PlayWorldSound("Sounds/Interact/sfx_interact_hologramBracelet_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.EmitMessage(gameObject, "An image of " + gameObject.GetTagOrStringProperty("HologramOf", null) + " appears.", ' ', false, false, false, null, null);
		}

		// Token: 0x0600A405 RID: 41989 RVA: 0x003BF3EC File Offset: 0x003BD5EC
		public ActivePartStatus CreateHolograms(GameObject Who = null)
		{
			if (this.Holograms.Count > 0)
			{
				this.DestroyHolograms(null, Who, false);
			}
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			ActivePartStatus activePartStatus = base.GetActivePartStatus(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				return activePartStatus;
			}
			if (Who == null)
			{
				GameObject gameObject;
				if ((gameObject = this.ParentObject.Equipped) == null)
				{
					Cell currentCell = this.ParentObject.GetCurrentCell();
					gameObject = ((currentCell != null) ? currentCell.GetCombatObject() : null);
				}
				Who = gameObject;
				if (Who == null)
				{
					return ActivePartStatus.LocallyDefinedFailure;
				}
			}
			if (Who.IsPlayer())
			{
				Cell cell = The.PlayerCell;
				int i = 0;
				while (i < this.MaxDecoys)
				{
					cell = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, 1, this.Range, cell.X, cell.Y, false, AllowVis.OnlyVisible, null, null, null, null, "Summon Decoy", false, true);
					if (cell == null)
					{
						break;
					}
					if (Who.DistanceTo(cell) > this.Range)
					{
						Popup.Show(string.Format("That is out of range ({0} {1})", this.Range, this.Range.Things("square", "squares")), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						this.PlaceHologram(cell, Who, num, i);
						i++;
					}
				}
			}
			else
			{
				List<Cell> list = Who.CurrentCell.GetAdjacentCells(2, true, true).ShuffleInPlace<Cell>(null);
				int num4 = 0;
				while (num4 < this.MaxDecoys && num4 < list.Count)
				{
					if (list[num4].IsEmpty())
					{
						this.PlaceHologram(list[num4], Who, num, num4);
					}
					num4++;
				}
			}
			this.HologramActive = this.Holograms.Count > 0;
			return activePartStatus;
		}

		// Token: 0x0600A406 RID: 41990 RVA: 0x003BF5A4 File Offset: 0x003BD7A4
		private bool ActivateHologramBracelet(GameObject Who, IEvent E = null)
		{
			if (Who.OnWorldMap())
			{
				if (Who.IsPlayer())
				{
					Popup.ShowFail("You cannot do that on the world map.", true, true, true);
				}
				return false;
			}
			if (!this.HologramActive)
			{
				ActivePartStatus activePartStatus = this.CreateHolograms(Who);
				if (this.HologramActive)
				{
					Who.UseEnergy(1000, "Item Activation", null, null, false);
					if (E != null)
					{
						E.RequestInterfaceExit();
					}
					this.SyncActivatedAbilityName(Who);
					return true;
				}
				if (Who.IsPlayer())
				{
					if (activePartStatus == ActivePartStatus.Booting && this.ParentObject.GetPart<BootSequence>().IsObvious())
					{
						Popup.Show(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + this.ParentObject.Is + " still starting up.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (activePartStatus == ActivePartStatus.Unpowered)
					{
						Popup.Show(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + this.ParentObject.GetVerb("do", true, false, false, true, false) + " not have enough charge to sustain the hologram.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (activePartStatus != ActivePartStatus.Operational)
					{
						Popup.Show(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + this.ParentObject.Is + " unresponsive.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
			}
			return false;
		}

		// Token: 0x0600A407 RID: 41991 RVA: 0x003BF744 File Offset: 0x003BD944
		public void DestroyHolograms(GameObject Hologram = null, GameObject Who = null, bool FromDialog = false)
		{
			if (Hologram != null)
			{
				if (!Hologram.IsInvalid())
				{
					IComponent<GameObject>.EmitMessage(Hologram, "An image of " + Hologram.GetTagOrStringProperty("HologramOf", null) + " disappears.", ' ', FromDialog, false, false, null, null);
					Hologram.Destroy(null, false, false, null);
				}
				this.Holograms.Remove(Hologram);
			}
			else
			{
				for (int i = this.Holograms.Count - 1; i >= 0; i--)
				{
					Hologram = this.Holograms[i];
					if (!Hologram.IsInvalid())
					{
						IComponent<GameObject>.EmitMessage(Hologram, "An image of " + Hologram.GetTagOrStringProperty("HologramOf", null) + " disappears.", ' ', FromDialog, false, false, null, null);
						Hologram.Destroy(null, false, false, null);
					}
					this.Holograms.RemoveAt(i);
				}
			}
			this.HologramActive = this.Holograms.Count > 0;
			this.SyncActivatedAbilityName(Who);
		}

		// Token: 0x0600A408 RID: 41992 RVA: 0x003BF828 File Offset: 0x003BDA28
		public void SetUpActivatedAbility(GameObject Who)
		{
			if (Who != null)
			{
				this.ActivatedAbilityID = Who.AddActivatedAbility(this.GetActivatedAbilityName(Who), "ToggleHologramEmitter", (Who == this.ParentObject) ? "Maneuvers" : "Items", null, "\u0001", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			}
		}

		// Token: 0x0600A409 RID: 41993 RVA: 0x003BF880 File Offset: 0x003BDA80
		public string GetActivatedAbilityName(GameObject Who = null)
		{
			if (Who == null)
			{
				Who = this.ParentObject.Equipped ?? this.ParentObject;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.HologramActive ? "Deactivate" : "Activate").Append(' ').Append((Who == null || Who == this.ParentObject) ? "Holographic Decoy" : Grammar.MakeTitleCase(this.ParentObject.BaseDisplayNameStripped));
			return stringBuilder.ToString();
		}

		// Token: 0x0600A40A RID: 41994 RVA: 0x003BF8FC File Offset: 0x003BDAFC
		public void SyncActivatedAbilityName(GameObject Who = null)
		{
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				return;
			}
			if (Who == null)
			{
				Who = this.ParentObject.Equipped ?? this.ParentObject;
			}
			Who.SetActivatedAbilityDisplayName(this.ActivatedAbilityID, this.GetActivatedAbilityName(Who));
		}

		// Token: 0x040043DD RID: 17373
		public int FLAG_STATIONARY = 1;

		// Token: 0x040043DE RID: 17374
		public string Blueprint;

		// Token: 0x040043DF RID: 17375
		public int Range = 10;

		// Token: 0x040043E0 RID: 17376
		public int MaxDecoys = 1;

		// Token: 0x040043E1 RID: 17377
		public int Difficulty = 15;

		// Token: 0x040043E2 RID: 17378
		public int Flags;

		// Token: 0x040043E3 RID: 17379
		public bool HologramActive;

		// Token: 0x040043E4 RID: 17380
		[NonSerialized]
		public List<GameObject> Holograms = new List<GameObject>();

		// Token: 0x040043E5 RID: 17381
		public Guid ActivatedAbilityID;
	}
}
