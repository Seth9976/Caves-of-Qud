using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016E RID: 366
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeLaneStatus_t
	{
		// Token: 0x04000A96 RID: 2710
		public int m_cbPendingUnreliable;

		// Token: 0x04000A97 RID: 2711
		public int m_cbPendingReliable;

		// Token: 0x04000A98 RID: 2712
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A99 RID: 2713
		public int _reservePad1;

		// Token: 0x04000A9A RID: 2714
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A9B RID: 2715
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public uint[] reserved;
	}
}
