using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000065 RID: 101
	[CallbackIdentity(4704)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryStartPurchaseResult_t
	{
		// Token: 0x04000180 RID: 384
		public const int k_iCallback = 4704;

		// Token: 0x04000181 RID: 385
		public EResult m_result;

		// Token: 0x04000182 RID: 386
		public ulong m_ulOrderID;

		// Token: 0x04000183 RID: 387
		public ulong m_ulTransID;
	}
}
