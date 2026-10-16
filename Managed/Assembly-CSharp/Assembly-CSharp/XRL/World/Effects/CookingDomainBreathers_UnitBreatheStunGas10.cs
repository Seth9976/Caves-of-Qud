using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C82 RID: 3202
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheStunGas10 : ProceduralCookingEffectUnitMutation<StunBreather>
	{
		// Token: 0x06008156 RID: 33110 RVA: 0x002F6270 File Offset: 0x002F4470
		public CookingDomainBreathers_UnitBreatheStunGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x002F628E File Offset: 0x002F448E
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
