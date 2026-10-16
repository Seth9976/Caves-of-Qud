using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C97 RID: 3223
	[Serializable]
	public class CookingDomainElectric_SmallElectricResist_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081A2 RID: 33186 RVA: 0x002F6941 File Offset: 0x002F4B41
		public override string GetDescription()
		{
			return "@they gain 40-50 Electric Resist for 6 hours.";
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x002F6948 File Offset: 0x002F4B48
		public override string GetNotification()
		{
			return "@they become fortified against electrical damage.";
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x002F694F File Offset: 0x002F4B4F
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainElectric_SmallElectricResist_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainElectric_SmallElectricResist_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
