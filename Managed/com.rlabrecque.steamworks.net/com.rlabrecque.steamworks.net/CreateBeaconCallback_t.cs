using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007A RID: 122
	[CallbackIdentity(5302)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateBeaconCallback_t
	{
		// Token: 0x040001E2 RID: 482
		public const int k_iCallback = 5302;

		// Token: 0x040001E3 RID: 483
		public EResult m_eResult;

		// Token: 0x040001E4 RID: 484
		public PartyBeaconID_t m_ulBeaconID;
	}
}
