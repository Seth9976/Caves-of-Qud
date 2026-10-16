using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000619 RID: 1561
	[Serializable]
	public class ReshephIsBorn : HistoricEvent
	{
		// Token: 0x060041C2 RID: 16834 RVA: 0x001506A4 File Offset: 0x0014E8A4
		public override void Generate()
		{
			this.duration = 0L;
			string text = "One starry evening, a babe was found swaddled with its mouth full of circuits by a group of baetyls in Red Rock. They took him into their fold and fostered him, and he became known as Resheph, the Coiled Lamb of Baetyls.";
			base.SetEventProperty("gospel", text);
			base.AddEntityListItem("cognomen", "the Coiled Lamb of Baetyls", false);
		}
	}
}
