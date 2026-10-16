using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C75 RID: 3189
	[Serializable]
	public class CookingDomainBreathers_UnitBreatheFire5 : ProceduralCookingEffectUnitMutation<FireBreather>
	{
		// Token: 0x0600813C RID: 33084 RVA: 0x002F6075 File Offset: 0x002F4275
		public CookingDomainBreathers_UnitBreatheFire5()
		{
			this.AddedTier = "5";
			this.BonusTier = "5";
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x002F6093 File Offset: 0x002F4293
		public override void Init(GameObject target)
		{
			base.Init(target);
		}
	}
}
