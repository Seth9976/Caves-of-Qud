using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C51 RID: 3153
	[Serializable]
	public class CookingDomainAgility_UnitAgility : ProceduralCookingEffectUnit
	{
		// Token: 0x0600809D RID: 32925 RVA: 0x002F4DAD File Offset: 0x002F2FAD
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Agility";
		}

		// Token: 0x0600809E RID: 32926 RVA: 0x002F4DC5 File Offset: 0x002F2FC5
		public override string GetTemplatedDescription()
		{
			return "+4 Agility";
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x002F4DCC File Offset: 0x002F2FCC
		public override void Init(GameObject target)
		{
			this.Bonus = 4;
		}

		// Token: 0x060080A0 RID: 32928 RVA: 0x002F4DD5 File Offset: 0x002F2FD5
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Agility"].Bonus += this.Bonus;
		}

		// Token: 0x060080A1 RID: 32929 RVA: 0x002F4DF9 File Offset: 0x002F2FF9
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Agility"].Bonus -= this.Bonus;
		}

		// Token: 0x04003901 RID: 14593
		public int Bonus;
	}
}
