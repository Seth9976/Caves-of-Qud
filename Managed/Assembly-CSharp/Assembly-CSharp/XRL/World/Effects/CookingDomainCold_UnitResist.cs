using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C8D RID: 3213
	[Serializable]
	public class CookingDomainCold_UnitResist : ProceduralCookingEffectUnit
	{
		// Token: 0x06008179 RID: 33145 RVA: 0x002F6535 File Offset: 0x002F4735
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Cold Resistance";
		}

		// Token: 0x0600817A RID: 33146 RVA: 0x002F654D File Offset: 0x002F474D
		public override string GetTemplatedDescription()
		{
			return "+10-15 Cold Resistance";
		}

		// Token: 0x0600817B RID: 33147 RVA: 0x002F6554 File Offset: 0x002F4754
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(10, 15);
		}

		// Token: 0x0600817C RID: 33148 RVA: 0x002F6565 File Offset: 0x002F4765
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["ColdResistance"].Bonus += this.Bonus;
		}

		// Token: 0x0600817D RID: 33149 RVA: 0x002F6589 File Offset: 0x002F4789
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["ColdResistance"].Bonus -= this.Bonus;
		}

		// Token: 0x0400391D RID: 14621
		public int Bonus;
	}
}
