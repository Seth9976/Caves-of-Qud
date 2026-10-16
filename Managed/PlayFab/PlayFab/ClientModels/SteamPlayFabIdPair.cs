using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class SteamPlayFabIdPair : PlayFabBaseModel
	{
		// Token: 0x04000EBA RID: 3770
		public string PlayFabId;

		// Token: 0x04000EBB RID: 3771
		public string SteamStringId;
	}
}
