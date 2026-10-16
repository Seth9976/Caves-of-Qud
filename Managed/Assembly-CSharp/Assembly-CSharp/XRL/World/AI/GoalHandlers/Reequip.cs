using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B8F RID: 2959
	[Serializable]
	public class Reequip : GoalHandler
	{
		// Token: 0x06007633 RID: 30259 RVA: 0x002C6CCA File Offset: 0x002C4ECA
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007634 RID: 30260 RVA: 0x002C6CCD File Offset: 0x002C4ECD
		public override void TakeAction()
		{
			this.ParentBrain.DoReequip = true;
			this.ParentBrain.DoPrimaryChoiceOnReequip = false;
			base.Pop();
		}
	}
}
