using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C79 RID: 3193
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheNormalityGas5 : ProceduralCookingEffectUnitMutation<NormalityBreather>
	{
		// Token: 0x06008144 RID: 33092 RVA: 0x002F6111 File Offset: 0x002F4311
		public CookingDomainBreathers_UnitBreatheNormalityGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x002F612F File Offset: 0x002F432F
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
