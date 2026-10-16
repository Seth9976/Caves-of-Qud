using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008D RID: 141
	[CallbackIdentity(4013)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x040001FF RID: 511
		public const int k_iCallback = 4013;

		// Token: 0x04000200 RID: 512
		public int nID;
	}
}
