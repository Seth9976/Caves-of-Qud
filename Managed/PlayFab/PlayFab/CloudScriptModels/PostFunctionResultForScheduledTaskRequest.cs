using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public class PostFunctionResultForScheduledTaskRequest : PlayFabRequestCommon
	{
		// Token: 0x040008DD RID: 2269
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008DE RID: 2270
		public EntityKey Entity;

		// Token: 0x040008DF RID: 2271
		public ExecuteFunctionResult FunctionResult;

		// Token: 0x040008E0 RID: 2272
		public NameIdentifier ScheduledTaskId;
	}
}
