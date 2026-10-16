using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004A RID: 74
	[CallbackIdentity(4508)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ChangedTitle_t
	{
		// Token: 0x04000112 RID: 274
		public const int k_iCallback = 4508;

		// Token: 0x04000113 RID: 275
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000114 RID: 276
		public string pchTitle;
	}
}
