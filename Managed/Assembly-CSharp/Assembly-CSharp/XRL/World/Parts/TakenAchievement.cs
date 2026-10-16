using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DFE RID: 3582
	[Serializable]
	public class TakenAchievement : IPart
	{
		// Token: 0x060090E4 RID: 37092 RVA: 0x003436F0 File Offset: 0x003418F0
		public override bool SameAs(IPart p)
		{
			TakenAchievement takenAchievement = p as TakenAchievement;
			return !(takenAchievement.AchievementID != this.AchievementID) && takenAchievement.Triggered == this.Triggered && base.SameAs(p);
		}

		// Token: 0x060090E5 RID: 37093 RVA: 0x00343730 File Offset: 0x00341930
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x060090E6 RID: 37094 RVA: 0x0034374E File Offset: 0x0034194E
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Trigger(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x060090E7 RID: 37095 RVA: 0x00343763 File Offset: 0x00341963
		public override bool HandleEvent(TakenEvent E)
		{
			this.Trigger(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x060090E8 RID: 37096 RVA: 0x00343778 File Offset: 0x00341978
		public void Trigger(GameObject who)
		{
			if (!this.Triggered && who != null && who.IsPlayer())
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
			}
		}

		// Token: 0x04003C6C RID: 15468
		public string AchievementID = "";

		// Token: 0x04003C6D RID: 15469
		public bool Triggered;
	}
}
