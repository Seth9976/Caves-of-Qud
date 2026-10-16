using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C52 RID: 3154
	[Serializable]
	public class CookingDomainArmor_LargeAVBuff_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060080A3 RID: 32931 RVA: 0x002F4E25 File Offset: 0x002F3025
		public override string GetDescription()
		{
			return "@they gain@s +6 AV for 50 turns.";
		}

		// Token: 0x060080A4 RID: 32932 RVA: 0x002F4E2C File Offset: 0x002F302C
		public override string GetNotification()
		{
			return "@they stiffen@s.";
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x002F4E33 File Offset: 0x002F3033
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainArmor_LargeAVBuff_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainArmor_LargeAVBuff_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
