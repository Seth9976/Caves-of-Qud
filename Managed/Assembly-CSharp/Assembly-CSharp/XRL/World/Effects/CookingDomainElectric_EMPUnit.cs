using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C9E RID: 3230
	[Serializable]
	public class CookingDomainElectric_EMPUnit : ProceduralCookingEffectUnitMutation<ElectromagneticPulse>
	{
		// Token: 0x060081B6 RID: 33206 RVA: 0x002F6AE5 File Offset: 0x002F4CE5
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x002F6AEE File Offset: 0x002F4CEE
		public CookingDomainElectric_EMPUnit()
		{
			this.AddedTier = "2-3";
			this.BonusTier = "3-4";
		}
	}
}
