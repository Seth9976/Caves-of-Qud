using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamPartyBeaconLocation_t
	{
		// Token: 0x04000A49 RID: 2633
		public ESteamPartyBeaconLocationType m_eType;

		// Token: 0x04000A4A RID: 2634
		public ulong m_ulLocationID;
	}
}
