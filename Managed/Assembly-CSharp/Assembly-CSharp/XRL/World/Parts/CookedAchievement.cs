using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DF0 RID: 3568
	[Serializable]
	public class CookedAchievement : IPart
	{
		// Token: 0x0600909D RID: 37021 RVA: 0x0034277E File Offset: 0x0034097E
		public CookedAchievement()
		{
		}

		// Token: 0x0600909E RID: 37022 RVA: 0x00342791 File Offset: 0x00340991
		public CookedAchievement(AchievementInfo Achievement)
		{
			this.AchievementID = Achievement.ID;
		}

		// Token: 0x0600909F RID: 37023 RVA: 0x003427B0 File Offset: 0x003409B0
		public CookedAchievement(string AchievementID)
		{
			this.AchievementID = AchievementID;
		}

		// Token: 0x060090A0 RID: 37024 RVA: 0x003427CC File Offset: 0x003409CC
		public override bool SameAs(IPart p)
		{
			CookedAchievement cookedAchievement = p as CookedAchievement;
			return cookedAchievement != null && this.AchievementID == cookedAchievement.AchievementID;
		}

		// Token: 0x060090A1 RID: 37025 RVA: 0x003427F6 File Offset: 0x003409F6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060090A2 RID: 37026 RVA: 0x003427F9 File Offset: 0x003409F9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("UsedAsIngredient");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090A3 RID: 37027 RVA: 0x00342810 File Offset: 0x00340A10
		public override bool FireEvent(Event E)
		{
			if (E.ID == "UsedAsIngredient" && !this.Triggered)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					AchievementManager.SetAchievement(this.AchievementID);
					this.Triggered = true;
					this.ParentObject.RemovePart(this);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C3C RID: 15420
		public string AchievementID = "";

		// Token: 0x04003C3D RID: 15421
		public bool Triggered;
	}
}
