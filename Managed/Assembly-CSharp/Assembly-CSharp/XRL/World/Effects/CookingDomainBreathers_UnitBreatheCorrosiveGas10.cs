using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C74 RID: 3188
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheCorrosiveGas10 : ProceduralCookingEffectUnitMutation<CorrosiveBreather>
	{
		// Token: 0x0600813A RID: 33082 RVA: 0x002F604E File Offset: 0x002F424E
		public CookingDomainBreathers_UnitBreatheCorrosiveGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x002F606C File Offset: 0x002F426C
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
