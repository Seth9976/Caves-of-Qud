using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005E RID: 94
	[CallbackIdentity(2802)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputDeviceDisconnected_t
	{
		// Token: 0x04000165 RID: 357
		public const int k_iCallback = 2802;

		// Token: 0x04000166 RID: 358
		public InputHandle_t m_ulDisconnectedDeviceHandle;
	}
}
