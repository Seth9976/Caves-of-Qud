using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E7 RID: 231
	// (Invoke) Token: 0x060004DC RID: 1244
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsServerNameCallback();
}
