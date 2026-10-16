using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005F RID: 95
	[CallbackIdentity(2803)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputConfigurationLoaded_t
	{
		// Token: 0x04000167 RID: 359
		public const int k_iCallback = 2803;

		// Token: 0x04000168 RID: 360
		public AppId_t m_unAppID;

		// Token: 0x04000169 RID: 361
		public InputHandle_t m_ulDeviceHandle;

		// Token: 0x0400016A RID: 362
		public CSteamID m_ulMappingCreator;

		// Token: 0x0400016B RID: 363
		public uint m_unMajorRevision;

		// Token: 0x0400016C RID: 364
		public uint m_unMinorRevision;

		// Token: 0x0400016D RID: 365
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUsesSteamInputAPI;

		// Token: 0x0400016E RID: 366
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUsesGamepadAPI;
	}
}
