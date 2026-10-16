using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000061 RID: 97
	[CallbackIdentity(4700)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryResultReady_t
	{
		// Token: 0x04000175 RID: 373
		public const int k_iCallback = 4700;

		// Token: 0x04000176 RID: 374
		public SteamInventoryResult_t m_handle;

		// Token: 0x04000177 RID: 375
		public EResult m_result;
	}
}
