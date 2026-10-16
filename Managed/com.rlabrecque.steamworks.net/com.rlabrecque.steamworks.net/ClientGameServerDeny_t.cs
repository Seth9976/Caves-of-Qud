using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CC RID: 204
	[CallbackIdentity(113)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClientGameServerDeny_t
	{
		// Token: 0x040002F9 RID: 761
		public const int k_iCallback = 113;

		// Token: 0x040002FA RID: 762
		public uint m_uAppID;

		// Token: 0x040002FB RID: 763
		public uint m_unGameServerIP;

		// Token: 0x040002FC RID: 764
		public ushort m_usGameServerPort;

		// Token: 0x040002FD RID: 765
		public ushort m_bSecure;

		// Token: 0x040002FE RID: 766
		public uint m_uReason;
	}
}
