using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B0 RID: 176
	[CallbackIdentity(1330)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x04000294 RID: 660
		public const int k_iCallback = 1330;

		// Token: 0x04000295 RID: 661
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000296 RID: 662
		public AppId_t m_nAppID;

		// Token: 0x04000297 RID: 663
		public ulong m_ulUnused;
	}
}
