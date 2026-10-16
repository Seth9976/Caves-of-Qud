using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001D RID: 29
	[CallbackIdentity(1030)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct TimedTrialStatus_t
	{
		// Token: 0x04000064 RID: 100
		public const int k_iCallback = 1030;

		// Token: 0x04000065 RID: 101
		public AppId_t m_unAppID;

		// Token: 0x04000066 RID: 102
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsOffline;

		// Token: 0x04000067 RID: 103
		public uint m_unSecondsAllowed;

		// Token: 0x04000068 RID: 104
		public uint m_unSecondsPlayed;
	}
}
