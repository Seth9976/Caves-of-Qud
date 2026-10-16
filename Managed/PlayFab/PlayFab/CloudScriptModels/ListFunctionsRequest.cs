using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	public class ListFunctionsRequest : PlayFabRequestCommon
	{
		// Token: 0x0400088F RID: 2191
		public Dictionary<string, string> CustomTags;
	}
}
