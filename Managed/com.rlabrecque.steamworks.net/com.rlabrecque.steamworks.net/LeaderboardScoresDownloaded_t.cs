using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DC RID: 220
	[CallbackIdentity(1105)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x04000337 RID: 823
		public const int k_iCallback = 1105;

		// Token: 0x04000338 RID: 824
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x04000339 RID: 825
		public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		// Token: 0x0400033A RID: 826
		public int m_cEntryCount;
	}
}
