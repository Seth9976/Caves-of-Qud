using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000064 RID: 100
	[CallbackIdentity(4703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x0400017B RID: 379
		public const int k_iCallback = 4703;

		// Token: 0x0400017C RID: 380
		public EResult m_result;

		// Token: 0x0400017D RID: 381
		public CSteamID m_steamID;

		// Token: 0x0400017E RID: 382
		public int m_numEligiblePromoItemDefs;

		// Token: 0x0400017F RID: 383
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
