using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000C2 RID: 194
	public static class NativeSliceUnsafeUtility
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00007D88 File Offset: 0x00005F88
		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			bool flag2 = stride < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new NativeSlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00007E08 File Offset: 0x00006008
		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00007E20 File Offset: 0x00006020
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}
	}
}
