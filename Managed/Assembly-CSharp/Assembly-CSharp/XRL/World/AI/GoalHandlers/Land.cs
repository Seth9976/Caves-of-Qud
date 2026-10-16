using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B88 RID: 2952
	[Serializable]
	public class Land : IMovementGoal
	{
		// Token: 0x06007609 RID: 30217 RVA: 0x002C585E File Offset: 0x002C3A5E
		public Land(bool overridesCombat = false)
		{
			this.overridesCombat = overridesCombat;
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x002C586D File Offset: 0x002C3A6D
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600760B RID: 30219 RVA: 0x002C5870 File Offset: 0x002C3A70
		public override bool CanFight()
		{
			return !this.overridesCombat;
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x002C587B File Offset: 0x002C3A7B
		public override void TakeAction()
		{
			if (!base.ParentObject.IsFlying)
			{
				base.Pop();
				return;
			}
			if (AttemptToLandEvent.Check(base.ParentObject) && !base.ParentObject.IsFlying)
			{
				base.Pop();
				return;
			}
		}

		// Token: 0x04003609 RID: 13833
		public bool overridesCombat;
	}
}
