using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class DelegateTests
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000030DC File Offset: 0x000012DC
		public static int A()
		{
			return 882;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000030F4 File Offset: 0x000012F4
		[MonoPInvokeCallback(typeof(DelegateTests.SomeDelegateFunctionPtr))]
		public static int B()
		{
			return 883;
		}

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ReturnDelegate(DelegateTests.SomeDelegate someDelegate);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ReturnDelegateFunctionPtr(DelegateTests.SomeDelegateFunctionPtr SomeDelegateFunctionPtr);

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x0600010F RID: 271
		public delegate int SomeDelegate();

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x06000113 RID: 275
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SomeDelegateFunctionPtr();
	}
}
