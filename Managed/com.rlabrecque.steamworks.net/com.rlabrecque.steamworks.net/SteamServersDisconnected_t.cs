using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CB RID: 203
	[CallbackIdentity(103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersDisconnected_t
	{
		// Token: 0x040002F7 RID: 759
		public const int k_iCallback = 103;

		// Token: 0x040002F8 RID: 760
		public EResult m_eResult;
	}
}
