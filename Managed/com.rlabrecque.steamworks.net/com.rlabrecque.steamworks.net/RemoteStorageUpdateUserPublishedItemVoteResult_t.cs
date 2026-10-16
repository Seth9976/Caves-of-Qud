using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AA RID: 170
	[CallbackIdentity(1324)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x0400027A RID: 634
		public const int k_iCallback = 1324;

		// Token: 0x0400027B RID: 635
		public EResult m_eResult;

		// Token: 0x0400027C RID: 636
		public PublishedFileId_t m_nPublishedFileId;
	}
}
