using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000062 RID: 98
	[CallbackIdentity(4701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryFullUpdate_t
	{
		// Token: 0x04000178 RID: 376
		public const int k_iCallback = 4701;

		// Token: 0x04000179 RID: 377
		public SteamInventoryResult_t m_handle;
	}
}
