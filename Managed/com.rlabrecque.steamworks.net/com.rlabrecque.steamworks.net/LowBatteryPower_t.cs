using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E5 RID: 229
	[CallbackIdentity(702)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LowBatteryPower_t
	{
		// Token: 0x04000356 RID: 854
		public const int k_iCallback = 702;

		// Token: 0x04000357 RID: 855
		public byte m_nMinutesBatteryLeft;
	}
}
