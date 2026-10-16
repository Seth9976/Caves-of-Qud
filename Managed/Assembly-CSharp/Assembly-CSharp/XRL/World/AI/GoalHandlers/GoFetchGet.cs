using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9F RID: 2975
	[Serializable]
	public class GoFetchGet : GoalHandler
	{
		// Token: 0x0600769D RID: 30365 RVA: 0x002C9605 File Offset: 0x002C7805
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x002C9608 File Offset: 0x002C7808
		public override void Create()
		{
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x002C960C File Offset: 0x002C780C
		public override void TakeAction()
		{
			base.Pop();
			if (base.ParentObject.CurrentCell != null)
			{
				GameObject randomElement = base.ParentObject.CurrentCell.GetObjects((GameObject o) => o.ShouldAutoget()).GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					this.ParentBrain.ParentObject.ReceiveObject(randomElement, false, null);
				}
			}
		}
	}
}
