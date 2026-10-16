using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DFD RID: 3581
	[Serializable]
	public class ReplicatedAchievement : IPart
	{
		// Token: 0x060090DF RID: 37087 RVA: 0x00343634 File Offset: 0x00341834
		public override bool SameAs(IPart p)
		{
			ReplicatedAchievement replicatedAchievement = p as ReplicatedAchievement;
			return replicatedAchievement.Triggered == this.Triggered && !(replicatedAchievement.AchievementID != this.AchievementID) && base.SameAs(p);
		}

		// Token: 0x060090E0 RID: 37088 RVA: 0x00343674 File Offset: 0x00341874
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060090E1 RID: 37089 RVA: 0x00343677 File Offset: 0x00341877
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == WasReplicatedEvent.ID && !this.Triggered);
		}

		// Token: 0x060090E2 RID: 37090 RVA: 0x00343698 File Offset: 0x00341898
		public override bool HandleEvent(WasReplicatedEvent E)
		{
			if (!this.Triggered && E.Actor != null && E.Actor.IsPlayer())
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C6A RID: 15466
		public bool Triggered;

		// Token: 0x04003C6B RID: 15467
		public string AchievementID;
	}
}
