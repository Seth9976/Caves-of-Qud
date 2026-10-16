using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public class ExecuteCloudScriptResult : PlayFabResultCommon
	{
		// Token: 0x04000B94 RID: 2964
		public int APIRequestsIssued;

		// Token: 0x04000B95 RID: 2965
		public ScriptExecutionError Error;

		// Token: 0x04000B96 RID: 2966
		public double ExecutionTimeSeconds;

		// Token: 0x04000B97 RID: 2967
		public string FunctionName;

		// Token: 0x04000B98 RID: 2968
		public object FunctionResult;

		// Token: 0x04000B99 RID: 2969
		public bool? FunctionResultTooLarge;

		// Token: 0x04000B9A RID: 2970
		public int HttpRequestsIssued;

		// Token: 0x04000B9B RID: 2971
		public List<LogStatement> Logs;

		// Token: 0x04000B9C RID: 2972
		public bool? LogsTooLarge;

		// Token: 0x04000B9D RID: 2973
		public uint MemoryConsumedBytes;

		// Token: 0x04000B9E RID: 2974
		public double ProcessorTimeSeconds;

		// Token: 0x04000B9F RID: 2975
		public int Revision;
	}
}
