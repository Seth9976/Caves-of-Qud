using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C4 RID: 196
	[CallbackIdentity(3415)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveAppDependencyResult_t
	{
		// Token: 0x040002DD RID: 733
		public const int k_iCallback = 3415;

		// Token: 0x040002DE RID: 734
		public EResult m_eResult;

		// Token: 0x040002DF RID: 735
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002E0 RID: 736
		public AppId_t m_nAppID;
	}
}
