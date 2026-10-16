using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamParamStringArray_t
	{
		// Token: 0x04000A53 RID: 2643
		public IntPtr m_ppStrings;

		// Token: 0x04000A54 RID: 2644
		public int m_nNumStrings;
	}
}
