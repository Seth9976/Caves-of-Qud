using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C3E RID: 3134
	[Serializable]
	public class BoostStatistic : Effect, ITierInitialized
	{
		// Token: 0x06007FDC RID: 32732 RVA: 0x002F1DEB File Offset: 0x002EFFEB
		public BoostStatistic()
		{
			this.DisplayName = "boosted " + this.Statistic.ToLower();
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x002F1E19 File Offset: 0x002F0019
		public BoostStatistic(int Duration, string Statistic, int Amount)
			: this()
		{
			this.Duration = Duration;
			this.Statistic = Statistic;
			this.Bonus = Amount;
			this.DisplayName = "boosted " + Statistic.ToLower();
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x002F1E4C File Offset: 0x002F004C
		public override string GetDescription()
		{
			return "boosted " + this.Statistic.ToLower();
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x002F1E64 File Offset: 0x002F0064
		public void Initialize(int Tier)
		{
			Tier = Math.Max(1, Math.Min(8, Stat.Random(Tier - 2, Tier + 2)));
			this.Statistic = BoostStatistic.Statistics.GetRandomElement<string>(null);
			this.Duration = Stat.Random(40, 200);
			this.Bonus = Tier + 2;
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x002F1EB6 File Offset: 0x002F00B6
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x002F1EBC File Offset: 0x002F00BC
		public override int GetEffectType()
		{
			int num = 0;
			if (XRL.World.Statistic.IsMental(this.Statistic))
			{
				num |= 2;
			}
			else
			{
				num |= 4;
			}
			if (this.Bonus < 0)
			{
				num |= 33554432;
			}
			return num;
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x002F1EF4 File Offset: 0x002F00F4
		public override bool SameAs(Effect e)
		{
			BoostStatistic boostStatistic = e as BoostStatistic;
			return !(boostStatistic.Statistic != this.Statistic) && boostStatistic.Bonus == this.Bonus && base.SameAs(e);
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x002F1F34 File Offset: 0x002F0134
		public override string GetDetails()
		{
			return this.Bonus.Signed(false) + " " + this.Statistic;
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x002F1F54 File Offset: 0x002F0154
		public override bool Apply(GameObject Object)
		{
			if (Object.FireEvent(Event.New("ApplyBoostStatistic", "Event", this)))
			{
				if (this.Bonus >= 0)
				{
					if (Object != null)
					{
						Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_genericBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
				}
				else if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_genericBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"Your ",
						this.Statistic,
						" ",
						(this.Bonus >= 0) ? "increases" : "decreases",
						"."
					}), (this.Bonus >= 0) ? 'g' : 'r', true);
				}
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x002F204B File Offset: 0x002F024B
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, this.Statistic, this.Bonus, false);
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x002F206C File Offset: 0x002F026C
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x002F207F File Offset: 0x002F027F
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your " + this.Statistic + " returns to normal.", 'r', true);
			}
			this.UnapplyStats();
		}

		// Token: 0x06007FE8 RID: 32744 RVA: 0x002F20AC File Offset: 0x002F02AC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007FE9 RID: 32745 RVA: 0x002F20CC File Offset: 0x002F02CC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038DF RID: 14559
		public string Statistic = "Strength";

		// Token: 0x040038E0 RID: 14560
		public int Bonus;

		// Token: 0x040038E1 RID: 14561
		public static string[] Statistics = new string[] { "Strength", "Agility", "Toughness", "Intelligence", "Willpower", "Ego" };
	}
}
