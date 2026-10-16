using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BC RID: 188
	[CallbackIdentity(3407)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x040002BF RID: 703
		public const int k_iCallback = 3407;

		// Token: 0x040002C0 RID: 704
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002C1 RID: 705
		public EResult m_eResult;

		// Token: 0x040002C2 RID: 706
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bWasAddRequest;
	}
}
