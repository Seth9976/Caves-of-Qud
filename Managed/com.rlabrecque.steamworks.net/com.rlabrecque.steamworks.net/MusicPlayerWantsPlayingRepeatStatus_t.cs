using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008E RID: 142
	[CallbackIdentity(4114)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x04000201 RID: 513
		public const int k_iCallback = 4114;

		// Token: 0x04000202 RID: 514
		public int m_nPlayingRepeatStatus;
	}
}
