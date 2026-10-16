using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000020 RID: 32
	[CallbackIdentity(332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000043E1 File Offset: 0x000025E1
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000043EE File Offset: 0x000025EE
		public string m_rgchServer
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchServer_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchServer_, 64);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000043FE File Offset: 0x000025FE
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000440B File Offset: 0x0000260B
		public string m_rgchPassword
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchPassword_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchPassword_, 64);
			}
		}

		// Token: 0x04000071 RID: 113
		public const int k_iCallback = 332;

		// Token: 0x04000072 RID: 114
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchServer_;

		// Token: 0x04000073 RID: 115
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_rgchPassword_;
	}
}
