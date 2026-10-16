using System;
using System.Text;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D85 RID: 3461
	[Serializable]
	public class Shamed : Effect, ITierInitialized
	{
		// Token: 0x06008989 RID: 35209 RVA: 0x00312CC9 File Offset: 0x00310EC9
		public Shamed()
		{
			this.DisplayName = "{{r|shamed}}";
		}

		// Token: 0x0600898A RID: 35210 RVA: 0x00312CEB File Offset: 0x00310EEB
		public Shamed(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600898B RID: 35211 RVA: 0x00312CFA File Offset: 0x00310EFA
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(50, 200);
		}

		// Token: 0x0600898C RID: 35212 RVA: 0x00312D0E File Offset: 0x00310F0E
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x0600898D RID: 35213 RVA: 0x00312D15 File Offset: 0x00310F15
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600898E RID: 35214 RVA: 0x00312D18 File Offset: 0x00310F18
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(-this.Penalty).Append(" DV\n").Append(-this.Penalty)
				.Append(" to-hit\n")
				.Append(-this.Penalty)
				.Append(" Willpower\n")
				.Append(-this.Penalty)
				.Append(" Ego\n");
			if (base.Object != null)
			{
				if (this.SpeedPenalty != 0)
				{
					stringBuilder.Append(-this.SpeedPenalty).Append(" Quickness");
				}
			}
			else
			{
				stringBuilder.Append(-this.SpeedPenaltyPercent).Append("% Quickness");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600898F RID: 35215 RVA: 0x00312DD0 File Offset: 0x00310FD0
		public override bool SameAs(Effect e)
		{
			Shamed shamed = e as Shamed;
			return shamed.Penalty == this.Penalty && shamed.SpeedPenaltyPercent == this.SpeedPenaltyPercent && shamed.SpeedPenalty == this.SpeedPenalty && base.SameAs(e);
		}

		// Token: 0x06008990 RID: 35216 RVA: 0x00312E1C File Offset: 0x0031101C
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("CanApplyShamed"))
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyShamed", "Duration", this.Duration)))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Shamed", this, null))
			{
				return false;
			}
			this.ApplyStats();
			return true;
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x00312E6F File Offset: 0x0031106F
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06008992 RID: 35218 RVA: 0x00312E7E File Offset: 0x0031107E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x06008993 RID: 35219 RVA: 0x00312E94 File Offset: 0x00311094
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == base.Object && E.Checking == "Actor")
			{
				E.Modifier -= this.Penalty;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x00312ED0 File Offset: 0x003110D0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("ApplyShamed");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanApplyShamed");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x00312F08 File Offset: 0x00311108
		private void ApplyStats()
		{
			this.SpeedPenalty = base.Object.Stat("Speed", 0) * this.SpeedPenaltyPercent / 100;
			base.StatShifter.SetStatShift("DV", -this.Penalty, false);
			base.StatShifter.SetStatShift("Ego", -this.Penalty, false);
			base.StatShifter.SetStatShift("Willpower", -this.Penalty, false);
			base.StatShifter.SetStatShift("Speed", -this.SpeedPenalty, false);
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x00312F9A File Offset: 0x0031119A
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x00312FA8 File Offset: 0x003111A8
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 40)
				{
					E.Tile = null;
					E.RenderString = ";";
					E.ColorString = "&r";
				}
			}
			return true;
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x00312FF0 File Offset: 0x003111F0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanApplyShamed")
			{
				return false;
			}
			if (E.ID == "ApplyShamed")
			{
				if (this.Duration > 0)
				{
					this.Duration = E.GetIntParameter("Duration", 0);
					return false;
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A7E RID: 14974
		public int Penalty = 4;

		// Token: 0x04003A7F RID: 14975
		public int SpeedPenaltyPercent = 10;

		// Token: 0x04003A80 RID: 14976
		public int SpeedPenalty;
	}
}
