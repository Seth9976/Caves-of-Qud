using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DA2 RID: 3490
	[Serializable]
	public class ToughnessDrained : Effect
	{
		// Token: 0x06008B4A RID: 35658 RVA: 0x00318CE5 File Offset: 0x00316EE5
		public ToughnessDrained()
		{
			this.DisplayName = "{{r|drained}}";
		}

		// Token: 0x06008B4B RID: 35659 RVA: 0x00318CF8 File Offset: 0x00316EF8
		public ToughnessDrained(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008B4C RID: 35660 RVA: 0x00318D07 File Offset: 0x00316F07
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008B4D RID: 35661 RVA: 0x00318D0E File Offset: 0x00316F0E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008B4E RID: 35662 RVA: 0x00318D11 File Offset: 0x00316F11
		public override bool SameAs(Effect e)
		{
			return (e as ToughnessDrained).Penalty == this.Penalty && base.SameAs(e);
		}

		// Token: 0x06008B4F RID: 35663 RVA: 0x00318D2F File Offset: 0x00316F2F
		public override string GetDetails()
		{
			return this.Penalty.ToString() + " Toughness";
		}

		// Token: 0x06008B50 RID: 35664 RVA: 0x00318D48 File Offset: 0x00316F48
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<ToughnessDrained>())
			{
				ToughnessDrained effect = Object.GetEffect<ToughnessDrained>();
				if (effect.Duration > this.Duration)
				{
					effect.Duration = this.Duration;
				}
				effect.UnapplyStats();
				effect.Penalty++;
				effect.ApplyStats();
				return false;
			}
			if (Object.FireEvent(Event.New("ApplyToughnessDrained", "Duration", this.Duration)))
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_negativeVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				this.Penalty = 1;
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x06008B51 RID: 35665 RVA: 0x00318DF4 File Offset: 0x00316FF4
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			this.Penalty = 0;
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x00318E03 File Offset: 0x00317003
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("Toughness", -this.Penalty, false);
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x00318E1E File Offset: 0x0031701E
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008B54 RID: 35668 RVA: 0x00318E2B File Offset: 0x0031702B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B55 RID: 35669 RVA: 0x00318E4B File Offset: 0x0031704B
		public override bool Render(RenderEvent E)
		{
			return true;
		}

		// Token: 0x06008B56 RID: 35670 RVA: 0x00318E4E File Offset: 0x0031704E
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

		// Token: 0x04003ACB RID: 15051
		private int Penalty;
	}
}
