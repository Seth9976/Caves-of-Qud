using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000054 RID: 84
	[CallbackIdentity(4522)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SetCursor_t
	{
		// Token: 0x04000144 RID: 324
		public const int k_iCallback = 4522;

		// Token: 0x04000145 RID: 325
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000146 RID: 326
		public uint eMouseCursor;
	}
}
