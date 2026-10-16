using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class BuildRegion : PlayFabBaseModel
	{
		// Token: 0x0400038A RID: 906
		public CurrentServerStats CurrentServerStats;

		// Token: 0x0400038B RID: 907
		public DynamicStandbySettings DynamicStandbySettings;

		// Token: 0x0400038C RID: 908
		public int MaxServers;

		// Token: 0x0400038D RID: 909
		public string Region;

		// Token: 0x0400038E RID: 910
		public int StandbyServers;

		// Token: 0x0400038F RID: 911
		public string Status;
	}
}
