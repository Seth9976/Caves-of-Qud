using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BB RID: 187
	[CallbackIdentity(3406)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadItemResult_t
	{
		// Token: 0x040002BB RID: 699
		public const int k_iCallback = 3406;

		// Token: 0x040002BC RID: 700
		public AppId_t m_unAppID;

		// Token: 0x040002BD RID: 701
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002BE RID: 702
		public EResult m_eResult;
	}
}
