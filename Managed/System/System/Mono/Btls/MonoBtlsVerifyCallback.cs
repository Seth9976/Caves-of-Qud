using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E5 RID: 229
	// (Invoke) Token: 0x060004D4 RID: 1236
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsVerifyCallback(MonoBtlsX509StoreCtx ctx);
}
