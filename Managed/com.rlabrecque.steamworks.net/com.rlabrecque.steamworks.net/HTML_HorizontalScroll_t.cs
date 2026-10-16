using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004D RID: 77
	[CallbackIdentity(4511)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HorizontalScroll_t
	{
		// Token: 0x0400011D RID: 285
		public const int k_iCallback = 4511;

		// Token: 0x0400011E RID: 286
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400011F RID: 287
		public uint unScrollMax;

		// Token: 0x04000120 RID: 288
		public uint unScrollCurrent;

		// Token: 0x04000121 RID: 289
		public float flPageScale;

		// Token: 0x04000122 RID: 290
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x04000123 RID: 291
		public uint unPageSize;
	}
}
