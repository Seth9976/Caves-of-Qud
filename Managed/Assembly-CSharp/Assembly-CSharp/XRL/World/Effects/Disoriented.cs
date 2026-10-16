using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D29 RID: 3369
	[Serializable]
	public class Disoriented : Effect, ITierInitialized
	{
		// Token: 0x060084CC RID: 33996 RVA: 0x002FFDD9 File Offset: 0x002FDFD9
		public Disoriented()
		{
			this.DisplayName = "{{r|disoriented}}";
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x002FFDEC File Offset: 0x002FDFEC
		public Disoriented(int Duration, int Level)
			: this()
		{
			this.Duration = Duration;
			this.Level = Level;
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x002FFE02 File Offset: 0x002FE002
		public void Initialize(int Tier)
		{
			this.Level = 4;
			this.Duration = Stat.Random(50, 200);
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x002FFE1D File Offset: 0x002FE01D
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x002FFE20 File Offset: 0x002FE020
		public override string GetDetails()
		{
			return string.Concat(new string[]
			{
				"-",
				this.Level.ToString(),
				" DV\n-",
				this.Level.ToString(),
				" MA"
			});
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x002FFE6C File Offset: 0x002FE06C
		public override bool SameAs(Effect e)
		{
			return (e as Disoriented).Level == this.Level && base.SameAs(e);
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x002FFE8A File Offset: 0x002FE08A
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x002FFE94 File Offset: 0x002FE094
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("ApplyDisoriented"))
			{
				return false;
			}
			Disoriented effect = Object.GetEffect<Disoriented>();
			if (effect != null)
			{
				if (effect.Level == this.Level && effect.Duration < this.Duration)
				{
					effect.Duration = this.Duration;
				}
				else if (effect.Level * effect.Duration < this.Level * this.Duration)
				{
					effect.Level = this.Level;
					effect.Duration = this.Duration;
				}
				return false;
			}
			this.ApplyStats();
			return true;
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x002FFF21 File Offset: 0x002FE121
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x060084D5 RID: 34005 RVA: 0x002FFF29 File Offset: 0x002FE129
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "DV", -this.Level, false);
			base.StatShifter.SetStatShift(base.Object, "MA", -this.Level, false);
		}

		// Token: 0x060084D6 RID: 34006 RVA: 0x002FFF69 File Offset: 0x002FE169
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x060084D7 RID: 34007 RVA: 0x002FFF7C File Offset: 0x002FE17C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084D8 RID: 34008 RVA: 0x002FFF9C File Offset: 0x002FE19C
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

		// Token: 0x040039A1 RID: 14753
		public int Level;
	}
}
