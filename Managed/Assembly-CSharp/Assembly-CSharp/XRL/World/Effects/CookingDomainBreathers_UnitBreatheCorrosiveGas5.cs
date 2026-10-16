using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C73 RID: 3187
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheCorrosiveGas5 : ProceduralCookingEffectUnitMutation<CorrosiveBreather>
	{
		// Token: 0x06008138 RID: 33080 RVA: 0x002F6027 File Offset: 0x002F4227
		public CookingDomainBreathers_UnitBreatheCorrosiveGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x002F6045 File Offset: 0x002F4245
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
