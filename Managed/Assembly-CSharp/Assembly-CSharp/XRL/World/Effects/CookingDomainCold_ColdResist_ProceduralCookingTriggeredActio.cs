using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C87 RID: 3207
	[Serializable]
	public class CookingDomainCold_ColdResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008166 RID: 33126 RVA: 0x002F6375 File Offset: 0x002F4575
		public override string GetDescription()
		{
			return "@they gain 40-50 Cold Resist for 6 hours.";
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x002F637C File Offset: 0x002F457C
		public override string GetNotification()
		{
			return "@they become fortified against the cold.";
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x002F6383 File Offset: 0x002F4583
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainCold_ColdResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainCold_ColdResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
