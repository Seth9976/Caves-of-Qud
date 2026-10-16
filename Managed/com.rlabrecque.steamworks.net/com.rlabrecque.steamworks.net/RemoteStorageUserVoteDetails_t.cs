using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AB RID: 171
	[CallbackIdentity(1325)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x0400027D RID: 637
		public const int k_iCallback = 1325;

		// Token: 0x0400027E RID: 638
		public EResult m_eResult;

		// Token: 0x0400027F RID: 639
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000280 RID: 640
		public EWorkshopVote m_eVote;
	}
}
