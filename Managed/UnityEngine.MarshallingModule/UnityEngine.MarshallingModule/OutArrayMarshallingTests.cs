using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[NativeHeader("Modules/Marshalling/OutArrayMarshallingTests.h")]
	[ExcludeFromDocs]
	internal static class OutArrayMarshallingTests
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000035C0 File Offset: 0x000017C0
		public unsafe static void OutArrayOfPrimitiveTypeWorks([Out] int[] array, int value)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (array != null)
				{
					fixed (int[] array2 = array)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				OutArrayMarshallingTests.OutArrayOfPrimitiveTypeWorks_Injected(out blittableArrayWrapper, value);
			}
			finally
			{
				int[] array2;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<int>(ref array2);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003610 File Offset: 0x00001810
		public unsafe static void OutArrayOfStringTypeWorks([Out] string[] array, string value)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = value.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				OutArrayMarshallingTests.OutArrayOfStringTypeWorks_Injected(array, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003668 File Offset: 0x00001868
		public unsafe static void OutArrayOfBlittableStructTypeWorks([Out] StructInt[] array, StructInt value)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (array != null)
				{
					fixed (StructInt[] array2 = array)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				OutArrayMarshallingTests.OutArrayOfBlittableStructTypeWorks_Injected(out blittableArrayWrapper, ref value);
			}
			finally
			{
				StructInt[] array2;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<StructInt>(ref array2);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000036B8 File Offset: 0x000018B8
		public static void OutArrayOfIntPtrObjectTypeWorks([Out] MyIntPtrObject[] array, MyIntPtrObject value)
		{
			OutArrayMarshallingTests.OutArrayOfIntPtrObjectTypeWorks_Injected(array, (value == null) ? ((IntPtr)0) : MyIntPtrObject.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000036DC File Offset: 0x000018DC
		public unsafe static void OutArrayOfNestedBlittableStructTypeWorks([Out] StructNestedBlittable[] array, StructNestedBlittable value)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (array != null)
				{
					fixed (StructNestedBlittable[] array2 = array)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				OutArrayMarshallingTests.OutArrayOfNestedBlittableStructTypeWorks_Injected(out blittableArrayWrapper, ref value);
			}
			finally
			{
				StructNestedBlittable[] array2;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<StructNestedBlittable>(ref array2);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000372C File Offset: 0x0000192C
		public static void OutArrayOfNonBlittableTypeWorks([Out] StructWithStringIntAndFloat[] array, StructWithStringIntAndFloat value)
		{
			OutArrayMarshallingTests.OutArrayOfNonBlittableTypeWorks_Injected(array, ref value);
		}

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfPrimitiveTypeWorks_Injected(out BlittableArrayWrapper array, int value);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfStringTypeWorks_Injected([Out] string[] array, ref ManagedSpanWrapper value);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfBlittableStructTypeWorks_Injected(out BlittableArrayWrapper array, [In] ref StructInt value);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfIntPtrObjectTypeWorks_Injected([Out] MyIntPtrObject[] array, IntPtr value);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfNestedBlittableStructTypeWorks_Injected(out BlittableArrayWrapper array, [In] ref StructNestedBlittable value);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OutArrayOfNonBlittableTypeWorks_Injected([Out] StructWithStringIntAndFloat[] array, [In] ref StructWithStringIntAndFloat value);
	}
}
