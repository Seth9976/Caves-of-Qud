using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CE6 RID: 3302
	[Serializable]
	public class CookingDomainQuickness_UnitQuickness : ProceduralCookingEffectUnit
	{
		// Token: 0x0600831D RID: 33565 RVA: 0x002FA0C4 File Offset: 0x002F82C4
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Quickness";
		}

		// Token: 0x0600831E RID: 33566 RVA: 0x002FA0DC File Offset: 0x002F82DC
		public override string GetTemplatedDescription()
		{
			return "+4-5 Quickness";
		}

		// Token: 0x0600831F RID: 33567 RVA: 0x002FA0E3 File Offset: 0x002F82E3
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(4, 5);
		}

		// Token: 0x06008320 RID: 33568 RVA: 0x002FA0F2 File Offset: 0x002F82F2
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Speed"].Bonus += this.Bonus;
		}

		// Token: 0x06008321 RID: 33569 RVA: 0x002FA116 File Offset: 0x002F8316
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Speed"].Bonus -= this.Bonus;
		}

		// Token: 0x0400395E RID: 14686
		public int Bonus;
	}
}
