using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001EF RID: 495
	[Serializable]
	public class GameServerRegionsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BC3 RID: 3011
		public string BuildVersion;

		// Token: 0x04000BC4 RID: 3012
		public string TitleId;
	}
}
