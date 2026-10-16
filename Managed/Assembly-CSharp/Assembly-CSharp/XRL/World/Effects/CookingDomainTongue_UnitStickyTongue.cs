using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D18 RID: 3352
	[Serializable]
	public class CookingDomainTongue_UnitStickyTongue : ProceduralCookingEffectUnitMutation<StickyTongue>
	{
		// Token: 0x060083FE RID: 33790 RVA: 0x002FBD68 File Offset: 0x002F9F68
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x060083FF RID: 33791 RVA: 0x002FBD71 File Offset: 0x002F9F71
		public CookingDomainTongue_UnitStickyTongue()
		{
			this.AddedTier = "4-5";
			this.BonusTier = "4-5";
		}
	}
}
