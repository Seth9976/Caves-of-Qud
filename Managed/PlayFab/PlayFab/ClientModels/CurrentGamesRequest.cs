using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	public class CurrentGamesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000B7C RID: 2940
		public string BuildVersion;

		// Token: 0x04000B7D RID: 2941
		public string GameMode;

		// Token: 0x04000B7E RID: 2942
		public Region? Region;

		// Token: 0x04000B7F RID: 2943
		public string StatisticName;

		// Token: 0x04000B80 RID: 2944
		public CollectionFilter TagFilter;
	}
}
