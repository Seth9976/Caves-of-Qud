using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DA1 RID: 3489
	[Serializable]
	public class ToughnessBoosted : Effect
	{
		// Token: 0x06008B3E RID: 35646 RVA: 0x00318B10 File Offset: 0x00316D10
		public ToughnessBoosted()
		{
			this.DisplayName = "{{g|vitalized}}";
		}

		// Token: 0x06008B3F RID: 35647 RVA: 0x00318B23 File Offset: 0x00316D23
		public ToughnessBoosted(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008B40 RID: 35648 RVA: 0x00318B34 File Offset: 0x00316D34
		public override int GetEffectType()
		{
			int num = 67108868;
			if (this.Bonus < 0)
			{
				num |= 33554432;
			}
			if (Math.Abs(this.Bonus) < 6)
			{
				num |= 16777216;
			}
			return num;
		}

		// Token: 0x06008B41 RID: 35649 RVA: 0x00318B6F File Offset: 0x00316D6F
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008B42 RID: 35650 RVA: 0x00318B72 File Offset: 0x00316D72
		public override bool SameAs(Effect e)
		{
			return (e as ToughnessBoosted).Bonus == this.Bonus && base.SameAs(e);
		}

		// Token: 0x06008B43 RID: 35651 RVA: 0x00318B90 File Offset: 0x00316D90
		public override string GetDetails()
		{
			return this.Bonus.Signed(false) + " Toughness";
		}

		// Token: 0x06008B44 RID: 35652 RVA: 0x00318BA8 File Offset: 0x00316DA8
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<ToughnessBoosted>())
			{
				ToughnessBoosted effect = Object.GetEffect<ToughnessBoosted>();
				if (effect.Duration < this.Duration)
				{
					effect.Duration = this.Duration;
				}
				effect.UnapplyStats();
				effect.Bonus++;
				effect.ApplyStats();
				return false;
			}
			if (Object.FireEvent(Event.New("ApplyToughnessBoosted", "Duration", this.Duration)))
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				this.Bonus = 1;
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x06008B45 RID: 35653 RVA: 0x00318C54 File Offset: 0x00316E54
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("Toughness", this.Bonus, false);
		}

		// Token: 0x06008B46 RID: 35654 RVA: 0x00318C6E File Offset: 0x00316E6E
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008B47 RID: 35655 RVA: 0x00318C7B File Offset: 0x00316E7B
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			this.Bonus = 0;
		}

		// Token: 0x06008B48 RID: 35656 RVA: 0x00318C8A File Offset: 0x00316E8A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B49 RID: 35657 RVA: 0x00318CAA File Offset: 0x00316EAA
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

		// Token: 0x04003ACA RID: 15050
		private int Bonus;
	}
}
