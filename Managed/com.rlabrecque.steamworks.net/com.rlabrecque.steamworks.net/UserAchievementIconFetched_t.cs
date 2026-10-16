using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E0 RID: 224
	[CallbackIdentity(1109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementIconFetched_t
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00004697 File Offset: 0x00002897
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000046A4 File Offset: 0x000028A4
		public string m_rgchAchievementName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchAchievementName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchAchievementName_, 128);
			}
		}

		// Token: 0x04000347 RID: 839
		public const int k_iCallback = 1109;

		// Token: 0x04000348 RID: 840
		public CGameID m_nGameID;

		// Token: 0x04000349 RID: 841
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x0400034A RID: 842
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAchieved;

		// Token: 0x0400034B RID: 843
		public int m_nIconHandle;
	}
}
