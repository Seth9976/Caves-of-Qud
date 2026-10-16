using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7A RID: 3194
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheNormalityGas10 : ProceduralCookingEffectUnitMutation<NormalityBreather>
	{
		// Token: 0x06008146 RID: 33094 RVA: 0x002F6138 File Offset: 0x002F4338
		public CookingDomainBreathers_UnitBreatheNormalityGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x002F6156 File Offset: 0x002F4356
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
