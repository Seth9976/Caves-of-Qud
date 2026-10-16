using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D0 RID: 208
	[CallbackIdentity(152)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x04000306 RID: 774
		public const int k_iCallback = 152;

		// Token: 0x04000307 RID: 775
		public uint m_unAppID;

		// Token: 0x04000308 RID: 776
		public ulong m_ulOrderID;

		// Token: 0x04000309 RID: 777
		public byte m_bAuthorized;
	}
}
