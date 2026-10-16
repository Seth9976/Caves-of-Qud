using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A6 RID: 166
	[CallbackIdentity(1320)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x0400026A RID: 618
		public const int k_iCallback = 1320;

		// Token: 0x0400026B RID: 619
		public EResult m_eResult;

		// Token: 0x0400026C RID: 620
		public PublishedFileId_t m_unPublishedFileId;

		// Token: 0x0400026D RID: 621
		public int m_nVotesFor;

		// Token: 0x0400026E RID: 622
		public int m_nVotesAgainst;

		// Token: 0x0400026F RID: 623
		public int m_nReports;

		// Token: 0x04000270 RID: 624
		public float m_fScore;
	}
}
