using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Bindings
{
	// Token: 0x02000362 RID: 866
	[VisibleToOtherModules]
	internal struct Unmarshal
	{
		// Token: 0x060028B5 RID: 10421 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T UnmarshalUnityObject<T>(IntPtr gcHandlePtr) where T : Object
		{
			bool flag = gcHandlePtr == IntPtr.Zero;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				T t2 = (T)((object)Unmarshal.FromIntPtrUnsafe(gcHandlePtr).Target);
				t = t2;
			}
			return t;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0004E940 File Offset: 0x0004CB40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static GCHandle FromIntPtrUnsafe(IntPtr gcHandle)
		{
			return *UnsafeUtility.As<IntPtr, GCHandle>(ref gcHandle);
		}
	}
}
