using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AF RID: 175
	[CallbackIdentity(1329)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileProgress_t
	{
		// Token: 0x04000291 RID: 657
		public const int k_iCallback = 1329;

		// Token: 0x04000292 RID: 658
		public double m_dPercentFile;

		// Token: 0x04000293 RID: 659
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPreview;
	}
}
