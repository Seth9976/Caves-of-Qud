using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000057 RID: 87
	[CallbackIdentity(4525)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_UpdateToolTip_t
	{
		// Token: 0x0400014D RID: 333
		public const int k_iCallback = 4525;

		// Token: 0x0400014E RID: 334
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400014F RID: 335
		public string pchMsg;
	}
}
