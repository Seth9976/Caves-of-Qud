using System;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D30 RID: 3376
	[Serializable]
	public class Emboldened : Effect, ITierInitialized
	{
		// Token: 0x0600851D RID: 34077 RVA: 0x00300E98 File Offset: 0x002FF098
		public Emboldened()
		{
			this.DisplayName = "Boosted " + Grammar.MakeTitleCase(XRL.World.Statistic.GetStatDisplayName(this.Statistic));
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x00300ECC File Offset: 0x002FF0CC
		public Emboldened(int Duration, string Statistic, int Amount)
		{
			this.Duration = Duration;
			this.Statistic = Statistic;
			this.Bonus = Amount;
			this.DisplayName = "Boosted " + Grammar.MakeTitleCase(XRL.World.Statistic.GetStatDisplayName(Statistic));
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x00300F1A File Offset: 0x002FF11A
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(200, 1000);
			this.Bonus = Stat.Random(1, 60) * 5;
		}

		// Token: 0x06008520 RID: 34080 RVA: 0x00300F41 File Offset: 0x002FF141
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008521 RID: 34081 RVA: 0x00300F44 File Offset: 0x002FF144
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x00300F47 File Offset: 0x002FF147
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008523 RID: 34083 RVA: 0x00300F4A File Offset: 0x002FF14A
		public override string GetDescription()
		{
			return "emboldened";
		}

		// Token: 0x06008524 RID: 34084 RVA: 0x00300F51 File Offset: 0x002FF151
		public override string GetDetails()
		{
			return this.Bonus.Signed(false) + " " + this.Statistic;
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x00300F70 File Offset: 0x002FF170
		public override bool Apply(GameObject Object)
		{
			Emboldened emboldened;
			if (Object.TryGetEffect<Emboldened>(out emboldened))
			{
				if (this.Duration > emboldened.Duration)
				{
					emboldened.Duration = this.Duration;
				}
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyBoostStatistic", "Event", this)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					"Your ",
					XRL.World.Statistic.GetStatDisplayName(this.Statistic),
					" ",
					XRL.World.Statistic.IsStatPlural(this.Statistic) ? "increase" : "increase",
					"!"
				}), null, true);
			}
			base.StatShifter.SetStatShift(this.Statistic, this.Bonus, false);
			return true;
		}

		// Token: 0x06008526 RID: 34086 RVA: 0x00301034 File Offset: 0x002FF234
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					"Your ",
					XRL.World.Statistic.GetStatDisplayName(this.Statistic),
					" ",
					XRL.World.Statistic.IsStatPlural(this.Statistic) ? "return" : "returns",
					" to normal."
				}), null, true);
			}
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x040039AE RID: 14766
		public string Statistic = "Hitpoints";

		// Token: 0x040039AF RID: 14767
		public int Bonus;
	}
}
