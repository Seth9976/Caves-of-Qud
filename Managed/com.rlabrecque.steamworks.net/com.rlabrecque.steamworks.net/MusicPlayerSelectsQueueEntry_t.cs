using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008C RID: 140
	[CallbackIdentity(4012)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x040001FD RID: 509
		public const int k_iCallback = 4012;

		// Token: 0x040001FE RID: 510
		public int nID;
	}
}
