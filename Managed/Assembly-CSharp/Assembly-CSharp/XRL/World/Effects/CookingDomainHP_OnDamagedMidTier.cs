using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CBE RID: 3262
	public class CookingDomainHP_OnDamagedMidTier : CookingDomainHP_OnDamaged
	{
		// Token: 0x06008237 RID: 33335 RVA: 0x002F7612 File Offset: 0x002F5812
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(12, 15);
			base.Init(target);
		}

		// Token: 0x06008238 RID: 33336 RVA: 0x002F762A File Offset: 0x002F582A
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 12-15% chance";
		}
	}
}
