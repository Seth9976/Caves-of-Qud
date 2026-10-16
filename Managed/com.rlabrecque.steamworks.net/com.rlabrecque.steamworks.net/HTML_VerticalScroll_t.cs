using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004E RID: 78
	[CallbackIdentity(4512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_VerticalScroll_t
	{
		// Token: 0x04000124 RID: 292
		public const int k_iCallback = 4512;

		// Token: 0x04000125 RID: 293
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000126 RID: 294
		public uint unScrollMax;

		// Token: 0x04000127 RID: 295
		public uint unScrollCurrent;

		// Token: 0x04000128 RID: 296
		public float flPageScale;

		// Token: 0x04000129 RID: 297
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x0400012A RID: 298
		public uint unPageSize;
	}
}
