using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D5D RID: 3421
	[Serializable]
	public class LongbladeStance_Aggressive : Effect, ITierInitialized
	{
		// Token: 0x06008754 RID: 34644 RVA: 0x0030976F File Offset: 0x0030796F
		public LongbladeStance_Aggressive()
		{
			this.DisplayName = "{{R|aggressive stance}}";
			this.Duration = 9999;
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x0030978D File Offset: 0x0030798D
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 500);
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x003097A1 File Offset: 0x003079A1
		public override string GetStateDescription()
		{
			return "{{R|in aggressive stance}}";
		}

		// Token: 0x06008757 RID: 34647 RVA: 0x003097A8 File Offset: 0x003079A8
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008758 RID: 34648 RVA: 0x003097AB File Offset: 0x003079AB
		public override string GetDescription()
		{
			if (base.Object != null && base.Object.HasEffect<Asleep>())
			{
				return null;
			}
			return base.GetDescription();
		}

		// Token: 0x06008759 RID: 34649 RVA: 0x003097CA File Offset: 0x003079CA
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x0600875A RID: 34650 RVA: 0x003097D1 File Offset: 0x003079D1
		public override bool Apply(GameObject Object)
		{
			Object.RemoveEffect(typeof(LongbladeStance_Aggressive));
			Object.RemoveEffect(typeof(LongbladeStance_Defensive));
			Object.RemoveEffect(typeof(LongbladeStance_Dueling));
			return true;
		}

		// Token: 0x0600875B RID: 34651 RVA: 0x00309807 File Offset: 0x00307A07
		public override string GetDetails()
		{
			if (base.Object.HasPart<LongBladesImprovedAggressiveStance>())
			{
				return "+2 to your penetration roll and -3 to hit while wielding a long blade in the primary hand.";
			}
			return "+1 to your penetration roll and -2 to hit while wielding a long blade in the primary hand.";
		}
	}
}
