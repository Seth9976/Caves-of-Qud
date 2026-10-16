using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public class RolloverContainerRegistryCredentialsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000580 RID: 1408
		public Dictionary<string, string> CustomTags;
	}
}
