using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B95 RID: 2965
	[Serializable]
	public class WanderRandomly : IMovementGoal
	{
		// Token: 0x0600765F RID: 30303 RVA: 0x002C7EA5 File Offset: 0x002C60A5
		public WanderRandomly()
		{
		}

		// Token: 0x06007660 RID: 30304 RVA: 0x002C7EAD File Offset: 0x002C60AD
		public WanderRandomly(int Turns)
			: this()
		{
			this.TurnsLeft = Turns;
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x002C7EBC File Offset: 0x002C60BC
		public override bool IsBusy()
		{
			return false;
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x002C7EBF File Offset: 0x002C60BF
		public override bool Finished()
		{
			return this.TurnsLeft <= 0;
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x002C7ECD File Offset: 0x002C60CD
		public override void Create()
		{
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x002C7ED0 File Offset: 0x002C60D0
		public override void TakeAction()
		{
			this.TurnsLeft--;
			if (this.TurnsLeft < 0)
			{
				base.Pop();
				return;
			}
			Cell currentCell = base.ParentObject.CurrentCell;
			if (currentCell.HasObjectWithPart("StairsUp") && base.ParentObject.HasTagOrProperty("WanderUpStairs") && 35.in100())
			{
				base.PushChildGoal(new Step("U", false, false, true, false, null, false), this.ParentHandler);
				return;
			}
			if (currentCell.HasObjectWithPart("StairsDown") && base.ParentObject.HasTagOrProperty("WanderDownStairs") && 35.in100())
			{
				base.PushChildGoal(new Step("D", false, false, true, false, null, false), this.ParentHandler);
				return;
			}
			WanderRandomly.dirs.Clear();
			foreach (Cell cell in currentCell.GetLocalAdjacentCells())
			{
				if (!this.ParentBrain.LimitToAquatic() || cell.HasAquaticSupportFor(base.ParentObject))
				{
					if (cell.HasObjectWithTagOrProperty("WanderStopper"))
					{
						return;
					}
					if (!cell.IsSolidFor(base.ParentObject, false))
					{
						int num = 10 - cell.GetNavigationWeightFor(base.ParentObject, false, false, false, false, false);
						if (num > 0)
						{
							string directionFromCell = currentCell.GetDirectionFromCell(cell, false);
							if (Directions.IsActualDirection(directionFromCell))
							{
								WanderRandomly.dirs[directionFromCell] = num;
							}
						}
					}
				}
			}
			string randomElement = WanderRandomly.dirs.GetRandomElement<string>(null);
			if (!string.IsNullOrEmpty(randomElement))
			{
				base.PushChildGoal(new Step(randomElement, false, false, true, false, null, false), this.ParentHandler);
			}
		}

		// Token: 0x04003641 RID: 13889
		public int TurnsLeft;

		// Token: 0x04003642 RID: 13890
		[NonSerialized]
		private static Dictionary<string, int> dirs = new Dictionary<string, int>(8);
	}
}
