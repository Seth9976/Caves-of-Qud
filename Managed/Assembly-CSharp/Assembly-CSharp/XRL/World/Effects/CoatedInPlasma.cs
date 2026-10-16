using System;
using System.Text;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C46 RID: 3142
	[Serializable]
	public class CoatedInPlasma : Effect, ITierInitialized
	{
		// Token: 0x0600804A RID: 32842 RVA: 0x002F4004 File Offset: 0x002F2204
		public CoatedInPlasma()
		{
			this.DisplayName = "{{coated in plasma|coated in plasma}}";
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x002F4017 File Offset: 0x002F2217
		public CoatedInPlasma(int Duration = 1, GameObject Owner = null)
			: this()
		{
			this.Duration = Duration;
			this.Owner = Owner;
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x002F402D File Offset: 0x002F222D
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Roll(20, 200);
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x002F4041 File Offset: 0x002F2241
		public override int GetEffectType()
		{
			return 100663328;
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x002F4048 File Offset: 0x002F2248
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600804F RID: 32847 RVA: 0x002F404B File Offset: 0x002F224B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x002F4050 File Offset: 0x002F2250
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("-100 heat resistance\n").Append("-100 cold resistance\n").Append("-100 electric resistance\n")
				.Append("Temperature does not passively return to ambient temperature\n")
				.Append("Patting or rolling firefighting actions are 25% as effective\n")
				.Append("Removes liquid coatings\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x002F40A8 File Offset: 0x002F22A8
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("CanApplyCoatedInPlasma"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyCoatedInPlasma"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "CoatedInPlasma", this, null))
			{
				return false;
			}
			CoatedInPlasma effect = Object.GetEffect<CoatedInPlasma>();
			if (effect != null)
			{
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
				}
				if (!GameObject.Validate(ref effect.Owner) && GameObject.Validate(ref this.Owner))
				{
					effect.Owner = this.Owner;
				}
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_electricalEffect", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Object.RemoveAllEffects<LiquidCovered>(true);
			this.ApplyStats();
			return true;
		}

		// Token: 0x06008052 RID: 32850 RVA: 0x002F416C File Offset: 0x002F236C
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06008053 RID: 32851 RVA: 0x002F417B File Offset: 0x002F237B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanTemperatureReturnToAmbientEvent>.ID || ID == PooledEvent<GetFirefightingPerformanceEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x002F41AC File Offset: 0x002F23AC
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject.Validate(ref this.Owner);
			base.Object.RemoveAllEffects<LiquidCovered>(true);
			if (base.Object.IsPlayer() && base.Object.IsAflame())
			{
				Achievement.AURORAL.Unlock();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x002F41FD File Offset: 0x002F23FD
		public override bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x002F4216 File Offset: 0x002F2416
		public override bool HandleEvent(GetFirefightingPerformanceEvent E)
		{
			if (E.Object == base.Object)
			{
				E.Result /= 4;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x002F423B File Offset: 0x002F243B
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x002F424B File Offset: 0x002F244B
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

		// Token: 0x06008059 RID: 32857 RVA: 0x002F4286 File Offset: 0x002F2486
		public void ApplyStats()
		{
			base.StatShifter.SetStatShift("HeatResistance", -100, false);
			base.StatShifter.SetStatShift("ColdResistance", -100, false);
			base.StatShifter.SetStatShift("ElectricResistance", -100, false);
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x002F42C4 File Offset: 0x002F24C4
		public void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x040038F6 RID: 14582
		public GameObject Owner;
	}
}
