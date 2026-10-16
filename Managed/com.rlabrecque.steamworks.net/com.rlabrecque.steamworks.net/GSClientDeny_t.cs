using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000037 RID: 55
	[CallbackIdentity(202)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientDeny_t
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00004468 File Offset: 0x00002668
		public string m_rgchOptionalText
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchOptionalText_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchOptionalText_, 128);
			}
		}

		// Token: 0x040000BE RID: 190
		public const int k_iCallback = 202;

		// Token: 0x040000BF RID: 191
		public CSteamID m_SteamID;

		// Token: 0x040000C0 RID: 192
		public EDenyReason m_eDenyReason;

		// Token: 0x040000C1 RID: 193
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchOptionalText_;
	}
}
