using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CA2 RID: 3234
	[Serializable]
	public class CookingDomainFear_FearImmunity_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081C8 RID: 33224 RVA: 0x002F6C34 File Offset: 0x002F4E34
		public override string GetDescription()
		{
			return "@they become@s immune to fear for 6 hours.";
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x002F6C3B File Offset: 0x002F4E3B
		public override string GetNotification()
		{
			return "@they become@s immune to fear.";
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x002F6C42 File Offset: 0x002F4E42
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainFear_FearImmunity_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainFear_FearImmunity_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
