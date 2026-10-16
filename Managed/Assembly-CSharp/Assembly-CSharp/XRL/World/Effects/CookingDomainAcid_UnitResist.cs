using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C4A RID: 3146
	[Serializable]
	public class CookingDomainAcid_UnitResist : ProceduralCookingEffectUnit
	{
		// Token: 0x06008087 RID: 32903 RVA: 0x002F4C39 File Offset: 0x002F2E39
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Acid Resistance";
		}

		// Token: 0x06008088 RID: 32904 RVA: 0x002F4C51 File Offset: 0x002F2E51
		public override string GetTemplatedDescription()
		{
			return "+10-15 Acid Resistance";
		}

		// Token: 0x06008089 RID: 32905 RVA: 0x002F4C58 File Offset: 0x002F2E58
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(10, 15);
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x002F4C69 File Offset: 0x002F2E69
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["AcidResistance"].Bonus += this.Bonus;
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x002F4C8D File Offset: 0x002F2E8D
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["AcidResistance"].Bonus -= this.Bonus;
		}

		// Token: 0x040038FE RID: 14590
		public int Bonus;
	}
}
