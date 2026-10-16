using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CE8 RID: 3304
	[Serializable]
	public class CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008329 RID: 33577 RVA: 0x002FA238 File Offset: 0x002F8438
		public override string GetDescription()
		{
			return "@they reflect 100% damage the next time @they take damage within 50 turns.";
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x002FA23F File Offset: 0x002F843F
		public override string GetNotification()
		{
			return "@they grow tiny spines all over your body.";
		}

		// Token: 0x0600832B RID: 33579 RVA: 0x002FA246 File Offset: 0x002F8446
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction_Effect>())
			{
				go.ApplyEffect(new CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction_Effect(), null);
			}
		}
	}
}
