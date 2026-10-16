using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E8 RID: 232
	[CallbackIdentity(705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature_t
	{
		// Token: 0x0400035D RID: 861
		public const int k_iCallback = 705;

		// Token: 0x0400035E RID: 862
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
