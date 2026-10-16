using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000182 RID: 386
	[Serializable]
	public class FunctionExecutionError : PlayFabBaseModel
	{
		// Token: 0x04000883 RID: 2179
		public string Error;

		// Token: 0x04000884 RID: 2180
		public string Message;

		// Token: 0x04000885 RID: 2181
		public string StackTrace;
	}
}
