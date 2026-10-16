using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public class BuildRegionParams : PlayFabBaseModel
	{
		// Token: 0x04000390 RID: 912
		public DynamicStandbySettings DynamicStandbySettings;

		// Token: 0x04000391 RID: 913
		public int MaxServers;

		// Token: 0x04000392 RID: 914
		public string Region;

		// Token: 0x04000393 RID: 915
		public int StandbyServers;
	}
}
