using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntry_t
	{
		// Token: 0x04000A70 RID: 2672
		public CSteamID m_steamIDUser;

		// Token: 0x04000A71 RID: 2673
		public int m_nGlobalRank;

		// Token: 0x04000A72 RID: 2674
		public int m_nScore;

		// Token: 0x04000A73 RID: 2675
		public int m_cDetails;

		// Token: 0x04000A74 RID: 2676
		public UGCHandle_t m_hUGC;
	}
}
