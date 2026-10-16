using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	public class RegisterQueuedFunctionRequest : PlayFabRequestCommon
	{
		// Token: 0x040008EC RID: 2284
		public string ConnectionString;

		// Token: 0x040008ED RID: 2285
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008EE RID: 2286
		public string FunctionName;

		// Token: 0x040008EF RID: 2287
		public string QueueName;
	}
}
