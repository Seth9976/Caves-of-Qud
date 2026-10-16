using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public class ListPartyQosServersRequest : PlayFabRequestCommon
	{
		// Token: 0x04000512 RID: 1298
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000513 RID: 1299
		public string Version;
	}
}
