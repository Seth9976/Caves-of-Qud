using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EF RID: 239
	[CallbackIdentity(1223)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkingFakeIPResult_t
	{
		// Token: 0x0400036E RID: 878
		public const int k_iCallback = 1223;

		// Token: 0x0400036F RID: 879
		public EResult m_eResult;

		// Token: 0x04000370 RID: 880
		public SteamNetworkingIdentity m_identity;

		// Token: 0x04000371 RID: 881
		public uint m_unIP;

		// Token: 0x04000372 RID: 882
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ushort[] m_unPorts;
	}
}
