using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000EE4 RID: 3812
	public class RermadonChord : INephalChord
	{
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600987F RID: 39039 RVA: 0x0037651D File Offset: 0x0037471D
		public override string Source
		{
			get
			{
				return "Rermadon";
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06009880 RID: 39040 RVA: 0x00376524 File Offset: 0x00374724
		public virtual int ResistBonus
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x06009881 RID: 39041 RVA: 0x00376528 File Offset: 0x00374728
		public override void Initialize()
		{
			base.StatShifter.SetStatShift("HeatResistance", this.ResistBonus, false);
			base.StatShifter.SetStatShift("ColdResistance", this.ResistBonus, false);
			base.StatShifter.SetStatShift("ElectricResistance", this.ResistBonus, false);
			base.StatShifter.SetStatShift("AcidResistance", this.ResistBonus, false);
		}

		// Token: 0x06009882 RID: 39042 RVA: 0x00376595 File Offset: 0x00374795
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06009883 RID: 39043 RVA: 0x003765A2 File Offset: 0x003747A2
		public override void AppendRules(StringBuilder Postfix)
		{
			Postfix.Append("\n• +").Append(this.ResistBonus).Append(" all resists");
		}
	}
}
