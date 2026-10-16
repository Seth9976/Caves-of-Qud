using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public class UpdateBuildAliasRequest : PlayFabRequestCommon
	{
		// Token: 0x040005BE RID: 1470
		public string AliasId;

		// Token: 0x040005BF RID: 1471
		public string AliasName;

		// Token: 0x040005C0 RID: 1472
		public List<BuildSelectionCriterion> BuildSelectionCriteria;

		// Token: 0x040005C1 RID: 1473
		public Dictionary<string, string> CustomTags;
	}
}
