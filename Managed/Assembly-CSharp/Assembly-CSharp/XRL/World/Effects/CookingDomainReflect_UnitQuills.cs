using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CF0 RID: 3312
	[Serializable]
	public class CookingDomainReflect_UnitQuills : ProceduralCookingEffectUnitMutation<Quills>
	{
		// Token: 0x0600834A RID: 33610 RVA: 0x002FA74D File Offset: 0x002F894D
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x002FA756 File Offset: 0x002F8956
		public CookingDomainReflect_UnitQuills()
		{
			this.AddedTier = "5-6";
			this.BonusTier = "3-4";
		}
	}
}
