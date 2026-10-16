using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	public class PostFunctionResultForFunctionExecutionRequest : PlayFabRequestCommon
	{
		// Token: 0x040008D5 RID: 2261
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008D6 RID: 2262
		public EntityKey Entity;

		// Token: 0x040008D7 RID: 2263
		public ExecuteFunctionResult FunctionResult;
	}
}
