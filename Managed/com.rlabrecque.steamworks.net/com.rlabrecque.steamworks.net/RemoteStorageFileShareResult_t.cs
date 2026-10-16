using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009B RID: 155
	[CallbackIdentity(1307)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00004537 File Offset: 0x00002737
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00004544 File Offset: 0x00002744
		public string m_rgchFilename
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchFilename_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchFilename_, 260);
			}
		}

		// Token: 0x04000225 RID: 549
		public const int k_iCallback = 1307;

		// Token: 0x04000226 RID: 550
		public EResult m_eResult;

		// Token: 0x04000227 RID: 551
		public UGCHandle_t m_hFile;

		// Token: 0x04000228 RID: 552
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		private byte[] m_rgchFilename_;
	}
}
