using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000022 RID: 34
	[CallbackIdentity(334)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AvatarImageLoaded_t
	{
		// Token: 0x04000077 RID: 119
		public const int k_iCallback = 334;

		// Token: 0x04000078 RID: 120
		public CSteamID m_steamID;

		// Token: 0x04000079 RID: 121
		public int m_iImage;

		// Token: 0x0400007A RID: 122
		public int m_iWide;

		// Token: 0x0400007B RID: 123
		public int m_iTall;
	}
}
