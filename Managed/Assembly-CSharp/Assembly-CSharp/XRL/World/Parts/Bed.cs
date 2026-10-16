using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F96 RID: 3990
	[Serializable]
	public class Bed : IActivePart
	{
		// Token: 0x06009E4E RID: 40526 RVA: 0x003951CE File Offset: 0x003933CE
		public Bed()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009E4F RID: 40527 RVA: 0x003951E4 File Offset: 0x003933E4
		public override bool SameAs(IPart p)
		{
			Bed bed = p as Bed;
			return !(bed.SleepEvent1 != this.SleepEvent1) && !(bed.SleepEvent2 != this.SleepEvent2) && !(bed.SleepEvent3 != this.SleepEvent3) && bed.SleepEventTurns1 == this.SleepEventTurns1 && bed.SleepEventTurns2 == this.SleepEventTurns2 && bed.SleepEventTurns3 == this.SleepEventTurns3 && bed.SleepEventLevel1 == this.SleepEventLevel1 && bed.SleepEventLevel2 == this.SleepEventLevel2 && bed.SleepEventLevel3 == this.SleepEventLevel3 && bed.NoSmartUse == this.NoSmartUse && bed.SleepEventSendSource1 == this.SleepEventSendSource1 && bed.SleepEventSendSource2 == this.SleepEventSendSource2 && bed.SleepEventSendSource3 == this.SleepEventSendSource3 && bed.BreakIfGiganticOnNonGigantic == this.BreakIfGiganticOnNonGigantic && !(bed.BreakIfUserHasPart != this.BreakIfUserHasPart) && bed.HealCounter == this.HealCounter && base.SameAs(p);
		}

		// Token: 0x06009E50 RID: 40528 RVA: 0x00395314 File Offset: 0x00393514
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == BeforeDestroyObjectEvent.ID || (ID == CanSmartUseEvent.ID && !this.NoSmartUse) || (ID == CommandSmartUseEvent.ID && !this.NoSmartUse) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID || ID == LeftCellEvent.ID || ID == PooledEvent<PollForHealingLocationEvent>.ID || ID == PooledEvent<UseHealingLocationEvent>.ID;
		}

		// Token: 0x06009E51 RID: 40529 RVA: 0x0039538D File Offset: 0x0039358D
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E52 RID: 40530 RVA: 0x0039539E File Offset: 0x0039359E
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E53 RID: 40531 RVA: 0x003953AF File Offset: 0x003935AF
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.DetachEffects(E.Cell, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E54 RID: 40532 RVA: 0x003953C8 File Offset: 0x003935C8
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (this.NoSmartUse || E.Actor == this.ParentObject || !E.Actor.PhaseMatches(this.ParentObject) || !E.Actor.FlightCanReach(this.ParentObject)) && base.HandleEvent(E);
		}

		// Token: 0x06009E55 RID: 40533 RVA: 0x0039541A File Offset: 0x0039361A
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (!this.NoSmartUse)
			{
				this.AttemptSleep(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E56 RID: 40534 RVA: 0x00395438 File Offset: 0x00393638
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood() && E.Actor.PhaseMatches(this.ParentObject) && E.Actor.FlightCanReach(this.ParentObject))
			{
				E.AddAction("Sleep", "sleep", "SleepOnBed", null, 's', false, 10, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E57 RID: 40535 RVA: 0x003954A3 File Offset: 0x003936A3
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "SleepOnBed" && this.AttemptSleep(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E58 RID: 40536 RVA: 0x003954D4 File Offset: 0x003936D4
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (this.ParentObject.GetIntProperty("DroppedByPlayer", 0) != 0)
			{
				return base.HandleEvent(E);
			}
			if (E.Actor == this.ParentObject)
			{
				return base.HandleEvent(E);
			}
			string owner = this.ParentObject.Owner;
			if (!owner.IsNullOrEmpty() && !E.Actor.HasTagOrProperty(owner) && E.Actor.DisplayNameOnly != owner && !E.Actor.BelongsToFaction(owner))
			{
				return base.HandleEvent(E);
			}
			bool flag = E.Actor.HasTag("Nocturnal");
			if ((!flag && base.IsNight()) || (flag && base.IsDay()))
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (E.Actor.Brain != null && E.Actor.HasTagOrProperty("SleepOnBed") && !E.Actor.HasTagOrProperty("NoSleep") && !E.Actor.HasPart<Robot>() && !E.Actor.IsFlying && currentCell.IsEmptyOfSolid(true) && currentCell.GetNavigationWeightFor(E.Actor, false, false, false, false, false) < 30)
				{
					if (IComponent<GameObject>.currentTurn - this.lastIdleUsed < 50L)
					{
						return base.HandleEvent(E);
					}
					this.lastIdleUsed = IComponent<GameObject>.currentTurn;
					GameObject who = E.Actor;
					who.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
					{
						if (who.CurrentCell.Objects.Contains(this.ParentObject))
						{
							this.AttemptSleep(who);
						}
						h.FailToParent();
					}, null, null));
					who.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E59 RID: 40537 RVA: 0x003956A4 File Offset: 0x003938A4
		public override bool HandleEvent(PollForHealingLocationEvent E)
		{
			if (E.Actor != this.ParentObject && E.Actor.PhaseMatches(this.ParentObject) && E.Actor.FlightCanReach(this.ParentObject))
			{
				E.Value = Math.Max(E.Value, Tier.Constrain(this.ParentObject.GetTier() / 2));
				if (E.First)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E5A RID: 40538 RVA: 0x00395719 File Offset: 0x00393919
		public override bool HandleEvent(UseHealingLocationEvent E)
		{
			return (E.Actor == this.ParentObject || E.Actor.HasEffect<Asleep>() || !this.AttemptSleep(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x06009E5B RID: 40539 RVA: 0x0039574D File Offset: 0x0039394D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009E5C RID: 40540 RVA: 0x00395750 File Offset: 0x00393950
		public bool AttemptSleep(GameObject who)
		{
			bool flag;
			bool flag2;
			bool flag3;
			this.AttemptSleep(who, out flag, out flag2, out flag3);
			return flag || flag2 || flag3;
		}

		// Token: 0x06009E5D RID: 40541 RVA: 0x00395770 File Offset: 0x00393970
		public void AttemptSleep(GameObject Actor, out bool SleepSuccessful, out bool MoveFailed, out bool Broke)
		{
			SleepSuccessful = false;
			MoveFailed = false;
			Broke = false;
			if (Actor == this.ParentObject)
			{
				Actor.ShowFailure("You cannot sleep on " + Actor.itself + ".");
				return;
			}
			if (this.ParentObject.CurrentCell == null)
			{
				Actor.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " must be laid out before you can do that.");
				return;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, true, false, false, true, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.Broken)
				{
					Actor.ShowFailure(this.ParentObject.Itis + " broken.");
					return;
				}
				Actor.ShowFailure(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " seem to be working.");
				return;
			}
			else
			{
				Hangable part = this.ParentObject.GetPart<Hangable>();
				if (part != null && !part.Hanging)
				{
					Actor.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " must be hung up before you can do that.");
					return;
				}
				if (Actor.HasEffect<Sitting>())
				{
					Actor.ShowFailure("You cannot do that while sitting.");
					return;
				}
				if (Actor.HasEffect<Burrowed>())
				{
					Actor.ShowFailure("You cannot do that while burrowed.");
					return;
				}
				if (!Actor.CanChangeBodyPosition("Prone", true, false, false, false))
				{
					return;
				}
				if (Actor.IsFlying)
				{
					Actor.ShowFailure("You cannot do that while flying.");
					return;
				}
				if (!Actor.FlightCanReach(this.ParentObject))
				{
					Actor.ShowFailure("You cannot reach " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					return;
				}
				if (!Actor.PhaseMatches(this.ParentObject))
				{
					Actor.ShowFailure("You are out of phase with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
					return;
				}
				if (!Actor.FireEvent("CanApplySleep"))
				{
					Actor.ShowFailure("You can't go to sleep right now.");
					return;
				}
				if (Actor.IsPlayer())
				{
					this.ParentObject.SetIntProperty("DroppedByPlayer", 1, false);
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				if (Actor.CurrentCell == currentCell || (Actor.Move(Actor.GetDirectionToward(this.ParentObject, false), false, false, false, false, false, false, null, null, false, null, null, null, true, false, null, null, 0) && Actor.CurrentCell == currentCell))
				{
					int num;
					if (Actor.IsPlayer())
					{
						num = Popup.PickOption("How long would you like to sleep?", null, "", "Sounds/UI/ui_notification", new List<string>
						{
							"Until " + Calendar.GetTime((int)(Calendar.TotalTimeTicks + 150L) % 1200),
							"Until " + Calendar.GetTime((int)(Calendar.TotalTimeTicks + 375L) % 1200),
							"Until " + Calendar.GetTime((int)(Calendar.TotalTimeTicks + 600L) % 1200)
						}.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
					}
					else
					{
						num = Stat.Random(0, 2);
					}
					if (num < 0)
					{
						return;
					}
					int num2 = 0;
					if (num == 0)
					{
						num2 = 150;
					}
					else if (num == 1)
					{
						num2 = 375;
					}
					else if (num == 2)
					{
						num2 = 600;
					}
					try
					{
						if (((!this.BreakIfUserHasPart.IsNullOrEmpty() && Actor.HasPart(this.BreakIfUserHasPart)) || (this.BreakIfGiganticOnNonGigantic && Actor.IsGiganticCreature && !this.ParentObject.IsGiganticEquipment)) && this.ParentObject.ApplyEffect(new Broken(), null) && base.IsBroken())
						{
							Broke = true;
							Actor.ShowFailure("You think you broke " + this.ParentObject.them + "...");
						}
						else
						{
							Actor.ForceApplyEffect(new Asleep(this.ParentObject, num2, true, true, true, true), null);
							if (Actor.IsPlayer() && this.ParentObject.Blueprint == "Bedroll")
							{
								MetricsManager.LogEvent("BedrollsSlept");
							}
							Event @event = Event.New("SleptIn", "Actor", Actor, "Object", this.ParentObject);
							this.ParentObject.FireEvent(@event);
							Actor.FireEvent(@event);
							SleepSuccessful = true;
						}
						return;
					}
					catch (Exception ex)
					{
						MetricsManager.LogError(ex);
						return;
					}
				}
				MoveFailed = true;
				return;
			}
		}

		// Token: 0x06009E5E RID: 40542 RVA: 0x00395C64 File Offset: 0x00393E64
		private void SendEvent(GameObject Actor, string ID, int Level, bool SendSource)
		{
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
				Actor.FireEvent(@event);
				return;
			}
			Actor.FireEvent(ID);
		}

		// Token: 0x06009E5F RID: 40543 RVA: 0x00395CBC File Offset: 0x00393EBC
		public void ProcessTurnAsleep(GameObject Actor, int TurnsAsleep)
		{
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.HealCounter + 1;
				this.HealCounter = num;
				if (num >= 10 - this.ParentObject.GetTier())
				{
					Actor.Heal(1, false, false, false);
					this.HealCounter = 0;
				}
				if (!this.SleepEvent1.IsNullOrEmpty() && this.SleepEventTurns1 > 0 && TurnsAsleep % this.SleepEventTurns1 == 0)
				{
					this.SendEvent(Actor, this.SleepEvent1, this.SleepEventLevel1, this.SleepEventSendSource1);
				}
				if (!this.SleepEvent2.IsNullOrEmpty() && this.SleepEventTurns2 > 0 && TurnsAsleep % this.SleepEventTurns2 == 0)
				{
					this.SendEvent(Actor, this.SleepEvent2, this.SleepEventLevel2, this.SleepEventSendSource2);
				}
				if (!this.SleepEvent3.IsNullOrEmpty() && this.SleepEventTurns3 > 0 && TurnsAsleep % this.SleepEventTurns3 == 0)
				{
					this.SendEvent(Actor, this.SleepEvent3, this.SleepEventLevel3, this.SleepEventSendSource3);
				}
			}
		}

		// Token: 0x06009E60 RID: 40544 RVA: 0x00395DD4 File Offset: 0x00393FD4
		private void DetachEffects(GameObject GO, IEvent Event)
		{
			Asleep effect = GO.GetEffect<Asleep>();
			if (effect != null && effect.AsleepOn == this.ParentObject)
			{
				GO.FireEvent("WakeUp");
				GO.RemoveEffect(effect, true);
				if (Event != null)
				{
					Event.RequestInterfaceExit();
				}
			}
		}

		// Token: 0x06009E61 RID: 40545 RVA: 0x00395E18 File Offset: 0x00394018
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

		// Token: 0x04004178 RID: 16760
		public string SleepEvent1;

		// Token: 0x04004179 RID: 16761
		public string SleepEvent2;

		// Token: 0x0400417A RID: 16762
		public string SleepEvent3;

		// Token: 0x0400417B RID: 16763
		public int SleepEventTurns1;

		// Token: 0x0400417C RID: 16764
		public int SleepEventTurns2;

		// Token: 0x0400417D RID: 16765
		public int SleepEventTurns3;

		// Token: 0x0400417E RID: 16766
		public int SleepEventLevel1;

		// Token: 0x0400417F RID: 16767
		public int SleepEventLevel2;

		// Token: 0x04004180 RID: 16768
		public int SleepEventLevel3;

		// Token: 0x04004181 RID: 16769
		public bool NoSmartUse;

		// Token: 0x04004182 RID: 16770
		public bool SleepEventSendSource1;

		// Token: 0x04004183 RID: 16771
		public bool SleepEventSendSource2;

		// Token: 0x04004184 RID: 16772
		public bool SleepEventSendSource3;

		// Token: 0x04004185 RID: 16773
		public bool BreakIfGiganticOnNonGigantic = true;

		// Token: 0x04004186 RID: 16774
		public string BreakIfUserHasPart;

		// Token: 0x04004187 RID: 16775
		public int HealCounter;

		// Token: 0x04004188 RID: 16776
		public long lastIdleUsed;
	}
}
