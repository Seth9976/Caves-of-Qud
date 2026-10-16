using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007B RID: 123
	[CallbackIdentity(5303)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ReservationNotificationCallback_t
	{
		// Token: 0x040001E5 RID: 485
		public const int k_iCallback = 5303;

		// Token: 0x040001E6 RID: 486
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x040001E7 RID: 487
		public CSteamID m_steamIDJoiner;
	}
}
