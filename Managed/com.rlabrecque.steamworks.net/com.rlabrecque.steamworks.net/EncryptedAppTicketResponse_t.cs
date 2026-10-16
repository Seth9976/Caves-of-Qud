using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D1 RID: 209
	[CallbackIdentity(154)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EncryptedAppTicketResponse_t
	{
		// Token: 0x0400030A RID: 778
		public const int k_iCallback = 154;

		// Token: 0x0400030B RID: 779
		public EResult m_eResult;
	}
}
