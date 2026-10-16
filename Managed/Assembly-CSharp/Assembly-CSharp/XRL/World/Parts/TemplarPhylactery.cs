using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is not by default, the summoned wraith-knight receives a
	/// Quickness bonus of ((power load - 100) / 10), i.e. 30 for the
	/// standard overload power load of 400.
	/// </remarks>
	// Token: 0x020010B1 RID: 4273
	[Serializable]
	public class TemplarPhylactery : IPoweredPart, IHackingSifrahHandler
	{
		// Token: 0x0600AA25 RID: 43557 RVA: 0x003E3904 File Offset: 0x003E1B04
		public TemplarPhylactery()
		{
			this.ChargeUse = 1;
			this.IsPowerLoadSensitive = GlobalConfig.GetBoolSetting("EnablePhylacteryOverload", false);
			this.WorksOnHolder = true;
			this.MustBeUnderstood = true;
			this.NameForStatus = "ContinuityEngine";
		}

		// Token: 0x0600AA26 RID: 43558 RVA: 0x003E393D File Offset: 0x003E1B3D
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AA27 RID: 43559 RVA: 0x003E3940 File Offset: 0x003E1B40
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == AIGetOffensiveItemListEvent.ID || ID == AfterObjectCreatedEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID || ID == OnDestroyObjectEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AA28 RID: 43560 RVA: 0x003E39BC File Offset: 0x003E1BBC
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			if (this.wraith == null && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.WantsToUse(E.Actor) && GameObject.Validate(E.Target) && E.Target.Brain != null && !E.Target.HasPart<MentalShield>())
			{
				E.Add(TemplarPhylactery.ACTIVATE_COMMAND_NAME, 100, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA29 RID: 43561 RVA: 0x003E3A4C File Offset: 0x003E1C4C
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (this.wraith != null && this.IsHacked() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.wraith.IsHostileTowards(E.Actor) && E.Actor.Stat("Intelligence", 0) >= 7)
			{
				E.Add(TemplarPhylactery.DEACTIVATE_COMMAND_NAME, 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA2A RID: 43562 RVA: 0x003E3AD8 File Offset: 0x003E1CD8
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (E.Object != this.wraith || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || !this.wraith.InSameZone(E.Object)) && base.HandleEvent(E);
		}

		// Token: 0x0600AA2B RID: 43563 RVA: 0x003E3B37 File Offset: 0x003E1D37
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.Despawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA2C RID: 43564 RVA: 0x003E3B46 File Offset: 0x003E1D46
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.Despawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA2D RID: 43565 RVA: 0x003E3B58 File Offset: 0x003E1D58
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA2E RID: 43566 RVA: 0x003E3B9C File Offset: 0x003E1D9C
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA2F RID: 43567 RVA: 0x003E3BE0 File Offset: 0x003E1DE0
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (GameObject.Validate(ref this.wraith) && base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA30 RID: 43568 RVA: 0x003E3C30 File Offset: 0x003E1E30
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.IsHacked() && E.Actor.IsPlayer() && Options.SifrahHacking)
			{
				E.AddAction("Hack", "hack", TemplarPhylactery.HACK_COMMAND_NAME, null, 'h', false, 0, 0, false, false, false, false, true, null, false);
			}
			if (this.ParentObject.Equipped != null)
			{
				if (GameObject.Validate(ref this.wraith))
				{
					E.AddAction("Deactivate", "deactivate", TemplarPhylactery.DEACTIVATE_COMMAND_NAME, null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Activate", "activate", TemplarPhylactery.ACTIVATE_COMMAND_NAME, null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA31 RID: 43569 RVA: 0x003E3CE8 File Offset: 0x003E1EE8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.IsHacked())
			{
				E.Infix.Compound(this.ParentObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " been hacked.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA32 RID: 43570 RVA: 0x003E3D4C File Offset: 0x003E1F4C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == TemplarPhylactery.ACTIVATE_COMMAND_NAME)
			{
				if (!E.Actor.CanMoveExtremities("Activate", true, false, true))
				{
					return false;
				}
				if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unresponsive.");
				}
				this.Spawn();
				E.RequestInterfaceExit();
			}
			else if (E.Command == TemplarPhylactery.DEACTIVATE_COMMAND_NAME)
			{
				this.Despawn();
				E.RequestInterfaceExit();
			}
			else if (E.Command == TemplarPhylactery.HACK_COMMAND_NAME && !this.IsHacked() && E.Actor.IsPlayer() && Options.SifrahHacking)
			{
				if (!base.IsReady(false, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
				{
					return E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
				}
				int techTier = this.ParentObject.GetTechTier();
				HackingSifrah hackingSifrah = new HackingSifrah(this.ParentObject, techTier, techTier, E.Actor.Stat("Intelligence", 0));
				hackingSifrah.HandlerID = this.ParentObject.ID;
				hackingSifrah.HandlerPartName = base.GetType().Name;
				hackingSifrah.Play(this.ParentObject);
				E.Actor.UseEnergy(1000, "Sifrah Hack TemplarPhylactery", null, null, false);
				if (!hackingSifrah.InterfaceExitRequested)
				{
					Statistic energy = E.Actor.Energy;
					if (energy == null || energy.Value >= 0)
					{
						goto IL_0225;
					}
				}
				E.RequestInterfaceExit();
			}
			IL_0225:
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA33 RID: 43571 RVA: 0x003E3F88 File Offset: 0x003E2188
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (E.Context != "Sample" && E.Context != "Initialization" && E.Context != "GameStarted" && this.wraithID == null && E.ReplacementObject == null)
			{
				GameObject gameObject = HeroMaker.MakeHero(GameObject.Create("Wraith-Knight Templar"), null, "SpecialFactionHeroTemplate_TemplarWraith", -1, "Hero");
				gameObject.RequirePart<HologramMaterial>(false);
				gameObject.RequirePart<HologramInvulnerability>(false);
				gameObject.RequirePart<Unreplicable>(false);
				gameObject.ModIntProperty("IgnoresWalls", 1, false);
				foreach (GameObject gameObject2 in gameObject.GetInventoryAndEquipment())
				{
					if (gameObject2.HasPropertyOrTag("MeleeWeapon"))
					{
						gameObject2.AddPart<ModPsionic>(new ModPsionic(), true, false);
					}
					else
					{
						gameObject2.Obliterate(null, false, null);
					}
				}
				this.ParentObject.Render.DisplayName = "phylactery of " + gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, true, false, false, false, null, false);
				ZoneManager zoneManager = The.ZoneManager;
				this.wraithID = ((zoneManager != null) ? zoneManager.CacheObject(gameObject, false, false) : null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA34 RID: 43572 RVA: 0x003E40E4 File Offset: 0x003E22E4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AdjustWeaponScore");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AA35 RID: 43573 RVA: 0x003E40F9 File Offset: 0x003E22F9
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AdjustWeaponScore" && this.WantsToUse(E.GetGameObjectParameter("User")))
			{
				E.ModParameter("Score", 100);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AA36 RID: 43574 RVA: 0x003E4135 File Offset: 0x003E2335
		public bool WantsToUse(GameObject Actor)
		{
			return Actor == null || Actor.Stat("Intelligence", 0) <= 22 || !this.WillBeHostileTowards(Actor, null);
		}

		// Token: 0x0600AA37 RID: 43575 RVA: 0x003E4158 File Offset: 0x003E2358
		public void Despawn()
		{
			if (GameObject.Validate(ref this.wraith))
			{
				this.wraith.Splatter("&M-");
				this.wraith.Splatter("&M.");
				this.wraith.Splatter("&M/");
				IComponent<GameObject>.XDidY(this.wraith, "disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
				this.wraith.Obliterate(null, false, null);
				this.wraith = null;
			}
		}

		// Token: 0x0600AA38 RID: 43576 RVA: 0x003E41D8 File Offset: 0x003E23D8
		public void Spawn()
		{
			this.Despawn();
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped == null || this.wraithID == null)
			{
				return;
			}
			Cell cell = equipped.GetCurrentCell();
			if (cell == null)
			{
				return;
			}
			cell = cell.GetEmptyAdjacentCells(1, 5).GetRandomElement<Cell>(null);
			if (cell == null)
			{
				return;
			}
			this.wraith = The.ZoneManager.peekCachedObject(this.wraithID).DeepCopy(false, true, null);
			int num = this.MyPowerLoadBonus(int.MinValue, 100, 10);
			if (num != 0 && this.wraith.HasStat("Speed"))
			{
				this.wraith.GetStat("Speed").BaseValue += num;
			}
			Temporary.AddHierarchically(this.wraith, 0, null, this.ParentObject, true);
			if (this.WillTakeOnAttitudesOf(equipped))
			{
				this.wraith.TakeAllegiance<AllySummon>(equipped);
			}
			this.wraith.MakeActive(false);
			equipped.PlayWorldOrUISound("Sounds/Interact/sfx_interact_phylactery_on", null);
			cell.AddObject(this.wraith, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.wraith.TeleportSwirl(null, "&B", true, null, 'ù', false);
			IComponent<GameObject>.XDidYToZ(equipped, "activate", this.wraith, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			IComponent<GameObject>.XDidY(this.wraith, "appear", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0600AA39 RID: 43577 RVA: 0x003E4344 File Offset: 0x003E2544
		public bool WillBeHostileTowards(GameObject Actor = null, GameObject Object = null)
		{
			if (Object == null)
			{
				Object = Actor;
			}
			if (GameObject.Validate(ref Object))
			{
				if (this.WillTakeOnAttitudesOf(Actor))
				{
					if (Actor == null && Object == null)
					{
						return false;
					}
					if (Actor == Object || !Actor.IsHostileTowards(Object))
					{
						return false;
					}
				}
				else if (this.wraithID != null && !The.ZoneManager.peekCachedObject(this.wraithID).IsHostileTowards(Object))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600AA3A RID: 43578 RVA: 0x003E43A3 File Offset: 0x003E25A3
		public bool WillTakeOnAttitudesOf(GameObject Actor)
		{
			return this.IsHacked() || (GameObject.Validate(ref Actor) && Actor.HasProperty("PsychicHunter") && Actor.HasPart<Extradimensional>() && !Actor.IsPlayerControlled());
		}

		// Token: 0x0600AA3B RID: 43579 RVA: 0x003E43D8 File Offset: 0x003E25D8
		public bool IsHacked()
		{
			return this.ParentObject.GetIntProperty("SifrahTemplarPhylacteryHack", 0) > 0;
		}

		// Token: 0x0600AA3C RID: 43580 RVA: 0x003E43F0 File Offset: 0x003E25F0
		public void HackingResultSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahTemplarPhylacteryHack", 1, false);
				if (this.ParentObject.GetIntProperty("SifrahTemplarPhylacteryHack", 1) > 0)
				{
					this.ChargeUse = 100;
					if (who.IsPlayer())
					{
						Popup.Show("You hack " + obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
				}
				else if (who.IsPlayer())
				{
					Popup.Show("You feel like you're making progress on hacking " + obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600AA3D RID: 43581 RVA: 0x003E44D4 File Offset: 0x003E26D4
		public void HackingResultExceptionalSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.SetIntProperty("SifrahTemplarPhylacteryHack", 1, false);
				this.ChargeUse = 10;
				if (who.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"You hack ",
						obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						", and find a way to reduce ",
						obj.its,
						" power consumption in the process!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600AA3E RID: 43582 RVA: 0x003E4574 File Offset: 0x003E2774
		public void HackingResultPartialSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject && who.IsPlayer())
			{
				Popup.Show("You feel like you're making progress on hacking " + obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600AA3F RID: 43583 RVA: 0x003E45D8 File Offset: 0x003E27D8
		public void HackingResultFailure(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahTemplarPhylacteryHack", -1, false);
				if (who.IsPlayer())
				{
					Popup.Show("You cannot seem to work out how to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600AA40 RID: 43584 RVA: 0x003E4650 File Offset: 0x003E2850
		public void HackingResultCriticalFailure(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahTemplarPhylacteryHack", -1, false);
				if (who.HasPart<Dystechnia>())
				{
					Dystechnia.CauseExplosion(this.ParentObject, who, null);
					game.RequestInterfaceExit();
					return;
				}
				if (who.IsPlayer())
				{
					Popup.Show("Your attempt to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " has gone very wrong.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
				Cell currentCell = who.CurrentCell;
				this.ParentObject.UseCharge(Stat.Random(5000, 15000), false, 0L, true, true, null);
				this.ParentObject.Discharge((currentCell != null && localAdjacentCells.Contains(currentCell)) ? currentCell : localAdjacentCells.GetRandomElement<Cell>(null), "3d8".RollCached(), 0, "2d4", null, who, obj, null, null, null, null, null, 0, false, false, false);
			}
		}

		// Token: 0x0400461F RID: 17951
		public static readonly string ACTIVATE_COMMAND_NAME = "ActivateTemplarPhylactery";

		// Token: 0x04004620 RID: 17952
		public static readonly string DEACTIVATE_COMMAND_NAME = "DeactivateTemplarPhylactery";

		// Token: 0x04004621 RID: 17953
		public static readonly string HACK_COMMAND_NAME = "HackTemplarPhylactery";

		// Token: 0x04004622 RID: 17954
		public string wraithID;

		// Token: 0x04004623 RID: 17955
		public GameObject wraith;
	}
}
