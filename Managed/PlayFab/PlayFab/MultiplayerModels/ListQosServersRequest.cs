using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	public class ListQosServersRequest : PlayFabRequestCommon
	{
		// Token: 0x0400051B RID: 1307
		public Dictionary<string, string> CustomTags;
	}
}
