using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CDC RID: 3292
	[Serializable]
	public class CookingDomainPlant_UnitBurgeoningLowTier : ProceduralCookingEffectUnitMutation<Burgeoning>
	{
		// Token: 0x060082C3 RID: 33475 RVA: 0x002F83CE File Offset: 0x002F65CE
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x060082C4 RID: 33476 RVA: 0x002F83D7 File Offset: 0x002F65D7
		public CookingDomainPlant_UnitBurgeoningLowTier()
		{
			this.AddedTier = "1-2";
			this.BonusTier = "2-3";
		}
	}
}
