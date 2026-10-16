using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	public class ExecuteCloudScriptResult : PlayFabResultCommon
	{
		// Token: 0x04000866 RID: 2150
		public int APIRequestsIssued;

		// Token: 0x04000867 RID: 2151
		public ScriptExecutionError Error;

		// Token: 0x04000868 RID: 2152
		public double ExecutionTimeSeconds;

		// Token: 0x04000869 RID: 2153
		public string FunctionName;

		// Token: 0x0400086A RID: 2154
		public object FunctionResult;

		// Token: 0x0400086B RID: 2155
		public bool? FunctionResultTooLarge;

		// Token: 0x0400086C RID: 2156
		public int HttpRequestsIssued;

		// Token: 0x0400086D RID: 2157
		public List<LogStatement> Logs;

		// Token: 0x0400086E RID: 2158
		public bool? LogsTooLarge;

		// Token: 0x0400086F RID: 2159
		public uint MemoryConsumedBytes;

		// Token: 0x04000870 RID: 2160
		public double ProcessorTimeSeconds;

		// Token: 0x04000871 RID: 2161
		public int Revision;
	}
}
