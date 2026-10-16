using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class UnlinkXboxAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F28 RID: 3880
		public Dictionary<string, string> CustomTags;
	}
}
