using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C7 RID: 199
	[CallbackIdentity(3418)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserSubscribedItemsListChanged_t
	{
		// Token: 0x040002EA RID: 746
		public const int k_iCallback = 3418;

		// Token: 0x040002EB RID: 747
		public AppId_t m_nAppID;
	}
}
