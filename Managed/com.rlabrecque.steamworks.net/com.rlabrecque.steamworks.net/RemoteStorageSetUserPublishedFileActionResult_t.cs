using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AD RID: 173
	[CallbackIdentity(1327)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x04000286 RID: 646
		public const int k_iCallback = 1327;

		// Token: 0x04000287 RID: 647
		public EResult m_eResult;

		// Token: 0x04000288 RID: 648
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000289 RID: 649
		public EWorkshopFileAction m_eAction;
	}
}
