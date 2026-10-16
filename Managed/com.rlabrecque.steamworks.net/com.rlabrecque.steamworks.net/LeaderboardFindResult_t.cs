using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DB RID: 219
	[CallbackIdentity(1104)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardFindResult_t
	{
		// Token: 0x04000334 RID: 820
		public const int k_iCallback = 1104;

		// Token: 0x04000335 RID: 821
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x04000336 RID: 822
		public byte m_bLeaderboardFound;
	}
}
