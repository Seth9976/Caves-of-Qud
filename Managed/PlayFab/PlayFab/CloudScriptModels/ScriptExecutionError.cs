using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public class ScriptExecutionError : PlayFabBaseModel
	{
		// Token: 0x040008F0 RID: 2288
		public string Error;

		// Token: 0x040008F1 RID: 2289
		public string Message;

		// Token: 0x040008F2 RID: 2290
		public string StackTrace;
	}
}
