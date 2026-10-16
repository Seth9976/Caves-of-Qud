using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public class UnlinkKongregateAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F1C RID: 3868
		public Dictionary<string, string> CustomTags;
	}
}
