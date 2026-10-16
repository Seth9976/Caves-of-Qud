using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C6 RID: 198
	[CallbackIdentity(3417)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DeleteItemResult_t
	{
		// Token: 0x040002E7 RID: 743
		public const int k_iCallback = 3417;

		// Token: 0x040002E8 RID: 744
		public EResult m_eResult;

		// Token: 0x040002E9 RID: 745
		public PublishedFileId_t m_nPublishedFileId;
	}
}
