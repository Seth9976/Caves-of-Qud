using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CC0 RID: 3264
	public class CookingDomainHP_OnLowHealthMidTier : CookingDomainHP_OnLowHealth
	{
		// Token: 0x06008240 RID: 33344 RVA: 0x002F7702 File Offset: 0x002F5902
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature drop@s below 30% HP,";
		}

		// Token: 0x06008241 RID: 33345 RVA: 0x002F7709 File Offset: 0x002F5909
		public override void Init(GameObject target)
		{
			this.LowAmount = 0.3f;
			base.Init(target);
		}
	}
}
