using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000090 RID: 144
	[CallbackIdentity(1203)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct P2PSessionConnectFail_t
	{
		// Token: 0x04000205 RID: 517
		public const int k_iCallback = 1203;

		// Token: 0x04000206 RID: 518
		public CSteamID m_steamIDRemote;

		// Token: 0x04000207 RID: 519
		public byte m_eP2PSessionError;
	}
}
