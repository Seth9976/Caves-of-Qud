using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C93 RID: 3219
	[Serializable]
	public class CookingDomainDissociation_UnitOtherBodyBonus : ProceduralCookingEffectUnit
	{
		// Token: 0x0600818E RID: 33166 RVA: 0x002F6764 File Offset: 0x002F4964
		public override string GetDescription()
		{
			return "As long as your body isn't your own, you gain +" + this.Bonus.ToString() + " Quickness.";
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x002F6780 File Offset: 0x002F4980
		public override string GetTemplatedDescription()
		{
			return "As long as your body isn't your own, you gain +40-70 Quickness.";
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x002F6787 File Offset: 0x002F4987
		public override void Init(GameObject target)
		{
			this.Bonus = Stat.Random(40, 70);
			this.Applied = false;
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x002F679F File Offset: 0x002F499F
		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer() && !Object.IsOriginalPlayerBody() && (Object.HasEffect<Dominated>() || Object.HasEffect<WakingDream>()))
			{
				Object.GetStat("Speed").BaseValue += this.Bonus;
			}
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x002F67DE File Offset: 0x002F49DE
		public override void Remove(GameObject Object, Effect parent)
		{
			if (this.Applied)
			{
				Object.GetStat("Speed").BaseValue -= this.Bonus;
			}
		}

		// Token: 0x04003921 RID: 14625
		public int Bonus = 30;

		// Token: 0x04003922 RID: 14626
		public bool Applied;
	}
}
