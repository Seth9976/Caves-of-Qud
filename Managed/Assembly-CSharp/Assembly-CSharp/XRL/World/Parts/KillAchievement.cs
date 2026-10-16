using System;

namespace XRL.World.Parts
{
	// Token: 0x02000DF9 RID: 3577
	[Serializable]
	public class KillAchievement : IPart
	{
		// Token: 0x060090CD RID: 37069 RVA: 0x0034331E File Offset: 0x0034151E
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090CE RID: 37070 RVA: 0x00343321 File Offset: 0x00341521
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDie");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090CF RID: 37071 RVA: 0x00343338 File Offset: 0x00341538
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDie" && !this.Triggered)
			{
				try
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Killer");
					if (gameObjectParameter != null && gameObjectParameter.IsPlayerControlled())
					{
						this.Triggered = true;
						AchievementManager.IncrementAchievement(this.AchievementID, 1);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Kill achievement", ex, "game_exception");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C5F RID: 15455
		public string AchievementID = "";

		// Token: 0x04003C60 RID: 15456
		public string Category = "";

		// Token: 0x04003C61 RID: 15457
		public int TargetAmount;

		// Token: 0x04003C62 RID: 15458
		public int IndicateIncrement = 10;

		// Token: 0x04003C63 RID: 15459
		public bool Triggered;
	}
}
