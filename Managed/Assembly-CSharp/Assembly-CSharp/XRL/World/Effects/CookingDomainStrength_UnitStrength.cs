using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D0F RID: 3343
	[Serializable]
	public class CookingDomainStrength_UnitStrength : ProceduralCookingEffectUnit
	{
		// Token: 0x060083D8 RID: 33752 RVA: 0x002FB9AC File Offset: 0x002F9BAC
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Strength";
		}

		// Token: 0x060083D9 RID: 33753 RVA: 0x002FB9C4 File Offset: 0x002F9BC4
		public override string GetTemplatedDescription()
		{
			return "+4 Strength";
		}

		// Token: 0x060083DA RID: 33754 RVA: 0x002FB9CB File Offset: 0x002F9BCB
		public override void Init(GameObject target)
		{
			this.Bonus = 4;
		}

		// Token: 0x060083DB RID: 33755 RVA: 0x002FB9D4 File Offset: 0x002F9BD4
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Strength"].Bonus += this.Bonus;
		}

		// Token: 0x060083DC RID: 33756 RVA: 0x002FB9F8 File Offset: 0x002F9BF8
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Strength"].Bonus -= this.Bonus;
		}

		// Token: 0x0400396D RID: 14701
		public int Bonus;
	}
}
