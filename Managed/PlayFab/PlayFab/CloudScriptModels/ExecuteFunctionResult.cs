using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000181 RID: 385
	[Serializable]
	public class ExecuteFunctionResult : PlayFabResultCommon
	{
		// Token: 0x0400087E RID: 2174
		public FunctionExecutionError Error;

		// Token: 0x0400087F RID: 2175
		public int ExecutionTimeMilliseconds;

		// Token: 0x04000880 RID: 2176
		public string FunctionName;

		// Token: 0x04000881 RID: 2177
		public object FunctionResult;

		// Token: 0x04000882 RID: 2178
		public bool? FunctionResultTooLarge;
	}
}
