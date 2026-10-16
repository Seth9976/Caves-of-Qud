using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x020005EB RID: 1515
	[Serializable]
	public class AddLocationToRegion : HistoricEvent
	{
		// Token: 0x0600414D RID: 16717 RVA: 0x00148959 File Offset: 0x00146B59
		public AddLocationToRegion(string _locationName)
		{
			this.locationName = _locationName;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x00148968 File Offset: 0x00146B68
		public override void Generate()
		{
			base.AddEntityListItem("locations", this.locationName, false);
			this.duration = 0L;
		}

		// Token: 0x04002004 RID: 8196
		private string locationName;
	}
}
