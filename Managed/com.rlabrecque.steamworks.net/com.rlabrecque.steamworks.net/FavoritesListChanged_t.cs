using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000067 RID: 103
	[CallbackIdentity(502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListChanged_t
	{
		// Token: 0x04000187 RID: 391
		public const int k_iCallback = 502;

		// Token: 0x04000188 RID: 392
		public uint m_nIP;

		// Token: 0x04000189 RID: 393
		public uint m_nQueryPort;

		// Token: 0x0400018A RID: 394
		public uint m_nConnPort;

		// Token: 0x0400018B RID: 395
		public uint m_nAppID;

		// Token: 0x0400018C RID: 396
		public uint m_nFlags;

		// Token: 0x0400018D RID: 397
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAdd;

		// Token: 0x0400018E RID: 398
		public AccountID_t m_unAccountId;
	}
}
