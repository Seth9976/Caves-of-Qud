using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000292 RID: 658
	[Serializable]
	public class MatchmakeRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DB9 RID: 3513
		public string BuildVersion;

		// Token: 0x04000DBA RID: 3514
		public string CharacterId;

		// Token: 0x04000DBB RID: 3515
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000DBC RID: 3516
		public string GameMode;

		// Token: 0x04000DBD RID: 3517
		public string LobbyId;

		// Token: 0x04000DBE RID: 3518
		public Region? Region;

		// Token: 0x04000DBF RID: 3519
		public bool? StartNewIfNoneFound;

		// Token: 0x04000DC0 RID: 3520
		public string StatisticName;

		// Token: 0x04000DC1 RID: 3521
		public CollectionFilter TagFilter;
	}
}
