using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DF2 RID: 3570
	[Serializable]
	public class EquippedAchievement : IPart
	{
		// Token: 0x060090AA RID: 37034 RVA: 0x0034292F File Offset: 0x00340B2F
		public EquippedAchievement()
		{
		}

		// Token: 0x060090AB RID: 37035 RVA: 0x00342942 File Offset: 0x00340B42
		public EquippedAchievement(AchievementInfo Achievement)
			: this()
		{
			this.AchievementID = Achievement.ID;
		}

		// Token: 0x060090AC RID: 37036 RVA: 0x00342956 File Offset: 0x00340B56
		public EquippedAchievement(string ID)
			: this()
		{
			this.AchievementID = ID;
		}

		// Token: 0x060090AD RID: 37037 RVA: 0x00342965 File Offset: 0x00340B65
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090AE RID: 37038 RVA: 0x00342968 File Offset: 0x00340B68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID;
		}

		// Token: 0x060090AF RID: 37039 RVA: 0x00342980 File Offset: 0x00340B80
		public override bool HandleEvent(EquippedEvent E)
		{
			if (!this.Triggered && !this.AchievementID.IsNullOrEmpty() && E.Actor.IsPlayer() && this.ParentObject.IsEquippedProperly(null))
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C40 RID: 15424
		public string AchievementID = "";

		// Token: 0x04003C41 RID: 15425
		public bool Triggered;
	}
}
