using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009D RID: 157
	[CallbackIdentity(1311)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		// Token: 0x0400022D RID: 557
		public const int k_iCallback = 1311;

		// Token: 0x0400022E RID: 558
		public EResult m_eResult;

		// Token: 0x0400022F RID: 559
		public PublishedFileId_t m_nPublishedFileId;
	}
}
