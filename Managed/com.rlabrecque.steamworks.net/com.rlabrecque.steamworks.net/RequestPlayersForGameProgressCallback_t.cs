using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000074 RID: 116
	[CallbackIdentity(5211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameProgressCallback_t
	{
		// Token: 0x040001C4 RID: 452
		public const int k_iCallback = 5211;

		// Token: 0x040001C5 RID: 453
		public EResult m_eResult;

		// Token: 0x040001C6 RID: 454
		public ulong m_ullSearchID;
	}
}
