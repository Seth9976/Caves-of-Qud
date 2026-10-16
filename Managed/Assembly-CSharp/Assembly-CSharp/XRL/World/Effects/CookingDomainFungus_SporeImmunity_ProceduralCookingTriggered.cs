using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CB4 RID: 3252
	[Serializable]
	public class CookingDomainFungus_SporeImmunity_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008206 RID: 33286 RVA: 0x002F71AD File Offset: 0x002F53AD
		public override string GetDescription()
		{
			return "@they are immune to fungal spores for 6 hours.";
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x002F71B4 File Offset: 0x002F53B4
		public override string GetNotification()
		{
			return "@they become impervious to fungal spores.";
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x002F71BB File Offset: 0x002F53BB
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainFungus_SporeImmunity_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainFungus_SporeImmunity_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
