using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	public class ListHttpFunctionsResult : PlayFabResultCommon
	{
		// Token: 0x04000891 RID: 2193
		public List<HttpFunctionModel> Functions;
	}
}
