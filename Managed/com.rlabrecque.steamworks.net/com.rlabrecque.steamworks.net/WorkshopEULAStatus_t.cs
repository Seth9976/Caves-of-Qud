using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C8 RID: 200
	[CallbackIdentity(3420)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct WorkshopEULAStatus_t
	{
		// Token: 0x040002EC RID: 748
		public const int k_iCallback = 3420;

		// Token: 0x040002ED RID: 749
		public EResult m_eResult;

		// Token: 0x040002EE RID: 750
		public AppId_t m_nAppID;

		// Token: 0x040002EF RID: 751
		public uint m_unVersion;

		// Token: 0x040002F0 RID: 752
		public RTime32 m_rtAction;

		// Token: 0x040002F1 RID: 753
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAccepted;

		// Token: 0x040002F2 RID: 754
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bNeedsAction;
	}
}
