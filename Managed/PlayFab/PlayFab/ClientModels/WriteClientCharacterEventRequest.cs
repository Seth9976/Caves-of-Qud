using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000333 RID: 819
	[Serializable]
	public class WriteClientCharacterEventRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FD2 RID: 4050
		public Dictionary<string, object> Body;

		// Token: 0x04000FD3 RID: 4051
		public string CharacterId;

		// Token: 0x04000FD4 RID: 4052
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FD5 RID: 4053
		public string EventName;

		// Token: 0x04000FD6 RID: 4054
		public DateTime? Timestamp;
	}
}
