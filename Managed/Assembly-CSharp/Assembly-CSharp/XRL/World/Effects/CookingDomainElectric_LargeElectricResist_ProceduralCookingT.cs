using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C99 RID: 3225
	[Serializable]
	public class CookingDomainElectric_LargeElectricResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081A7 RID: 33191 RVA: 0x002F6985 File Offset: 0x002F4B85
		public override string GetDescription()
		{
			return "@they gain 125-175 Electric Resist for 50 turns.";
		}

		// Token: 0x060081A8 RID: 33192 RVA: 0x002F698C File Offset: 0x002F4B8C
		public override string GetNotification()
		{
			return "@they become impervious to electrical damage.";
		}

		// Token: 0x060081A9 RID: 33193 RVA: 0x002F6993 File Offset: 0x002F4B93
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainElectric_LargeElectricResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainElectric_LargeElectricResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
