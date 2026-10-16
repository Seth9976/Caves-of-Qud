using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B8 RID: 184
	[CallbackIdentity(3403)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateItemResult_t
	{
		// Token: 0x040002AE RID: 686
		public const int k_iCallback = 3403;

		// Token: 0x040002AF RID: 687
		public EResult m_eResult;

		// Token: 0x040002B0 RID: 688
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002B1 RID: 689
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
