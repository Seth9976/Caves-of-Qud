using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005C RID: 92
	[CallbackIdentity(2103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestDataReceived_t
	{
		// Token: 0x0400015E RID: 350
		public const int k_iCallback = 2103;

		// Token: 0x0400015F RID: 351
		public HTTPRequestHandle m_hRequest;

		// Token: 0x04000160 RID: 352
		public ulong m_ulContextValue;

		// Token: 0x04000161 RID: 353
		public uint m_cOffset;

		// Token: 0x04000162 RID: 354
		public uint m_cBytesReceived;
	}
}
