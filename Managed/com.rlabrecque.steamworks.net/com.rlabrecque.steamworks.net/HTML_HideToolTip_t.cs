using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000058 RID: 88
	[CallbackIdentity(4526)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HideToolTip_t
	{
		// Token: 0x04000150 RID: 336
		public const int k_iCallback = 4526;

		// Token: 0x04000151 RID: 337
		public HHTMLBrowser unBrowserHandle;
	}
}
