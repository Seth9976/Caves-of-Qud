using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A7 RID: 423
	// (Invoke) Token: 0x06000A1C RID: 2588
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SteamInputActionEventCallbackPointer(IntPtr SteamInputActionEvent);
}
