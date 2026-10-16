using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public class ListContainerImagesRequest : PlayFabRequestCommon
	{
		// Token: 0x040004FB RID: 1275
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004FC RID: 1276
		public int? PageSize;

		// Token: 0x040004FD RID: 1277
		public string SkipToken;
	}
}
