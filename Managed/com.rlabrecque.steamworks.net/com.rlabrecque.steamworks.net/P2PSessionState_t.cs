using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000166 RID: 358
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionState_t
	{
		// Token: 0x04000A4B RID: 2635
		public byte m_bConnectionActive;

		// Token: 0x04000A4C RID: 2636
		public byte m_bConnecting;

		// Token: 0x04000A4D RID: 2637
		public byte m_eP2PSessionError;

		// Token: 0x04000A4E RID: 2638
		public byte m_bUsingRelay;

		// Token: 0x04000A4F RID: 2639
		public int m_nBytesQueuedForSend;

		// Token: 0x04000A50 RID: 2640
		public int m_nPacketsQueuedForSend;

		// Token: 0x04000A51 RID: 2641
		public uint m_nRemoteIP;

		// Token: 0x04000A52 RID: 2642
		public ushort m_nRemotePort;
	}
}
