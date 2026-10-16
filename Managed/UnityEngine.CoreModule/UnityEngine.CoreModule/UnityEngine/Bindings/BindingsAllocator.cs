using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000367 RID: 871
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Scripting/Marshalling/BindingsAllocator.h")]
	[StaticAccessor("Marshalling::BindingsAllocator", StaticAccessorType.DoubleColon)]
	internal static class BindingsAllocator
	{
		// Token: 0x060028C1 RID: 10433
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Malloc(int size);

		// Token: 0x060028C2 RID: 10434
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* ptr);

		// Token: 0x060028C3 RID: 10435
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FreeNativeOwnedMemory(void* ptr);

		// Token: 0x060028C4 RID: 10436 RVA: 0x0004EA4C File Offset: 0x0004CC4C
		public unsafe static void* GetNativeOwnedDataPointer(void* ptr)
		{
			return ((BindingsAllocator.NativeOwnedMemory*)ptr)->data;
		}

		// Token: 0x02000368 RID: 872
		private struct NativeOwnedMemory
		{
			// Token: 0x04000B81 RID: 2945
			public unsafe void* data;
		}
	}
}
