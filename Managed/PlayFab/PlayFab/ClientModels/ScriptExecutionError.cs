using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C2 RID: 706
	[Serializable]
	public class ScriptExecutionError : PlayFabBaseModel
	{
		// Token: 0x04000E81 RID: 3713
		public string Error;

		// Token: 0x04000E82 RID: 3714
		public string Message;

		// Token: 0x04000E83 RID: 3715
		public string StackTrace;
	}
}
