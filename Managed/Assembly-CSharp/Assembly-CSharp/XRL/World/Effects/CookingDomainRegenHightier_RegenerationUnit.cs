using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CFD RID: 3325
	[Serializable]
	public class CookingDomainRegenHightier_RegenerationUnit : CookingDomainRegenLowtier_RegenerationUnit
	{
		// Token: 0x06008385 RID: 33669 RVA: 0x002FAF18 File Offset: 0x002F9118
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(100, 100);
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x002FAF29 File Offset: 0x002F9129
		public override string GetTemplatedDescription()
		{
			return "+100% to natural healing rate";
		}
	}
}
