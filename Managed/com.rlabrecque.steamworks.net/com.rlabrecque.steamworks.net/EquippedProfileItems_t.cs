using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000033 RID: 51
	[CallbackIdentity(351)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EquippedProfileItems_t
	{
		// Token: 0x040000B0 RID: 176
		public const int k_iCallback = 351;

		// Token: 0x040000B1 RID: 177
		public EResult m_eResult;

		// Token: 0x040000B2 RID: 178
		public CSteamID m_steamID;

		// Token: 0x040000B3 RID: 179
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasAnimatedAvatar;

		// Token: 0x040000B4 RID: 180
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasAvatarFrame;

		// Token: 0x040000B5 RID: 181
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasProfileModifier;

		// Token: 0x040000B6 RID: 182
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasProfileBackground;

		// Token: 0x040000B7 RID: 183
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasMiniProfileBackground;
	}
}
