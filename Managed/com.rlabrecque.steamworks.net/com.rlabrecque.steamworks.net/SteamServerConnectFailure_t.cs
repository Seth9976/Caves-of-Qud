using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CA RID: 202
	[CallbackIdentity(102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServerConnectFailure_t
	{
		// Token: 0x040002F4 RID: 756
		public const int k_iCallback = 102;

		// Token: 0x040002F5 RID: 757
		public EResult m_eResult;

		// Token: 0x040002F6 RID: 758
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bStillRetrying;
	}
}
