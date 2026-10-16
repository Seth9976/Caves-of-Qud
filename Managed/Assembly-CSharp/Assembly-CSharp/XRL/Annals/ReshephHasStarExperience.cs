using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000616 RID: 1558
	[Serializable]
	public class ReshephHasStarExperience : HistoricEvent
	{
		// Token: 0x060041BC RID: 16828 RVA: 0x001505CC File Offset: 0x0014E7CC
		public override void Generate()
		{
			this.duration = 0L;
			string text = "While traveling alone through the Rust Wells, Resheph came across a trove of gleaming stars in a bottle. From that day forth, he always kept some stardust on his person.";
			base.SetEventProperty("gospel", text);
		}
	}
}
