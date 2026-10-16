using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CA8 RID: 3240
	[Serializable]
	public class CookingDomainHeat_FlamingHandsCharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060081E2 RID: 33250 RVA: 0x002F6ECA File Offset: 0x002F50CA
		public override string GetDescription()
		{
			return "@they emit a ray of flame per Flaming Hands at level 5-6.";
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x002F6ED1 File Offset: 0x002F50D1
		public override string GetNotification()
		{
			return "@they emit a powerful ray of flame.";
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x002F6ED8 File Offset: 0x002F50D8
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				FlamingRay.Cast(null, "5-6");
			}
		}
	}
}
