using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005B RID: 91
	[CallbackIdentity(2102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x0400015B RID: 347
		public const int k_iCallback = 2102;

		// Token: 0x0400015C RID: 348
		public HTTPRequestHandle m_hRequest;

		// Token: 0x0400015D RID: 349
		public ulong m_ulContextValue;
	}
}
