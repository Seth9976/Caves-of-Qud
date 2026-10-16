using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000039 RID: 57
	[CallbackIdentity(206)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientAchievementStatus_t
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000447B File Offset: 0x0000267B
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00004488 File Offset: 0x00002688
		public string m_pchAchievement
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchAchievement_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchAchievement_, 128);
			}
		}

		// Token: 0x040000C5 RID: 197
		public const int k_iCallback = 206;

		// Token: 0x040000C6 RID: 198
		public ulong m_SteamID;

		// Token: 0x040000C7 RID: 199
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_pchAchievement_;

		// Token: 0x040000C8 RID: 200
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUnlocked;
	}
}
