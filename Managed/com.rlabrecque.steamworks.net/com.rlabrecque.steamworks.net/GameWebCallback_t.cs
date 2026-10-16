using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D3 RID: 211
	[CallbackIdentity(164)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameWebCallback_t
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00004637 File Offset: 0x00002837
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00004644 File Offset: 0x00002844
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 256);
			}
		}

		// Token: 0x0400030F RID: 783
		public const int k_iCallback = 164;

		// Token: 0x04000310 RID: 784
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_szURL_;
	}
}
