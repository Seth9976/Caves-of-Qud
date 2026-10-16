using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DEF RID: 3567
	[Serializable]
	public class ChatAchievement : IPart
	{
		// Token: 0x06009098 RID: 37016 RVA: 0x003426D4 File Offset: 0x003408D4
		public override bool SameAs(IPart p)
		{
			ChatAchievement chatAchievement = p as ChatAchievement;
			return !(chatAchievement.AchievementID != this.AchievementID) && chatAchievement.Triggered == this.Triggered && base.SameAs(p);
		}

		// Token: 0x06009099 RID: 37017 RVA: 0x00342714 File Offset: 0x00340914
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeConversationEvent.ID;
		}

		// Token: 0x0600909A RID: 37018 RVA: 0x0034272A File Offset: 0x0034092A
		public override bool HandleEvent(BeforeConversationEvent E)
		{
			if (!this.Triggered && E.SpeakingWith == this.ParentObject && E.Actor.IsPlayer())
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600909B RID: 37019 RVA: 0x00342768 File Offset: 0x00340968
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003C3A RID: 15418
		public string AchievementID = "";

		// Token: 0x04003C3B RID: 15419
		public bool Triggered;
	}
}
