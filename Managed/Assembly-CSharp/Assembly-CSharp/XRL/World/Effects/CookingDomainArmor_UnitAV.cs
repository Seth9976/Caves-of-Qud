using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C55 RID: 3157
	[Serializable]
	public class CookingDomainArmor_UnitAV : ProceduralCookingEffectUnit
	{
		// Token: 0x060080AE RID: 32942 RVA: 0x002F4F31 File Offset: 0x002F3131
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " AV";
		}

		// Token: 0x060080AF RID: 32943 RVA: 0x002F4F49 File Offset: 0x002F3149
		public override string GetTemplatedDescription()
		{
			return "+2 AV";
		}

		// Token: 0x060080B0 RID: 32944 RVA: 0x002F4F50 File Offset: 0x002F3150
		public override void Init(GameObject target)
		{
			this.Bonus = 2;
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x002F4F59 File Offset: 0x002F3159
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["AV"].Bonus += this.Bonus;
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x002F4F7D File Offset: 0x002F317D
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["AV"].Bonus -= this.Bonus;
		}

		// Token: 0x04003903 RID: 14595
		public int Bonus;
	}
}
