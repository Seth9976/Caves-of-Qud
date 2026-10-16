using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	public class ListQueuedFunctionsResult : PlayFabResultCommon
	{
		// Token: 0x04000892 RID: 2194
		public List<QueuedFunctionModel> Functions;
	}
}
