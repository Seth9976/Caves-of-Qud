using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000043 RID: 67
	[CallbackIdentity(4501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x040000ED RID: 237
		public const int k_iCallback = 4501;

		// Token: 0x040000EE RID: 238
		public HHTMLBrowser unBrowserHandle;
	}
}
