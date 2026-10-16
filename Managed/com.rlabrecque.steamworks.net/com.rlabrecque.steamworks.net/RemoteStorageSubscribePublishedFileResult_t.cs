using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009F RID: 159
	[CallbackIdentity(1313)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x04000235 RID: 565
		public const int k_iCallback = 1313;

		// Token: 0x04000236 RID: 566
		public EResult m_eResult;

		// Token: 0x04000237 RID: 567
		public PublishedFileId_t m_nPublishedFileId;
	}
}
