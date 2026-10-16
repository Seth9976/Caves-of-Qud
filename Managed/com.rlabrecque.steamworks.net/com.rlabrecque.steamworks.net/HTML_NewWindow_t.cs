using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000053 RID: 83
	[CallbackIdentity(4521)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NewWindow_t
	{
		// Token: 0x0400013C RID: 316
		public const int k_iCallback = 4521;

		// Token: 0x0400013D RID: 317
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400013E RID: 318
		public string pchURL;

		// Token: 0x0400013F RID: 319
		public uint unX;

		// Token: 0x04000140 RID: 320
		public uint unY;

		// Token: 0x04000141 RID: 321
		public uint unWide;

		// Token: 0x04000142 RID: 322
		public uint unTall;

		// Token: 0x04000143 RID: 323
		public HHTMLBrowser unNewWindow_BrowserHandle_IGNORE;
	}
}
