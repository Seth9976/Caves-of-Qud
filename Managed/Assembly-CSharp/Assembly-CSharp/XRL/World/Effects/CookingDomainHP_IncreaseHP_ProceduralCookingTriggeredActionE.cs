using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CBC RID: 3260
	[Serializable]
	public class CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredActionEffect : BasicTriggeredCookingEffect
	{
		// Token: 0x0600822C RID: 33324 RVA: 0x002F74AF File Offset: 0x002F56AF
		public CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredActionEffect()
		{
			this.Duration = 50;
		}

		// Token: 0x0600822D RID: 33325 RVA: 0x002F74BF File Offset: 0x002F56BF
		public CookingDomainHP_IncreaseHP_ProceduralCookingTriggeredActionEffect(int Tier)
		{
			this.Tier = Tier;
			this.Duration = 50;
		}

		// Token: 0x0600822E RID: 33326 RVA: 0x002F74D6 File Offset: 0x002F56D6
		public override string GetDetails()
		{
			return this.Tier.Signed(false) + "% max HP";
		}

		// Token: 0x0600822F RID: 33327 RVA: 0x002F74F0 File Offset: 0x002F56F0
		public override bool Apply(GameObject Object)
		{
			this.Bonus = (int)Math.Ceiling((double)((float)Object.Statistics["Hitpoints"].BaseValue * ((float)this.Tier * 0.01f)));
			Object.Statistics["Hitpoints"].BaseValue += this.Bonus;
			return true;
		}

		// Token: 0x06008230 RID: 33328 RVA: 0x002F7551 File Offset: 0x002F5751
		public override void Remove(GameObject Object)
		{
			Object.Statistics["Hitpoints"].BaseValue -= this.Bonus;
			this.Bonus = 0;
		}

		// Token: 0x0400392F RID: 14639
		public int Tier;

		// Token: 0x04003930 RID: 14640
		public int Bonus;
	}
}
