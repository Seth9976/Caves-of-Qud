using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class GetGlobalPolicyRequest : PlayFabRequestCommon
	{
		// Token: 0x04000322 RID: 802
		public Dictionary<string, string> CustomTags;
	}
}
