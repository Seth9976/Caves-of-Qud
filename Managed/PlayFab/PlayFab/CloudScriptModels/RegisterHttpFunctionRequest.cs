using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public class RegisterHttpFunctionRequest : PlayFabRequestCommon
	{
		// Token: 0x040008E9 RID: 2281
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008EA RID: 2282
		public string FunctionName;

		// Token: 0x040008EB RID: 2283
		public string FunctionUrl;
	}
}
