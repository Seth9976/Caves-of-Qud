using System;
using System.Collections.Generic;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B93 RID: 2963
	[Serializable]
	public class Wander : IMovementGoal
	{
		// Token: 0x06007654 RID: 30292 RVA: 0x002C7B5F File Offset: 0x002C5D5F
		public override bool IsBusy()
		{
			return false;
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x002C7B64 File Offset: 0x002C5D64
		private bool SuitableToWander(Cell C, Cell CC, int MaxWeight, ref int Nav)
		{
			return !C.IsOccluding() && !C.IsSolidFor(base.ParentObject, false) && CC.PathDistanceTo(C) <= this.ParentBrain.MaxWanderRadius && C.IsReachable() && !C.HasObjectWithTagOrProperty("WanderStopper") && C.NavigationWeight(base.ParentObject, ref Nav) < MaxWeight;
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x002C7BD0 File Offset: 0x002C5DD0
		public override void Create()
		{
			base.Pop();
			Cell currentCell = base.ParentObject.CurrentCell;
			Zone parentZone = currentCell.ParentZone;
			int num = 268435456;
			int num2 = 0;
			Cell cell = parentZone.GetRandomCell(0);
			while (!this.SuitableToWander(cell, currentCell, (num2 < 50) ? 3 : 10, ref num))
			{
				if (++num2 >= 100)
				{
					return;
				}
				cell = parentZone.GetRandomCell(0);
			}
			this.Target = cell;
			base.Think("I'll wander around to " + this.Target.X.ToString() + "," + this.Target.Y.ToString());
			if (this.Target.HasObjectWithPart("StairsUp") && base.ParentObject.HasTagOrProperty("WanderUpStairs"))
			{
				base.PushChildGoal(new Step("U", true, false, true, false, null, false), this.ParentHandler);
			}
			else if (this.Target.HasObjectWithPart("StairsDown") && base.ParentObject.HasTagOrProperty("WanderDownStairs"))
			{
				base.PushChildGoal(new Step("D", true, false, true, false, null, false), this.ParentHandler);
			}
			base.PushChildGoal(new MoveTo(parentZone.ZoneID, this.Target.X, this.Target.Y, true, false, 0, true, false, false, -1, -1, 95), this.ParentHandler);
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x002C7D29 File Offset: 0x002C5F29
		public override void TakeAction()
		{
		}

		// Token: 0x0400363B RID: 13883
		public Cell Target;

		// Token: 0x0400363C RID: 13884
		[NonSerialized]
		public static List<AICommandList> CommandList = new List<AICommandList>();
	}
}
