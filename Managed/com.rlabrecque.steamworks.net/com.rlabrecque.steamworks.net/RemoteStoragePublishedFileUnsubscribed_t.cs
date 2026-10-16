using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A8 RID: 168
	[CallbackIdentity(1322)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x04000274 RID: 628
		public const int k_iCallback = 1322;

		// Token: 0x04000275 RID: 629
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000276 RID: 630
		public AppId_t m_nAppID;
	}
}
