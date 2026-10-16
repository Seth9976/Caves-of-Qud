using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CEE RID: 3310
	public class CookingDomainReflect_OnDamagedHighTier : CookingDomainReflect_OnDamaged
	{
		// Token: 0x06008341 RID: 33601 RVA: 0x002FA69E File Offset: 0x002F889E
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.Tier = Stat.Random(16, 20);
		}

		// Token: 0x06008342 RID: 33602 RVA: 0x002FA6B6 File Offset: 0x002F88B6
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 16-20% chance";
		}
	}
}
