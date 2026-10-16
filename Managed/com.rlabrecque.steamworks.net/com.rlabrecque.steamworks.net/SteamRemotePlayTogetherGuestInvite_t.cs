using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009A RID: 154
	[CallbackIdentity(5703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamRemotePlayTogetherGuestInvite_t
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00004517 File Offset: 0x00002717
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00004524 File Offset: 0x00002724
		public string m_szConnectURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectURL_, 1024);
			}
		}

		// Token: 0x04000223 RID: 547
		public const int k_iCallback = 5703;

		// Token: 0x04000224 RID: 548
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] m_szConnectURL_;
	}
}
