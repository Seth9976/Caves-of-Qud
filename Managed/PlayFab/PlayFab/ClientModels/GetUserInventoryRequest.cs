using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200024F RID: 591
	[Serializable]
	public class GetUserInventoryRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C9C RID: 3228
		public Dictionary<string, string> CustomTags;
	}
}
