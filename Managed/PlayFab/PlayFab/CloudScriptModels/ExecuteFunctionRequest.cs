using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000180 RID: 384
	[Serializable]
	public class ExecuteFunctionRequest : PlayFabRequestCommon
	{
		// Token: 0x04000879 RID: 2169
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400087A RID: 2170
		public EntityKey Entity;

		// Token: 0x0400087B RID: 2171
		public string FunctionName;

		// Token: 0x0400087C RID: 2172
		public object FunctionParameter;

		// Token: 0x0400087D RID: 2173
		public bool? GeneratePlayStreamEvent;
	}
}
