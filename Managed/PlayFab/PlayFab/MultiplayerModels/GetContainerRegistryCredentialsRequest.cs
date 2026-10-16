using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public class GetContainerRegistryCredentialsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000489 RID: 1161
		public Dictionary<string, string> CustomTags;
	}
}
