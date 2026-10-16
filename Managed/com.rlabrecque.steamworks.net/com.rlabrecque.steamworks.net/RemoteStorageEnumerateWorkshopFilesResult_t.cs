using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A5 RID: 165
	[CallbackIdentity(1319)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x04000262 RID: 610
		public const int k_iCallback = 1319;

		// Token: 0x04000263 RID: 611
		public EResult m_eResult;

		// Token: 0x04000264 RID: 612
		public int m_nResultsReturned;

		// Token: 0x04000265 RID: 613
		public int m_nTotalResultCount;

		// Token: 0x04000266 RID: 614
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000267 RID: 615
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public float[] m_rgScore;

		// Token: 0x04000268 RID: 616
		public AppId_t m_nAppId;

		// Token: 0x04000269 RID: 617
		public uint m_unStartIndex;
	}
}
