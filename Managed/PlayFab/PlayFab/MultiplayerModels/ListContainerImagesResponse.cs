using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public class ListContainerImagesResponse : PlayFabResultCommon
	{
		// Token: 0x040004FE RID: 1278
		public List<string> Images;

		// Token: 0x040004FF RID: 1279
		public int PageSize;

		// Token: 0x04000500 RID: 1280
		public string SkipToken;
	}
}
