using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A1 RID: 161
	[CallbackIdentity(1315)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x0400023E RID: 574
		public const int k_iCallback = 1315;

		// Token: 0x0400023F RID: 575
		public EResult m_eResult;

		// Token: 0x04000240 RID: 576
		public PublishedFileId_t m_nPublishedFileId;
	}
}
