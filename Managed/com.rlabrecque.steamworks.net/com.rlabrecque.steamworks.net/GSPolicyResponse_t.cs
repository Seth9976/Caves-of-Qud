using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003A RID: 58
	[CallbackIdentity(115)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSPolicyResponse_t
	{
		// Token: 0x040000C9 RID: 201
		public const int k_iCallback = 115;

		// Token: 0x040000CA RID: 202
		public byte m_bSecure;
	}
}
