using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9E RID: 2974
	[Serializable]
	public class GoFetch : GoalHandler
	{
		// Token: 0x06007698 RID: 30360 RVA: 0x002C95A3 File Offset: 0x002C77A3
		public GoFetch(Cell c)
		{
			this.targetCell = c;
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x002C95B2 File Offset: 0x002C77B2
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x002C95B5 File Offset: 0x002C77B5
		public override void Create()
		{
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x002C95B8 File Offset: 0x002C77B8
		public override void TakeAction()
		{
			base.Pop();
			this.ParentBrain.PushGoal(new GoFetchGet());
			this.ParentBrain.PushGoal(new MoveTo(this.targetCell, false, false, 0, false, false, false, -1, -1, 95));
		}

		// Token: 0x04003657 RID: 13911
		private Cell targetCell;
	}
}
