using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000334 RID: 820
	[Serializable]
	public class WriteClientPlayerEventRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FD7 RID: 4055
		public Dictionary<string, object> Body;

		// Token: 0x04000FD8 RID: 4056
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FD9 RID: 4057
		public string EventName;

		// Token: 0x04000FDA RID: 4058
		public DateTime? Timestamp;
	}
}
