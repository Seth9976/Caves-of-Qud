using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public class UnregisterFunctionRequest : PlayFabRequestCommon
	{
		// Token: 0x0400090A RID: 2314
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400090B RID: 2315
		public string FunctionName;
	}
}
