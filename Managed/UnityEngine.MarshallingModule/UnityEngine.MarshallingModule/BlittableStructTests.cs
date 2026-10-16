using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class BlittableStructTests
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002750 File Offset: 0x00000950
		[NativeThrows]
		public static void ParameterStructInt(StructInt param)
		{
			BlittableStructTests.ParameterStructInt_Injected(ref param);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002764 File Offset: 0x00000964
		public static void ParameterStructInt2(StructInt2 param)
		{
			BlittableStructTests.ParameterStructInt2_Injected(ref param);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002778 File Offset: 0x00000978
		public static StructInt ReturnStructInt()
		{
			StructInt structInt;
			BlittableStructTests.ReturnStructInt_Injected(out structInt);
			return structInt;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002790 File Offset: 0x00000990
		[NativeThrows]
		public static void ParameterNestedBlittableStruct(StructNestedBlittable s)
		{
			BlittableStructTests.ParameterNestedBlittableStruct_Injected(ref s);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027A4 File Offset: 0x000009A4
		public static StructNestedBlittable ReturnNestedBlittableStruct()
		{
			StructNestedBlittable structNestedBlittable;
			BlittableStructTests.ReturnNestedBlittableStruct_Injected(out structNestedBlittable);
			return structNestedBlittable;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000027BC File Offset: 0x000009BC
		[NativeThrows]
		public unsafe static void ParameterStructIntDynamicArray(StructInt[] param)
		{
			Span<StructInt> span = new Span<StructInt>(param);
			fixed (StructInt* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BlittableStructTests.ParameterStructIntDynamicArray_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027F4 File Offset: 0x000009F4
		public static StructInt[] ReturnStructIntDynamicArray()
		{
			StructInt[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				BlittableStructTests.ReturnStructIntDynamicArray_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				StructInt[] array;
				blittableArrayWrapper.Unmarshal<StructInt>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002828 File Offset: 0x00000A28
		[NativeThrows]
		public unsafe static void ParameterStructNestedBlittableDynamicArray(StructNestedBlittable[] param)
		{
			Span<StructNestedBlittable> span = new Span<StructNestedBlittable>(param);
			fixed (StructNestedBlittable* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BlittableStructTests.ParameterStructNestedBlittableDynamicArray_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002860 File Offset: 0x00000A60
		public static StructNestedBlittable[] ReturnStructNestedBlittableDynamicArray()
		{
			StructNestedBlittable[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				BlittableStructTests.ReturnStructNestedBlittableDynamicArray_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				StructNestedBlittable[] array;
				blittableArrayWrapper.Unmarshal<StructNestedBlittable>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002894 File Offset: 0x00000A94
		[NativeThrows]
		public static void ParameterStructFixedBuffer(StructFixedBuffer param)
		{
			BlittableStructTests.ParameterStructFixedBuffer_Injected(ref param);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static StructFixedBuffer ReturnStructFixedBuffer()
		{
			StructFixedBuffer structFixedBuffer;
			BlittableStructTests.ReturnStructFixedBuffer_Injected(out structFixedBuffer);
			return structFixedBuffer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static StructInt structIntProperty
		{
			get
			{
				StructInt structInt;
				BlittableStructTests.get_structIntProperty_Injected(out structInt);
				return structInt;
			}
			set
			{
				BlittableStructTests.set_structIntProperty_Injected(ref value);
			}
		}

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructInt_Injected([In] ref StructInt param);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructInt2_Injected([In] ref StructInt2 param);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructInt_Injected(out StructInt ret);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterNestedBlittableStruct_Injected([In] ref StructNestedBlittable s);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnNestedBlittableStruct_Injected(out StructNestedBlittable ret);

		// Token: 0x0600005C RID: 92
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructIntDynamicArray_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600005D RID: 93
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructIntDynamicArray_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructNestedBlittableDynamicArray_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructNestedBlittableDynamicArray_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructFixedBuffer_Injected([In] ref StructFixedBuffer param);

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructFixedBuffer_Injected(out StructFixedBuffer ret);

		// Token: 0x06000062 RID: 98
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_structIntProperty_Injected(out StructInt ret);

		// Token: 0x06000063 RID: 99
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_structIntProperty_Injected([In] ref StructInt value);
	}
}
