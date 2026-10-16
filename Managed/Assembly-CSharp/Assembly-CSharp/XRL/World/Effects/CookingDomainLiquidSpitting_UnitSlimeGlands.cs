using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CC2 RID: 3266
	[Serializable]
	public class CookingDomainLiquidSpitting_UnitSlimeGlands : ProceduralCookingEffectUnitMutation<SlimeGlands>
	{
		// Token: 0x06008249 RID: 33353 RVA: 0x002F77F7 File Offset: 0x002F59F7
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x0600824A RID: 33354 RVA: 0x002F7800 File Offset: 0x002F5A00
		public CookingDomainLiquidSpitting_UnitSlimeGlands()
		{
			this.AddedTier = "1";
			this.BonusTier = "0";
		}
	}
}
