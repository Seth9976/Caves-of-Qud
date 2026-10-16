using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class BuildAliasDetailsResponse : PlayFabResultCommon
	{
		// Token: 0x04000384 RID: 900
		public string AliasId;

		// Token: 0x04000385 RID: 901
		public string AliasName;

		// Token: 0x04000386 RID: 902
		public List<BuildSelectionCriterion> BuildSelectionCriteria;

		// Token: 0x04000387 RID: 903
		public int PageSize;

		// Token: 0x04000388 RID: 904
		public string SkipToken;
	}
}
