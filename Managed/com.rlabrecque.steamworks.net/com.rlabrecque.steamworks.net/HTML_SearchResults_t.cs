using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004B RID: 75
	[CallbackIdentity(4509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SearchResults_t
	{
		// Token: 0x04000115 RID: 277
		public const int k_iCallback = 4509;

		// Token: 0x04000116 RID: 278
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000117 RID: 279
		public uint unResults;

		// Token: 0x04000118 RID: 280
		public uint unCurrentMatch;
	}
}
