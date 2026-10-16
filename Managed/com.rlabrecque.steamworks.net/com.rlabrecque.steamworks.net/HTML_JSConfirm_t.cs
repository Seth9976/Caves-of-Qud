using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000051 RID: 81
	[CallbackIdentity(4515)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSConfirm_t
	{
		// Token: 0x04000135 RID: 309
		public const int k_iCallback = 4515;

		// Token: 0x04000136 RID: 310
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000137 RID: 311
		public string pchMessage;
	}
}
