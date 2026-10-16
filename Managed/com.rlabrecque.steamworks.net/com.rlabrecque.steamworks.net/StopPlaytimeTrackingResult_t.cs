using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C0 RID: 192
	[CallbackIdentity(3411)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x040002CF RID: 719
		public const int k_iCallback = 3411;

		// Token: 0x040002D0 RID: 720
		public EResult m_eResult;
	}
}
