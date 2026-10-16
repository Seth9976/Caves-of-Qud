using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016B RID: 363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CallbackMsg_t
	{
		// Token: 0x04000A77 RID: 2679
		public int m_hSteamUser;

		// Token: 0x04000A78 RID: 2680
		public int m_iCallback;

		// Token: 0x04000A79 RID: 2681
		public IntPtr m_pubParam;

		// Token: 0x04000A7A RID: 2682
		public int m_cubParam;
	}
}
