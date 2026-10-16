using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EA9 RID: 3753
	[Serializable]
	public class Engulfing : IPoweredPart
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060096AC RID: 38572 RVA: 0x0036A23F File Offset: 0x0036843F
		// (set) Token: 0x060096AB RID: 38571 RVA: 0x0036A22F File Offset: 0x0036842F
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

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060096AD RID: 38573 RVA: 0x0036A247 File Offset: 0x00368447
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

		// Token: 0x060096AE RID: 38574 RVA: 0x0036A268 File Offset: 0x00368468
		public Engulfing()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
			base.IsBioScannable = true;
		}

		// Token: 0x060096AF RID: 38575 RVA: 0x0036A2BC File Offset: 0x003684BC
		public override bool SameAs(IPart p)
		{
			Engulfing engulfing = p as Engulfing;
			return engulfing.AVBonus == this.AVBonus && engulfing.DVPenalty == this.DVPenalty && engulfing.EnterSaveTarget == this.EnterSaveTarget && engulfing.ExitSaveTarget == this.ExitSaveTarget && engulfing.DamageChance == this.DamageChance && engulfing.EnterDamageChance == this.EnterDamageChance && engulfing.ExitDamageChance == this.ExitDamageChance && engulfing.DamageBloodSplatterChance == this.DamageBloodSplatterChance && engulfing.PeriodicEventTurns1 == this.PeriodicEventTurns1 && engulfing.PeriodicEventTurns2 == this.PeriodicEventTurns2 && engulfing.PeriodicEventTurns3 == this.PeriodicEventTurns3 && !(engulfing.AffectedProperties != this.AffectedProperties) && !(engulfing.EnterSaveStat != this.EnterSaveStat) && !(engulfing.ExitSaveStat != this.ExitSaveStat) && !(engulfing.Damage != this.Damage) && !(engulfing.DamageAttributes != this.DamageAttributes) && !(engulfing.EnterEventSelf != this.EnterEventSelf) && !(engulfing.EnterEventUser != this.EnterEventUser) && !(engulfing.ExitEventSelf != this.ExitEventSelf) && !(engulfing.ExitEventUser != this.ExitEventUser) && !(engulfing.ApplyChangesEventSelf != this.ApplyChangesEventSelf) && !(engulfing.ApplyChangesEventUser != this.ApplyChangesEventUser) && !(engulfing.UnapplyChangesEventSelf != this.UnapplyChangesEventSelf) && !(engulfing.UnapplyChangesEventUser != this.UnapplyChangesEventUser) && !(engulfing.EffectDescriptionPrefix != this.EffectDescriptionPrefix) && !(engulfing.EffectDescriptionPostfix != this.EffectDescriptionPostfix) && !(engulfing.PeriodicEvent1 != this.PeriodicEvent1) && !(engulfing.PeriodicEvent2 != this.PeriodicEvent2) && !(engulfing.PeriodicEvent3 != this.PeriodicEvent3) && engulfing.SizeInsensitive == this.SizeInsensitive && engulfing.NoDamageWhenDisabled == this.NoDamageWhenDisabled && engulfing.EnterDamageFailOnly == this.EnterDamageFailOnly && engulfing.ExitDamageFailOnly == this.ExitDamageFailOnly && base.SameAs(p);
		}

		// Token: 0x060096B0 RID: 38576 RVA: 0x0036A544 File Offset: 0x00368744
		public virtual void CollectStats(Templates.StatCollector stats)
		{
			string text = "";
			if (this.ParentObject.HasPart("EngulfingDamage"))
			{
				text += "Hey you do damage!";
			}
			stats.Set("Postfix", text, false, 0);
		}

		// Token: 0x060096B1 RID: 38577 RVA: 0x0036A584 File Offset: 0x00368784
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EnteredCellEvent.ID || ID == OnDestroyObjectEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x060096B2 RID: 38578 RVA: 0x0036A5D5 File Offset: 0x003687D5
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject);
			return base.HandleEvent(E);
		}

		// Token: 0x060096B3 RID: 38579 RVA: 0x0036A600 File Offset: 0x00368800
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Engulfing.COMMAND_NAME)
			{
				Cell cell = E.TargetCell ?? base.PickDirection("Engulf", null);
				if (cell == null)
				{
					return false;
				}
				GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, false, false, null);
				if (combatTarget == null || combatTarget == this.ParentObject)
				{
					return false;
				}
				if (!this.Engulf(combatTarget, null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096B4 RID: 38580 RVA: 0x0036A678 File Offset: 0x00368878
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (E.Object == this.ParentObject && E.To == "Flying" && this.CheckEngulfed())
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot fly while engulfing " + this.Engulfed.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096B5 RID: 38581 RVA: 0x0036A704 File Offset: 0x00368904
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (!this.CheckEngulfed() && E.Distance == 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Target.IsCombatObject() && E.Actor.PhaseAndFlightMatches(E.Target) && !E.Actor.IsInStasis())
			{
				E.Add(Engulfing.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096B6 RID: 38582 RVA: 0x0036A778 File Offset: 0x00368978
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.CheckEngulfed())
			{
				if (this.ParentObject.CurrentCell != null)
				{
					if (this.Engulfed.CurrentCell != null && this.Engulfed.CurrentCell != this.ParentObject.CurrentCell)
					{
						this.Engulfed.CurrentCell.RemoveObject(this.Engulfed, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
					if (!this.ParentObject.CurrentCell.Objects.Contains(this.Engulfed))
					{
						this.ParentObject.CurrentCell.AddObject(this.Engulfed, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				this.Engulfed.FireEvent(Event.New("EngulfDragged", "Object", this.ParentObject));
				this.ParentObject.FireEvent(Event.New("EngulferDragged", "Object", this.Engulfed));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096B7 RID: 38583 RVA: 0x0036A876 File Offset: 0x00368A76
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.CheckEngulfed() && !this.ParentObject.Brain.HasGoal("FleeLocation"))
			{
				this.ParentObject.Brain.Goals.Clear();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096B8 RID: 38584 RVA: 0x0036A8B3 File Offset: 0x00368AB3
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.EndAllEngulfment();
			return base.HandleEvent(E);
		}

		// Token: 0x060096B9 RID: 38585 RVA: 0x0036A8C2 File Offset: 0x00368AC2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingTaken");
			Registrar.Register("ObjectEnteredAdjacentCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096BA RID: 38586 RVA: 0x0036A8E4 File Offset: 0x00368AE4
		public override void Initialize()
		{
			base.Initialize();
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Engulf", Engulfing.COMMAND_NAME, "Physical Mutations", null, "@", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x060096BB RID: 38587 RVA: 0x0036A92C File Offset: 0x00368B2C
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

		// Token: 0x060096BC RID: 38588 RVA: 0x0036A994 File Offset: 0x00368B94
		public bool CheckEnterDamage(GameObject who, bool Failed)
		{
			return this.EnterDamageChance > 0 && (!this.EnterDamageFailOnly || Failed) && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && this.EnterDamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x060096BD RID: 38589 RVA: 0x0036AA04 File Offset: 0x00368C04
		public bool CheckExitDamage(GameObject who, bool Failed)
		{
			return this.ExitDamageChance > 0 && (!this.ExitDamageFailOnly || Failed) && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && this.ExitDamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x060096BE RID: 38590 RVA: 0x0036AA74 File Offset: 0x00368C74
		public bool CheckPeriodicDamage(GameObject who)
		{
			return this.DamageChance > 0 && (!this.NoDamageWhenDisabled || !base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && this.DamageChance.in100() && this.PerformDamage(who);
		}

		// Token: 0x060096BF RID: 38591 RVA: 0x0036AAD4 File Offset: 0x00368CD4
		public override bool Render(RenderEvent E)
		{
			if (GameObject.Validate(ref this.Engulfed))
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num >= 31 && num <= 60)
				{
					E.ColorString = this.Engulfed.Render.ColorString;
					E.DetailColor = this.Engulfed.Render.DetailColor;
					E.Tile = this.Engulfed.Render.Tile;
					E.RenderString = this.Engulfed.Render.RenderString;
				}
			}
			return base.Render(E);
		}

		// Token: 0x060096C0 RID: 38592 RVA: 0x0036AB60 File Offset: 0x00368D60
		public bool Engulf(GameObject who, Event E = null)
		{
			if (this.CheckEngulfed())
			{
				this.EndEngulfment(this.Engulfed, null, null);
				if (this.Engulfed != null)
				{
					return false;
				}
			}
			if (who.SameAs(this.ParentObject))
			{
				return false;
			}
			if (!this.SizeInsensitive && who.IsGiganticCreature && !this.ParentObject.IsGiganticCreature)
			{
				this.ParentObject.ShowFailure(who.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " too large for you to engulf.");
				return false;
			}
			if (!who.CanChangeMovementMode("Engulfed", false, true, false, true) || !who.CanChangeBodyPosition("Engulfed", false, true, false, true))
			{
				this.ParentObject.ShowFailure(string.Concat(new string[]
				{
					"You cannot do that while ",
					who.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" in ",
					who.its,
					" present situation."
				}));
				return false;
			}
			if (!who.PhaseAndFlightMatches(this.ParentObject))
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (this.EnterSaveTarget > 0 && who.MakeSave(this.EnterSaveStat, this.EnterSaveTarget, this.ParentObject, "Strength", "Engulfment", false, false, false, false, null))
			{
				if (this.ParentObject.IsPlayer())
				{
					this.ParentObject.Fail("You fail to engulf " + who.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				else if (who.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("try", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to engulf you, but" + this.ParentObject.GetVerb("fail", true, false, false, true, false) + ".", null, true);
				}
				else if (IComponent<GameObject>.Visible(this.ParentObject))
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						this.ParentObject.Does("try", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" to engulf ",
						who.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						", but",
						this.ParentObject.GetVerb("fail", true, false, false, true, false),
						"."
					}), null, true);
				}
				this.CheckEnterDamage(who, true);
				this.ParentObject.UseEnergy(1000, "Position", null, null, false);
				if (E != null)
				{
					E.RequestInterfaceExit();
				}
				return false;
			}
			if (this.Pull)
			{
				if (who.CurrentCell != this.ParentObject.CurrentCell && !who.DirectMoveTo(this.ParentObject.CurrentCell, 0, false, true, false, null))
				{
					if (E != null)
					{
						E.RequestInterfaceExit();
					}
					return false;
				}
			}
			else if (who.CurrentCell != currentCell && !this.ParentObject.DirectMoveTo(who.CurrentCell, 0, false, true, false, null))
			{
				if (E != null)
				{
					E.RequestInterfaceExit();
				}
				return false;
			}
			this.CheckEnterDamage(who, false);
			IComponent<GameObject>.XDidYToZ(who, "are", "engulfed by", this.ParentObject, null, null, null, null, null, who, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
			if (!this.EnterEventSelf.IsNullOrEmpty())
			{
				this.ParentObject.FireEvent(this.EnterEventSelf);
			}
			if (!this.EnterEventUser.IsNullOrEmpty())
			{
				who.FireEvent(this.EnterEventUser);
			}
			this.Engulfed = who;
			who.ApplyEffect(new Engulfed(this.ParentObject), null);
			this.ParentObject.UseEnergy(1000, "Position", null, null, false);
			this.ParentObject.FireEvent(Event.New("Engulfed", "Object", who));
			who.FireEvent(Event.New("ObjectEngulfed", "Object", this.ParentObject));
			if (E != null)
			{
				E.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x060096C1 RID: 38593 RVA: 0x0036AFD4 File Offset: 0x003691D4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectEnteredAdjacentCell")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null && this.ParentObject.IsHostileTowards(gameObjectParameter) && this.CheckEngulfed() && !gameObjectParameter.MakeSave("Strength", 20, null, null, "Engulfment", false, false, false, false, null))
				{
					this.Engulf(gameObjectParameter, null);
				}
			}
			else if (E.ID == "BeginBeingTaken")
			{
				this.EndAllEngulfment();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060096C2 RID: 38594 RVA: 0x0036B060 File Offset: 0x00369260
		public bool EndEngulfment(GameObject who, Event E = null, Engulfed enc = null)
		{
			if (who == null)
			{
				this.Engulfed = null;
				return true;
			}
			if (enc == null)
			{
				enc = who.GetEffect<Engulfed>();
				if (enc == null)
				{
					this.Engulfed = null;
					return true;
				}
			}
			if (enc.EngulfedBy != this.ParentObject)
			{
				this.Engulfed = null;
				return true;
			}
			this.CheckExitDamage(who, true);
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
			this.Engulfed = null;
			return true;
		}

		// Token: 0x060096C3 RID: 38595 RVA: 0x0036B158 File Offset: 0x00369358
		public void EndAllEngulfment()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				currentCell.ForeachObject(delegate(GameObject obj)
				{
					Engulfed effect = obj.GetEffect<Engulfed>();
					if (effect != null && effect.EngulfedBy == this.ParentObject)
					{
						obj.RemoveEffect(effect, true);
					}
				});
			}
		}

		// Token: 0x060096C4 RID: 38596 RVA: 0x0036B188 File Offset: 0x00369388
		public void ProcessTurnEngulfed(GameObject who, int TurnsEngulfed)
		{
			bool flag = base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (!this.ParentObject.IsPlayer() && who.InSamePartyAs(this.ParentObject))
			{
				this.EndAllEngulfment();
				return;
			}
			who.FireEvent(Event.New("StartTurnEngulfed", "Object", this.ParentObject, "Disabled", flag ? 1 : 0));
			this.ParentObject.FireEvent(Event.New("StartTurnEngulfing", "Object", who, "Disabled", flag ? 1 : 0));
			this.CheckPeriodicDamage(who);
			if (!flag)
			{
				if (!this.PeriodicEvent1.IsNullOrEmpty() && this.PeriodicEventTurns1 > 0 && TurnsEngulfed % this.PeriodicEventTurns1 == 0)
				{
					who.FireEvent(this.PeriodicEvent1);
				}
				if (!this.PeriodicEvent2.IsNullOrEmpty() && this.PeriodicEventTurns2 > 0 && TurnsEngulfed % this.PeriodicEventTurns2 == 0)
				{
					who.FireEvent(this.PeriodicEvent2);
				}
				if (!this.PeriodicEvent3.IsNullOrEmpty() && this.PeriodicEventTurns3 > 0 && TurnsEngulfed % this.PeriodicEventTurns3 == 0)
				{
					who.FireEvent(this.PeriodicEvent3);
				}
			}
			who.FireEvent(Event.New("EndTurnEngulfed", "Object", this.ParentObject, "Disabled", flag ? 1 : 0));
			this.ParentObject.FireEvent(Event.New("EndTurnEngulfing", "Object", who, "Disabled", flag ? 1 : 0));
		}

		// Token: 0x060096C5 RID: 38597 RVA: 0x0036B314 File Offset: 0x00369514
		public bool IsOurEffect(Effect FX)
		{
			Engulfed engulfed = FX as Engulfed;
			return engulfed != null && engulfed.EngulfedBy == this.ParentObject;
		}

		// Token: 0x060096C6 RID: 38598 RVA: 0x0036B33C File Offset: 0x0036953C
		public bool CheckEngulfed()
		{
			if (GameObject.Validate(ref this.Engulfed) && !this.Engulfed.HasEffect(typeof(Engulfed), new Predicate<Effect>(this.IsOurEffect)))
			{
				this.Engulfed = null;
			}
			return this.Engulfed != null;
		}

		// Token: 0x04003E6D RID: 15981
		public static readonly string COMMAND_NAME = "CommandEngulf";

		// Token: 0x04003E6E RID: 15982
		public int AVBonus;

		// Token: 0x04003E6F RID: 15983
		public int DVPenalty;

		// Token: 0x04003E70 RID: 15984
		public int EnterSaveTarget;

		// Token: 0x04003E71 RID: 15985
		public int ExitSaveTarget;

		// Token: 0x04003E72 RID: 15986
		public int DamageChance;

		// Token: 0x04003E73 RID: 15987
		public int EnterDamageChance;

		// Token: 0x04003E74 RID: 15988
		public int ExitDamageChance;

		// Token: 0x04003E75 RID: 15989
		public int DamageBloodSplatterChance = 50;

		// Token: 0x04003E76 RID: 15990
		public int PeriodicEventTurns1;

		// Token: 0x04003E77 RID: 15991
		public int PeriodicEventTurns2;

		// Token: 0x04003E78 RID: 15992
		public int PeriodicEventTurns3;

		// Token: 0x04003E79 RID: 15993
		public string _AffectedProperties;

		// Token: 0x04003E7A RID: 15994
		public string EnterSaveStat = "Agility";

		// Token: 0x04003E7B RID: 15995
		public string ExitSaveStat = "Strength";

		// Token: 0x04003E7C RID: 15996
		public string Damage;

		// Token: 0x04003E7D RID: 15997
		public string DamageAttributes;

		// Token: 0x04003E7E RID: 15998
		public string EnterEventSelf;

		// Token: 0x04003E7F RID: 15999
		public string EnterEventUser;

		// Token: 0x04003E80 RID: 16000
		public string ExitEventSelf;

		// Token: 0x04003E81 RID: 16001
		public string ExitEventUser;

		// Token: 0x04003E82 RID: 16002
		public string ApplyChangesEventSelf;

		// Token: 0x04003E83 RID: 16003
		public string ApplyChangesEventUser;

		// Token: 0x04003E84 RID: 16004
		public string UnapplyChangesEventSelf;

		// Token: 0x04003E85 RID: 16005
		public string UnapplyChangesEventUser;

		// Token: 0x04003E86 RID: 16006
		public string EffectDescriptionPrefix;

		// Token: 0x04003E87 RID: 16007
		public string EffectDescriptionPostfix;

		// Token: 0x04003E88 RID: 16008
		public string PeriodicEvent1;

		// Token: 0x04003E89 RID: 16009
		public string PeriodicEvent2;

		// Token: 0x04003E8A RID: 16010
		public string PeriodicEvent3;

		// Token: 0x04003E8B RID: 16011
		public bool SizeInsensitive = true;

		// Token: 0x04003E8C RID: 16012
		public bool NoDamageWhenDisabled;

		// Token: 0x04003E8D RID: 16013
		public bool EnterDamageFailOnly;

		// Token: 0x04003E8E RID: 16014
		public bool ExitDamageFailOnly;

		// Token: 0x04003E8F RID: 16015
		public bool Pull;

		// Token: 0x04003E90 RID: 16016
		public GameObject Engulfed;

		// Token: 0x04003E91 RID: 16017
		public Guid ActivatedAbilityID;

		// Token: 0x04003E92 RID: 16018
		private Dictionary<string, int> _PropertyMap;
	}
}
