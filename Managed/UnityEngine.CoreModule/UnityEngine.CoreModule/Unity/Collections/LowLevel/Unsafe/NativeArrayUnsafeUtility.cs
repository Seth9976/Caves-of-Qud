using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000C1 RID: 193
	public static class NativeArrayUnsafeUtility
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator
			};
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00007CDC File Offset: 0x00005EDC
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<[IsUnmanaged] T>(Span<T> data, Allocator allocator) where T : struct, ValueType
		{
			fixed (T* pinnableReference = data.GetPinnableReference())
			{
				T* ptr = pinnableReference;
				return new NativeArray<T>
				{
					m_Buffer = (void*)ptr,
					m_Length = data.Length,
					m_AllocatorLabel = allocator
				};
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00007D28 File Offset: 0x00005F28
		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00007D40 File Offset: 0x00005F40
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00007D58 File Offset: 0x00005F58
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T>.ReadOnly nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00007D70 File Offset: 0x00005F70
		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
