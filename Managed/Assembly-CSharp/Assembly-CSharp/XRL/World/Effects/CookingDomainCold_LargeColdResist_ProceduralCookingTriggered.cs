using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C89 RID: 3209
	[Serializable]
	public class CookingDomainCold_LargeColdResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600816B RID: 33131 RVA: 0x002F63B9 File Offset: 0x002F45B9
		public override string GetDescription()
		{
			return "@they gain 125-175 cold resist for 50 turns.";
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x002F63C0 File Offset: 0x002F45C0
		public override string GetNotification()
		{
			return "@they become impervious to cold.";
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x002F63C7 File Offset: 0x002F45C7
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainCold_LargeColdResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainCold_LargeColdResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
