using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A3 RID: 163
	[CallbackIdentity(1317)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00004557 File Offset: 0x00002757
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00004564 File Offset: 0x00002764
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x04000245 RID: 581
		public const int k_iCallback = 1317;

		// Token: 0x04000246 RID: 582
		public EResult m_eResult;

		// Token: 0x04000247 RID: 583
		public UGCHandle_t m_hFile;

		// Token: 0x04000248 RID: 584
		public AppId_t m_nAppID;

		// Token: 0x04000249 RID: 585
		public int m_nSizeInBytes;

		// Token: 0x0400024A RID: 586
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x0400024B RID: 587
		public ulong m_ulSteamIDOwner;
	}
}
