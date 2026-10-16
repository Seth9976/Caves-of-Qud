using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public class ListContainerImageTagsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000501 RID: 1281
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000502 RID: 1282
		public string ImageName;
	}
}
