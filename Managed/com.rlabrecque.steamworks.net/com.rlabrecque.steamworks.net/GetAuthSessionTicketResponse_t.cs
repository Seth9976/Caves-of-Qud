using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D2 RID: 210
	[CallbackIdentity(163)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x0400030C RID: 780
		public const int k_iCallback = 163;

		// Token: 0x0400030D RID: 781
		public HAuthTicket m_hAuthTicket;

		// Token: 0x0400030E RID: 782
		public EResult m_eResult;
	}
}
