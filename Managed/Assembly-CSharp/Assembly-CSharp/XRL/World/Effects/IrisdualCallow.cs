using System;
using System.Text;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D57 RID: 3415
	[Serializable]
	public class IrisdualCallow : Effect
	{
		// Token: 0x060086F4 RID: 34548 RVA: 0x00307415 File Offset: 0x00305615
		public IrisdualCallow()
		{
			this.DisplayName = "{{r|callow}}";
		}

		// Token: 0x060086F5 RID: 34549 RVA: 0x00307428 File Offset: 0x00305628
		public IrisdualCallow(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060086F6 RID: 34550 RVA: 0x00307437 File Offset: 0x00305637
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060086F7 RID: 34551 RVA: 0x0030743A File Offset: 0x0030563A
		public override int GetEffectType()
		{
			return 33554560;
		}

		// Token: 0x060086F8 RID: 34552 RVA: 0x00307444 File Offset: 0x00305644
		public override string GetDetails()
		{
			if (this.Details.IsNullOrEmpty())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				if (this.AVShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "AV", this.AVShift, '\n', false, false);
				}
				if (this.DVShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "DV", this.DVShift, '\n', false, false);
				}
				if (this.MAShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "MA", this.MAShift, '\n', false, false);
				}
				if (this.SpeedShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "Speed", this.SpeedShift, '\n', false, false);
				}
				if (this.HeatResistanceShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "HeatResistance", this.HeatResistanceShift, '\n', false, false);
				}
				if (this.ColdResistanceShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "ColdResistance", this.ColdResistanceShift, '\n', false, false);
				}
				if (this.ElectricResistanceShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "ElectricResistance", this.ElectricResistanceShift, '\n', false, false);
				}
				if (this.AcidResistanceShift != 0)
				{
					Statistic.CompoundStatAdjustDescription(stringBuilder, "AcidResistance", this.AcidResistanceShift, '\n', false, false);
				}
				this.Details = Event.FinalizeString(stringBuilder);
			}
			return this.Details;
		}

		// Token: 0x060086F9 RID: 34553 RVA: 0x00307564 File Offset: 0x00305764
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<IrisdualCallow>())
			{
				return false;
			}
			int num = Stats.GetCombatAV(Object) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("AV", this.AVShift = -num, false);
			}
			num = Stats.GetCombatDV(Object) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("DV", this.DVShift = -num, false);
			}
			num = Stats.GetCombatMA(Object) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("MA", this.MAShift = -num, false);
			}
			num = Mathf.RoundToInt((float)Object.GetStatValue("Speed", 0) * 0.25f);
			if (num > 0)
			{
				base.StatShifter.SetStatShift("Speed", this.SpeedShift = -num, false);
			}
			num = Object.GetStatValue("HeatResistance", 0) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("HeatResistance", this.HeatResistanceShift = -num, false);
			}
			num = Object.GetStatValue("ColdResistance", 0) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("ColdResistance", this.ColdResistanceShift = -num, false);
			}
			num = Object.GetStatValue("ElectricResistance", 0) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("ElectricResistance", this.ElectricResistanceShift = -num, false);
			}
			num = Object.GetStatValue("AcidResistance", 0) / 2;
			if (num > 0)
			{
				base.StatShifter.SetStatShift("AcidResistance", this.AcidResistanceShift = -num, false);
			}
			Object.EmitMessage(string.Concat(new string[]
			{
				Object.Poss("rind", true),
				" softens while ",
				Object.it,
				Object.GetVerb("recrystallize", true, true, false, true, false),
				"!"
			}), null, IComponent<GameObject>.ConsequentialColor(null, Object), false);
			return true;
		}

		// Token: 0x060086FA RID: 34554 RVA: 0x00307749 File Offset: 0x00305949
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			Object.EmitMessage(Object.Poss("rind", true) + " recrystallizes and hardens once more.", null, IComponent<GameObject>.ConsequentialColor(Object, null), false);
		}

		// Token: 0x04003A00 RID: 14848
		public int AVShift;

		// Token: 0x04003A01 RID: 14849
		public int DVShift;

		// Token: 0x04003A02 RID: 14850
		public int MAShift;

		// Token: 0x04003A03 RID: 14851
		public int SpeedShift;

		// Token: 0x04003A04 RID: 14852
		public int HeatResistanceShift;

		// Token: 0x04003A05 RID: 14853
		public int ColdResistanceShift;

		// Token: 0x04003A06 RID: 14854
		public int ElectricResistanceShift;

		// Token: 0x04003A07 RID: 14855
		public int AcidResistanceShift;

		// Token: 0x04003A08 RID: 14856
		[NonSerialized]
		private string Details;
	}
}
