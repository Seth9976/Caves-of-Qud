using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000034 RID: 52
	[CallbackIdentity(1701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GCMessageAvailable_t
	{
		// Token: 0x040000B8 RID: 184
		public const int k_iCallback = 1701;

		// Token: 0x040000B9 RID: 185
		public uint m_nMessageSize;
	}
}
