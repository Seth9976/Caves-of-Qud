using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Marshalling/SystemReflectionFieldInfoMarshallingTests.h")]
	[ExcludeFromDocs]
	internal static class SystemReflectionFieldInfoMarshallingTests
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00002F8C File Offset: 0x0000118C
		public static string CanMarshallFieldInfoArgumentToScriptingFieldPtr(FieldInfo param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemReflectionFieldInfoMarshallingTests.CanMarshallFieldInfoArgumentToScriptingFieldPtr_Injected(param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002FBC File Offset: 0x000011BC
		public static string CanMarshallSystemReflectionFieldInfoStructField(StructSystemReflectionFieldInfo param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemReflectionFieldInfoMarshallingTests.CanMarshallSystemReflectionFieldInfoStructField_Injected(ref param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static string[] CanMarshallSystemReflectionFieldInfoArrayStructField(StructSystemReflectionFieldInfoArray param)
		{
			return SystemReflectionFieldInfoMarshallingTests.CanMarshallSystemReflectionFieldInfoArrayStructField_Injected(ref param);
		}

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanMarshallArrayOfFieldInfoArgumentToDynamicArrayOfScriptingFieldInfoObjectPtr(FieldInfo[] param);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanMarshallArrayOfFieldInfoArgumentToDynamicArrayOfScriptingFieldPtr(FieldInfo[] param);

		// Token: 0x060000E7 RID: 231 RVA: 0x00003004 File Offset: 0x00001204
		public static StructSystemReflectionFieldInfo CanUnmarshallSystemReflectionFieldInfoStructField()
		{
			StructSystemReflectionFieldInfo structSystemReflectionFieldInfo;
			SystemReflectionFieldInfoMarshallingTests.CanUnmarshallSystemReflectionFieldInfoStructField_Injected(out structSystemReflectionFieldInfo);
			return structSystemReflectionFieldInfo;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000301C File Offset: 0x0000121C
		public static StructSystemReflectionFieldInfoArray CanUnmarshallSystemReflectionFieldInfoArrayStructField()
		{
			StructSystemReflectionFieldInfoArray structSystemReflectionFieldInfoArray;
			SystemReflectionFieldInfoMarshallingTests.CanUnmarshallSystemReflectionFieldInfoArrayStructField_Injected(out structSystemReflectionFieldInfoArray);
			return structSystemReflectionFieldInfoArray;
		}

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FieldInfo CanUnmarshallScriptingFieldInfoObjectPtrToFieldInfo();

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FieldInfo CanUnmarshallScriptingFieldPtrToFieldInfo();

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FieldInfo[] CanUnmarshallScriptingArrayPtrToFieldInfoArray();

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FieldInfo[] CanUnmarshallArrayOfScriptingFieldInfoObjectPtrToFieldInfoArray();

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern FieldInfo[] CanUnmarshallArrayOfScriptingFieldPtrToFieldInfoArray();

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallFieldInfoArgumentToScriptingFieldPtr_Injected(FieldInfo param, out ManagedSpanWrapper ret);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallSystemReflectionFieldInfoStructField_Injected([In] ref StructSystemReflectionFieldInfo param, out ManagedSpanWrapper ret);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] CanMarshallSystemReflectionFieldInfoArrayStructField_Injected([In] ref StructSystemReflectionFieldInfoArray param);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemReflectionFieldInfoStructField_Injected(out StructSystemReflectionFieldInfo ret);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemReflectionFieldInfoArrayStructField_Injected(out StructSystemReflectionFieldInfoArray ret);
	}
}
