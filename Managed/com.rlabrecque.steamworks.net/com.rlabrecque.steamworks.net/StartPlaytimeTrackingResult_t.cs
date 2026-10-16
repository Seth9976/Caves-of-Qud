using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BF RID: 191
	[CallbackIdentity(3410)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x040002CD RID: 717
		public const int k_iCallback = 3410;

		// Token: 0x040002CE RID: 718
		public EResult m_eResult;
	}
}
