using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000029 RID: 41
	[CallbackIdentity(341)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadClanActivityCountsResult_t
	{
		// Token: 0x04000092 RID: 146
		public const int k_iCallback = 341;

		// Token: 0x04000093 RID: 147
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;
	}
}
