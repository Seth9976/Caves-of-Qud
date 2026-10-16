using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000078 RID: 120
	[CallbackIdentity(5215)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EndGameResultCallback_t
	{
		// Token: 0x040001DA RID: 474
		public const int k_iCallback = 5215;

		// Token: 0x040001DB RID: 475
		public EResult m_eResult;

		// Token: 0x040001DC RID: 476
		public ulong ullUniqueGameID;
	}
}
