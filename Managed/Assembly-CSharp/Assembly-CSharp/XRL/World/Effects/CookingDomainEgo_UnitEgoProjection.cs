using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C94 RID: 3220
	[Serializable]
	public class CookingDomainEgo_UnitEgoProjection : ProceduralCookingEffectUnitMutation<WillForce>
	{
		// Token: 0x06008194 RID: 33172 RVA: 0x002F6815 File Offset: 0x002F4A15
		public override void Init(GameObject target)
		{
			base.Init(target);
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x002F681E File Offset: 0x002F4A1E
		public CookingDomainEgo_UnitEgoProjection()
		{
			this.AddedTier = "4-5";
			this.BonusTier = "4-5";
		}
	}
}
