using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CE9 RID: 3305
	[Serializable]
	public class CookingDomainReflect_Reflect100HighTier_ProceduralCookingTriggeredAction : CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction
	{
		// Token: 0x0600832D RID: 33581 RVA: 0x002FA265 File Offset: 0x002F8465
		public override string GetDescription()
		{
			return "@they reflect 100% damage the next 3 times @they take damage within 50 turns.";
		}

		// Token: 0x0600832E RID: 33582 RVA: 0x002FA26C File Offset: 0x002F846C
		public override string GetNotification()
		{
			return "@they grow spines all over your body.";
		}

		// Token: 0x0600832F RID: 33583 RVA: 0x002FA273 File Offset: 0x002F8473
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainReflect_Reflect100HighTier_ProceduralCookingTriggeredAction_Effect>())
			{
				go.ApplyEffect(new CookingDomainReflect_Reflect100HighTier_ProceduralCookingTriggeredAction_Effect(), null);
			}
		}
	}
}
