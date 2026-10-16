using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000095 RID: 149
	[CallbackIdentity(1222)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetAuthenticationStatus_t
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000044D7 File Offset: 0x000026D7
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000044E4 File Offset: 0x000026E4
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

		// Token: 0x04000215 RID: 533
		public const int k_iCallback = 1222;

		// Token: 0x04000216 RID: 534
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x04000217 RID: 535
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
