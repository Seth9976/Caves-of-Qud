using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000031 RID: 49
	[CallbackIdentity(349)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct OverlayBrowserProtocolNavigation_t
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000443B File Offset: 0x0000263B
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00004448 File Offset: 0x00002648
		public string rgchURI
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.rgchURI_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.rgchURI_, 1024);
			}
		}

		// Token: 0x040000AC RID: 172
		public const int k_iCallback = 349;

		// Token: 0x040000AD RID: 173
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		private byte[] rgchURI_;
	}
}
