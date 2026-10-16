using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AC RID: 172
	[CallbackIdentity(1326)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x04000281 RID: 641
		public const int k_iCallback = 1326;

		// Token: 0x04000282 RID: 642
		public EResult m_eResult;

		// Token: 0x04000283 RID: 643
		public int m_nResultsReturned;

		// Token: 0x04000284 RID: 644
		public int m_nTotalResultCount;

		// Token: 0x04000285 RID: 645
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
