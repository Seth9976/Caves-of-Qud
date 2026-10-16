using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C83 RID: 3203
	[Serializable]
	public class CookingDomainBurrowing_UnitBurrowingClaws : ProceduralCookingEffectUnitMutation<BurrowingClaws>
	{
		// Token: 0x06008158 RID: 33112 RVA: 0x002F6297 File Offset: 0x002F4497
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x002F62A0 File Offset: 0x002F44A0
		public CookingDomainBurrowing_UnitBurrowingClaws()
		{
			this.AddedTier = "5-6";
			this.BonusTier = "3-4";
		}
	}
}
