using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C2 RID: 194
	[CallbackIdentity(3413)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveUGCDependencyResult_t
	{
		// Token: 0x040002D5 RID: 725
		public const int k_iCallback = 3413;

		// Token: 0x040002D6 RID: 726
		public EResult m_eResult;

		// Token: 0x040002D7 RID: 727
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002D8 RID: 728
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
