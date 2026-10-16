using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A7 RID: 167
	[CallbackIdentity(1321)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x04000271 RID: 625
		public const int k_iCallback = 1321;

		// Token: 0x04000272 RID: 626
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000273 RID: 627
		public AppId_t m_nAppID;
	}
}
