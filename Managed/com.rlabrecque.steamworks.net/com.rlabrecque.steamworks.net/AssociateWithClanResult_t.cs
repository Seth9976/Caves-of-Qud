using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003E RID: 62
	[CallbackIdentity(210)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AssociateWithClanResult_t
	{
		// Token: 0x040000DD RID: 221
		public const int k_iCallback = 210;

		// Token: 0x040000DE RID: 222
		public EResult m_eResult;
	}
}
