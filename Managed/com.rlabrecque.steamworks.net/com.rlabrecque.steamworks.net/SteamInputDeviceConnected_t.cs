using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005D RID: 93
	[CallbackIdentity(2801)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputDeviceConnected_t
	{
		// Token: 0x04000163 RID: 355
		public const int k_iCallback = 2801;

		// Token: 0x04000164 RID: 356
		public InputHandle_t m_ulConnectedDeviceHandle;
	}
}
