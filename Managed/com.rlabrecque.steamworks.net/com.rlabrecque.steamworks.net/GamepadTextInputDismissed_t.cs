using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E9 RID: 233
	[CallbackIdentity(714)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GamepadTextInputDismissed_t
	{
		// Token: 0x0400035F RID: 863
		public const int k_iCallback = 714;

		// Token: 0x04000360 RID: 864
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSubmitted;

		// Token: 0x04000361 RID: 865
		public uint m_unSubmittedText;

		// Token: 0x04000362 RID: 866
		public AppId_t m_unAppID;
	}
}
