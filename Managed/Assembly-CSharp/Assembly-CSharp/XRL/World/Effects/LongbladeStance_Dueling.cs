using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D5F RID: 3423
	[Serializable]
	public class LongbladeStance_Dueling : Effect, ITierInitialized
	{
		// Token: 0x06008764 RID: 34660 RVA: 0x003098D3 File Offset: 0x00307AD3
		public LongbladeStance_Dueling()
		{
			this.DisplayName = "{{W|dueling stance}}";
			this.Duration = 9999;
		}

		// Token: 0x06008765 RID: 34661 RVA: 0x003098F1 File Offset: 0x00307AF1
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 500);
		}

		// Token: 0x06008766 RID: 34662 RVA: 0x00309905 File Offset: 0x00307B05
		public override string GetStateDescription()
		{
			return "{{W|in dueling stance}}";
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x0030990C File Offset: 0x00307B0C
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008768 RID: 34664 RVA: 0x0030990F File Offset: 0x00307B0F
		public override string GetDescription()
		{
			if (base.Object != null && base.Object.HasEffect<Asleep>())
			{
				return null;
			}
			return base.GetDescription();
		}

		// Token: 0x06008769 RID: 34665 RVA: 0x0030992E File Offset: 0x00307B2E
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x0600876A RID: 34666 RVA: 0x00309935 File Offset: 0x00307B35
		public override bool Apply(GameObject Object)
		{
			Object.RemoveEffect(typeof(LongbladeStance_Aggressive));
			Object.RemoveEffect(typeof(LongbladeStance_Defensive));
			Object.RemoveEffect(typeof(LongbladeStance_Dueling));
			return true;
		}

		// Token: 0x0600876B RID: 34667 RVA: 0x0030996B File Offset: 0x00307B6B
		public override string GetDetails()
		{
			if (base.Object.HasPart<LongBladesImprovedDuelistStance>())
			{
				return "+3 to hit while wielding a long blade in the primary hand.";
			}
			return "+2 to hit while wielding a long blade in the primary hand.";
		}
	}
}
