using System;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E33 RID: 3635
	[Serializable]
	public class AIWallPhaser : AIBehaviorPart
	{
		// Token: 0x06009390 RID: 37776 RVA: 0x00352090 File Offset: 0x00350290
		public int GetPhase()
		{
			long timeTicks = The.Game.TimeTicks;
			if (timeTicks != this.LastTick)
			{
				this.LastTick = timeTicks;
				this.LastPhase = this.ParentObject.GetPhase();
			}
			return this.LastPhase;
		}

		// Token: 0x06009391 RID: 37777 RVA: 0x003520D0 File Offset: 0x003502D0
		public bool TryMoveNearestWall(Cell Origin = null, bool IgnoreCombat = false, bool ClearMovement = false)
		{
			if (this.GetPhase() != 2)
			{
				return false;
			}
			if (Origin == null)
			{
				Origin = this.ParentObject.CurrentCell;
			}
			Cell cell = Origin;
			int num = int.MaxValue;
			foreach (GameObject gameObject in Origin.ParentZone.IterateObjects())
			{
				if (gameObject.IsWall() && !this.ParentObject.PhaseMatches(gameObject))
				{
					Cell currentCell = gameObject.CurrentCell;
					int num2 = Origin.DistanceTo(currentCell);
					if (num2 > 0 && num2 < num)
					{
						cell = currentCell;
						num = num2;
					}
				}
			}
			if (cell != Origin && cell != this.ParentObject.CurrentCell)
			{
				Brain brain = this.ParentObject.Brain;
				if (ClearMovement)
				{
					brain.RemoveGoalsDescendedFrom<IMovementGoal>();
				}
				brain.PushGoal(new MoveTo(cell, false, IgnoreCombat, 0, false, false, false, -1, -1, 95));
				return true;
			}
			return false;
		}

		// Token: 0x06009392 RID: 37778 RVA: 0x003521A8 File Offset: 0x003503A8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == ActorGetNavigationWeightEvent.ID;
		}

		// Token: 0x06009393 RID: 37779 RVA: 0x003521C8 File Offset: 0x003503C8
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.TryMoveNearestWall(null, false, false))
			{
				return false;
			}
			if (this.ParentObject.CurrentCell.HasWall())
			{
				this.ParentObject.Brain.PushGoal(new Wait(Stat.Random(5, 10), "I like walls"));
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009394 RID: 37780 RVA: 0x00352220 File Offset: 0x00350420
		public override bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			if (E.Weight < 3 && this.GetPhase() == 2 && !E.Cell.HasWall())
			{
				E.Weight = 3;
				E.Uncacheable = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009395 RID: 37781 RVA: 0x00352256 File Offset: 0x00350456
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009396 RID: 37782 RVA: 0x00352259 File Offset: 0x00350459
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AICantAttackRange");
		}

		// Token: 0x06009397 RID: 37783 RVA: 0x00352268 File Offset: 0x00350468
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "AICantAttackRange") || this.ParentObject.Target == null || (!this.TryMoveNearestWall(this.ParentObject.Target.CurrentCell, false, true) && !this.ParentObject.CurrentCell.HasWall())) && base.FireEvent(E);
		}

		// Token: 0x04003D0A RID: 15626
		[NonSerialized]
		private long LastTick = -1L;

		// Token: 0x04003D0B RID: 15627
		[NonSerialized]
		private int LastPhase;
	}
}
