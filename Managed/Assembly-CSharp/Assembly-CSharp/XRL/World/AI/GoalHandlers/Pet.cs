using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8E RID: 2958
	[Serializable]
	public class Pet : GoalHandler
	{
		// Token: 0x0600762F RID: 30255 RVA: 0x002C6B83 File Offset: 0x002C4D83
		public Pet()
		{
		}

		// Token: 0x06007630 RID: 30256 RVA: 0x002C6B99 File Offset: 0x002C4D99
		public Pet(GameObject Target, int GoToTries = 5, int MaxDistance = -1)
			: this()
		{
			this.Target = Target;
			this.GoToTries = GoToTries;
			this.MaxDistance = MaxDistance;
		}

		// Token: 0x06007631 RID: 30257 RVA: 0x002C6BB6 File Offset: 0x002C4DB6
		public override bool Finished()
		{
			return this.Done;
		}

		// Token: 0x06007632 RID: 30258 RVA: 0x002C6BC0 File Offset: 0x002C4DC0
		public override void TakeAction()
		{
			if (!GameObject.Validate(ref this.Target) || !base.ParentObject.InSameZone(this.Target))
			{
				base.FailToParent();
				return;
			}
			if (this.MaxDistance > -1 && base.ParentObject.DistanceTo(this.Target) > this.MaxDistance)
			{
				base.FailToParent();
				return;
			}
			if (base.ParentObject.InSameOrAdjacentCellTo(this.Target))
			{
				InventoryActionEvent.Check(this.Target, this.ParentBrain.ParentObject, this.Target, "Pet", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				this.Done = true;
				return;
			}
			Cell currentCell = this.Target.CurrentCell;
			if (currentCell == null || currentCell.ParentZone != base.ParentObject.CurrentZone)
			{
				base.FailToParent();
				return;
			}
			int num = this.GoToTries - 1;
			this.GoToTries = num;
			if (num <= 0)
			{
				base.FailToParent();
				return;
			}
			this.PushChildGoal(new MoveTo(this.Target, false, false, 1, false, false, false, -1, this.MaxDistance, 95));
		}

		// Token: 0x04003628 RID: 13864
		public GameObject Target;

		// Token: 0x04003629 RID: 13865
		public Cell Destination;

		// Token: 0x0400362A RID: 13866
		public bool Done;

		// Token: 0x0400362B RID: 13867
		public int GoToTries = 5;

		// Token: 0x0400362C RID: 13868
		public int MaxDistance = -1;
	}
}
