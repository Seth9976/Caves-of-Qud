using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000EE2 RID: 3810
	public class QasChord : INephalChord
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06009877 RID: 39031 RVA: 0x003764B3 File Offset: 0x003746B3
		public override string Source
		{
			get
			{
				return "Qas";
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06009878 RID: 39032 RVA: 0x003764BA File Offset: 0x003746BA
		public virtual int SpeedBonus
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x06009879 RID: 39033 RVA: 0x003764BE File Offset: 0x003746BE
		public override void Initialize()
		{
			base.StatShifter.SetStatShift("Speed", this.SpeedBonus, false);
		}

		// Token: 0x0600987A RID: 39034 RVA: 0x003764D8 File Offset: 0x003746D8
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x0600987B RID: 39035 RVA: 0x003764E5 File Offset: 0x003746E5
		public override void AppendRules(StringBuilder Postfix)
		{
			Postfix.Append("\n• ");
			Statistic.AppendStatAdjustDescription(Postfix, "Speed", this.SpeedBonus, false, false);
		}
	}
}
