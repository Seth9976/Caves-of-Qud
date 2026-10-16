using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CA6 RID: 3238
	[Serializable]
	public class CookingDomainFear_UnitBonusMA : ProceduralCookingEffectUnit
	{
		// Token: 0x060081D9 RID: 33241 RVA: 0x002F6D7B File Offset: 0x002F4F7B
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " MA";
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x002F6D93 File Offset: 0x002F4F93
		public override string GetTemplatedDescription()
		{
			return "+2 MA";
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x002F6D9A File Offset: 0x002F4F9A
		public override void Init(GameObject target)
		{
			this.Bonus = 2;
		}

		// Token: 0x060081DC RID: 33244 RVA: 0x002F6DA3 File Offset: 0x002F4FA3
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["MA"].Bonus += this.Bonus;
		}

		// Token: 0x060081DD RID: 33245 RVA: 0x002F6DC7 File Offset: 0x002F4FC7
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["MA"].Bonus -= this.Bonus;
			this.Bonus = 0;
		}

		// Token: 0x04003928 RID: 14632
		public int Bonus;
	}
}
