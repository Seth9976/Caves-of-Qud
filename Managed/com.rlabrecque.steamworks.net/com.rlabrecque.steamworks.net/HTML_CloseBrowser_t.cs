using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000046 RID: 70
	[CallbackIdentity(4504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CloseBrowser_t
	{
		// Token: 0x04000102 RID: 258
		public const int k_iCallback = 4504;

		// Token: 0x04000103 RID: 259
		public HHTMLBrowser unBrowserHandle;
	}
}
