using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D4 RID: 212
	[CallbackIdentity(165)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00004657 File Offset: 0x00002857
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00004664 File Offset: 0x00002864
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 512);
			}
		}

		// Token: 0x04000311 RID: 785
		public const int k_iCallback = 165;

		// Token: 0x04000312 RID: 786
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		private byte[] m_szURL_;
	}
}
