using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class DeleteBuildAliasRequest : PlayFabRequestCommon
	{
		// Token: 0x04000446 RID: 1094
		public string AliasId;

		// Token: 0x04000447 RID: 1095
		public Dictionary<string, string> CustomTags;
	}
}
