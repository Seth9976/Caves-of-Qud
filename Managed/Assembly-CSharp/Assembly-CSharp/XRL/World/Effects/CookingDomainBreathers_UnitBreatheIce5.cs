using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C77 RID: 3191
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheIce5 : ProceduralCookingEffectUnitMutation<IceBreather>
	{
		// Token: 0x06008140 RID: 33088 RVA: 0x002F60C3 File Offset: 0x002F42C3
		public CookingDomainBreathers_UnitBreatheIce5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008141 RID: 33089 RVA: 0x002F60E1 File Offset: 0x002F42E1
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
