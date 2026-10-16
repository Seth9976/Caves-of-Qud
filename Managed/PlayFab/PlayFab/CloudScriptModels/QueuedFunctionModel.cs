using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public class QueuedFunctionModel : PlayFabBaseModel
	{
		// Token: 0x040008E6 RID: 2278
		public string ConnectionString;

		// Token: 0x040008E7 RID: 2279
		public string FunctionName;

		// Token: 0x040008E8 RID: 2280
		public string QueueName;
	}
}
