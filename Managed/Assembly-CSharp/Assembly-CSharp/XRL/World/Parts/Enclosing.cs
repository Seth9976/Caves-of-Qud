using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FA4 RID: 4004
	[Serializable]
	public class Enclosing : IPoweredPart
	{
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06009F02 RID: 40706 RVA: 0x0039AF27 File Offset: 0x00399127
		// (set) Token: 0x06009F01 RID: 40705 RVA: 0x0039AF17 File Offset: 0x00399117
		public string AffectedProperties
		{
			get
			{
				return this._AffectedProperties;
			}
			set
			{
				this._AffectedProperties = value;
				this._PropertyMap = null;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06009F03 RID: 40707 RVA: 0x0039AF2F File Offset: 0x0039912F
		public Dictionary<string, int> PropertyMap
		{
			get
			{
				if (this._PropertyMap == null)
				{
					this._PropertyMap = IComponent<GameObject>.MapFromString(this._AffectedProperties);
				}
				return this._PropertyMap;
			}
		}

		// Token: 0x06009F04 RID: 40708 RVA: 0x0039AF50 File Offset: 0x00399150
		public Enclosing()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x06009F05 RID: 40709 RVA: 0x0039AFB4 File Offset: 0x003991B4
		public override bool SameAs(IPart p)
		{
			Enclosing enclosing = p as Enclosing;
			return enclosing.AVBonus == this.AVBonus && enclosing.DVPenalty == this.DVPenalty && enclosing.EnterSaveTarget == this.EnterSaveTarget && enclosing.ExitSaveTarget == this.ExitSaveTarget && enclosing.DamageChance == this.DamageChance && enclosing.EnterDamageChance == this.EnterDamageChance && enclosing.ExitDamageChance == this.ExitDamageChance && enclosing.DamageBloodSplatterChance == this.DamageBloodSplatterChance && enclosing.PeriodicEventTurns1 == this.PeriodicEventTurns1 && enclosing.PeriodicEventTurns2 == this.PeriodicEventTurns2 && enclosing.PeriodicEventTurns3 == this.PeriodicEventTurns3 && enclosing.PeriodicEventLevel1 == this.PeriodicEventLevel1 && enclosing.PeriodicEventLevel2 == this.PeriodicEventLevel2 && enclosing.PeriodicEventLevel3 == this.PeriodicEventLevel3 && !(enclosing.AffectedProperties != this.AffectedProperties) && !(enclosing.EnterSaveStat != this.EnterSaveStat) && !(enclosing.ExitSaveStat != this.ExitSaveStat) && !(enclosing.Damage != this.Damage) && !(enclosing.DamageAttributes != this.DamageAttributes) && !(enclosing.EnterEventSelf != this.EnterEventSelf) && !(enclosing.EnterEventUser != this.EnterEventUser) && !(enclosing.ExitEventSelf != this.ExitEventSelf) && !(enclosing.ExitEventUser != this.ExitEventUser) && !(enclosing.ApplyChangesEventSelf != this.ApplyChangesEventSelf) && !(enclosing.ApplyChangesEventUser != this.ApplyChangesEventUser) && !(enclosing.UnapplyChangesEventSelf != this.UnapplyChangesEventSelf) && !(enclosing.UnapplyChangesEventUser != this.UnapplyChangesEventUser) && !(enclosing.EffectDescriptionPrefix != this.EffectDescriptionPrefix) && !(enclosing.EffectDescriptionPostfix != this.EffectDescriptionPostfix) && !(enclosing.RequiresLiquid != this.RequiresLiquid) && !(enclosing.PeriodicEvent1 != this.PeriodicEvent1) && !(enclosing.PeriodicEvent2 != this.PeriodicEvent2) && !(enclosing.PeriodicEvent3 != this.PeriodicEvent3) && enclosing.SizeInsensitive == this.SizeInsensitive && enclosing.NoDamageWhenDisabled == this.NoDamageWhenDisabled && enclosing.EnterDamageFailOnly == this.EnterDamageFailOnly && enclosing.ExitDamageFailOnly == this.ExitDamageFailOnly && enclosing.PeriodicEventSendSource1 == this.PeriodicEventSendSource1 && enclosing.PeriodicEventSendSource2 == this.PeriodicEventSendSource2 && enclosing.PeriodicEventSendSource3 == this.PeriodicEventSendSource3 && enclosing.PeriodicEventOnSelf1 == this.PeriodicEventOnSelf1 && enclosing.PeriodicEventOnSelf2 == this.PeriodicEventOnSelf2 && enclosing.PeriodicEventOnSelf3 == this.PeriodicEventOnSelf3 && enclosing.PeriodicEventUseGenericNotify1 == this.PeriodicEventUseGenericNotify1 && enclosing.PeriodicEventUseGenericNotify2 == this.PeriodicEventUseGenericNotify2 && enclosing.PeriodicEventUseGenericNotify3 == this.PeriodicEventUseGenericNotify3 && enclosing.ShowGeneralInfoInShortDescription == this.ShowGeneralInfoInShortDescription && !(enclosing.OpenColor != this.OpenColor) && !(enclosing.ClosedColor != this.ClosedColor) && !(enclosing.OpenTileColor != this.OpenTileColor) && !(enclosing.ClosedTileColor != this.ClosedTileColor) && base.SameAs(p);
		}

		// Token: 0x06009F06 RID: 40710 RVA: 0x0039B374 File Offset: 0x00399574
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == BeforeDestroyObjectEvent.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID || ID == LeftCellEvent.ID || ID == OnDestroyObjectEvent.ID;
		}

		// Token: 0x06009F07 RID: 40711 RVA: 0x0039B3C5 File Offset: 0x003995C5
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F08 RID: 40712 RVA: 0x0039B3D6 File Offset: 0x003995D6
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F09 RID: 40713 RVA: 0x0039B3E7 File Offset: 0x003995E7
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.DetachEffects(E.Cell, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F0A RID: 40714 RVA: 0x0039B400 File Offset: 0x00399600
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				Enclosed effect = IComponent<GameObject>.ThePlayer.GetEffect<Enclosed>();
				if (effect != null && effect.EnclosedBy == this.ParentObject)
				{
					E.AddAction("Exit", "exit", "ExitEnclosing", null, 'e', false, 15, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Exit", "enter", "EnterEnclosing", null, 'e', false, 15, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F0B RID: 40715 RVA: 0x0039B488 File Offset: 0x00399688
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "EnterEnclosing")
			{
				this.EnterEnclosure(E.Actor, E);
			}
			else if (E.Command == "ExitEnclosing")
			{
				this.ExitEnclosure(E.Actor, E, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F0C RID: 40716 RVA: 0x0039B4E0 File Offset: 0x003996E0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.EffectDescriptionPrefix.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(this.EffectDescriptionPrefix, base.GetEventSensitiveAddStatusSummary(E));
			}
			if (this.ShowGeneralInfoInShortDescription)
			{
				if (this.EnterSaveTarget > 0)
				{
					if (!this.EnterSaveStat.IsNullOrEmpty())
					{
						E.Postfix.AppendRules("Entering may not succeed, and is a task dependent on " + this.EnterSaveStat + ".");
					}
					else
					{
						E.Postfix.AppendRules("Entering may not succeed.");
					}
				}
				if (!this.Damage.IsNullOrEmpty() && this.EnterDamageChance > 0)
				{
					if (this.EnterDamageChance >= 100)
					{
						if (this.EnterSaveTarget > 0)
						{
							if (this.EnterDamageFailOnly)
							{
								E.Postfix.AppendRules("Attempting to enter and failing inflicts damage.");
							}
							else
							{
								E.Postfix.AppendRules("Attempting to enter inflicts damage.");
							}
						}
						else
						{
							E.Postfix.AppendRules("Entering inflicts damage.");
						}
					}
					else if (this.EnterSaveTarget > 0)
					{
						if (this.EnterDamageFailOnly)
						{
							E.Postfix.AppendRules("Attempting to enter and failing may inflict damage.");
						}
						else
						{
							E.Postfix.AppendRules("Attempting to enter may inflict damage.");
						}
					}
					else
					{
						E.Postfix.AppendRules("Entering may inflict damage.");
					}
				}
				if (this.AVBonus != 0)
				{
					E.Postfix.AppendRules(this.AVBonus.Signed(false) + " AV to occupant.");
				}
				if (this.DVPenalty != 0)
				{
					E.Postfix.AppendRules((-this.DVPenalty).Signed(false) + " DV to occupant.");
				}
				if (!this.Damage.IsNullOrEmpty() && this.DamageChance > 0)
				{
					if (this.DamageChance >= 100)
					{
						E.Postfix.AppendRules("Inflicts periodic damage to occupant.");
					}
					else
					{
						E.Postfix.AppendRules("May inflict periodic damage to occupant.");
					}
				}
				E.Postfix.AppendRules("Must spend a turn exiting before moving.");
				if (this.ExitSaveTarget > 0)
				{
					if (!this.ExitSaveStat.IsNullOrEmpty())
					{
						E.Postfix.AppendRules("Exiting may not succeed, and is a task dependent on " + this.ExitSaveStat + ".");
					}
					else
					{
						E.Postfix.AppendRules("Exiting may not succeed.");
					}
				}
				if (!this.Damage.IsNullOrEmpty() && this.ExitDamageChance > 0)
				{
					if (this.ExitDamageChance >= 100)
					{
						if (this.ExitSaveTarget > 0)
						{
							if (this.ExitDamageFailOnly)
							{
								E.Postfix.AppendRules("Attempting to exit and failing inflicts damage.");
							}
							else
							{
								E.Postfix.AppendRules("Attempting to exit inflicts damage.");
							}
						}
						else
						{
							E.Postfix.AppendRules("Exiting inflicts damage.");
						}
					}
					else if (this.ExitSaveTarget > 0)
					{
						if (this.ExitDamageFailOnly)
						{
							E.Postfix.AppendRules("Attempting to exit and failing may inflict damage.");
						}
						else
						{
							E.Postfix.AppendRules("Attempting to exit may inflict damage.");
						}
					}
					else
					{
						E.Postfix.AppendRules("Exiting may inflict damage.");
					}
				}
				if (!this.RequiresLiquid.IsNullOrEmpty())
				{
					E.Postfix.AppendRules("Must be completely filled with " + this.RequiresLiquid + " to be fully functional.");
				}
			}
			if (!this.EffectDescriptionPostfix.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(this.EffectDescriptionPostfix);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F0D RID: 40717 RVA: 0x0039B820 File Offset: 0x00399A20
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.EndAllEnclosure();
			return base.HandleEvent(E);
		}

		// Token: 0x06009F0E RID: 40718 RVA: 0x0039B82F File Offset: 0x00399A2F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009F0F RID: 40719 RVA: 0x0039B832 File Offset: 0x00399A32
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingTaken");
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			Registrar.Register("LeaveCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009F10 RID: 40720 RVA: 0x0039B868 File Offset: 0x00399A68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LeaveCell" || E.ID == "BeginBeingTaken")
			{
				this.EndAllEnclosure();
			}
			else if (E.ID == "CanSmartUse")
			{
				if (this.ParentObject.Understood())
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUse")
			{
				this.ParentObject.Twiddle(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009F11 RID: 40721 RVA: 0x0039B8E8 File Offset: 0x00399AE8
		private void EndAllEnclosure()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				currentCell.ForeachObject(delegate(GameObject obj)
				{
					Enclosed effect = obj.GetEffect<Enclosed>();
					if (effect != null && effect.EnclosedBy == this.ParentObject)
					{
						obj.RemoveEffect(effect, true);
					}
				});
			}
		}

		// Token: 0x06009F12 RID: 40722 RVA: 0x0039B918 File Offset: 0x00399B18
		public override bool GetActivePartLocallyDefinedFailure()
		{
			if (!this.RequiresLiquid.IsNullOrEmpty())
			{
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume == null)
				{
					return true;
				}
				if (liquidVolume.ComponentLiquids.Count != 1)
				{
					return true;
				}
				if (liquidVolume.Volume < liquidVolume.MaxVolume)
				{
					return true;
				}
				if (!liquidVolume.ComponentLiquids.ContainsKey(this.RequiresLiquid))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009F13 RID: 40723 RVA: 0x0039B979 File Offset: 0x00399B79
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "ProcessInputMissing";
		}

		// Token: 0x06009F14 RID: 40724 RVA: 0x0039B980 File Offset: 0x00399B80
		public bool PerformDamage(GameObject who)
		{
			if (this.Damage.IsNullOrEmpty())
			{
				return false;
			}
			bool flag = who.TakeDamage(this.Damage.RollCached(), "from %O!", this.DamageAttributes, null, null, this.ParentObject, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			if (flag && this.DamageBloodSplatterChance.in100())
			{
				who.Bloodsplatter();
			}
			return flag;
		}

		// Token: 0x06009F15 RID: 40725 RVA: 0x0039B9E8 File Offset: 0x00399BE8
		public bool AnyEnterDamage()
		{
			return this.EnterDamageChance > 0 && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x06009F16 RID: 40726 RVA: 0x0039BA34 File Offset: 0x00399C34
		public bool CheckEnterDamage(GameObject who, bool Failed)
		{
			return this.EnterDamageChance > 0 && (!this.EnterDamageFailOnly || Failed) && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && this.EnterDamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x06009F17 RID: 40727 RVA: 0x0039BAA4 File Offset: 0x00399CA4
		public bool AnyExitDamage()
		{
			return this.ExitDamageChance > 0 && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null));
		}

		// Token: 0x06009F18 RID: 40728 RVA: 0x0039BAF0 File Offset: 0x00399CF0
		public bool CheckExitDamage(GameObject who, bool Failed)
		{
			return this.ExitDamageChance > 0 && (!this.ExitDamageFailOnly || Failed) && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && this.ExitDamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x06009F19 RID: 40729 RVA: 0x0039BB60 File Offset: 0x00399D60
		public bool CheckPeriodicDamage(GameObject who)
		{
			return this.DamageChance > 0 && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && !this.DamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x06009F1A RID: 40730 RVA: 0x0039BBC0 File Offset: 0x00399DC0
		public bool EnterEnclosure(GameObject who, IEvent E = null)
		{
			if (this.ParentObject == who)
			{
				who.ShowFailure("You cannot enter " + who.itself + ".");
				return false;
			}
			if (!this.SizeInsensitive && who.IsGiganticCreature && !this.ParentObject.IsGiganticEquipment)
			{
				who.ShowFailure("You are too large to enter " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			if (this.ParentObject.HasTagOrProperty("Sealed"))
			{
				who.ShowFailure(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " sealed.");
				return false;
			}
			if (who.HasEffect<Enclosed>((Enclosed FX) => FX.EnclosedBy == this.ParentObject))
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail("You are already in " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
				}
				return false;
			}
			if (!this.ParentObject.FireEvent("BeforeClosing"))
			{
				return false;
			}
			if (!who.CanChangeBodyPosition("Enclosed", true, false, false, false))
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (who.CurrentCell != currentCell && (!who.DirectMoveTo(currentCell, 0, false, false, false, null) || who.CurrentCell != currentCell))
			{
				if (E != null)
				{
					E.RequestInterfaceExit();
				}
				return false;
			}
			if (this.EnterSaveTarget > 0 && !who.MakeSave(this.EnterSaveStat, this.EnterSaveTarget, null, null, "Enclosure Entry Access", false, false, false, false, this.ParentObject))
			{
				if (who.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"You fail to get ",
						who.itself,
						" into ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (IComponent<GameObject>.Visible(who))
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						who.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						who.GetVerb("try", true, false, false, true, false),
						" to get ",
						this.ParentObject.itself,
						" into ",
						this.ParentObject.the,
						this.ParentObject.DisplayNameOnly,
						", but",
						who.GetVerb("fail", true, false, false, true, false),
						"."
					}), null, true);
				}
				this.CheckEnterDamage(who, true);
				who.UseEnergy(1000, "Position", null, null, false);
				if (E != null)
				{
					E.RequestInterfaceExit();
				}
				return false;
			}
			this.CheckEnterDamage(who, false);
			who.PlayWorldSound("Sounds/Interact/sfx_interact_furniture_enter", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			IComponent<GameObject>.XDidYToZ(who, "enter", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, who.IsPlayer(), null);
			if (!this.EnterEventSelf.IsNullOrEmpty())
			{
				this.ParentObject.FireEvent(this.EnterEventSelf);
			}
			if (!this.EnterEventUser.IsNullOrEmpty())
			{
				who.FireEvent(this.EnterEventUser);
			}
			who.ApplyEffect(new Enclosed(this.ParentObject), null);
			who.UseEnergy(1000, "Position", null, null, false);
			Event @event = Event.New("Entered", "Who", who, "Object", this.ParentObject);
			this.ParentObject.FireEvent(@event);
			who.FireEvent(Event.New("Entered", "Object", this.ParentObject));
			this.ParentObject.FireEvent(Event.New("ObjectEntered", "Object", who));
			if (E != null)
			{
				E.RequestInterfaceExit();
			}
			if (this.ClosedColor != null)
			{
				this.ParentObject.Render.ColorString = this.ClosedColor;
			}
			if (this.ClosedTileColor != null)
			{
				this.ParentObject.Render.TileColor = this.ClosedTileColor;
			}
			if (this.ClosedRenderString != null)
			{
				this.ParentObject.Render.RenderString = this.ClosedRenderString;
			}
			if (this.ClosedTile != null)
			{
				this.ParentObject.Render.Tile = this.ClosedTile;
			}
			if (this.ClosedLayer != -2147483648)
			{
				this.ParentObject.Render.RenderLayer = this.ClosedLayer;
			}
			this.ParentObject.FireEvent("SyncClosed");
			return true;
		}

		// Token: 0x06009F1B RID: 40731 RVA: 0x0039C0D8 File Offset: 0x0039A2D8
		public bool ExitEnclosure(GameObject who, IEvent E = null, Enclosed enc = null)
		{
			if (enc == null)
			{
				enc = who.GetEffect<Enclosed>();
				if (enc == null)
				{
					if (who.IsPlayer())
					{
						Popup.ShowFail("You are not enclosed.", true, true, true);
					}
					return false;
				}
			}
			if (enc.EnclosedBy != this.ParentObject)
			{
				if (who.IsPlayer())
				{
					Popup.Show("It is not " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " that you are enclosed by.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			if (this.ExitSaveTarget > 0)
			{
				Event @event = E as Event;
				if ((@event == null || !@event.HasParameter("Teleporting")) && !who.MakeSave(this.ExitSaveStat, this.ExitSaveTarget, null, null, "Enclosure Exit Restraint", false, false, false, false, this.ParentObject))
				{
					if (who.IsPlayer())
					{
						Popup.Show(string.Concat(new string[]
						{
							"You fail to extricate ",
							who.itself,
							" from ",
							this.ParentObject.the,
							this.ParentObject.DisplayNameOnly,
							"!"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (IComponent<GameObject>.Visible(who))
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							who.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							who.GetVerb("try", true, false, false, true, false),
							" to extricate ",
							this.ParentObject.itself,
							" from ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							", but",
							who.GetVerb("fail", true, false, false, true, false),
							"!"
						}), null, true);
					}
					bool flag = this.CheckExitDamage(who, true);
					bool flag2 = false;
					if (@event == null || (!@event.HasParameter("Dragging") && !@event.HasParameter("Forced")))
					{
						who.UseEnergy(1000, "Position", null, null, false);
						flag2 = true;
					}
					if ((flag || flag2) && E != null)
					{
						E.RequestInterfaceExit();
					}
					return false;
				}
			}
			this.CheckExitDamage(who, true);
			if (who.IsPlayer())
			{
				Popup.Show(string.Concat(new string[]
				{
					"You extricate ",
					who.itself,
					" from ",
					this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					"."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else if (IComponent<GameObject>.Visible(who))
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					who.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					who.GetVerb("extricate", true, false, false, true, false),
					" ",
					this.ParentObject.itself,
					" from ",
					this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					"."
				}), null, true);
			}
			if (!this.ExitEventSelf.IsNullOrEmpty())
			{
				this.ParentObject.FireEvent(this.ExitEventSelf);
			}
			if (!this.ExitEventUser.IsNullOrEmpty())
			{
				who.FireEvent(this.ExitEventUser);
			}
			who.RemoveEffect(enc, true);
			who.UseEnergy(1000, "Position", null, null, false);
			who.FireEvent(Event.New("Exited", "Object", this.ParentObject));
			this.ParentObject.FireEvent(Event.New("ObjectExited", "Object", who));
			if (E != null)
			{
				E.RequestInterfaceExit();
			}
			if (this.OpenColor != null)
			{
				this.ParentObject.Render.ColorString = this.OpenColor;
			}
			if (this.OpenTileColor != null)
			{
				this.ParentObject.Render.TileColor = this.OpenTileColor;
			}
			if (this.OpenTile != null)
			{
				this.ParentObject.Render.Tile = this.OpenTile;
			}
			if (this.OpenRenderString != null)
			{
				this.ParentObject.Render.RenderString = this.OpenRenderString;
			}
			if (this.OpenLayer != -2147483648)
			{
				this.ParentObject.Render.RenderLayer = this.OpenLayer;
			}
			this.ParentObject.FireEvent("SyncOpened");
			return true;
		}

		// Token: 0x06009F1C RID: 40732 RVA: 0x0039C5B0 File Offset: 0x0039A7B0
		public bool EnclosureExitImpeded(GameObject who, bool ShowMessage = false, Enclosed Effect = null)
		{
			if (this.ExitSaveTarget > 0 || this.AnyExitDamage())
			{
				if (ShowMessage && who.IsPlayer())
				{
					Popup.ShowFail("You cannot do that while enclosed by " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06009F1D RID: 40733 RVA: 0x0039C620 File Offset: 0x0039A820
		private void SendEvent(GameObject who, string ID, int Level, bool SendSource, bool UseGenericNotify, bool OnSelf)
		{
			GameObject gameObject = (OnSelf ? this.ParentObject : who);
			if (UseGenericNotify)
			{
				GenericNotifyEvent.Send(gameObject, ID, who, this.ParentObject, Level);
				return;
			}
			if (SendSource || Level != 0)
			{
				Event @event = Event.New(ID, 0, 0, 0);
				if (SendSource)
				{
					@event.SetParameter("Source", this.ParentObject);
				}
				if (Level != 0)
				{
					@event.SetParameter("Level", Level);
				}
				gameObject.FireEvent(@event);
				return;
			}
			gameObject.FireEvent(ID);
		}

		// Token: 0x06009F1E RID: 40734 RVA: 0x0039C69C File Offset: 0x0039A89C
		public void ProcessTurnEnclosed(GameObject who, int TurnsEnclosed)
		{
			bool flag = base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			who.FireEvent(Event.New("StartTurnEnclosed", "Enclosing", this.ParentObject, "Disabled", flag ? 1 : 0));
			this.ParentObject.FireEvent(Event.New("StartTurnEnclosing", "Object", who, "Disabled", flag ? 1 : 0));
			this.CheckPeriodicDamage(who);
			if (!flag)
			{
				if (!this.PeriodicEvent1.IsNullOrEmpty() && this.PeriodicEventTurns1 > 0 && TurnsEnclosed % this.PeriodicEventTurns1 == 0)
				{
					this.SendEvent(who, this.PeriodicEvent1, this.PeriodicEventLevel1, this.PeriodicEventSendSource1, this.PeriodicEventUseGenericNotify1, this.PeriodicEventOnSelf1);
				}
				if (!this.PeriodicEvent2.IsNullOrEmpty() && this.PeriodicEventTurns2 > 0 && TurnsEnclosed % this.PeriodicEventTurns2 == 0)
				{
					this.SendEvent(who, this.PeriodicEvent2, this.PeriodicEventLevel2, this.PeriodicEventSendSource2, this.PeriodicEventUseGenericNotify1, this.PeriodicEventOnSelf1);
				}
				if (!this.PeriodicEvent3.IsNullOrEmpty() && this.PeriodicEventTurns3 > 0 && TurnsEnclosed % this.PeriodicEventTurns3 == 0)
				{
					this.SendEvent(who, this.PeriodicEvent3, this.PeriodicEventLevel3, this.PeriodicEventSendSource3, this.PeriodicEventUseGenericNotify1, this.PeriodicEventOnSelf1);
				}
			}
			who.FireEvent(Event.New("EndTurnEnclosed", "Enclosing", this.ParentObject, "Disabled", flag ? 1 : 0));
			this.ParentObject.FireEvent(Event.New("EndTurnEnclosing", "Object", who, "Disabled", flag ? 1 : 0));
		}

		// Token: 0x06009F1F RID: 40735 RVA: 0x0039C850 File Offset: 0x0039AA50
		private void DetachEffects(GameObject GO, IEvent Event)
		{
			Enclosed effect = GO.GetEffect<Enclosed>();
			if (effect != null && effect.EnclosedBy == this.ParentObject)
			{
				GO.RemoveEffect(effect, true);
				GO.ApplyEffect(new Prone(), null);
				if (Event != null)
				{
					Event.RequestInterfaceExit();
				}
			}
		}

		// Token: 0x06009F20 RID: 40736 RVA: 0x0039C894 File Offset: 0x0039AA94
		private void DetachEffects(Cell C = null, IEvent Event = null)
		{
			if (C == null)
			{
				C = this.ParentObject.GetCurrentCell();
			}
			if (C != null)
			{
				C.SafeForeachObject(delegate(GameObject o)
				{
					this.DetachEffects(o, Event);
				});
			}
		}

		// Token: 0x040041CD RID: 16845
		public int AVBonus;

		// Token: 0x040041CE RID: 16846
		public int DVPenalty;

		// Token: 0x040041CF RID: 16847
		public int EnterSaveTarget;

		// Token: 0x040041D0 RID: 16848
		public int ExitSaveTarget;

		// Token: 0x040041D1 RID: 16849
		public int DamageChance;

		// Token: 0x040041D2 RID: 16850
		public int EnterDamageChance;

		// Token: 0x040041D3 RID: 16851
		public int ExitDamageChance;

		// Token: 0x040041D4 RID: 16852
		public int DamageBloodSplatterChance = 50;

		// Token: 0x040041D5 RID: 16853
		public int PeriodicEventTurns1;

		// Token: 0x040041D6 RID: 16854
		public int PeriodicEventTurns2;

		// Token: 0x040041D7 RID: 16855
		public int PeriodicEventTurns3;

		// Token: 0x040041D8 RID: 16856
		public int PeriodicEventLevel1;

		// Token: 0x040041D9 RID: 16857
		public int PeriodicEventLevel2;

		// Token: 0x040041DA RID: 16858
		public int PeriodicEventLevel3;

		// Token: 0x040041DB RID: 16859
		public string _AffectedProperties;

		// Token: 0x040041DC RID: 16860
		public string EnterSaveStat = "Agility";

		// Token: 0x040041DD RID: 16861
		public string ExitSaveStat = "Strength";

		// Token: 0x040041DE RID: 16862
		public string Damage;

		// Token: 0x040041DF RID: 16863
		public string DamageAttributes;

		// Token: 0x040041E0 RID: 16864
		public string EnterEventSelf;

		// Token: 0x040041E1 RID: 16865
		public string EnterEventUser;

		// Token: 0x040041E2 RID: 16866
		public string ExitEventSelf;

		// Token: 0x040041E3 RID: 16867
		public string ExitEventUser;

		// Token: 0x040041E4 RID: 16868
		public string ApplyChangesEventSelf;

		// Token: 0x040041E5 RID: 16869
		public string ApplyChangesEventUser;

		// Token: 0x040041E6 RID: 16870
		public string UnapplyChangesEventSelf;

		// Token: 0x040041E7 RID: 16871
		public string UnapplyChangesEventUser;

		// Token: 0x040041E8 RID: 16872
		public string EffectDescriptionPrefix;

		// Token: 0x040041E9 RID: 16873
		public string EffectDescriptionPostfix;

		// Token: 0x040041EA RID: 16874
		public string RequiresLiquid;

		// Token: 0x040041EB RID: 16875
		public string PeriodicEvent1;

		// Token: 0x040041EC RID: 16876
		public string PeriodicEvent2;

		// Token: 0x040041ED RID: 16877
		public string PeriodicEvent3;

		// Token: 0x040041EE RID: 16878
		public string OpenTile;

		// Token: 0x040041EF RID: 16879
		public string OpenRenderString;

		// Token: 0x040041F0 RID: 16880
		public int OpenLayer = int.MinValue;

		// Token: 0x040041F1 RID: 16881
		public string ClosedTile;

		// Token: 0x040041F2 RID: 16882
		public string ClosedRenderString;

		// Token: 0x040041F3 RID: 16883
		public int ClosedLayer = int.MinValue;

		// Token: 0x040041F4 RID: 16884
		public bool SizeInsensitive;

		// Token: 0x040041F5 RID: 16885
		public bool NoDamageWhenDisabled;

		// Token: 0x040041F6 RID: 16886
		public bool EnterDamageFailOnly;

		// Token: 0x040041F7 RID: 16887
		public bool ExitDamageFailOnly;

		// Token: 0x040041F8 RID: 16888
		public bool PeriodicEventSendSource1;

		// Token: 0x040041F9 RID: 16889
		public bool PeriodicEventSendSource2;

		// Token: 0x040041FA RID: 16890
		public bool PeriodicEventSendSource3;

		// Token: 0x040041FB RID: 16891
		public bool PeriodicEventOnSelf1;

		// Token: 0x040041FC RID: 16892
		public bool PeriodicEventOnSelf2;

		// Token: 0x040041FD RID: 16893
		public bool PeriodicEventOnSelf3;

		// Token: 0x040041FE RID: 16894
		public bool PeriodicEventUseGenericNotify1;

		// Token: 0x040041FF RID: 16895
		public bool PeriodicEventUseGenericNotify2;

		// Token: 0x04004200 RID: 16896
		public bool PeriodicEventUseGenericNotify3;

		// Token: 0x04004201 RID: 16897
		public bool ShowGeneralInfoInShortDescription = true;

		// Token: 0x04004202 RID: 16898
		public string OpenColor;

		// Token: 0x04004203 RID: 16899
		public string ClosedColor;

		// Token: 0x04004204 RID: 16900
		public string OpenTileColor;

		// Token: 0x04004205 RID: 16901
		public string ClosedTileColor;

		// Token: 0x04004206 RID: 16902
		private Dictionary<string, int> _PropertyMap;
	}
}
