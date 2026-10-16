using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class CreateBuildAliasRequest : PlayFabRequestCommon
	{
		// Token: 0x040003BB RID: 955
		public string AliasName;

		// Token: 0x040003BC RID: 956
		public List<BuildSelectionCriterion> BuildSelectionCriteria;

		// Token: 0x040003BD RID: 957
		public Dictionary<string, string> CustomTags;
	}
}
