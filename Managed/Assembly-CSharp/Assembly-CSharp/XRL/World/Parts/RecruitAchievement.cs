using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DFC RID: 3580
	[Serializable]
	public class RecruitAchievement : IPart
	{
		// Token: 0x060090DB RID: 37083 RVA: 0x00343570 File Offset: 0x00341770
		public override bool SameAs(IPart p)
		{
			RecruitAchievement recruitAchievement = p as RecruitAchievement;
			return recruitAchievement.Triggered == this.Triggered && !(recruitAchievement.AchievementID != this.AchievementID) && base.SameAs(p);
		}

		// Token: 0x060090DC RID: 37084 RVA: 0x003435B0 File Offset: 0x003417B0
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<AfterChangePartyLeaderEvent>.ID;
		}

		// Token: 0x060090DD RID: 37085 RVA: 0x003435C8 File Offset: 0x003417C8
		public override bool HandleEvent(AfterChangePartyLeaderEvent E)
		{
			if (!this.Triggered && !E.Transient && E.NewLeader != null && E.NewLeader.IsPlayerControlled() && !this.AchievementID.IsNullOrEmpty())
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C68 RID: 15464
		public bool Triggered;

		// Token: 0x04003C69 RID: 15465
		public string AchievementID;
	}
}
