using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF9 RID: 3321
	public class CookingDomainRegenHightier_OnDamaged : CookingDomainRegenLowtier_OnDamaged
	{
		// Token: 0x0600836F RID: 33647 RVA: 0x002FAD2D File Offset: 0x002F8F2D
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(30, 40);
			base.Init(target);
		}

		// Token: 0x06008370 RID: 33648 RVA: 0x002FAD45 File Offset: 0x002F8F45
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s damage, there's a 30-40% chance";
		}
	}
}
