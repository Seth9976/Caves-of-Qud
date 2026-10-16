using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000055 RID: 85
	[CallbackIdentity(4523)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StatusText_t
	{
		// Token: 0x04000147 RID: 327
		public const int k_iCallback = 4523;

		// Token: 0x04000148 RID: 328
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000149 RID: 329
		public string pchMsg;
	}
}
