using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000091 RID: 145
	[CallbackIdentity(1201)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SocketStatusCallback_t
	{
		// Token: 0x04000208 RID: 520
		public const int k_iCallback = 1201;

		// Token: 0x04000209 RID: 521
		public SNetSocket_t m_hSocket;

		// Token: 0x0400020A RID: 522
		public SNetListenSocket_t m_hListenSocket;

		// Token: 0x0400020B RID: 523
		public CSteamID m_steamIDRemote;

		// Token: 0x0400020C RID: 524
		public int m_eSNetSocketState;
	}
}
