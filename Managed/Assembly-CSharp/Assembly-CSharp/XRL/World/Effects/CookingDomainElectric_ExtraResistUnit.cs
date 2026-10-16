using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CA0 RID: 3232
	[Serializable]
	public class CookingDomainElectric_ExtraResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x060081BE RID: 33214 RVA: 0x002F6B82 File Offset: 0x002F4D82
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Electric Resistance";
		}

		// Token: 0x060081BF RID: 33215 RVA: 0x002F6B9A File Offset: 0x002F4D9A
		public override string GetTemplatedDescription()
		{
			return "+50-75 Electric Resist";
		}

		// Token: 0x060081C0 RID: 33216 RVA: 0x002F6BA1 File Offset: 0x002F4DA1
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(50, 75);
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x002F6BB2 File Offset: 0x002F4DB2
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.GetStat("ElectricResistance").Bonus += this.Bonus;
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x002F6BD1 File Offset: 0x002F4DD1
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.GetStat("ElectricResistance").Bonus -= this.Bonus;
			this.Bonus = 0;
		}

		// Token: 0x04003927 RID: 14631
		public int Bonus;
	}
}
