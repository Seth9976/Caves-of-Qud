using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D3B RID: 3387
	[Serializable]
	public class Frenzied : Effect, ITierInitialized
	{
		// Token: 0x060085A4 RID: 34212 RVA: 0x003033DB File Offset: 0x003015DB
		public Frenzied()
		{
			this.DisplayName = "{{B|frenzied}}";
			this.Duration = 1;
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x0030340C File Offset: 0x0030160C
		public Frenzied(int Duration = 1, int QuicknessBonus = 10, int MaxKillRadiusBonus = 10, int BerserkDuration = 5, bool BerserkImmediately = false, bool BerserkOnDealDamage = false, bool PreferBleedingTarget = false)
			: this()
		{
			this.Duration = Duration;
			this.QuicknessBonus = QuicknessBonus;
			this.MaxKillRadiusBonus = MaxKillRadiusBonus;
			this.BerserkDuration = BerserkDuration;
			this.BerserkImmediately = BerserkImmediately;
			this.BerserkOnDealDamage = BerserkOnDealDamage;
			this.PreferBleedingTarget = PreferBleedingTarget;
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x00303449 File Offset: 0x00301649
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 200);
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x0030345D File Offset: 0x0030165D
		public override int GetEffectType()
		{
			return 83886082;
		}

		// Token: 0x060085A8 RID: 34216 RVA: 0x00303464 File Offset: 0x00301664
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060085A9 RID: 34217 RVA: 0x00303468 File Offset: 0x00301668
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.QuicknessBonus != 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('\n');
				}
				if (this.QuicknessBonus > 0)
				{
					stringBuilder.Append('+');
				}
				stringBuilder.Append(this.QuicknessBonus).Append(" Quickness");
			}
			stringBuilder.Compound("More likely to attack things.", '\n');
			if (this.PreferBleedingTarget)
			{
				stringBuilder.Compound("Will prefer to attack creatures who are bleeding.", '\n');
			}
			if (this.BerserkOnDealDamage)
			{
				stringBuilder.Compound("Will go berserk the next time ").Append(base.Object.it).Append(base.Object.GetVerb("deal", true, false, false, true, false))
					.Append(" damage in combat.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x00303534 File Offset: 0x00301734
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Frenzied>())
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyFrenzied", "Duration", this.Duration)))
			{
				return false;
			}
			Brain brain = Object.Brain;
			if (brain != null)
			{
				brain.Goals.Clear();
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x00303587 File Offset: 0x00301787
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x00303598 File Offset: 0x00301798
		private void ApplyChanges()
		{
			if (this.MaxKillRadiusBonus != 0 && base.Object.Brain != null)
			{
				base.Object.Brain.MaxKillRadius += this.MaxKillRadiusBonus;
			}
			base.StatShifter.SetStatShift("Speed", this.QuicknessBonus, false);
			if (this.BerserkImmediately)
			{
				this.TriggerBerserk();
			}
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x003035FD File Offset: 0x003017FD
		private void UnapplyChanges()
		{
			if (this.MaxKillRadiusBonus != 0 && base.Object.Brain != null)
			{
				base.Object.Brain.MaxKillRadius -= this.MaxKillRadiusBonus;
			}
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x0030363C File Offset: 0x0030183C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AttackerDealtDamageEvent.ID && this.BerserkOnDealDamage) || (ID == PooledEvent<PreferTargetEvent>.ID && this.PreferBleedingTarget);
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x0030366A File Offset: 0x0030186A
		public override bool HandleEvent(AttackerDealtDamageEvent E)
		{
			if (this.BerserkOnDealDamage && !base.Object.HasEffect<Berserk>())
			{
				this.TriggerBerserk();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085B0 RID: 34224 RVA: 0x00303690 File Offset: 0x00301890
		public override bool HandleEvent(PreferTargetEvent E)
		{
			if (E.Result == 0)
			{
				E.Result = (E.Target1.HasEffect<Bleeding>() && E.Target1.GetBleedLiquid("blood-1000") == "blood-1000").CompareTo(E.Target2.HasEffect<Bleeding>() && E.Target2.GetBleedLiquid("blood-1000") == "blood-1000");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x0030370E File Offset: 0x0030190E
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (!E.Target.HasEffect(typeof(Frenzied)))
			{
				E.Feeling = -100;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085B2 RID: 34226 RVA: 0x00303738 File Offset: 0x00301938
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x060085B3 RID: 34227 RVA: 0x00303765 File Offset: 0x00301965
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x003037A0 File Offset: 0x003019A0
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.RenderString = "!";
					E.ColorString = "&R";
				}
			}
			return true;
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x003037E0 File Offset: 0x003019E0
		public void TriggerBerserk()
		{
			if (base.Object.ApplyEffect(new Berserk(this.BerserkDuration), null))
			{
				base.DidX("enter", "a berserk fury", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
				this.BerserkOnDealDamage = false;
			}
		}

		// Token: 0x040039BE RID: 14782
		public const int DEFAULT_MAX_KILL_RADIUS_BONUS = 10;

		// Token: 0x040039BF RID: 14783
		public const int DEFAULT_QUICKNESS_BONUS = 10;

		// Token: 0x040039C0 RID: 14784
		public const int DEFAULT_BERSERK_DURATION = 5;

		// Token: 0x040039C1 RID: 14785
		public int QuicknessBonus = 10;

		// Token: 0x040039C2 RID: 14786
		public int MaxKillRadiusBonus = 10;

		// Token: 0x040039C3 RID: 14787
		public int BerserkDuration = 5;

		// Token: 0x040039C4 RID: 14788
		public bool BerserkImmediately;

		// Token: 0x040039C5 RID: 14789
		public bool BerserkOnDealDamage;

		// Token: 0x040039C6 RID: 14790
		public bool PreferBleedingTarget;
	}
}
