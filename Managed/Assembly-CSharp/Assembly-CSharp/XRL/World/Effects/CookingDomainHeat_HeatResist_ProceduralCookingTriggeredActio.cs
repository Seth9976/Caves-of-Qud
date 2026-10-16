using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CAA RID: 3242
	[Serializable]
	public class CookingDomainHeat_HeatResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081EA RID: 33258 RVA: 0x002F6F41 File Offset: 0x002F5141
		public override string GetDescription()
		{
			return "@they gain 40-50 Heat Resist for 6 hours.";
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x002F6F48 File Offset: 0x002F5148
		public override string GetNotification()
		{
			return "@they become fortified against the heat.";
		}

		// Token: 0x060081EC RID: 33260 RVA: 0x002F6F4F File Offset: 0x002F514F
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainHeat_HeatResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainHeat_HeatResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
