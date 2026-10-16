using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000059 RID: 89
	[CallbackIdentity(4527)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserRestarted_t
	{
		// Token: 0x04000152 RID: 338
		public const int k_iCallback = 4527;

		// Token: 0x04000153 RID: 339
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000154 RID: 340
		public HHTMLBrowser unOldBrowserHandle;
	}
}
