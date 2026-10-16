using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B4 RID: 180
	[CallbackIdentity(2301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotReady_t
	{
		// Token: 0x040002A0 RID: 672
		public const int k_iCallback = 2301;

		// Token: 0x040002A1 RID: 673
		public ScreenshotHandle m_hLocal;

		// Token: 0x040002A2 RID: 674
		public EResult m_eResult;
	}
}
