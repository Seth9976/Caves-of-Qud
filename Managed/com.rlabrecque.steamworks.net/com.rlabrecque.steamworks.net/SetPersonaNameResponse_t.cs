using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002F RID: 47
	[CallbackIdentity(347)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetPersonaNameResponse_t
	{
		// Token: 0x040000A7 RID: 167
		public const int k_iCallback = 347;

		// Token: 0x040000A8 RID: 168
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;

		// Token: 0x040000A9 RID: 169
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocalSuccess;

		// Token: 0x040000AA RID: 170
		public EResult m_result;
	}
}
