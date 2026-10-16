using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CD RID: 205
	[CallbackIdentity(117)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCFailure_t
	{
		// Token: 0x040002FF RID: 767
		public const int k_iCallback = 117;

		// Token: 0x04000300 RID: 768
		public byte m_eFailureType;
	}
}
