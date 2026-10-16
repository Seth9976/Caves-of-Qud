using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000050 RID: 80
	[CallbackIdentity(4514)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSAlert_t
	{
		// Token: 0x04000132 RID: 306
		public const int k_iCallback = 4514;

		// Token: 0x04000133 RID: 307
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000134 RID: 308
		public string pchMessage;
	}
}
