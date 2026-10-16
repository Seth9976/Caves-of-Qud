using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class UnityObjectTests
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002B64 File Offset: 0x00000D64
		[NativeThrows]
		public static void ParameterUnityObject(MarshallingTestObject param)
		{
			UnityObjectTests.ParameterUnityObject_Injected(Object.MarshalledUnityObject.Marshal<MarshallingTestObject>(param));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002B7C File Offset: 0x00000D7C
		[NativeThrows]
		public static void ParameterUnityObjectByRef(ref MarshallingTestObject param)
		{
			UnityObjectTests.ParameterUnityObjectByRef_Injected(Object.MarshalledUnityObject.Marshal<MarshallingTestObject>(param));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002B98 File Offset: 0x00000D98
		[NativeThrows]
		public static void ParameterUnityObjectPPtr(MarshallingTestObject param)
		{
			UnityObjectTests.ParameterUnityObjectPPtr_Injected(Object.MarshalledUnityObject.Marshal<MarshallingTestObject>(param));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002BB0 File Offset: 0x00000DB0
		[NativeThrows]
		public static void ParameterStructUnityObject(StructUnityObject param)
		{
			UnityObjectTests.ParameterStructUnityObject_Injected(ref param);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002BC4 File Offset: 0x00000DC4
		[NativeThrows]
		public static void ParameterStructUnityObjectPPtr(StructUnityObjectPPtr param)
		{
			UnityObjectTests.ParameterStructUnityObjectPPtr_Injected(ref param);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002BD8 File Offset: 0x00000DD8
		[NativeThrows]
		public static void ParameterStructUnityObjectDynamicArray(StructUnityObjectDynamicArray param)
		{
			UnityObjectTests.ParameterStructUnityObjectDynamicArray_Injected(ref param);
		}

		// Token: 0x06000095 RID: 149
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterUnityObjectDynamicArray(MarshallingTestObject[] param);

		// Token: 0x06000096 RID: 150
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterUnityObjectPPtrDynamicArray(MarshallingTestObject[] param);

		// Token: 0x06000097 RID: 151 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static MarshallingTestObject ReturnUnityObject()
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(UnityObjectTests.ReturnUnityObject_Injected());
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002C04 File Offset: 0x00000E04
		public static MarshallingTestObject ReturnInUnityObject(MarshallingTestObject obj)
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(UnityObjectTests.ReturnInUnityObject_Injected(Object.MarshalledUnityObject.Marshal<MarshallingTestObject>(obj)));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002C24 File Offset: 0x00000E24
		public static MarshallingTestObject ReturnUnityObjectFakeNull()
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(UnityObjectTests.ReturnUnityObjectFakeNull_Injected());
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static MarshallingTestObject ReturnUnassignedErrorObject()
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(UnityObjectTests.ReturnUnassignedErrorObject_Injected());
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002C54 File Offset: 0x00000E54
		public static MarshallingTestObject ReturnUnityObjectPPtr()
		{
			return Unmarshal.UnmarshalUnityObject<MarshallingTestObject>(UnityObjectTests.ReturnUnityObjectPPtr_Injected());
		}

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MarshallingTestObject[] ReturnUnityObjectDynamicArray();

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MarshallingTestObject[] ReturnUnityObjectPPtrDynamicArray();

		// Token: 0x0600009E RID: 158 RVA: 0x00002C6C File Offset: 0x00000E6C
		public static StructUnityObject ReturnStructUnityObject()
		{
			StructUnityObject structUnityObject;
			UnityObjectTests.ReturnStructUnityObject_Injected(out structUnityObject);
			return structUnityObject;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002C84 File Offset: 0x00000E84
		public static StructUnityObjectPPtr ReturnStructUnityObjectPPtr()
		{
			StructUnityObjectPPtr structUnityObjectPPtr;
			UnityObjectTests.ReturnStructUnityObjectPPtr_Injected(out structUnityObjectPPtr);
			return structUnityObjectPPtr;
		}

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StructUnityObject[] ReturnStructUnityObjectDynamicArray();

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StructUnityObjectPPtr[] ReturnStructUnityObjectPPtrDynamicArray();

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern StructUnityObjectDynamicArray[] ReturnStructUnityObjectDynamicArrayDynamicArray();

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterUnityObject_Injected(IntPtr param);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterUnityObjectByRef_Injected(IntPtr param);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterUnityObjectPPtr_Injected(IntPtr param);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructUnityObject_Injected([In] ref StructUnityObject param);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructUnityObjectPPtr_Injected([In] ref StructUnityObjectPPtr param);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructUnityObjectDynamicArray_Injected([In] ref StructUnityObjectDynamicArray param);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnUnityObject_Injected();

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnInUnityObject_Injected(IntPtr obj);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnUnityObjectFakeNull_Injected();

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnUnassignedErrorObject_Injected();

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ReturnUnityObjectPPtr_Injected();

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructUnityObject_Injected(out StructUnityObject ret);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructUnityObjectPPtr_Injected(out StructUnityObjectPPtr ret);
	}
}
