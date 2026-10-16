using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DF1 RID: 3569
	[Serializable]
	public class EatenAchievement : IPart
	{
		// Token: 0x060090A4 RID: 37028 RVA: 0x00342873 File Offset: 0x00340A73
		public EatenAchievement()
		{
		}

		// Token: 0x060090A5 RID: 37029 RVA: 0x00342886 File Offset: 0x00340A86
		public EatenAchievement(AchievementInfo Achievement)
		{
			this.AchievementID = Achievement.ID;
		}

		// Token: 0x060090A6 RID: 37030 RVA: 0x003428A5 File Offset: 0x00340AA5
		public EatenAchievement(string AchievementID)
		{
			this.AchievementID = AchievementID;
		}

		// Token: 0x060090A7 RID: 37031 RVA: 0x003428BF File Offset: 0x00340ABF
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090A8 RID: 37032 RVA: 0x003428C2 File Offset: 0x00340AC2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eaten");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090A9 RID: 37033 RVA: 0x003428D8 File Offset: 0x00340AD8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eaten" && !this.Triggered)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					AchievementManager.SetAchievement(this.AchievementID);
					this.Triggered = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C3E RID: 15422
		public string AchievementID = "";

		// Token: 0x04003C3F RID: 15423
		public bool Triggered;
	}
}
