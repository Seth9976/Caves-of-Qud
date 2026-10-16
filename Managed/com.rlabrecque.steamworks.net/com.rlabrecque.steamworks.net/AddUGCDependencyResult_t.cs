using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C1 RID: 193
	[CallbackIdentity(3412)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddUGCDependencyResult_t
	{
		// Token: 0x040002D1 RID: 721
		public const int k_iCallback = 3412;

		// Token: 0x040002D2 RID: 722
		public EResult m_eResult;

		// Token: 0x040002D3 RID: 723
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002D4 RID: 724
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
