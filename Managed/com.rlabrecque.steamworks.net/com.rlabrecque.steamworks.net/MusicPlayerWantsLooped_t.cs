using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008A RID: 138
	[CallbackIdentity(4110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsLooped_t
	{
		// Token: 0x040001F9 RID: 505
		public const int k_iCallback = 4110;

		// Token: 0x040001FA RID: 506
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLooped;
	}
}
