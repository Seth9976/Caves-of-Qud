using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x020011A3 RID: 4515
	[Serializable]
	public class Explores : IPart
	{
		// Token: 0x0600B388 RID: 45960 RVA: 0x00417F57 File Offset: 0x00416157
		public override bool WantTurnTick()
		{
			return !this.WantExplore;
		}

		// Token: 0x0600B389 RID: 45961 RVA: 0x00417F62 File Offset: 0x00416162
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Tick(Amount);
		}

		// Token: 0x0600B38A RID: 45962 RVA: 0x00417F6B File Offset: 0x0041616B
		public void Tick(int Increment = 1)
		{
			if (Stat.Random(1, this.ExploreOneIn) <= Increment)
			{
				this.WantExplore = true;
			}
		}

		// Token: 0x0600B38B RID: 45963 RVA: 0x00417F84 File Offset: 0x00416184
		public bool Explore()
		{
			Brain brain = this.ParentObject.Brain;
			if (((brain != null) ? brain.PartyLeader : null) == null || brain.Staying)
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.OnWorldMap())
			{
				return false;
			}
			if (!currentCell.IsReachable() || !currentCell.ParentZone.IsActive())
			{
				return false;
			}
			List<Cell> cells = currentCell.ParentZone.GetCells(new Predicate<Cell>(this.IsValidTarget));
			if (cells.IsNullOrEmpty<Cell>())
			{
				return false;
			}
			if (this.WaitGoal == null)
			{
				this.WaitGoal = new DelegateGoal(new Action<GoalHandler>(this.WaitAction), new Func<GoalHandler, bool>(this.WaitFinished), null)
				{
					SetCanFight = new bool?(false),
					SetNonAggressive = new bool?(true)
				};
			}
			base.EmitMessage(GameText.VariableReplace(this.ExploreMessage, this.ParentObject, null, false), ' ', false, false, false, null, null);
			brain.PushGoal(this.WaitGoal);
			brain.PushGoal(new MoveTo(cells.GetRandomElement<Cell>(null), false, false, 0, false, false, false, -1, -1, 95));
			return true;
		}

		// Token: 0x0600B38C RID: 45964 RVA: 0x00418096 File Offset: 0x00416296
		private bool IsValidTarget(Cell C)
		{
			return !C.IsExplored() && C.IsReachable() && C.IsPassable(this.ParentObject, true);
		}

		// Token: 0x0600B38D RID: 45965 RVA: 0x004180B7 File Offset: 0x004162B7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (this.WantExplore && ID == PooledEvent<AIBoredEvent>.ID);
		}

		// Token: 0x0600B38E RID: 45966 RVA: 0x004180D7 File Offset: 0x004162D7
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.WantExplore)
			{
				this.WantExplore = false;
				if (this.Explore())
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B38F RID: 45967 RVA: 0x004180FC File Offset: 0x004162FC
		public bool WaitFinished(GoalHandler Goal)
		{
			if (this.WaitForLeader)
			{
				GameObject partyLeader = Goal.ParentBrain.PartyLeader;
				return partyLeader == null || partyLeader.InSameOrAdjacentCellTo(Goal.ParentObject);
			}
			return Goal.CurrentCell.IsExplored();
		}

		// Token: 0x0600B390 RID: 45968 RVA: 0x0041813A File Offset: 0x0041633A
		public void WaitAction(GoalHandler Goal)
		{
			Goal.ParentObject.ForfeitTurn(false);
		}

		// Token: 0x04004861 RID: 18529
		public int ExploreOneIn = 200;

		// Token: 0x04004862 RID: 18530
		public string ExploreMessage = "=subject.T= =verb:float= off.";

		/// <summary>Wait until leader adjacent, otherwise wait until explored.</summary>
		// Token: 0x04004863 RID: 18531
		public bool WaitForLeader;

		// Token: 0x04004864 RID: 18532
		[NonSerialized]
		private DelegateGoal WaitGoal;

		// Token: 0x04004865 RID: 18533
		[NonSerialized]
		private bool WantExplore;
	}
}
