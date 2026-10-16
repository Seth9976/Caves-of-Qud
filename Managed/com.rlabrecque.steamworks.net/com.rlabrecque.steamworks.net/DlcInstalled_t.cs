using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000019 RID: 25
	[CallbackIdentity(1005)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DlcInstalled_t
	{
		// Token: 0x04000057 RID: 87
		public const int k_iCallback = 1005;

		// Token: 0x04000058 RID: 88
		public AppId_t m_nAppID;
	}
}
