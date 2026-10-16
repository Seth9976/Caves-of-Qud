using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000610 RID: 1552
	[Serializable]
	public class ReshephBecomesSultan : HistoricEvent
	{
		// Token: 0x060041B0 RID: 16816 RVA: 0x00150424 File Offset: 0x0014E624
		public override void Generate()
		{
			this.duration = 0L;
			string text = "Acting against a prohibition on the practice of worshipping the stars, Resheph led an army to the gates of Omonporch. There he defeated his brothers in battle and liberated its citizens, and they crowned him the sultan of Qud.";
			base.SetEventProperty("gospel", text);
			base.SetEntityProperty("isSultan", "true");
		}
	}
}
