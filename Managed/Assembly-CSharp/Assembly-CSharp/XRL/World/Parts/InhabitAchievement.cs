using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DF8 RID: 3576
	[Serializable]
	public class InhabitAchievement : IPart
	{
		// Token: 0x060090C8 RID: 37064 RVA: 0x0034325C File Offset: 0x0034145C
		public override bool SameAs(IPart p)
		{
			InhabitAchievement inhabitAchievement = p as InhabitAchievement;
			return inhabitAchievement.Triggered == this.Triggered && !(inhabitAchievement.AchievementID != this.AchievementID) && base.SameAs(p);
		}

		// Token: 0x060090C9 RID: 37065 RVA: 0x0034329C File Offset: 0x0034149C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060090CA RID: 37066 RVA: 0x0034329F File Offset: 0x0034149F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090CB RID: 37067 RVA: 0x003432B4 File Offset: 0x003414B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction" && !this.Triggered && this.ParentObject != null && this.ParentObject.IsPlayer() && !string.IsNullOrEmpty(this.AchievementID))
			{
				AchievementManager.SetAchievement(this.AchievementID);
				this.Triggered = true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C5D RID: 15453
		public bool Triggered;

		// Token: 0x04003C5E RID: 15454
		public string AchievementID;
	}
}
