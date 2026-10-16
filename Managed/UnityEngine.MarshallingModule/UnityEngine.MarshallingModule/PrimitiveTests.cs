using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("MarshallingScriptingClasses.h")]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class PrimitiveTests
	{
		// Token: 0x06000001 RID: 1
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterBool(bool param1, bool param2, int param3);

		// Token: 0x06000002 RID: 2
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterInt(int param);

		// Token: 0x06000003 RID: 3
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterOutInt(out int param);

		// Token: 0x06000004 RID: 4
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterRefInt(ref int param);

		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ReturnInt();

		// Token: 0x06000006 RID: 6 RVA: 0x00002050 File Offset: 0x00000250
		[NativeThrows]
		public unsafe static void ParameterIntDynamicArray(int[] param)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				PrimitiveTests.ParameterIntDynamicArray_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002088 File Offset: 0x00000288
		[NativeThrows]
		public unsafe static void ParameterIntNullableDynamicArray(int[] param)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				PrimitiveTests.ParameterIntNullableDynamicArray_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public static int[] ReturnIntDynamicArray()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PrimitiveTests.ReturnIntDynamicArray_Injected(out blittableArrayWrapper);
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

		// Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		public static int[] ReturnNullIntDynamicArray()
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PrimitiveTests.ReturnNullIntDynamicArray_Injected(out blittableArrayWrapper);
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

		// Token: 0x0600000A RID: 10 RVA: 0x00002128 File Offset: 0x00000328
		public static bool[] ReturnBoolDynamicArray()
		{
			bool[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PrimitiveTests.ReturnBoolDynamicArray_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				bool[] array;
				blittableArrayWrapper.Unmarshal<bool>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215C File Offset: 0x0000035C
		public static char[] ReturnCharDynamicArray()
		{
			char[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PrimitiveTests.ReturnCharDynamicArray_Injected(out blittableArrayWrapper);
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

		// Token: 0x0600000D RID: 13
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntDynamicArray_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntNullableDynamicArray_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnIntDynamicArray_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnNullIntDynamicArray_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnBoolDynamicArray_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnCharDynamicArray_Injected(out BlittableArrayWrapper ret);
	}
}
