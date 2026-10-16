using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CC1 RID: 3265
	[Serializable]
	public class CookingDomainHP_UnitHP : ProceduralCookingEffectUnit
	{
		// Token: 0x06008243 RID: 33347 RVA: 0x002F7725 File Offset: 0x002F5925
		public override string GetDescription()
		{
			return this.Tier.Signed(false) + "% max HP";
		}

		// Token: 0x06008244 RID: 33348 RVA: 0x002F773D File Offset: 0x002F593D
		public override string GetTemplatedDescription()
		{
			return "+10-15% max HP";
		}

		// Token: 0x06008245 RID: 33349 RVA: 0x002F7744 File Offset: 0x002F5944
		public override void Init(GameObject target)
		{
			this.Bonus = 0;
			this.Tier = Stat.Random(10, 15);
			base.Init(target);
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x002F7764 File Offset: 0x002F5964
		public override void Apply(GameObject Object, Effect parent)
		{
			this.Bonus = (int)Math.Ceiling((double)((float)this.Tier * 0.01f * (float)Object.Statistics["Hitpoints"].BaseValue));
			Object.Statistics["Hitpoints"].BaseValue += this.Bonus;
		}

		// Token: 0x06008247 RID: 33351 RVA: 0x002F77C4 File Offset: 0x002F59C4
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Hitpoints"].BaseValue -= this.Bonus;
			this.Bonus = 0;
		}

		// Token: 0x04003934 RID: 14644
		public int Tier;

		// Token: 0x04003935 RID: 14645
		public int Bonus;
	}
}
