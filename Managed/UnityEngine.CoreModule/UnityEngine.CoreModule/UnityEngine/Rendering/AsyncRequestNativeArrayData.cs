using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000479 RID: 1145
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[UsedByNativeCode]
	internal struct AsyncRequestNativeArrayData
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x000555B0 File Offset: 0x000537B0
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeArray<T> array) where T : struct
		{
			bool flag = array.m_AllocatorLabel == Allocator.Temp || array.m_AllocatorLabel == Allocator.TempJob;
			if (flag)
			{
				throw new ArgumentException("AsyncGPUReadback cannot use Temp memory as input since the result may only become available at an unspecified point in the future.");
			}
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x00055614 File Offset: 0x00053814
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeSlice<T> array) where T : struct
		{
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x04000C83 RID: 3203
		public unsafe void* nativeArrayBuffer;

		// Token: 0x04000C84 RID: 3204
		public long lengthInBytes;
	}
}
