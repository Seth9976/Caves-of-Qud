using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000ED RID: 237
	[CallbackIdentity(4611)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000046B7 File Offset: 0x000028B7
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000046C4 File Offset: 0x000028C4
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x04000367 RID: 871
		public const int k_iCallback = 4611;

		// Token: 0x04000368 RID: 872
		public EResult m_eResult;

		// Token: 0x04000369 RID: 873
		public AppId_t m_unVideoAppID;

		// Token: 0x0400036A RID: 874
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchURL_;
	}
}
