using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000036 RID: 54
	[CallbackIdentity(201)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientApprove_t
	{
		// Token: 0x040000BB RID: 187
		public const int k_iCallback = 201;

		// Token: 0x040000BC RID: 188
		public CSteamID m_SteamID;

		// Token: 0x040000BD RID: 189
		public CSteamID m_OwnerSteamID;
	}
}
