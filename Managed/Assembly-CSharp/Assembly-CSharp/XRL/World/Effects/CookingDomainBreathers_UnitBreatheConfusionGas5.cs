using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C71 RID: 3185
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheConfusionGas5 : ProceduralCookingEffectUnitMutation<ConfusionBreather>
	{
		// Token: 0x06008134 RID: 33076 RVA: 0x002F5FD9 File Offset: 0x002F41D9
		public CookingDomainBreathers_UnitBreatheConfusionGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x002F5FF7 File Offset: 0x002F41F7
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
