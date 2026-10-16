using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C80 RID: 3200
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheSleepGas10 : ProceduralCookingEffectUnitMutation<SleepBreather>
	{
		// Token: 0x06008152 RID: 33106 RVA: 0x002F6222 File Offset: 0x002F4422
		public CookingDomainBreathers_UnitBreatheSleepGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x002F6240 File Offset: 0x002F4440
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
