using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C76 RID: 3190
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheFire10 : ProceduralCookingEffectUnitMutation<FireBreather>
	{
		// Token: 0x0600813E RID: 33086 RVA: 0x002F609C File Offset: 0x002F429C
		public CookingDomainBreathers_UnitBreatheFire10()
		{
			this.AddedTier = "10";
			this.BonusTier = "10";
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x002F60BA File Offset: 0x002F42BA
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
