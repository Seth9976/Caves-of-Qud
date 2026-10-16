using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D7 RID: 215
	[CallbackIdentity(168)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetTicketForWebApiResponse_t
	{
		// Token: 0x04000322 RID: 802
		public const int k_iCallback = 168;

		// Token: 0x04000323 RID: 803
		public HAuthTicket m_hAuthTicket;

		// Token: 0x04000324 RID: 804
		public EResult m_eResult;

		// Token: 0x04000325 RID: 805
		public int m_cubTicket;

		// Token: 0x04000326 RID: 806
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2560)]
		public byte[] m_rgubTicket;
	}
}
