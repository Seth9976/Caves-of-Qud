using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200061B RID: 1563
	[Serializable]
	public class ReshephMeetsRebekah : HistoricEvent
	{
		// Token: 0x060041C6 RID: 16838 RVA: 0x00150714 File Offset: 0x0014E914
		public override void Generate()
		{
			this.duration = 0L;
			string text = "While traveling near Bethesda Susa, Resheph lost control of his chariot and drove it off a cliff. Luckily, a local physician named Rebekah came to his rescue. Moved by her kindness, Resheph enrolled at a nearby hospice as her apprentice.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("rebekah", "true");
			base.SetEventProperty("rebekahWasHealer", "true");
		}
	}
}
