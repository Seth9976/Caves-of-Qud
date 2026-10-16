using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	public class DeleteAssetRequest : PlayFabRequestCommon
	{
		// Token: 0x04000444 RID: 1092
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000445 RID: 1093
		public string FileName;
	}
}
