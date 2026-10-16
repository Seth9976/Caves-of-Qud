using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000049 RID: 73
	[CallbackIdentity(4507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x0400010F RID: 271
		public const int k_iCallback = 4507;

		// Token: 0x04000110 RID: 272
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000111 RID: 273
		public string pchURL;
	}
}
