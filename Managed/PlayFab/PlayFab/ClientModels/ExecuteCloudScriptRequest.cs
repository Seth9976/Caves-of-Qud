using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	public class ExecuteCloudScriptRequest : PlayFabRequestCommon
	{
		// Token: 0x04000B8E RID: 2958
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000B8F RID: 2959
		public string FunctionName;

		// Token: 0x04000B90 RID: 2960
		public object FunctionParameter;

		// Token: 0x04000B91 RID: 2961
		public bool? GeneratePlayStreamEvent;

		// Token: 0x04000B92 RID: 2962
		public CloudScriptRevisionOption? RevisionSelection;

		// Token: 0x04000B93 RID: 2963
		public int? SpecificRevision;
	}
}
