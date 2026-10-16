using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016C RID: 364
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionInfo_t
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00004787 File Offset: 0x00002987
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00004794 File Offset: 0x00002994
		public string m_szEndDebug
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szEndDebug_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szEndDebug_, 128);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000047A7 File Offset: 0x000029A7
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000047B4 File Offset: 0x000029B4
		public string m_szConnectionDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectionDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectionDescription_, 128);
			}
		}

		// Token: 0x04000A7B RID: 2683
		public SteamNetworkingIdentity m_identityRemote;

		// Token: 0x04000A7C RID: 2684
		public long m_nUserData;

		// Token: 0x04000A7D RID: 2685
		public HSteamListenSocket m_hListenSocket;

		// Token: 0x04000A7E RID: 2686
		public SteamNetworkingIPAddr m_addrRemote;

		// Token: 0x04000A7F RID: 2687
		public ushort m__pad1;

		// Token: 0x04000A80 RID: 2688
		public SteamNetworkingPOPID m_idPOPRemote;

		// Token: 0x04000A81 RID: 2689
		public SteamNetworkingPOPID m_idPOPRelay;

		// Token: 0x04000A82 RID: 2690
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A83 RID: 2691
		public int m_eEndReason;

		// Token: 0x04000A84 RID: 2692
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szEndDebug_;

		// Token: 0x04000A85 RID: 2693
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szConnectionDescription_;

		// Token: 0x04000A86 RID: 2694
		public int m_nFlags;

		// Token: 0x04000A87 RID: 2695
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
		public uint[] reserved;
	}
}
