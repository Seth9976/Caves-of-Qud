using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public class ListQosServersForTitleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000517 RID: 1303
		public Dictionary<string, string> CustomTags;
	}
}
