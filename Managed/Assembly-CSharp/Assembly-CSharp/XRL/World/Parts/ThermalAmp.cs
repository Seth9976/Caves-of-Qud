using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x020010B2 RID: 4274
	[Serializable]
	public class ThermalAmp : IPoweredPart
	{
		// Token: 0x0600AA42 RID: 43586 RVA: 0x003E4783 File Offset: 0x003E2983
		public ThermalAmp()
		{
			this.ChargeUse = 0;
			this.WorksOnWearer = true;
			this.IsPowerLoadSensitive = false;
			this.NameForStatus = "ThermalAmp";
		}

		// Token: 0x0600AA43 RID: 43587 RVA: 0x003E47AB File Offset: 0x003E29AB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AA44 RID: 43588 RVA: 0x003E47AE File Offset: 0x003E29AE
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600AA45 RID: 43589 RVA: 0x003E47B4 File Offset: 0x003E29B4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || (this.WorksOnWearer && ID == EquippedEvent.ID) || (this.WorksOnWearer && ID == UnequippedEvent.ID) || (this.WorksOnSelf && ID == BeforeApplyDamageEvent.ID);
		}

		// Token: 0x0600AA46 RID: 43590 RVA: 0x003E4808 File Offset: 0x003E2A08
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int num = this.MyPowerLoadLevel();
			string statusSummary = base.GetStatusSummary();
			this.AppendRule(E.Postfix, this.GetPercentage(this.HeatDamage, num), "heat damage dealt", "R", "r", statusSummary);
			this.AppendRule(E.Postfix, this.GetPercentage(this.ColdDamage, num), "cold damage dealt", "C", "c", statusSummary);
			this.AppendRule(E.Postfix, this.GetPercentage(this.ModifyHeat, num), "to the intensity of your heating effects", "R", "r", statusSummary);
			this.AppendRule(E.Postfix, this.GetPercentage(this.ModifyCold, num), "to the intensity of your cooling effects", "C", "c", statusSummary);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA47 RID: 43591 RVA: 0x003E48D0 File Offset: 0x003E2AD0
		public void AppendRule(StringBuilder SB, int Value, string Effect, string Positive = "rules", string Negative = "rules", string Status = null)
		{
			if (Value == 0)
			{
				return;
			}
			SB.Compound("{{", '\n').Append((Value > 0) ? Positive : Negative).Append('|')
				.AppendSigned(Value)
				.Append('%')
				.Compound(Effect)
				.Append("}}");
			base.AddStatusSummary(SB, Status);
		}

		// Token: 0x0600AA48 RID: 43592 RVA: 0x003E492B File Offset: 0x003E2B2B
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.WorksOnWearer)
			{
				E.Actor.RegisterPartEvent(this, "AttackerDealingDamage");
				E.Actor.RegisterPartEvent(this, "AttackerBeforeTemperatureChange");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA49 RID: 43593 RVA: 0x003E495E File Offset: 0x003E2B5E
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.WorksOnWearer)
			{
				E.Actor.UnregisterPartEvent(this, "AttackerDealingDamage");
				E.Actor.UnregisterPartEvent(this, "AttackerBeforeTemperatureChange");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA4A RID: 43594 RVA: 0x003E4991 File Offset: 0x003E2B91
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (this.WorksOnSelf)
			{
				this.AmplifyDamage(E.Damage);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA4B RID: 43595 RVA: 0x003E49AE File Offset: 0x003E2BAE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.WorksOnSelf)
			{
				Registrar.Register("AttackerDealingDamage");
				Registrar.Register("AttackerBeforeTemperatureChange");
			}
		}

		// Token: 0x0600AA4C RID: 43596 RVA: 0x003E49D0 File Offset: 0x003E2BD0
		public void AmplifyDamage(Damage Damage)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				if (this.HeatDamage != 0 && Damage.IsHeatDamage())
				{
					Damage.Amount *= 100 + this.GetPercentage(this.HeatDamage, num);
					Damage.Amount /= 100;
					return;
				}
				if (this.ColdDamage != 0 && Damage.IsColdDamage())
				{
					Damage.Amount *= 100 + this.GetPercentage(this.ColdDamage, num);
					Damage.Amount /= 100;
				}
			}
		}

		// Token: 0x0600AA4D RID: 43597 RVA: 0x003E4A88 File Offset: 0x003E2C88
		public void AmplifyTemperature(ref int Amount)
		{
			int num = this.MyPowerLoadLevel();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
			{
				if (Amount > 0 && this.ModifyHeat != 0)
				{
					Amount *= 100 + this.GetPercentage(this.ModifyHeat, num);
					Amount /= 100;
					return;
				}
				if (Amount < 0 && this.ModifyCold != 0)
				{
					Amount *= 100 + this.GetPercentage(this.ModifyCold, num);
					Amount /= 100;
				}
			}
		}

		// Token: 0x0600AA4E RID: 43598 RVA: 0x003E4B18 File Offset: 0x003E2D18
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerDealingDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null)
				{
					this.AmplifyDamage(damage);
					goto IL_0063;
				}
			}
			if (E.ID == "AttackerBeforeTemperatureChange")
			{
				int intParameter = E.GetIntParameter("Amount", 0);
				this.AmplifyTemperature(ref intParameter);
				E.SetParameter("Amount", intParameter);
			}
			IL_0063:
			return base.FireEvent(E);
		}

		// Token: 0x0600AA4F RID: 43599 RVA: 0x003E4B8F File Offset: 0x003E2D8F
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AA50 RID: 43600 RVA: 0x003E4B94 File Offset: 0x003E2D94
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ChargeUse > 0 && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600AA51 RID: 43601 RVA: 0x003E4BD8 File Offset: 0x003E2DD8
		public int GetPercentage(int Value, int PowerLoad)
		{
			int num = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num == 0)
			{
				return Value;
			}
			return Value * (100 + num) / 100;
		}

		// Token: 0x04004624 RID: 17956
		public int HeatDamage;

		// Token: 0x04004625 RID: 17957
		public int ColdDamage;

		// Token: 0x04004626 RID: 17958
		public int ModifyHeat;

		// Token: 0x04004627 RID: 17959
		public int ModifyCold;
	}
}
