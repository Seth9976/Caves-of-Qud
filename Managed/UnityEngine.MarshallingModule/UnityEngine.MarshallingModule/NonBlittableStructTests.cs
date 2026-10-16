using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class NonBlittableStructTests
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00003518 File Offset: 0x00001718
		[NativeThrows]
		public static void ParameterStructWithStringIntAndFloat(StructWithStringIntAndFloat param)
		{
			NonBlittableStructTests.ParameterStructWithStringIntAndFloat_Injected(ref param);
		}

		// Token: 0x0600013A RID: 314
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RefParameterStructWithStringIntAndFloat(ref StructWithStringIntAndFloat param);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void OutParameterStructWithStringIntAndFloat(out StructWithStringIntAndFloat param);

		// Token: 0x0600013C RID: 316 RVA: 0x0000352C File Offset: 0x0000172C
		public static void ParameterStructWithStringIntAndFloat2(StructWithStringIntAndFloat2 param)
		{
			NonBlittableStructTests.ParameterStructWithStringIntAndFloat2_Injected(ref param);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003540 File Offset: 0x00001740
		[NativeThrows]
		public static void ParameterStructWithStringIgnoredIntAndFloat(StructWithStringIgnoredIntAndFloat param)
		{
			NonBlittableStructTests.ParameterStructWithStringIgnoredIntAndFloat_Injected(ref param);
		}

		// Token: 0x0600013E RID: 318
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterStructWithStringIntAndFloatArray(StructWithStringIntAndFloat[] param);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StructWithStringIntAndFloat[] ReturnStructWithStringIntAndFloatArray();

		// Token: 0x06000140 RID: 320 RVA: 0x00003554 File Offset: 0x00001754
		[NativeThrows]
		public static void ParameterStructWithNonBlittableArrayField(StructWithNonBlittableArrayField param)
		{
			NonBlittableStructTests.ParameterStructWithNonBlittableArrayField_Injected(ref param);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003568 File Offset: 0x00001768
		public static StructWithNonBlittableArrayField ReturnStructWithNonBlittableArrayField()
		{
			StructWithNonBlittableArrayField structWithNonBlittableArrayField;
			NonBlittableStructTests.ReturnStructWithNonBlittableArrayField_Injected(out structWithNonBlittableArrayField);
			return structWithNonBlittableArrayField;
		}

		// Token: 0x06000142 RID: 322
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CanMarshalManagedObjectToStruct(ClassToStruct param);

		// Token: 0x06000143 RID: 323
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CanMarshalOutManagedObjectToStruct([Out] ClassToStruct param);

		// Token: 0x06000144 RID: 324 RVA: 0x00003580 File Offset: 0x00001780
		[NativeThrows]
		public static void CanMarshalStructWithNativeAsStructField(StructWithClassToStruct param)
		{
			NonBlittableStructTests.CanMarshalStructWithNativeAsStructField_Injected(ref param);
		}

		// Token: 0x06000145 RID: 325
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CanMarshalNativeAsStructArray(ClassToStruct[] param);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ClassToStruct CanUnmarshalManagedObjectFromStruct();

		// Token: 0x06000147 RID: 327 RVA: 0x00003594 File Offset: 0x00001794
		public static StructWithClassToStruct CanUnmarshalStructWithNativeAsStructField()
		{
			StructWithClassToStruct structWithClassToStruct;
			NonBlittableStructTests.CanUnmarshalStructWithNativeAsStructField_Injected(out structWithClassToStruct);
			return structWithClassToStruct;
		}

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ClassToStruct[] CanUnmarshalNativeAsStructArray();

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWithStringIntAndFloat_Injected([In] ref StructWithStringIntAndFloat param);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWithStringIntAndFloat2_Injected([In] ref StructWithStringIntAndFloat2 param);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWithStringIgnoredIntAndFloat_Injected([In] ref StructWithStringIgnoredIntAndFloat param);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWithNonBlittableArrayField_Injected([In] ref StructWithNonBlittableArrayField param);

		// Token: 0x0600014E RID: 334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructWithNonBlittableArrayField_Injected(out StructWithNonBlittableArrayField ret);

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshalStructWithNativeAsStructField_Injected([In] ref StructWithClassToStruct param);

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshalStructWithNativeAsStructField_Injected(out StructWithClassToStruct ret);
	}
}
