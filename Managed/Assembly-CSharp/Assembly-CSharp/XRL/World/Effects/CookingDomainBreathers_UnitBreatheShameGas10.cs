using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7E RID: 3198
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheShameGas10 : ProceduralCookingEffectUnitMutation<ShameBreather>
	{
		// Token: 0x0600814E RID: 33102 RVA: 0x002F61D4 File Offset: 0x002F43D4
		public CookingDomainBreathers_UnitBreatheShameGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x002F61F2 File Offset: 0x002F43F2
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
