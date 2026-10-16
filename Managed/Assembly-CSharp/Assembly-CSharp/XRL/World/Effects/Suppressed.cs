using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D99 RID: 3481
	[Serializable]
	public class Suppressed : Effect, ITierInitialized
	{
		// Token: 0x06008ACE RID: 35534 RVA: 0x00317964 File Offset: 0x00315B64
		public Suppressed()
		{
			this.DisplayName = "suppressed";
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x00317977 File Offset: 0x00315B77
		public Suppressed(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008AD0 RID: 35536 RVA: 0x00317986 File Offset: 0x00315B86
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(2, 8);
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x00317995 File Offset: 0x00315B95
		public override int GetEffectType()
		{
			return 100663424;
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x0031799C File Offset: 0x00315B9C
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x0031799F File Offset: 0x00315B9F
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x003179A2 File Offset: 0x00315BA2
		public override string GetDetails()
		{
			return "Can't move.";
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x003179AC File Offset: 0x00315BAC
		public override bool Apply(GameObject Object)
		{
			if (!Object.IsPotentiallyMobile())
			{
				return false;
			}
			if (!Object.FireEvent("ApplySuppressed"))
			{
				return false;
			}
			Suppressed effect = Object.GetEffect<Suppressed>();
			if (effect != null && this.Duration > effect.Duration)
			{
				effect.Duration = this.Duration;
			}
			if (Object.IsPlayer())
			{
				base.DidX("are", "suppressed", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			}
			Object.ParticleText("*suppressed*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			return true;
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x00317A3F File Offset: 0x00315C3F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == LeaveCellEvent.ID;
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x00317A58 File Offset: 0x00315C58
		public override bool HandleEvent(LeaveCellEvent E)
		{
			if (this.Duration > 0 && !E.Forced && E.Type != "Teleporting")
			{
				base.Object.UseEnergy(1000, "Suppression", null, null, false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x00317AB4 File Offset: 0x00315CB4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 50 && num < 60)
				{
					E.Tile = null;
					E.RenderString = "\u000f";
					E.ColorString = "&C^K";
				}
			}
			return true;
		}
	}
}
