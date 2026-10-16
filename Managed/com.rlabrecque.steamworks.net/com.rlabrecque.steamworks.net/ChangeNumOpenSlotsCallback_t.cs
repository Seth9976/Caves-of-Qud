using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007C RID: 124
	[CallbackIdentity(5304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ChangeNumOpenSlotsCallback_t
	{
		// Token: 0x040001E8 RID: 488
		public const int k_iCallback = 5304;

		// Token: 0x040001E9 RID: 489
		public EResult m_eResult;
	}
}
