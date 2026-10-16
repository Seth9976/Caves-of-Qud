using System;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: the player's effective level for dominating the
	/// target is increased by the standard power load bonus, i.e. 2 for the
	/// standard overload power load of 400, and charge use is increased
	/// treating the power load as a percentage.
	/// </remarks>
	// Token: 0x020010AE RID: 4270
	[Serializable]
	public class Teleprojector : IPart, IHackingSifrahHandler
	{
		// Token: 0x0600A9DC RID: 43484 RVA: 0x003E1A64 File Offset: 0x003DFC64
		public override bool SameAs(IPart p)
		{
			Teleprojector teleprojector = p as Teleprojector;
			return teleprojector.InitialChargeUse == this.InitialChargeUse && teleprojector.MaintainChargeUse == this.MaintainChargeUse && !(teleprojector.ActivatedAbilityID != this.ActivatedAbilityID) && teleprojector.Target == this.Target && base.SameAs(p);
		}

		// Token: 0x0600A9DD RID: 43485 RVA: 0x003E1AC4 File Offset: 0x003DFCC4
		public int GetCooldown()
		{
			return 200;
		}

		// Token: 0x0600A9DE RID: 43486 RVA: 0x003E1ACB File Offset: 0x003DFCCB
		public int GetDuration()
		{
			return 400;
		}

		// Token: 0x0600A9DF RID: 43487 RVA: 0x003E1AD4 File Offset: 0x003DFCD4
		public void CollectStats(Templates.StatCollector stats)
		{
			if (this.ParentObject.Equipped == null)
			{
				return;
			}
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, this.ParentObject.Equipped);
			int num = this.MyPowerLoadBonus(int.MinValue, 100, 150);
			if (num > 0)
			{
				stats.postfix += string.Format("\nAttack roll increased by {0} due to high power load.", num);
			}
			if (num < 0)
			{
				stats.postfix += string.Format("\nAttack roll reduced by {0} due to low power load.", 0 - num);
			}
			int num2 = GetAvailableComputePowerEvent.GetFor(this.ParentObject.Equipped) / 5;
			stats.AddComputePowerPostfix("Attack roll", num2);
			int num3 = this.ParentObject.Equipped.GetStat("Level").Value + 4;
			int num4 = num3 + num + num2;
			stats.Set("Attack", "1d8+" + num4.ToString(), num4 != num3, num4 - num3);
			int num5 = stats.CollectComputePowerAdjustUp(activatedAbilityEntry, "Duration", this.GetDuration(), 1f);
			stats.Set("Duration", num5, num5 != this.GetDuration(), num5 - this.GetDuration());
			stats.CollectCooldownTurns(activatedAbilityEntry, this.GetCooldown(), 0);
		}

		// Token: 0x0600A9E0 RID: 43488 RVA: 0x003E1C1C File Offset: 0x003DFE1C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == EquippedEvent.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<IsOverloadableEvent>.ID || ID == UnequippedEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600A9E1 RID: 43489 RVA: 0x003E1C7D File Offset: 0x003DFE7D
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), this.ParentObject.Equipped);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E2 RID: 43490 RVA: 0x003E1CAA File Offset: 0x003DFEAA
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ActivatedAbilityID != Guid.Empty)
			{
				E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E3 RID: 43491 RVA: 0x003E1CD8 File Offset: 0x003DFED8
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "ActivateTeleprojector");
			E.Actor.RegisterPartEvent(this, "BeginTakeAction");
			E.Actor.RegisterPartEvent(this, "ChainInterruptDomination");
			E.Actor.RegisterPartEvent(this, "DominationBroken");
			E.Actor.RegisterPartEvent(this, "EarlyBeforeDeathRemoval");
			E.Actor.RegisterPartEvent(this, "TookDamage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E4 RID: 43492 RVA: 0x003E1D54 File Offset: 0x003DFF54
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.EndDomination(E.Actor);
			E.Actor.UnregisterPartEvent(this, "ActivateTeleprojector");
			E.Actor.UnregisterPartEvent(this, "BeginTakeAction");
			E.Actor.UnregisterPartEvent(this, "ChainInterruptDomination");
			E.Actor.UnregisterPartEvent(this, "DominationBroken");
			E.Actor.UnregisterPartEvent(this, "EarlyBeforeDeathRemoval");
			this.RemoveAbility(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E5 RID: 43493 RVA: 0x003E1DD8 File Offset: 0x003DFFD8
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			if (this.ParentObject.Equipped != null)
			{
				if (this.ParentObject.Equipped.IsPlayer())
				{
					Popup.Show(this.ParentObject.Does("attune", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to your physiology.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.ActivatedAbilityID = this.ParentObject.Equipped.AddActivatedAbility("Activate " + Grammar.MakeTitleCase(this.ParentObject.BaseDisplayNameStripped), "ActivateTeleprojector", "Items", null, "÷", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E6 RID: 43494 RVA: 0x003E1EAC File Offset: 0x003E00AC
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.RemoveAbility(null);
			this.EndDomination(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E7 RID: 43495 RVA: 0x003E1EC4 File Offset: 0x003E00C4
		public override bool HandleEvent(IsOverloadableEvent E)
		{
			return false;
		}

		// Token: 0x0600A9E8 RID: 43496 RVA: 0x003E1EC8 File Offset: 0x003E00C8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsActive())
			{
				E.AddAction("Activate", "activate", "ActivateTeleprojector", null, 'a', true, 0, 0, false, false, false, false, false, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9E9 RID: 43497 RVA: 0x003E1F07 File Offset: 0x003E0107
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateTeleprojector" && this.ActivateTeleprojector())
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9EA RID: 43498 RVA: 0x003E1F30 File Offset: 0x003E0130
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A9EB RID: 43499 RVA: 0x003E1F33 File Offset: 0x003E0133
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ActivateTeleprojector");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A9EC RID: 43500 RVA: 0x003E1F48 File Offset: 0x003E0148
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DominationBroken")
			{
				this.EndDomination(null);
			}
			else if (E.ID == "ActivateTeleprojector")
			{
				if (this.ActivateTeleprojector())
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.ID == "BeginTakeAction")
			{
				if (GameObject.Validate(ref this.Target) && this.MaintainChargeUse > 0 && !this.ParentObject.UseCharge(this.MaintainChargeUse * this.MyPowerLoadLevel() / 100, false, 0L, true, true, null))
				{
					this.InterruptDomination();
				}
			}
			else if (E.ID == "ChainInterruptDomination")
			{
				if (GameObject.Validate(ref this.Target) && !this.Target.FireEvent("InterruptDomination"))
				{
					return false;
				}
			}
			else if (E.ID == "EarlyBeforeDeathRemoval")
			{
				this.PerformMetempsychosis(null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A9ED RID: 43501 RVA: 0x003E2054 File Offset: 0x003E0254
		private bool ActivateTeleprojector()
		{
			if (!this.IsActive())
			{
				return false;
			}
			GameObject actor = this.ParentObject.Equipped;
			if (!actor.IsPlayer())
			{
				return false;
			}
			if (!actor.IsActivatedAbilityUsable(this.ActivatedAbilityID))
			{
				return actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " still cooling down.");
			}
			Cell cell = base.PickDirection(true, "Activate Teleprojector", null);
			if (cell == null)
			{
				return false;
			}
			GameObject actor2 = actor;
			if (actor2 != null)
			{
				actor2.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_mental_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			bool flag = false;
			Predicate<Dominated> <>9__0;
			foreach (GameObject gameObject in cell.GetObjectsWithPart("Robot"))
			{
				if (gameObject.HasStat("Level"))
				{
					flag = true;
					if (gameObject.Brain == null)
					{
						return actor.Fail("There seems to be no digital mind in " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to dominate.");
					}
					if (gameObject.HasCopyRelationship(actor))
					{
						return actor.Fail("You can't dominate " + actor.itself + "!");
					}
					GameObject gameObject2 = gameObject;
					Predicate<Dominated> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = (Dominated e) => e.Dominator == actor);
					}
					if (gameObject2.GetEffect<Dominated>(predicate) != null)
					{
						return actor.Fail("You can't dominate someone you are already dominating.");
					}
					if (gameObject.HasEffect<Dominated>())
					{
						return actor.Fail("You can't do that.");
					}
					if (!gameObject.FireEvent("CanApplyRoboDomination") || !CanApplyEffectEvent.Check(gameObject, "RoboDomination", 0))
					{
						return actor.Fail(gameObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have a digital mind you can make electronic contact with.");
					}
					if (!gameObject.CheckInfluence("RoboDomination", actor, false))
					{
						return false;
					}
					int num = this.MyPowerLoadLevel();
					if (this.ParentObject.UseCharge(this.InitialChargeUse * num / 100, false, 0L, true, true, new int?(num)))
					{
						gameObject.AddOpinion<OpinionDominate>(actor, 1f);
						int num2 = IComponent<GameObject>.PowerLoadBonus(num, 100, 150) + actor.Stat("Level", 0) + GetAvailableComputePowerEvent.GetFor(actor) / 5;
						int num3 = gameObject.Stat("Level", 0);
						if (Options.SifrahHacking)
						{
							HackingSifrah hackingSifrah = new HackingSifrah(gameObject, num3 / 5, Stats.GetCombatMA(gameObject) / 2, num2);
							hackingSifrah.HandlerID = this.ParentObject.ID;
							hackingSifrah.HandlerPartName = "Teleprojector";
							hackingSifrah.Play(gameObject);
							if (hackingSifrah.InterfaceExitRequested)
							{
								return false;
							}
							num2 = num2 * this.HackPerformance / 100;
						}
						base.PerformMentalAttack(new Mental.Attack(this.RoboDom), actor, gameObject, null, "Domination Teleprojector", "1d8+4", 0, this.GetDuration(), int.MinValue, num2, num3);
						actor.CooldownActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, false);
						return true;
					}
					return actor.Fail(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " have enough charge to function.");
				}
			}
			if (!flag)
			{
				Popup.ShowFail("There is nothing there that " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " can uplink with.", true, true, true);
			}
			return false;
		}

		// Token: 0x0600A9EE RID: 43502 RVA: 0x003E24D8 File Offset: 0x003E06D8
		public bool RoboDom(MentalAttackEvent E)
		{
			GameObject attacker = E.Attacker;
			GameObject defender = E.Defender;
			if (E.Penetrations > 0)
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(attacker, E.Magnitude);
				Dominated dominated = new Dominated(attacker, num, true);
				if (defender.ApplyEffect(dominated, null))
				{
					this.Target = defender;
					defender.Sparksplatter();
					Popup.Show("You take control of " + defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					attacker.Brain.PushGoal(new Dormant(-1));
					XRLCore.Core.Game.Player.Body = defender;
					IComponent<GameObject>.ThePlayer.Target = null;
					return true;
				}
			}
			IComponent<GameObject>.XDidYToZ(defender, "resist", attacker, "domination", "!", null, null, defender, null, false, false, false, false, true, null, null, null, false, false, false, true, false, null);
			return false;
		}

		// Token: 0x0600A9EF RID: 43503 RVA: 0x003E25D4 File Offset: 0x003E07D4
		private bool IsActive()
		{
			if (this.ParentObject.Equipped == null)
			{
				return false;
			}
			BootSequence part = this.ParentObject.GetPart<BootSequence>();
			return part == null || part.BootTimeLeft <= 0;
		}

		// Token: 0x0600A9F0 RID: 43504 RVA: 0x003E260B File Offset: 0x003E080B
		private void RemoveAbility(GameObject GO = null)
		{
			if (GO == null)
			{
				GO = this.ParentObject.Equipped;
			}
			if (GO != null)
			{
				GO.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			}
			this.ActivatedAbilityID = Guid.Empty;
		}

		// Token: 0x0600A9F1 RID: 43505 RVA: 0x003E2638 File Offset: 0x003E0838
		private bool EndDomination(GameObject actor = null)
		{
			if (!GameObject.Validate(this.ParentObject))
			{
				return false;
			}
			if (!GameObject.Validate(ref this.Target))
			{
				return false;
			}
			if (actor == null)
			{
				actor = this.ParentObject.Equipped;
			}
			Dominated effect = this.Target.GetEffect<Dominated>((Dominated e) => e.Dominator == actor);
			if (effect != null && !effect.BeingRemovedBySource)
			{
				effect.BeingRemovedBySource = true;
				this.Target.RemoveEffect(effect, true);
			}
			if (this.Target.OnWorldMap())
			{
				this.Target.PullDown(false);
			}
			this.Target = null;
			if (actor != null)
			{
				XRLCore.Core.Game.Player.Body = actor;
				IComponent<GameObject>.ThePlayer.Target = null;
				if (actor.IsPlayer())
				{
					Popup.Show("{{r|Your domination is broken!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				actor.Sparksplatter();
				actor.Brain.Goals.Clear();
			}
			Sidebar.UpdateState();
			return true;
		}

		// Token: 0x0600A9F2 RID: 43506 RVA: 0x003E2756 File Offset: 0x003E0956
		public void InterruptDomination()
		{
			if (GameObject.Validate(ref this.Target))
			{
				this.Target.FireEvent("InterruptDomination");
			}
		}

		// Token: 0x0600A9F3 RID: 43507 RVA: 0x003E2778 File Offset: 0x003E0978
		public bool PerformMetempsychosis(GameObject Actor = null)
		{
			if (!GameObject.Validate(ref this.Target))
			{
				return false;
			}
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped;
			}
			Dominated effect = this.Target.GetEffect<Dominated>((Dominated e) => e.Dominator == Actor);
			if (effect != null && !effect.BeingRemovedBySource)
			{
				effect.BeingRemovedBySource = true;
				effect.Metempsychosis = true;
				this.Target.RemoveEffect(effect, true);
				Domination.Metempsychosis(this.Target, effect.FromOriginalPlayerBody);
			}
			this.Target = null;
			return true;
		}

		// Token: 0x0600A9F4 RID: 43508 RVA: 0x003E2812 File Offset: 0x003E0A12
		public void HackingResultSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			this.HackPerformance = 150;
		}

		// Token: 0x0600A9F5 RID: 43509 RVA: 0x003E281F File Offset: 0x003E0A1F
		public void HackingResultExceptionalSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			this.HackPerformance = 300;
		}

		// Token: 0x0600A9F6 RID: 43510 RVA: 0x003E282C File Offset: 0x003E0A2C
		public void HackingResultPartialSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			this.HackPerformance = 100;
		}

		// Token: 0x0600A9F7 RID: 43511 RVA: 0x003E2836 File Offset: 0x003E0A36
		public void HackingResultFailure(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			this.HackPerformance = 50;
		}

		// Token: 0x0600A9F8 RID: 43512 RVA: 0x003E2840 File Offset: 0x003E0A40
		public void HackingResultCriticalFailure(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			this.HackPerformance = 0;
		}

		// Token: 0x04004604 RID: 17924
		public int InitialChargeUse = 1000;

		// Token: 0x04004605 RID: 17925
		public int MaintainChargeUse = 10;

		// Token: 0x04004606 RID: 17926
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004607 RID: 17927
		public GameObject Target;

		// Token: 0x04004608 RID: 17928
		[NonSerialized]
		private int HackPerformance;
	}
}
