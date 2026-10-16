using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D5E RID: 3422
	[Serializable]
	public class LongbladeStance_Defensive : Effect, ITierInitialized
	{
		// Token: 0x0600875C RID: 34652 RVA: 0x00309821 File Offset: 0x00307A21
		public LongbladeStance_Defensive()
		{
			this.DisplayName = "{{G|defensive stance}}";
			this.Duration = 9999;
		}

		// Token: 0x0600875D RID: 34653 RVA: 0x0030983F File Offset: 0x00307A3F
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 500);
		}

		// Token: 0x0600875E RID: 34654 RVA: 0x00309853 File Offset: 0x00307A53
		public override string GetStateDescription()
		{
			return "{{G|in defensive stance}}";
		}

		// Token: 0x0600875F RID: 34655 RVA: 0x0030985A File Offset: 0x00307A5A
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008760 RID: 34656 RVA: 0x0030985D File Offset: 0x00307A5D
		public override string GetDescription()
		{
			if (base.Object != null && base.Object.HasEffect<Asleep>())
			{
				return null;
			}
			return base.GetDescription();
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x0030987C File Offset: 0x00307A7C
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008762 RID: 34658 RVA: 0x00309883 File Offset: 0x00307A83
		public override string GetDetails()
		{
			if (base.Object.HasPart<LongBladesImprovedDefensiveStance>())
			{
				return "+3 DV while wielding a long blade in the primary hand.";
			}
			return "+2 DV while wielding a long blade in the primary hand.";
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x0030989D File Offset: 0x00307A9D
		public override bool Apply(GameObject Object)
		{
			Object.RemoveEffect(typeof(LongbladeStance_Aggressive));
			Object.RemoveEffect(typeof(LongbladeStance_Defensive));
			Object.RemoveEffect(typeof(LongbladeStance_Dueling));
			return true;
		}
	}
}
