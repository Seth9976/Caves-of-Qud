using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000038 RID: 56
	[CallbackIdentity(203)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientKick_t
	{
		// Token: 0x040000C2 RID: 194
		public const int k_iCallback = 203;

		// Token: 0x040000C3 RID: 195
		public CSteamID m_SteamID;

		// Token: 0x040000C4 RID: 196
		public EDenyReason m_eDenyReason;
	}
}
