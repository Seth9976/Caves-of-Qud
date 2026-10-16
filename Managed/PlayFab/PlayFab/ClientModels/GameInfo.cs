using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public class GameInfo : PlayFabBaseModel
	{
		// Token: 0x04000BB0 RID: 2992
		public string BuildVersion;

		// Token: 0x04000BB1 RID: 2993
		public string GameMode;

		// Token: 0x04000BB2 RID: 2994
		public string GameServerData;

		// Token: 0x04000BB3 RID: 2995
		public GameInstanceState? GameServerStateEnum;

		// Token: 0x04000BB4 RID: 2996
		public DateTime? LastHeartbeat;

		// Token: 0x04000BB5 RID: 2997
		public string LobbyID;

		// Token: 0x04000BB6 RID: 2998
		public int? MaxPlayers;

		// Token: 0x04000BB7 RID: 2999
		public List<string> PlayerUserIds;

		// Token: 0x04000BB8 RID: 3000
		public Region? Region;

		// Token: 0x04000BB9 RID: 3001
		public uint RunTime;

		// Token: 0x04000BBA RID: 3002
		public string ServerIPV4Address;

		// Token: 0x04000BBB RID: 3003
		public string ServerIPV6Address;

		// Token: 0x04000BBC RID: 3004
		public int? ServerPort;

		// Token: 0x04000BBD RID: 3005
		public string ServerPublicDNSName;

		// Token: 0x04000BBE RID: 3006
		public string StatisticName;

		// Token: 0x04000BBF RID: 3007
		public Dictionary<string, string> Tags;
	}
}
