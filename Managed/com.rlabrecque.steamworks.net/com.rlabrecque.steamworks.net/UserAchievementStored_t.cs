using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DA RID: 218
	[CallbackIdentity(1103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored_t
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00004677 File Offset: 0x00002877
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00004684 File Offset: 0x00002884
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

		// Token: 0x0400032E RID: 814
		public const int k_iCallback = 1103;

		// Token: 0x0400032F RID: 815
		public ulong m_nGameID;

		// Token: 0x04000330 RID: 816
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement;

		// Token: 0x04000331 RID: 817
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x04000332 RID: 818
		public uint m_nCurProgress;

		// Token: 0x04000333 RID: 819
		public uint m_nMaxProgress;
	}
}
