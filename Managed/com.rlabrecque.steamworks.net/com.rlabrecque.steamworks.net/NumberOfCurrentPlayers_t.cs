using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DE RID: 222
	[CallbackIdentity(1107)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NumberOfCurrentPlayers_t
	{
		// Token: 0x04000342 RID: 834
		public const int k_iCallback = 1107;

		// Token: 0x04000343 RID: 835
		public byte m_bSuccess;

		// Token: 0x04000344 RID: 836
		public int m_cPlayers;
	}
}
