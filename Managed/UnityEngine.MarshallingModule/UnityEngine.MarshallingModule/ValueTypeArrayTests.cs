using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000041 RID: 65
	[NativeType("Modules/Marshalling/MarshallingTests.h")]
	internal class ValueTypeArrayTests
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00003790 File Offset: 0x00001990
		[NativeThrows]
		public unsafe static void ParameterIntArrayReadOnly(int[] param)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeArrayTests.ParameterIntArrayReadOnly_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000037C8 File Offset: 0x000019C8
		[NativeThrows]
		public unsafe static void ParameterIntArrayWritable(int[] param)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeArrayTests.ParameterIntArrayWritable_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003800 File Offset: 0x00001A00
		[NativeThrows]
		public unsafe static void ParameterIntArrayEmpty(int[] param, int[] param2)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<int> span2 = new Span<int>(param2);
				fixed (int* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					ValueTypeArrayTests.ParameterIntArrayEmpty_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003864 File Offset: 0x00001A64
		public unsafe static void ParameterIntArrayNullExceptions([NotNull] int[] param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeArrayTests.ParameterIntArrayNullExceptions_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000038AC File Offset: 0x00001AAC
		[NativeThrows]
		public unsafe static void ParameterIntMultidimensionalArray(int[,] param)
		{
			int num;
			void* ptr;
			if (param == null || (num = param.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&param[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			ValueTypeArrayTests.ParameterIntMultidimensionalArray_Injected(ref managedSpanWrapper);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000038EC File Offset: 0x00001AEC
		public unsafe static void ParameterIntMultidimensionalArrayNullExceptions([NotNull] int[,] param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			int num;
			void* ptr;
			if (param == null || (num = param.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&param[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			ValueTypeArrayTests.ParameterIntMultidimensionalArrayNullExceptions_Injected(ref managedSpanWrapper);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00003938 File Offset: 0x00001B38
		[NativeThrows]
		public unsafe static void ParameterCharArrayReadOnly(char[] param)
		{
			Span<char> span = new Span<char>(param);
			fixed (char* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeArrayTests.ParameterCharArrayReadOnly_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003970 File Offset: 0x00001B70
		[NativeThrows]
		public unsafe static void ParameterBlittableCornerCaseStructArrayReadOnly(BlittableCornerCases[] param)
		{
			Span<BlittableCornerCases> span = new Span<BlittableCornerCases>(param);
			fixed (BlittableCornerCases* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeArrayTests.ParameterBlittableCornerCaseStructArrayReadOnly_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000039A8 File Offset: 0x00001BA8
		[NativeThrows]
		public unsafe static void ParameterIntArrayOutAttr([Out] int[] param)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (param != null)
				{
					fixed (int[] array = param)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				ValueTypeArrayTests.ParameterIntArrayOutAttr_Injected(out blittableArrayWrapper);
			}
			finally
			{
				int[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<int>(ref array);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000039F8 File Offset: 0x00001BF8
		[NativeThrows]
		public unsafe static void ParameterCharArrayOutAttr([Out] char[] param)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (param != null)
				{
					fixed (char[] array = param)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				ValueTypeArrayTests.ParameterCharArrayOutAttr_Injected(out blittableArrayWrapper);
			}
			finally
			{
				char[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<char>(ref array);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003A48 File Offset: 0x00001C48
		[NativeThrows]
		public unsafe static void ParameterBlittableCornerCaseStructArrayOutAttr([Out] BlittableCornerCases[] param)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (param != null)
				{
					fixed (BlittableCornerCases[] array = param)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				ValueTypeArrayTests.ParameterBlittableCornerCaseStructArrayOutAttr_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableCornerCases[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<BlittableCornerCases>(ref array);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003A98 File Offset: 0x00001C98
		public static int[] ParameterIntArrayReturn()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ValueTypeArrayTests.ParameterIntArrayReturn_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003ACC File Offset: 0x00001CCC
		public static int[] ParameterIntArrayReturnEmpty()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ValueTypeArrayTests.ParameterIntArrayReturnEmpty_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003B00 File Offset: 0x00001D00
		public static int[] ParameterIntArrayReturnNull()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ValueTypeArrayTests.ParameterIntArrayReturnNull_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003B34 File Offset: 0x00001D34
		public static char[] ParameterCharArrayReturn()
		{
			char[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ValueTypeArrayTests.ParameterCharArrayReturn_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				char[] array;
				blittableArrayWrapper.Unmarshal<char>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003B68 File Offset: 0x00001D68
		public static BlittableCornerCases[] ParameterBlittableCornerCaseStructArrayReturn()
		{
			BlittableCornerCases[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ValueTypeArrayTests.ParameterBlittableCornerCaseStructArrayReturn_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				BlittableCornerCases[] array;
				blittableArrayWrapper.Unmarshal<BlittableCornerCases>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000183 RID: 387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayReadOnly_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayWritable_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayEmpty_Injected(ref ManagedSpanWrapper param, ref ManagedSpanWrapper param2);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayNullExceptions_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntMultidimensionalArray_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntMultidimensionalArrayNullExceptions_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterCharArrayReadOnly_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterBlittableCornerCaseStructArrayReadOnly_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayOutAttr_Injected(out BlittableArrayWrapper param);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterCharArrayOutAttr_Injected(out BlittableArrayWrapper param);

		// Token: 0x0600018D RID: 397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterBlittableCornerCaseStructArrayOutAttr_Injected(out BlittableArrayWrapper param);

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayReturn_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600018F RID: 399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayReturnEmpty_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntArrayReturnNull_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000191 RID: 401
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterCharArrayReturn_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000192 RID: 402
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterBlittableCornerCaseStructArrayReturn_Injected(out BlittableArrayWrapper ret);
	}
}
