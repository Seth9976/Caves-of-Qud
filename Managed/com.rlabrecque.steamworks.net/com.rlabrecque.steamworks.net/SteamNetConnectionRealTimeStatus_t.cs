using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeStatus_t
	{
		// Token: 0x04000A88 RID: 2696
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A89 RID: 2697
		public int m_nPing;

		// Token: 0x04000A8A RID: 2698
		public float m_flConnectionQualityLocal;

		// Token: 0x04000A8B RID: 2699
		public float m_flConnectionQualityRemote;

		// Token: 0x04000A8C RID: 2700
		public float m_flOutPacketsPerSec;

		// Token: 0x04000A8D RID: 2701
		public float m_flOutBytesPerSec;

		// Token: 0x04000A8E RID: 2702
		public float m_flInPacketsPerSec;

		// Token: 0x04000A8F RID: 2703
		public float m_flInBytesPerSec;

		// Token: 0x04000A90 RID: 2704
		public int m_nSendRateBytesPerSecond;

		// Token: 0x04000A91 RID: 2705
		public int m_cbPendingUnreliable;

		// Token: 0x04000A92 RID: 2706
		public int m_cbPendingReliable;

		// Token: 0x04000A93 RID: 2707
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A94 RID: 2708
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A95 RID: 2709
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] reserved;
	}
}
