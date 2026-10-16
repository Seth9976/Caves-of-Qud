using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001B RID: 27
	[CallbackIdentity(1021)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000043C1 File Offset: 0x000025C1
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x000043CE File Offset: 0x000025CE
		public string m_rgchKey
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchKey_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchKey_, 240);
			}
		}

		// Token: 0x0400005A RID: 90
		public const int k_iCallback = 1021;

		// Token: 0x0400005B RID: 91
		public EResult m_eResult;

		// Token: 0x0400005C RID: 92
		public uint m_nAppID;

		// Token: 0x0400005D RID: 93
		public uint m_cchKeyLength;

		// Token: 0x0400005E RID: 94
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)]
		private byte[] m_rgchKey_;
	}
}
