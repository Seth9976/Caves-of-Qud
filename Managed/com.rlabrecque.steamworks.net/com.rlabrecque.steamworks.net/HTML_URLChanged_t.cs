using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000047 RID: 71
	[CallbackIdentity(4505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_URLChanged_t
	{
		// Token: 0x04000104 RID: 260
		public const int k_iCallback = 4505;

		// Token: 0x04000105 RID: 261
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000106 RID: 262
		public string pchURL;

		// Token: 0x04000107 RID: 263
		public string pchPostData;

		// Token: 0x04000108 RID: 264
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;

		// Token: 0x04000109 RID: 265
		public string pchPageTitle;

		// Token: 0x0400010A RID: 266
		[MarshalAs(UnmanagedType.I1)]
		public bool bNewNavigation;
	}
}
