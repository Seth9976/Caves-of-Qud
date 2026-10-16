using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002E RID: 46
	[CallbackIdentity(346)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsEnumerateFollowingList_t
	{
		// Token: 0x040000A2 RID: 162
		public const int k_iCallback = 346;

		// Token: 0x040000A3 RID: 163
		public EResult m_eResult;

		// Token: 0x040000A4 RID: 164
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public CSteamID[] m_rgSteamID;

		// Token: 0x040000A5 RID: 165
		public int m_nResultsReturned;

		// Token: 0x040000A6 RID: 166
		public int m_nTotalResultCount;
	}
}
