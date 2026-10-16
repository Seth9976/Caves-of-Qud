using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000060 RID: 96
	[CallbackIdentity(2804)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputGamepadSlotChange_t
	{
		// Token: 0x0400016F RID: 367
		public const int k_iCallback = 2804;

		// Token: 0x04000170 RID: 368
		public AppId_t m_unAppID;

		// Token: 0x04000171 RID: 369
		public InputHandle_t m_ulDeviceHandle;

		// Token: 0x04000172 RID: 370
		public ESteamInputType m_eDeviceType;

		// Token: 0x04000173 RID: 371
		public int m_nOldGamepadSlot;

		// Token: 0x04000174 RID: 372
		public int m_nNewGamepadSlot;
	}
}
