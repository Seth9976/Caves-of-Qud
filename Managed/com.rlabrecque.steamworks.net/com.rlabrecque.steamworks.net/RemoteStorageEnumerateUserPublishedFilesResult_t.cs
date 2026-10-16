using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009E RID: 158
	[CallbackIdentity(1312)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x04000230 RID: 560
		public const int k_iCallback = 1312;

		// Token: 0x04000231 RID: 561
		public EResult m_eResult;

		// Token: 0x04000232 RID: 562
		public int m_nResultsReturned;

		// Token: 0x04000233 RID: 563
		public int m_nTotalResultCount;

		// Token: 0x04000234 RID: 564
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
