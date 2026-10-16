using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public class UnlinkNintendoServiceAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F1D RID: 3869
		public Dictionary<string, string> CustomTags;
	}
}
