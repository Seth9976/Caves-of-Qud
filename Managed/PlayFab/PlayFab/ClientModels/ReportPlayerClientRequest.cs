using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	public class ReportPlayerClientRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E6F RID: 3695
		public string Comment;

		// Token: 0x04000E70 RID: 3696
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E71 RID: 3697
		public string ReporteeId;
	}
}
