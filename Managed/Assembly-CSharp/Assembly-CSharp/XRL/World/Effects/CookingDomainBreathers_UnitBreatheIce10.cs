using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C78 RID: 3192
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheIce10 : ProceduralCookingEffectUnitMutation<IceBreather>
	{
		// Token: 0x06008142 RID: 33090 RVA: 0x002F60EA File Offset: 0x002F42EA
		public CookingDomainBreathers_UnitBreatheIce10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x002F6108 File Offset: 0x002F4308
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
