using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public class CreateRemoteUserRequest : PlayFabRequestCommon
	{
		// Token: 0x04000423 RID: 1059
		public string BuildId;

		// Token: 0x04000424 RID: 1060
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000425 RID: 1061
		public DateTime? ExpirationTime;

		// Token: 0x04000426 RID: 1062
		public string Region;

		// Token: 0x04000427 RID: 1063
		public string Username;

		// Token: 0x04000428 RID: 1064
		public string VmId;
	}
}
