using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EC RID: 236
	[CallbackIdentity(739)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FilterTextDictionaryChanged_t
	{
		// Token: 0x04000365 RID: 869
		public const int k_iCallback = 739;

		// Token: 0x04000366 RID: 870
		public int m_eLanguage;
	}
}
