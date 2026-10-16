using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CDD RID: 3293
	[Serializable]
	public class CookingDomainPlant_UnitBurgeoningHighTier : ProceduralCookingEffectUnitMutation<Burgeoning>
	{
		// Token: 0x060082C5 RID: 33477 RVA: 0x002F83F5 File Offset: 0x002F65F5
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x060082C6 RID: 33478 RVA: 0x002F83FE File Offset: 0x002F65FE
		public CookingDomainPlant_UnitBurgeoningHighTier()
		{
			this.AddedTier = "7-8";
			this.BonusTier = "5-6";
		}
	}
}
