using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7B RID: 3195
	[Serializable]
	public class CookingDomainBreathers_UnitBreathePoisonGas5 : ProceduralCookingEffectUnitMutation<PoisonBreather>
	{
		// Token: 0x06008148 RID: 33096 RVA: 0x002F615F File Offset: 0x002F435F
		public CookingDomainBreathers_UnitBreathePoisonGas5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x002F617D File Offset: 0x002F437D
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
