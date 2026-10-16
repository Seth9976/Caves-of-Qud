using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000089 RID: 137
	[CallbackIdentity(4109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x040001F7 RID: 503
		public const int k_iCallback = 4109;

		// Token: 0x040001F8 RID: 504
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bShuffled;
	}
}
