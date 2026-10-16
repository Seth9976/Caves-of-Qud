using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BA RID: 186
	[CallbackIdentity(3405)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ItemInstalled_t
	{
		// Token: 0x040002B6 RID: 694
		public const int k_iCallback = 3405;

		// Token: 0x040002B7 RID: 695
		public AppId_t m_unAppID;

		// Token: 0x040002B8 RID: 696
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002B9 RID: 697
		public UGCHandle_t m_hLegacyContent;

		// Token: 0x040002BA RID: 698
		public ulong m_unManifestID;
	}
}
