using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class GameServerRegionsResult : PlayFabResultCommon
	{
		// Token: 0x04000BC5 RID: 3013
		public List<RegionInfo> Regions;
	}
}
