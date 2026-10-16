using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009C RID: 156
	[CallbackIdentity(1309)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x04000229 RID: 553
		public const int k_iCallback = 1309;

		// Token: 0x0400022A RID: 554
		public EResult m_eResult;

		// Token: 0x0400022B RID: 555
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400022C RID: 556
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
