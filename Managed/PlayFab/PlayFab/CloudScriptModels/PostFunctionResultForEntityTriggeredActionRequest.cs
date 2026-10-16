using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	public class PostFunctionResultForEntityTriggeredActionRequest : PlayFabRequestCommon
	{
		// Token: 0x040008D2 RID: 2258
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008D3 RID: 2259
		public EntityKey Entity;

		// Token: 0x040008D4 RID: 2260
		public ExecuteFunctionResult FunctionResult;
	}
}
