using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E6 RID: 230
	[CallbackIdentity(703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICallCompleted_t
	{
		// Token: 0x04000358 RID: 856
		public const int k_iCallback = 703;

		// Token: 0x04000359 RID: 857
		public SteamAPICall_t m_hAsyncCall;

		// Token: 0x0400035A RID: 858
		public int m_iCallback;

		// Token: 0x0400035B RID: 859
		public uint m_cubParam;
	}
}
