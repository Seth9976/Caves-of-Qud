using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class DeleteRemoteUserRequest : PlayFabRequestCommon
	{
		// Token: 0x04000451 RID: 1105
		public string BuildId;

		// Token: 0x04000452 RID: 1106
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000453 RID: 1107
		public string Region;

		// Token: 0x04000454 RID: 1108
		public string Username;

		// Token: 0x04000455 RID: 1109
		public string VmId;
	}
}
