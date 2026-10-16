using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008F RID: 143
	[CallbackIdentity(1202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionRequest_t
	{
		// Token: 0x04000203 RID: 515
		public const int k_iCallback = 1202;

		// Token: 0x04000204 RID: 516
		public CSteamID m_steamIDRemote;
	}
}
