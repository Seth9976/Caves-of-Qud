using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C5 RID: 197
	[CallbackIdentity(3416)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAppDependenciesResult_t
	{
		// Token: 0x040002E1 RID: 737
		public const int k_iCallback = 3416;

		// Token: 0x040002E2 RID: 738
		public EResult m_eResult;

		// Token: 0x040002E3 RID: 739
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040002E4 RID: 740
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public AppId_t[] m_rgAppIDs;

		// Token: 0x040002E5 RID: 741
		public uint m_nNumAppDependencies;

		// Token: 0x040002E6 RID: 742
		public uint m_nTotalNumAppDependencies;
	}
}
