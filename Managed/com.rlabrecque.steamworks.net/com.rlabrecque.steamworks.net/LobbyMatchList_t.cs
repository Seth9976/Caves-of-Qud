using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006E RID: 110
	[CallbackIdentity(510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyMatchList_t
	{
		// Token: 0x040001AB RID: 427
		public const int k_iCallback = 510;

		// Token: 0x040001AC RID: 428
		public uint m_nLobbiesMatching;
	}
}
