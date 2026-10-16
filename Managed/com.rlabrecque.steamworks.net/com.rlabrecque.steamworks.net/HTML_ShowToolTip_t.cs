using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000056 RID: 86
	[CallbackIdentity(4524)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ShowToolTip_t
	{
		// Token: 0x0400014A RID: 330
		public const int k_iCallback = 4524;

		// Token: 0x0400014B RID: 331
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400014C RID: 332
		public string pchMsg;
	}
}
