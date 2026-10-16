using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000032 RID: 50
	[CallbackIdentity(350)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EquippedProfileItemsChanged_t
	{
		// Token: 0x040000AE RID: 174
		public const int k_iCallback = 350;

		// Token: 0x040000AF RID: 175
		public CSteamID m_steamID;
	}
}
