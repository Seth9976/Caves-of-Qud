using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000045 RID: 69
	[CallbackIdentity(4503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StartRequest_t
	{
		// Token: 0x040000FC RID: 252
		public const int k_iCallback = 4503;

		// Token: 0x040000FD RID: 253
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000FE RID: 254
		public string pchURL;

		// Token: 0x040000FF RID: 255
		public string pchTarget;

		// Token: 0x04000100 RID: 256
		public string pchPostData;

		// Token: 0x04000101 RID: 257
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;
	}
}
