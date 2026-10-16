using System;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F99 RID: 3993
	[Serializable]
	public class Chair : IActivePart
	{
		// Token: 0x06009E6C RID: 40556 RVA: 0x003960CE File Offset: 0x003942CE
		public Chair()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009E6D RID: 40557 RVA: 0x003960EC File Offset: 0x003942EC
		public override bool SameAs(IPart p)
		{
			Chair chair = p as Chair;
			return chair.Level == this.Level && chair.LevelWhenDisabled == this.LevelWhenDisabled && !(chair.DamageAttributes != this.DamageAttributes) && chair.NoSmartUse == this.NoSmartUse && chair.BreakIfGiganticOnNonGigantic == this.BreakIfGiganticOnNonGigantic && chair.Securing == this.Securing && base.SameAs(p);
		}

		// Token: 0x06009E6E RID: 40558 RVA: 0x0039616C File Offset: 0x0039436C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == BeforeDestroyObjectEvent.ID || (ID == CanSmartUseEvent.ID && !this.NoSmartUse) || (ID == CommandSmartUseEvent.ID && !this.NoSmartUse) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID || ID == LeftCellEvent.ID || (ID == EnteredCellEvent.ID && this.Securing) || ID == PooledEvent<PollForHealingLocationEvent>.ID || ID == PooledEvent<UseHealingLocationEvent>.ID;
		}

		// Token: 0x06009E6F RID: 40559 RVA: 0x00396213 File Offset: 0x00394413
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x00396224 File Offset: 0x00394424
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.DetachEffects(null, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x00396238 File Offset: 0x00394438
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (this.Securing)
			{
				Cell cell = E.Cell;
				this.Seated = ((cell != null) ? cell.GetFirstObject(new Predicate<GameObject>(this.IsSeated)) : null);
			}
			else
			{
				this.DetachEffects(E.Cell, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E72 RID: 40562 RVA: 0x00396287 File Offset: 0x00394487
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Securing && GameObject.Validate(ref this.Seated))
			{
				this.MoveSeated(this.Seated, E.Cell, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E73 RID: 40563 RVA: 0x003962B8 File Offset: 0x003944B8
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.SyncEffects();
			return base.HandleEvent(E);
		}

		// Token: 0x06009E74 RID: 40564 RVA: 0x003962C7 File Offset: 0x003944C7
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.SyncEffects();
			return base.HandleEvent(E);
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x003962D8 File Offset: 0x003944D8
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ChargeUse > 0)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && currentCell.HasObjectWithEffect("Sitting", new Predicate<GameObject>(this.IsSittingOnThis)))
				{
					base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
					this.SyncEffects();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E76 RID: 40566 RVA: 0x0039634C File Offset: 0x0039454C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.Understood())
			{
				if (E.Actor.HasEffect<Sitting>((Sitting fx) => fx.SittingOn == this.ParentObject))
				{
					E.AddAction("Stand", "stand", "StandUpFromChair", null, 's', false, 5, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Sit", "sit", "SitOnChair", null, 's', false, 5, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E77 RID: 40567 RVA: 0x003963D0 File Offset: 0x003945D0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "SitOnChair")
			{
				this.SitDown(E.Actor, E);
			}
			else if (E.Command == "StandUpFromChair")
			{
				this.StandUp(E.Actor, E, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E78 RID: 40568 RVA: 0x00396428 File Offset: 0x00394628
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (this.NoSmartUse || E.Actor == this.ParentObject || !this.ParentObject.Understood() || !E.Actor.PhaseMatches(this.ParentObject) || !E.Actor.FlightCanReach(this.ParentObject)) && base.HandleEvent(E);
		}

		// Token: 0x06009E79 RID: 40569 RVA: 0x00396488 File Offset: 0x00394688
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (!this.NoSmartUse && E.Actor != this.ParentObject && this.ParentObject.Understood() && E.Actor.PhaseMatches(this.ParentObject) && E.Actor.FlightCanReach(this.ParentObject))
			{
				if (E.Actor.HasEffect<Sitting>((Sitting fx) => fx.SittingOn == this.ParentObject))
				{
					this.StandUp(E.Actor, E, null);
				}
				else
				{
					this.SitDown(E.Actor, E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E7A RID: 40570 RVA: 0x00396520 File Offset: 0x00394720
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (this.Level > -10 && E.Actor != this.ParentObject)
			{
				string owner = this.ParentObject.Owner;
				if ((owner.IsNullOrEmpty() || E.Actor.HasTagOrProperty(owner) || E.Actor.DisplayNameOnly == owner || E.Actor.BelongsToFaction(owner)) && 1.in100())
				{
					Sitting effect = E.Actor.GetEffect<Sitting>();
					if (effect != null)
					{
						if (effect.SittingOn == this.ParentObject && this.StandUp(E.Actor, null, null))
						{
							return false;
						}
					}
					else if (IComponent<GameObject>.currentTurn - this.LastIdleUsed >= 50L)
					{
						int num = this.ParentObject.DistanceTo(E.Actor);
						if (num <= 1)
						{
							this.LastIdleUsed = IComponent<GameObject>.currentTurn;
							if (this.SitDown(E.Actor, null))
							{
								return false;
							}
						}
						else if (Stat.Random(1, 40) > num)
						{
							this.LastIdleUsed = IComponent<GameObject>.currentTurn;
							GameObject who = E.Actor;
							who.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
							{
								if (who.CurrentCell.Objects.Contains(this.ParentObject))
								{
									this.SitDown(who, null);
								}
								h.FailToParent();
							}, null, null));
							who.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E7B RID: 40571 RVA: 0x00396698 File Offset: 0x00394898
		public override bool HandleEvent(PollForHealingLocationEvent E)
		{
			if (E.Actor != this.ParentObject && E.Actor.PhaseMatches(this.ParentObject) && E.Actor.FlightCanReach(this.ParentObject))
			{
				int num = this.EffectiveLevel();
				if (num > -10)
				{
					E.Value = Math.Max(E.Value, Math.Min(Math.Max(1, num), 9));
					if (E.First)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x00396715 File Offset: 0x00394915
		public override bool HandleEvent(UseHealingLocationEvent E)
		{
			if (this.EffectiveLevel() > -10 && !E.Actor.HasEffect<Sitting>())
			{
				this.SitDown(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E7D RID: 40573 RVA: 0x00396743 File Offset: 0x00394943
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x00396748 File Offset: 0x00394948
		public int EffectiveLevel()
		{
			if (!base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return this.Level;
			}
			return this.LevelWhenDisabled;
		}

		// Token: 0x06009E7F RID: 40575 RVA: 0x0039678C File Offset: 0x0039498C
		private void SyncObjectEffect(GameObject GO)
		{
			Sitting effect = GO.GetEffect<Sitting>((Sitting fx) => fx.SittingOn == this.ParentObject);
			if (effect != null)
			{
				effect.Level = this.EffectiveLevel();
				effect.DamageAttributes = this.DamageAttributes;
			}
		}

		// Token: 0x06009E80 RID: 40576 RVA: 0x003967C7 File Offset: 0x003949C7
		public void SyncEffects()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.ForeachObject(new Action<GameObject>(this.SyncObjectEffect));
		}

		// Token: 0x06009E81 RID: 40577 RVA: 0x003967EC File Offset: 0x003949EC
		private void DetachEffects(GameObject GO, IEvent Event)
		{
			Sitting effect = GO.GetEffect<Sitting>((Sitting fx) => fx.SittingOn == this.ParentObject);
			if (effect != null)
			{
				GO.RemoveEffect(effect, true);
				GO.ApplyEffect(new Prone(), null);
				if (Event != null)
				{
					Event.RequestInterfaceExit();
				}
			}
		}

		// Token: 0x06009E82 RID: 40578 RVA: 0x00396830 File Offset: 0x00394A30
		private void DetachEffects(Cell Cell = null, IEvent Event = null)
		{
			if (Cell == null)
			{
				Cell = this.ParentObject.GetCurrentCell();
			}
			if (Cell != null)
			{
				Cell.SafeForeachObject(delegate(GameObject o)
				{
					this.DetachEffects(o, Event);
				});
			}
		}

		// Token: 0x06009E83 RID: 40579 RVA: 0x00396878 File Offset: 0x00394A78
		private bool IsSeated(GameObject Object)
		{
			Sitting sitting;
			return Object.TryGetEffect<Sitting>(out sitting) && sitting.SittingOn == this.ParentObject;
		}

		// Token: 0x06009E84 RID: 40580 RVA: 0x003968A0 File Offset: 0x00394AA0
		public bool IsOccupiedFor(GameObject Object)
		{
			foreach (GameObject gameObject in this.ParentObject.CurrentCell.Objects)
			{
				if (gameObject.IsCombatObject() && gameObject != Object && (gameObject != this.ParentObject || !gameObject.IsAlliedTowards(Object)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009E85 RID: 40581 RVA: 0x0039691C File Offset: 0x00394B1C
		private void MoveSeated(GameObject Object, Cell Target, IEvent Event)
		{
			if (Object.DistanceTo(Target) > 0)
			{
				if (!Object.SystemMoveTo(Target, new int?(0), false, true, false, false))
				{
					this.DetachEffects(Object, Event);
					return;
				}
				if (Event != null)
				{
					Event.RequestInterfaceExit();
				}
			}
		}

		// Token: 0x06009E86 RID: 40582 RVA: 0x00396950 File Offset: 0x00394B50
		public bool SitDown(GameObject Actor, IEvent FromEvent = null)
		{
			if (Actor == this.ParentObject)
			{
				return Actor.Fail("You cannot sit on " + Actor.itself + ".");
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, true, false, false, true, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.Broken)
				{
					return Actor.Fail(this.ParentObject.Itis + " broken.");
				}
				return Actor.Fail(this.ParentObject.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " seem to be working.");
			}
			else
			{
				if (Actor.HasEffect<Sitting>())
				{
					return Actor.Fail("You are already sitting down.");
				}
				if (Actor.HasEffect<Enclosed>())
				{
					return Actor.Fail("You cannot do that while enclosed.");
				}
				if (Actor.HasEffect<Burrowed>())
				{
					return Actor.Fail("You cannot do that while burrowed.");
				}
				if (!Actor.CanChangeBodyPosition("Sitting", true, false, false, false))
				{
					return false;
				}
				if (!Actor.FlightCanReach(this.ParentObject))
				{
					return Actor.Fail("You cannot reach " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				if (!Actor.PhaseMatches(this.ParentObject))
				{
					return Actor.Fail("You are out of phase with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				Cell cell = this.ParentObject.CurrentCell;
				if (cell == null)
				{
					cell = Actor.CurrentCell;
					if (cell == null)
					{
						return false;
					}
					if (this.ParentObject.Equipped == Actor)
					{
						Event @event = Event.New("CommandUnequipObject", 0, 0, 0);
						@event.SetParameter("Object", this.ParentObject);
						@event.SetFlag("NoStack", true);
						if (!Actor.FireEvent(@event))
						{
							return Actor.Fail("You cannot unequip " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
						}
					}
					if (this.ParentObject.InInventory == Actor)
					{
						this.ParentObject.SplitFromStack();
						if (!InventoryActionEvent.Check(Actor, Actor, this.ParentObject, "CommandDropObject", false, false, false, false, false, 0, 0, 0, null, null, null, null))
						{
							return Actor.Fail("You cannot set " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " down!");
						}
					}
					if (this.ParentObject.CurrentCell != cell)
					{
						return false;
					}
				}
				else
				{
					this.ParentObject.SplitFromStack();
				}
				if (Actor.CurrentCell != cell && (!Actor.Move(Actor.GetDirectionToward(this.ParentObject, false), false, false, false, false, false, false, null, null, false, null, null, null, true, false, null, null, 0) || Actor.CurrentCell != cell))
				{
					if (FromEvent != null)
					{
						FromEvent.RequestInterfaceExit();
					}
					this.ParentObject.CheckStack();
					return false;
				}
				if (this.BreakIfGiganticOnNonGigantic && Actor.IsGiganticCreature && !this.ParentObject.IsGiganticEquipment && this.ParentObject.ApplyEffect(new Broken(), null) && base.IsBroken())
				{
					Actor.Fail("You think you broke " + this.ParentObject.them + "...");
					this.ParentObject.CheckStack();
					return true;
				}
				IComponent<GameObject>.XDidYToZ(Actor, "sit", "down on", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				Actor.ApplyEffect(new Sitting(this.ParentObject, this.EffectiveLevel(), this.DamageAttributes), null);
				if (Actor.IsPlayer())
				{
					this.ParentObject.SetIntProperty("DroppedByPlayer", 1, false);
					MetricsManager.LogEvent("ChairsSat");
				}
				Actor.UseEnergy(1000, "Position", null, null, false);
				if (FromEvent != null)
				{
					FromEvent.RequestInterfaceExit();
				}
				Actor.FireEvent(Event.New("SatIn", "Object", this.ParentObject));
				this.ParentObject.FireEvent(Event.New("BeingSatOn", "Object", Actor));
				return true;
			}
		}

		// Token: 0x06009E87 RID: 40583 RVA: 0x00396DC8 File Offset: 0x00394FC8
		public bool StandUp(GameObject Actor, IEvent FromEvent = null, Sitting S = null)
		{
			if (S == null)
			{
				S = Actor.GetEffect<Sitting>();
				if (S == null)
				{
					return Actor.Fail("You are not sitting down.");
				}
			}
			if (S.SittingOn != this.ParentObject)
			{
				return Actor.Fail("It is not " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " that you are sitting on.");
			}
			IComponent<GameObject>.XDidY(Actor, "stand", "up", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			Actor.RemoveEffect(S, true);
			Actor.UseEnergy(1000, "Position", null, null, false);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x06009E88 RID: 40584 RVA: 0x00396E8C File Offset: 0x0039508C
		private bool IsSittingOnThis(GameObject obj)
		{
			Sitting effect = obj.GetEffect<Sitting>();
			return effect != null && effect.SittingOn == this.ParentObject;
		}

		// Token: 0x0400418B RID: 16779
		public int Level;

		// Token: 0x0400418C RID: 16780
		public int LevelWhenDisabled = -1;

		// Token: 0x0400418D RID: 16781
		public string DamageAttributes;

		// Token: 0x0400418E RID: 16782
		public bool NoSmartUse;

		// Token: 0x0400418F RID: 16783
		public bool BreakIfGiganticOnNonGigantic = true;

		// Token: 0x04004190 RID: 16784
		public bool Securing;

		// Token: 0x04004191 RID: 16785
		public long LastIdleUsed;

		// Token: 0x04004192 RID: 16786
		[NonSerialized]
		private GameObject Seated;
	}
}
