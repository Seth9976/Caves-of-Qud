using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000044 RID: 68
	[CallbackIdentity(4502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NeedsPaint_t
	{
		// Token: 0x040000EF RID: 239
		public const int k_iCallback = 4502;

		// Token: 0x040000F0 RID: 240
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000F1 RID: 241
		public IntPtr pBGRA;

		// Token: 0x040000F2 RID: 242
		public uint unWide;

		// Token: 0x040000F3 RID: 243
		public uint unTall;

		// Token: 0x040000F4 RID: 244
		public uint unUpdateX;

		// Token: 0x040000F5 RID: 245
		public uint unUpdateY;

		// Token: 0x040000F6 RID: 246
		public uint unUpdateWide;

		// Token: 0x040000F7 RID: 247
		public uint unUpdateTall;

		// Token: 0x040000F8 RID: 248
		public uint unScrollX;

		// Token: 0x040000F9 RID: 249
		public uint unScrollY;

		// Token: 0x040000FA RID: 250
		public float flPageScale;

		// Token: 0x040000FB RID: 251
		public uint unPageSerial;
	}
}
