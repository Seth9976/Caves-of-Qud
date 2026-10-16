using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000364 RID: 868
	internal readonly struct ArrayHandleOnStack
	{
		// Token: 0x02000365 RID: 869
		// (Invoke) Token: 0x060028BA RID: 10426
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal unsafe delegate void* CreateArrayDelegate(void* targetRef, int size);
	}
}
