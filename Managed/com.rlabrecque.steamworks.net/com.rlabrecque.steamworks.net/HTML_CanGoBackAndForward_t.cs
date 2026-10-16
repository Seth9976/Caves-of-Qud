using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004C RID: 76
	[CallbackIdentity(4510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x04000119 RID: 281
		public const int k_iCallback = 4510;

		// Token: 0x0400011A RID: 282
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400011B RID: 283
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoBack;

		// Token: 0x0400011C RID: 284
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoForward;
	}
}
