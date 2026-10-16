using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DD RID: 221
	[CallbackIdentity(1106)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoreUploaded_t
	{
		// Token: 0x0400033B RID: 827
		public const int k_iCallback = 1106;

		// Token: 0x0400033C RID: 828
		public byte m_bSuccess;

		// Token: 0x0400033D RID: 829
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x0400033E RID: 830
		public int m_nScore;

		// Token: 0x0400033F RID: 831
		public byte m_bScoreChanged;

		// Token: 0x04000340 RID: 832
		public int m_nGlobalRankNew;

		// Token: 0x04000341 RID: 833
		public int m_nGlobalRankPrevious;
	}
}
