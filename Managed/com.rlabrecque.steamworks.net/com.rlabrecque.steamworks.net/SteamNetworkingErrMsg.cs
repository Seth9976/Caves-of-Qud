using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public struct SteamNetworkingErrMsg
	{
		// Token: 0x04000AEC RID: 2796
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}
