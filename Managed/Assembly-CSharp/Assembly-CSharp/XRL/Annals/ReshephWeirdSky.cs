using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200061D RID: 1565
	[Serializable]
	public class ReshephWeirdSky : HistoricEvent
	{
		// Token: 0x060041CA RID: 16842 RVA: 0x001507B8 File Offset: 0x0014E9B8
		public override void Generate()
		{
			this.duration = 0L;
			string text = "At twilight in the shadow of the Spindle, the people of Omonporch saw an image on the horizon that looked like a ghost bathed in cerulean. It was Resheph, and after he came and left Omonporch, the people built a monument to him, and thenceforth called him Ghost-in-Cerulean.";
			base.SetEventProperty("gospel", text);
			base.AddEntityListItem("cognomen", "Ghost-in-Cerulean", false);
		}
	}
}
