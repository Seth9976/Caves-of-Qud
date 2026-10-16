using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000071 RID: 113
	[CallbackIdentity(516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x040001B4 RID: 436
		public const int k_iCallback = 516;

		// Token: 0x040001B5 RID: 437
		public EResult m_eResult;
	}
}
