using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000096 RID: 150
	[CallbackIdentity(1281)]
	public struct SteamRelayNetworkStatus_t
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000044F7 File Offset: 0x000026F7
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00004504 File Offset: 0x00002704
		public string m_debugMsg
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_debugMsg_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_debugMsg_, 256);
			}
		}

		// Token: 0x04000218 RID: 536
		public const int k_iCallback = 1281;

		// Token: 0x04000219 RID: 537
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x0400021A RID: 538
		public int m_bPingMeasurementInProgress;

		// Token: 0x0400021B RID: 539
		public ESteamNetworkingAvailability m_eAvailNetworkConfig;

		// Token: 0x0400021C RID: 540
		public ESteamNetworkingAvailability m_eAvailAnyRelay;

		// Token: 0x0400021D RID: 541
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
