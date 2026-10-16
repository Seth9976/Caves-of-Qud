using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E2 RID: 226
	[CallbackIdentity(1111)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardUGCSet_t
	{
		// Token: 0x0400034F RID: 847
		public const int k_iCallback = 1111;

		// Token: 0x04000350 RID: 848
		public EResult m_eResult;

		// Token: 0x04000351 RID: 849
		public SteamLeaderboard_t m_hSteamLeaderboard;
	}
}
