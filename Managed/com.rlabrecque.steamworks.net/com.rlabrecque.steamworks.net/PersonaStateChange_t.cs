using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001E RID: 30
	[CallbackIdentity(304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PersonaStateChange_t
	{
		// Token: 0x04000069 RID: 105
		public const int k_iCallback = 304;

		// Token: 0x0400006A RID: 106
		public ulong m_ulSteamID;

		// Token: 0x0400006B RID: 107
		public EPersonaChange m_nChangeFlags;
	}
}
