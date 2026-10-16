using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C72 RID: 3186
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheConfusionGas10 : ProceduralCookingEffectUnitMutation<ConfusionBreather>
	{
		// Token: 0x06008136 RID: 33078 RVA: 0x002F6000 File Offset: 0x002F4200
		public CookingDomainBreathers_UnitBreatheConfusionGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x002F601E File Offset: 0x002F421E
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
