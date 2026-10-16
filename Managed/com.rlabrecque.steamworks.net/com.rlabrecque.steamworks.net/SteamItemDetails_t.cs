using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000164 RID: 356
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamItemDetails_t
	{
		// Token: 0x04000A45 RID: 2629
		public SteamItemInstanceID_t m_itemId;

		// Token: 0x04000A46 RID: 2630
		public SteamItemDef_t m_iDefinition;

		// Token: 0x04000A47 RID: 2631
		public ushort m_unQuantity;

		// Token: 0x04000A48 RID: 2632
		public ushort m_unFlags;
	}
}
