using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D84 RID: 3460
	[Serializable]
	public class Shaken : Effect, ITierInitialized
	{
		// Token: 0x0600897C RID: 35196 RVA: 0x00312B1E File Offset: 0x00310D1E
		public Shaken()
		{
			this.DisplayName = "shaken";
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x00312B31 File Offset: 0x00310D31
		public Shaken(int Duration, int Level)
			: this()
		{
			this.Duration = Duration;
			this.Level = Level;
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x00312B47 File Offset: 0x00310D47
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(50, 200);
			this.Level = Stat.Random(1, 20);
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x00312B69 File Offset: 0x00310D69
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x06008980 RID: 35200 RVA: 0x00312B70 File Offset: 0x00310D70
		public override bool SameAs(Effect e)
		{
			return (e as Shaken).Level == this.Level && base.SameAs(e);
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x00312B8E File Offset: 0x00310D8E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x00312B91 File Offset: 0x00310D91
		public override string GetDetails()
		{
			return "-" + this.Level.ToString() + " DV";
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x00312BB0 File Offset: 0x00310DB0
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("CanApplyShaken"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyShaken"))
			{
				return false;
			}
			Shaken effect = Object.GetEffect<Shaken>();
			if (effect != null)
			{
				bool flag = false;
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
					flag = true;
				}
				if (this.Level > effect.Level)
				{
					effect.Level = this.Level;
					flag = true;
				}
				if (flag)
				{
					effect.ApplyStats();
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x00312C2B File Offset: 0x00310E2B
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06008985 RID: 35205 RVA: 0x00312C3A File Offset: 0x00310E3A
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "DV", -this.Level, false);
		}

		// Token: 0x06008986 RID: 35206 RVA: 0x00312C5B File Offset: 0x00310E5B
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008987 RID: 35207 RVA: 0x00312C6E File Offset: 0x00310E6E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008988 RID: 35208 RVA: 0x00312C8E File Offset: 0x00310E8E
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

		// Token: 0x04003A7D RID: 14973
		public int Level;
	}
}
