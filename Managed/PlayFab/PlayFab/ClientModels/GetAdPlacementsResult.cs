using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public class GetAdPlacementsResult : PlayFabResultCommon
	{
		// Token: 0x04000BD1 RID: 3025
		public List<AdPlacementDetails> AdPlacements;
	}
}
