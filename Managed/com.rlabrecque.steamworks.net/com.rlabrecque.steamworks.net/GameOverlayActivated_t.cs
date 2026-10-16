using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001F RID: 31
	[CallbackIdentity(331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameOverlayActivated_t
	{
		// Token: 0x0400006C RID: 108
		public const int k_iCallback = 331;

		// Token: 0x0400006D RID: 109
		public byte m_bActive;

		// Token: 0x0400006E RID: 110
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserInitiated;

		// Token: 0x0400006F RID: 111
		public AppId_t m_nAppID;

		// Token: 0x04000070 RID: 112
		public uint m_dwOverlayPID;
	}
}
