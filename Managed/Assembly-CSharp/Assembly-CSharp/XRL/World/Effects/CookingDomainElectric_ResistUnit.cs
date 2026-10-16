using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C9F RID: 3231
	[Serializable]
	public class CookingDomainElectric_ResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x060081B8 RID: 33208 RVA: 0x002F6B0C File Offset: 0x002F4D0C
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Electric Resistance";
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x002F6B24 File Offset: 0x002F4D24
		public override string GetTemplatedDescription()
		{
			return "+10-15 Electric Resist";
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x002F6B2B File Offset: 0x002F4D2B
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(10, 15);
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x002F6B3C File Offset: 0x002F4D3C
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.GetStat("ElectricResistance").Bonus += this.Bonus;
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x002F6B5B File Offset: 0x002F4D5B
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.GetStat("ElectricResistance").Bonus -= this.Bonus;
		}

		// Token: 0x04003926 RID: 14630
		public int Bonus;
	}
}
