using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B7 RID: 183
	[CallbackIdentity(3402)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x040002AB RID: 683
		public const int k_iCallback = 3402;

		// Token: 0x040002AC RID: 684
		public SteamUGCDetails_t m_details;

		// Token: 0x040002AD RID: 685
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
