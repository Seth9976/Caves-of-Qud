using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000336 RID: 822
	[Serializable]
	public class WriteTitleEventRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FDC RID: 4060
		public Dictionary<string, object> Body;

		// Token: 0x04000FDD RID: 4061
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FDE RID: 4062
		public string EventName;

		// Token: 0x04000FDF RID: 4063
		public DateTime? Timestamp;
	}
}
