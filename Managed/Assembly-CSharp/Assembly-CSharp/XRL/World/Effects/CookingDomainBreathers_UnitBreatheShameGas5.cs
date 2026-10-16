using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7D RID: 3197
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheShameGas5 : ProceduralCookingEffectUnitMutation<ShameBreather>
	{
		// Token: 0x0600814C RID: 33100 RVA: 0x002F61AD File Offset: 0x002F43AD
		public CookingDomainBreathers_UnitBreatheShameGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x002F61CB File Offset: 0x002F43CB
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
