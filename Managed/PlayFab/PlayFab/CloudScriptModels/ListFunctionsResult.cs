using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	public class ListFunctionsResult : PlayFabResultCommon
	{
		// Token: 0x04000890 RID: 2192
		public List<FunctionModel> Functions;
	}
}
