using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class IntPtrObjectTests
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000029EC File Offset: 0x00000BEC
		[NativeThrows]
		public static void ParameterIntPtrObject(MyIntPtrObject param)
		{
			IntPtrObjectTests.ParameterIntPtrObject_Injected((param == null) ? ((IntPtr)0) : MyIntPtrObject.BindingsMarshaller.ConvertToNative(param));
		}

		// Token: 0x06000074 RID: 116
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterIntPtrObjectDynamicArray(MyIntPtrObject[] param);

		// Token: 0x06000075 RID: 117 RVA: 0x00002A10 File Offset: 0x00000C10
		[NativeThrows]
		public static void ParameterStructIntPtrObject(StructIntPtrObject param)
		{
			IntPtrObjectTests.ParameterStructIntPtrObject_Injected(ref param);
		}

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MyIntPtrObject[] ReturnIntPtrObjectDynamicArray();

		// Token: 0x06000077 RID: 119 RVA: 0x00002A24 File Offset: 0x00000C24
		[NativeThrows]
		public static void ParameterStructIntPtrObjectDynamicArray(StructIntPtrObjectDynamicArray param)
		{
			IntPtrObjectTests.ParameterStructIntPtrObjectDynamicArray_Injected(ref param);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002A38 File Offset: 0x00000C38
		public static MyIntPtrObject ReturnIntPtrObject(int value)
		{
			IntPtr intPtr = IntPtrObjectTests.ReturnIntPtrObject_Injected(value);
			return (intPtr == 0) ? null : MyIntPtrObject.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntPtrObject_Injected(IntPtr param);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructIntPtrObject_Injected([In] ref StructIntPtrObject param);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructIntPtrObjectDynamicArray_Injected([In] ref StructIntPtrObjectDynamicArray param);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnIntPtrObject_Injected(int value);
	}
}
