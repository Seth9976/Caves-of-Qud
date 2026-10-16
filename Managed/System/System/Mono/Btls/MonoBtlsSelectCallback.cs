using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E6 RID: 230
	// (Invoke) Token: 0x060004D8 RID: 1240
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsSelectCallback(string[] acceptableIssuers);
}
