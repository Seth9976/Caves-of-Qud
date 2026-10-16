using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C7C RID: 3196
	[Serializable]
	public class CookingDomainBreathers_UnitBreathePoisonGas10 : ProceduralCookingEffectUnitMutation<PoisonBreather>
	{
		// Token: 0x0600814A RID: 33098 RVA: 0x002F6186 File Offset: 0x002F4386
		public CookingDomainBreathers_UnitBreathePoisonGas10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x002F61A4 File Offset: 0x002F43A4
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
