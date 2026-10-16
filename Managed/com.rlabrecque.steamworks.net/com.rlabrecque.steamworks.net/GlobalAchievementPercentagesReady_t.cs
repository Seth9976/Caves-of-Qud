using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E1 RID: 225
	[CallbackIdentity(1110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x0400034C RID: 844
		public const int k_iCallback = 1110;

		// Token: 0x0400034D RID: 845
		public ulong m_nGameID;

		// Token: 0x0400034E RID: 846
		public EResult m_eResult;
	}
}
