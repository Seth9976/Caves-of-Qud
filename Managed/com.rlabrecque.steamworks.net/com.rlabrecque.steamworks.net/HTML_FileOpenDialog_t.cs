using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000052 RID: 82
	[CallbackIdentity(4516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FileOpenDialog_t
	{
		// Token: 0x04000138 RID: 312
		public const int k_iCallback = 4516;

		// Token: 0x04000139 RID: 313
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400013A RID: 314
		public string pchTitle;

		// Token: 0x0400013B RID: 315
		public string pchInitialFile;
	}
}
