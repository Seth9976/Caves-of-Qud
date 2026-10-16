using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004F RID: 79
	[CallbackIdentity(4513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_LinkAtPosition_t
	{
		// Token: 0x0400012B RID: 299
		public const int k_iCallback = 4513;

		// Token: 0x0400012C RID: 300
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400012D RID: 301
		public uint x;

		// Token: 0x0400012E RID: 302
		public uint y;

		// Token: 0x0400012F RID: 303
		public string pchURL;

		// Token: 0x04000130 RID: 304
		[MarshalAs(UnmanagedType.I1)]
		public bool bInput;

		// Token: 0x04000131 RID: 305
		[MarshalAs(UnmanagedType.I1)]
		public bool bLiveLink;
	}
}
