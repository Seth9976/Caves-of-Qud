using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	public class ExecuteEntityCloudScriptRequest : PlayFabRequestCommon
	{
		// Token: 0x04000872 RID: 2162
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000873 RID: 2163
		public EntityKey Entity;

		// Token: 0x04000874 RID: 2164
		public string FunctionName;

		// Token: 0x04000875 RID: 2165
		public object FunctionParameter;

		// Token: 0x04000876 RID: 2166
		public bool? GeneratePlayStreamEvent;

		// Token: 0x04000877 RID: 2167
		public CloudScriptRevisionOption? RevisionSelection;

		// Token: 0x04000878 RID: 2168
		public int? SpecificRevision;
	}
}
