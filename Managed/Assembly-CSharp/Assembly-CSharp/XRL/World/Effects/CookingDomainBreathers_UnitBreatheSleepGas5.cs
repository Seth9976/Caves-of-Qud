using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7F RID: 3199
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheSleepGas5 : ProceduralCookingEffectUnitMutation<SleepBreather>
	{
		// Token: 0x06008150 RID: 33104 RVA: 0x002F61FB File Offset: 0x002F43FB
		public CookingDomainBreathers_UnitBreatheSleepGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x002F6219 File Offset: 0x002F4419
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
