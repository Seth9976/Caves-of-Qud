using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F7C RID: 3964
	[Serializable]
	public class AchievementOnConsume : IPart
	{
		// Token: 0x06009DB9 RID: 40377 RVA: 0x00391EBB File Offset: 0x003900BB
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009DBA RID: 40378 RVA: 0x00391EBE File Offset: 0x003900BE
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == AfterConsumeEvent.ID;
		}

		// Token: 0x06009DBB RID: 40379 RVA: 0x00391ED4 File Offset: 0x003900D4
		public override bool HandleEvent(AfterConsumeEvent E)
		{
			if (!this.AchievementID.IsNullOrEmpty() && this.ParentObject == E.Object)
			{
				AchievementManager.IncrementAchievement(this.AchievementID, 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004134 RID: 16692
		public string AchievementID;
	}
}
