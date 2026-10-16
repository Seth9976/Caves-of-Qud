using System;
using System.Collections.Generic;
using XRL.Core;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B94 RID: 2964
	[Serializable]
	public class WanderDuration : IMovementGoal
	{
		// Token: 0x06007659 RID: 30297 RVA: 0x002C7D37 File Offset: 0x002C5F37
		public WanderDuration()
		{
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x002C7D3F File Offset: 0x002C5F3F
		public WanderDuration(long duration)
			: this()
		{
			this.endturn = XRLCore.Core.Game.Turns + duration;
			this.duration = duration;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x002C7D65 File Offset: 0x002C5F65
		public override bool IsBusy()
		{
			return false;
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x002C7D68 File Offset: 0x002C5F68
		public override void Create()
		{
			Cell currentCell = base.ParentObject.CurrentCell;
			Zone parentZone = currentCell.ParentZone;
			Cell cell = parentZone.GetRandomCell(0);
			int num = 268435456;
			int num2 = 0;
			while (cell.IsOccluding() || (long)currentCell.PathDistanceTo(cell) > this.duration || cell.IsSolidFor(base.ParentObject, false) || !cell.IsReachable() || cell.HasObjectWithTagOrProperty("WanderStopper") || cell.NavigationWeight(base.ParentObject, ref num) >= 10)
			{
				if (++num2 >= 300)
				{
					return;
				}
				cell = parentZone.GetRandomCell(0);
			}
			this.Target = cell;
			base.Think("I'll wander around to " + this.Target.X.ToString() + "," + this.Target.Y.ToString());
			base.PushChildGoal(new MoveTo(parentZone.ZoneID, this.Target.X, this.Target.Y, true, false, 0, true, false, false, -1, -1, 95), this.ParentHandler);
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x002C7E73 File Offset: 0x002C6073
		public override void TakeAction()
		{
			if (XRLCore.Core.Game.Turns >= this.endturn)
			{
				base.Pop();
				return;
			}
			this.Create();
		}

		// Token: 0x0400363D RID: 13885
		public long duration;

		// Token: 0x0400363E RID: 13886
		public long endturn;

		// Token: 0x0400363F RID: 13887
		public Cell Target;

		// Token: 0x04003640 RID: 13888
		[NonSerialized]
		public static List<AICommandList> CommandList = new List<AICommandList>();
	}
}
