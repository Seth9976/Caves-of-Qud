using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D65 RID: 3429
	[Serializable]
	public class MobilityImpaired : Effect
	{
		// Token: 0x060087B3 RID: 34739 RVA: 0x0030AC1D File Offset: 0x00308E1D
		public MobilityImpaired()
		{
			this.DisplayName = "{{B|mobility impaired}}";
			this.Duration = 1;
		}

		// Token: 0x060087B4 RID: 34740 RVA: 0x0030AC37 File Offset: 0x00308E37
		public MobilityImpaired(int _Amount)
			: this()
		{
			this.Amount = _Amount;
		}

		// Token: 0x060087B5 RID: 34741 RVA: 0x0030AC46 File Offset: 0x00308E46
		public override int GetEffectType()
		{
			return 33555456;
		}

		// Token: 0x060087B6 RID: 34742 RVA: 0x0030AC4D File Offset: 0x00308E4D
		public override bool SameAs(Effect e)
		{
			return (e as MobilityImpaired).Amount == this.Amount && base.SameAs(e);
		}

		// Token: 0x060087B7 RID: 34743 RVA: 0x0030AC6B File Offset: 0x00308E6B
		public override string GetDetails()
		{
			if (this.Amount > 0)
			{
				return "-" + this.Amount.ToString() + " move speed due to missing or broken limbs";
			}
			return "Mobility impaired due to missing or broken limbs.";
		}

		// Token: 0x060087B8 RID: 34744 RVA: 0x0030AC98 File Offset: 0x00308E98
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<MobilityImpaired>())
			{
				return false;
			}
			if (Object.Brain != null && !Object.Brain.Mobile)
			{
				return false;
			}
			if (Object.IsPlayer() || base.Visible())
			{
				Object.ParticleText("*mobility impaired*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			}
			return true;
		}

		// Token: 0x04003A27 RID: 14887
		public int Amount;
	}
}
