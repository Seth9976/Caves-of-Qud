using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003C RID: 60
	[CallbackIdentity(208)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GSClientGroupStatus_t
	{
		// Token: 0x040000D0 RID: 208
		public const int k_iCallback = 208;

		// Token: 0x040000D1 RID: 209
		public CSteamID m_SteamIDUser;

		// Token: 0x040000D2 RID: 210
		public CSteamID m_SteamIDGroup;

		// Token: 0x040000D3 RID: 211
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bMember;

		// Token: 0x040000D4 RID: 212
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bOfficer;
	}
}
