using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200061A RID: 1562
	[Serializable]
	public class ReshephLearnsCurse : HistoricEvent
	{
		// Token: 0x060041C4 RID: 16836 RVA: 0x001506E4 File Offset: 0x0014E8E4
		public override void Generate()
		{
			this.duration = 0L;
			string text = "At Starfarer's Quay, Resheph consulted with mysterious strangers and learned the secret of the Gyre. By cause of the misdeeds of the elder saads and sultans, star beings had levied a curse on Qud. Resheph vowed to make right from this wrong and redeem our doomed world.";
			base.SetEventProperty("gospel", text);
		}
	}
}
