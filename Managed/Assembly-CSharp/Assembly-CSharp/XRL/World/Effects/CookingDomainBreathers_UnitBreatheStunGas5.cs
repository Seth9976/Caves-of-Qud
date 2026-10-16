using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C81 RID: 3201
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheStunGas5 : ProceduralCookingEffectUnitMutation<StunBreather>
	{
		// Token: 0x06008154 RID: 33108 RVA: 0x002F6249 File Offset: 0x002F4449
		public CookingDomainBreathers_UnitBreatheStunGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x002F6267 File Offset: 0x002F4467
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
