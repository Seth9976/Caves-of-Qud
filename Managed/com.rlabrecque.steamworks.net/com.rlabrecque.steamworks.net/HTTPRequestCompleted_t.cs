using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005A RID: 90
	[CallbackIdentity(2101)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestCompleted_t
	{
		// Token: 0x04000155 RID: 341
		public const int k_iCallback = 2101;

		// Token: 0x04000156 RID: 342
		public HTTPRequestHandle m_hRequest;

		// Token: 0x04000157 RID: 343
		public ulong m_ulContextValue;

		// Token: 0x04000158 RID: 344
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bRequestSuccessful;

		// Token: 0x04000159 RID: 345
		public EHTTPStatusCode m_eStatusCode;

		// Token: 0x0400015A RID: 346
		public uint m_unBodySize;
	}
}
