using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C91 RID: 3217
	[Serializable]
	public class CookingDomainDarkness_UnitDV : ProceduralCookingEffectUnit
	{
		// Token: 0x06008182 RID: 33154 RVA: 0x002F65CD File Offset: 0x002F47CD
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " DV";
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x002F65E5 File Offset: 0x002F47E5
		public override string GetTemplatedDescription()
		{
			return "+4 DV";
		}

		// Token: 0x06008184 RID: 33156 RVA: 0x002F65EC File Offset: 0x002F47EC
		public override void Init(GameObject target)
		{
			this.Bonus = 4;
		}

		// Token: 0x06008185 RID: 33157 RVA: 0x002F65F5 File Offset: 0x002F47F5
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["DV"].Bonus += this.Bonus;
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x002F6619 File Offset: 0x002F4819
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["DV"].Bonus -= this.Bonus;
		}

		// Token: 0x0400391E RID: 14622
		public int Bonus;
	}
}
