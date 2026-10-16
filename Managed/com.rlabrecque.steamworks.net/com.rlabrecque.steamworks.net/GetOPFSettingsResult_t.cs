using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EE RID: 238
	[CallbackIdentity(4624)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetOPFSettingsResult_t
	{
		// Token: 0x0400036B RID: 875
		public const int k_iCallback = 4624;

		// Token: 0x0400036C RID: 876
		public EResult m_eResult;

		// Token: 0x0400036D RID: 877
		public AppId_t m_unVideoAppID;
	}
}
