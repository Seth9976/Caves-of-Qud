using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003B RID: 59
	[CallbackIdentity(207)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSGameplayStats_t
	{
		// Token: 0x040000CB RID: 203
		public const int k_iCallback = 207;

		// Token: 0x040000CC RID: 204
		public EResult m_eResult;

		// Token: 0x040000CD RID: 205
		public int m_nRank;

		// Token: 0x040000CE RID: 206
		public uint m_unTotalConnects;

		// Token: 0x040000CF RID: 207
		public uint m_unTotalMinutesPlayed;
	}
}
