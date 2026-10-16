using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000048 RID: 72
	[CallbackIdentity(4506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FinishedRequest_t
	{
		// Token: 0x0400010B RID: 267
		public const int k_iCallback = 4506;

		// Token: 0x0400010C RID: 268
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400010D RID: 269
		public string pchURL;

		// Token: 0x0400010E RID: 270
		public string pchPageTitle;
	}
}
