using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AE RID: 174
	[CallbackIdentity(1328)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x0400028A RID: 650
		public const int k_iCallback = 1328;

		// Token: 0x0400028B RID: 651
		public EResult m_eResult;

		// Token: 0x0400028C RID: 652
		public EWorkshopFileAction m_eAction;

		// Token: 0x0400028D RID: 653
		public int m_nResultsReturned;

		// Token: 0x0400028E RID: 654
		public int m_nTotalResultCount;

		// Token: 0x0400028F RID: 655
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000290 RID: 656
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeUpdated;
	}
}
