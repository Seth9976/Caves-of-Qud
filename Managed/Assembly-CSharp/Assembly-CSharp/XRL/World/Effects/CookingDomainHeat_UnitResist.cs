using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CB0 RID: 3248
	[Serializable]
	public class CookingDomainHeat_UnitResist : ProceduralCookingEffectUnit
	{
		// Token: 0x060081FD RID: 33277 RVA: 0x002F710E File Offset: 0x002F530E
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Heat Resistance";
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x002F7126 File Offset: 0x002F5326
		public override string GetTemplatedDescription()
		{
			return "+10-15 Heat Resistance";
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x002F712D File Offset: 0x002F532D
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(10, 15);
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x002F713E File Offset: 0x002F533E
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["HeatResistance"].Bonus += this.Bonus;
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x002F7162 File Offset: 0x002F5362
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["HeatResistance"].Bonus -= this.Bonus;
			this.Bonus = 0;
		}

		// Token: 0x0400392A RID: 14634
		public int Bonus;
	}
}
