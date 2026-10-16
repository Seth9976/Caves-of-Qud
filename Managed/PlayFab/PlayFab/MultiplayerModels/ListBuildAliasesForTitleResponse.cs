using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public class ListBuildAliasesForTitleResponse : PlayFabResultCommon
	{
		// Token: 0x040004EE RID: 1262
		public List<BuildAliasDetailsResponse> BuildAliases;
	}
}
