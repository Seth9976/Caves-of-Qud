using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B9 RID: 185
	[CallbackIdentity(3404)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SubmitItemUpdateResult_t
	{
		// Token: 0x040002B2 RID: 690
		public const int k_iCallback = 3404;

		// Token: 0x040002B3 RID: 691
		public EResult m_eResult;

		// Token: 0x040002B4 RID: 692
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x040002B5 RID: 693
		public PublishedFileId_t m_nPublishedFileId;
	}
}
