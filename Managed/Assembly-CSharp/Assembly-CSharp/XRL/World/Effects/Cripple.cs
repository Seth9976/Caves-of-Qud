using System;
using XRL.Core;
using XRL.Messages;

namespace XRL.World.Effects
{
	// Token: 0x02000D1C RID: 3356
	[Serializable]
	public class Cripple : Effect
	{
		// Token: 0x06008417 RID: 33815 RVA: 0x002FBFD9 File Offset: 0x002FA1D9
		public Cripple()
		{
			this.DisplayName = "{{R|crippled}}";
		}

		// Token: 0x06008418 RID: 33816 RVA: 0x002FBFEC File Offset: 0x002FA1EC
		public Cripple(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008419 RID: 33817 RVA: 0x002FBFFB File Offset: 0x002FA1FB
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x002FBFFE File Offset: 0x002FA1FE
		public override int GetEffectType()
		{
			return 100664320;
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x002FC005 File Offset: 0x002FA205
		public override string GetDetails()
		{
			return "-50 Quickness";
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x002FC00C File Offset: 0x002FA20C
		public override bool Apply(GameObject Object)
		{
			if (!Object.Statistics.ContainsKey("Energy"))
			{
				return false;
			}
			if (!Object.Statistics.ContainsKey("Speed"))
			{
				return false;
			}
			if (Object.HasEffect<Cripple>())
			{
				return false;
			}
			if (Object.FireEvent("ApplyCripple"))
			{
				if (Object.IsPlayer())
				{
					Object.ParticleText("*crippled*", 'R', false, 1.5f, -8f);
					MessageQueue.AddPlayerMessage("You are crippled for " + this.Duration.Things("turn", null) + "!", 'R', true);
				}
				else
				{
					Object.ParticleText("*crippled*", 'C', false, 1.5f, -8f);
				}
				Object.ForfeitTurn(false);
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x0600841D RID: 33821 RVA: 0x002FC0CA File Offset: 0x002FA2CA
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x0600841E RID: 33822 RVA: 0x002FC0D2 File Offset: 0x002FA2D2
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "Speed", -50, false);
		}

		// Token: 0x0600841F RID: 33823 RVA: 0x002FC0EE File Offset: 0x002FA2EE
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008420 RID: 33824 RVA: 0x002FC101 File Offset: 0x002FA301
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008421 RID: 33825 RVA: 0x002FC12C File Offset: 0x002FA32C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 20 && num < 25)
			{
				E.RenderString = "X";
				E.ColorString = "&R^c";
				return false;
			}
			return true;
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x002FC168 File Offset: 0x002FA368
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				this.Duration = 0;
				base.DidX("are", "no longer crippled", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
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
	}
}
