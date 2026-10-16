using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A2 RID: 162
	[CallbackIdentity(1316)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x04000241 RID: 577
		public const int k_iCallback = 1316;

		// Token: 0x04000242 RID: 578
		public EResult m_eResult;

		// Token: 0x04000243 RID: 579
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000244 RID: 580
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
