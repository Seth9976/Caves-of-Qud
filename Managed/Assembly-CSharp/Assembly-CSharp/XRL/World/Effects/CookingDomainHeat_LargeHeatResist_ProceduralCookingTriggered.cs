using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CAC RID: 3244
	[Serializable]
	public class CookingDomainHeat_LargeHeatResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081EF RID: 33263 RVA: 0x002F6F85 File Offset: 0x002F5185
		public override string GetDescription()
		{
			return "@they gain 125-175 Heat Resist for 50 turns.";
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x002F6F8C File Offset: 0x002F518C
		public override string GetNotification()
		{
			return "@they become impervious to the heat.";
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x002F6F93 File Offset: 0x002F5193
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainHeat_LargeHeatResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainHeat_LargeHeatResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
