using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D6 RID: 214
	[CallbackIdentity(167)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DurationControl_t
	{
		// Token: 0x04000319 RID: 793
		public const int k_iCallback = 167;

		// Token: 0x0400031A RID: 794
		public EResult m_eResult;

		// Token: 0x0400031B RID: 795
		public AppId_t m_appid;

		// Token: 0x0400031C RID: 796
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bApplicable;

		// Token: 0x0400031D RID: 797
		public int m_csecsLast5h;

		// Token: 0x0400031E RID: 798
		public EDurationControlProgress m_progress;

		// Token: 0x0400031F RID: 799
		public EDurationControlNotification m_notification;

		// Token: 0x04000320 RID: 800
		public int m_csecsToday;

		// Token: 0x04000321 RID: 801
		public int m_csecsRemaining;
	}
}
