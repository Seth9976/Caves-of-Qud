using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF1 RID: 3313
	[Serializable]
	public class CookingDomainReflect_UnitReflectDamageHighTier : CookingDomainReflect_UnitReflectDamage
	{
		// Token: 0x0600834D RID: 33613 RVA: 0x002FA77C File Offset: 0x002F897C
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(15, 18);
		}

		// Token: 0x0600834E RID: 33614 RVA: 0x002FA78D File Offset: 0x002F898D
		public override string GetTemplatedDescription()
		{
			return "Reflect 15-18% damage back at @their attackers, rounded up.";
		}
	}
}
