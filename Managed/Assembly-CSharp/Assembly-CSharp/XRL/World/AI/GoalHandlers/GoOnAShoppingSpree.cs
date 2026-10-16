using System;
using System.Collections.Generic;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B76 RID: 2934
	[Serializable]
	public class GoOnAShoppingSpree : GoalHandler
	{
		// Token: 0x0600758C RID: 30092 RVA: 0x002C1D90 File Offset: 0x002BFF90
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600758D RID: 30093 RVA: 0x002C1D93 File Offset: 0x002BFF93
		public void MoveToNewZone()
		{
			this.ParentBrain.PushGoal(new MoveToGlobal(GoOnAShoppingSpree.TargetZones.GetRandomElement<string>(null), 38, 10));
		}

		// Token: 0x0600758E RID: 30094 RVA: 0x002C1DB8 File Offset: 0x002BFFB8
		public void LookForMerchant()
		{
			if (!base.ParentObject.InACell())
			{
				return;
			}
			GameObject gameObject = base.CurrentZone.FindClosestObjectWithPart(base.ParentObject, "ConversationScript", false, true);
			if (gameObject == null || !gameObject.InACell())
			{
				base.Pop();
				return;
			}
			List<Cell> list = gameObject.CurrentCell.GetLocalAdjacentCells().ShuffleInPlace<Cell>(null);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].IsEmpty())
				{
					this.ParentBrain.PushGoal(new MoveTo(base.CurrentZone.ZoneID, list[i].X, list[i].Y, true, false, 0, false, false, false, -1, -1, 95));
					return;
				}
			}
			this.ParentBrain.PushGoal(new WanderRandomly(6));
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x002C1E80 File Offset: 0x002C0080
		public override void TakeAction()
		{
			if (25.in100())
			{
				this.MoveToNewZone();
				return;
			}
			this.LookForMerchant();
		}

		// Token: 0x040035D3 RID: 13779
		public static List<string> TargetZones = new List<string> { "JoppaWorld.5.2.0.0.10", "JoppaWorld.5.2.0.1.10", "JoppaWorld.5.2.0.2.10", "JoppaWorld.5.2.1.0.10", "JoppaWorld.5.2.1.2.10", "JoppaWorld.5.2.2.0.10", "JoppaWorld.5.2.2.1.10", "JoppaWorld.5.2.2.2.10" };
	}
}
