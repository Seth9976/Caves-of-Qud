using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	public class StartGameRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E98 RID: 3736
		public string BuildVersion;

		// Token: 0x04000E99 RID: 3737
		public string CharacterId;

		// Token: 0x04000E9A RID: 3738
		public string CustomCommandLineData;

		// Token: 0x04000E9B RID: 3739
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E9C RID: 3740
		public string GameMode;

		// Token: 0x04000E9D RID: 3741
		public Region Region;

		// Token: 0x04000E9E RID: 3742
		public string StatisticName;
	}
}
