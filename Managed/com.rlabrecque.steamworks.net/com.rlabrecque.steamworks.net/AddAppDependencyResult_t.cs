using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C3 RID: 195
	[CallbackIdentity(3414)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddAppDependencyResult_t
	{
		// Token: 0x040002D9 RID: 729
		public const int k_iCallback = 3414;

		// Token: 0x040002DA RID: 730
		public EResult m_eResult;

		// Token: 0x040002DB RID: 731
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002DC RID: 732
		public AppId_t m_nAppID;
	}
}
