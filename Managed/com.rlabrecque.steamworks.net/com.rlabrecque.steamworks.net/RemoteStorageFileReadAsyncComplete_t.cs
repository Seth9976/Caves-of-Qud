using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B2 RID: 178
	[CallbackIdentity(1332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x0400029A RID: 666
		public const int k_iCallback = 1332;

		// Token: 0x0400029B RID: 667
		public SteamAPICall_t m_hFileReadAsync;

		// Token: 0x0400029C RID: 668
		public EResult m_eResult;

		// Token: 0x0400029D RID: 669
		public uint m_nOffset;

		// Token: 0x0400029E RID: 670
		public uint m_cubRead;
	}
}
