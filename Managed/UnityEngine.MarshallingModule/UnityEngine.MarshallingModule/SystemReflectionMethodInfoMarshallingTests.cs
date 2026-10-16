using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Marshalling/SystemReflectionMethodInfoMarshallingTests.h")]
	[ExcludeFromDocs]
	internal static class SystemReflectionMethodInfoMarshallingTests
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00003034 File Offset: 0x00001234
		public static string CanMarshallMethodInfoArgumentToScriptingMethodPtr(MethodInfo param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemReflectionMethodInfoMarshallingTests.CanMarshallMethodInfoArgumentToScriptingMethodPtr_Injected(param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003064 File Offset: 0x00001264
		public static string CanMarshallSystemReflectionMethodInfoStructField(StructSystemReflectionMethodInfo param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemReflectionMethodInfoMarshallingTests.CanMarshallSystemReflectionMethodInfoStructField_Injected(ref param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003098 File Offset: 0x00001298
		public static string[] CanMarshallSystemReflectionMethodInfoArrayStructField(StructSystemReflectionMethodInfoArray param)
		{
			return SystemReflectionMethodInfoMarshallingTests.CanMarshallSystemReflectionMethodInfoArrayStructField_Injected(ref param);
		}

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanMarshallArrayOfMethodInfoArgumentToDynamicArrayOfScriptingMethodInfoObjectPtr(MethodInfo[] param);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanMarshallArrayOfMethodInfoArgumentToDynamicArrayOfScriptingMethodPtr(MethodInfo[] param);

		// Token: 0x060000F8 RID: 248 RVA: 0x000030AC File Offset: 0x000012AC
		public static StructSystemReflectionMethodInfo CanUnmarshallSystemReflectionMethodInfoStructField()
		{
			StructSystemReflectionMethodInfo structSystemReflectionMethodInfo;
			SystemReflectionMethodInfoMarshallingTests.CanUnmarshallSystemReflectionMethodInfoStructField_Injected(out structSystemReflectionMethodInfo);
			return structSystemReflectionMethodInfo;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000030C4 File Offset: 0x000012C4
		public static StructSystemReflectionMethodInfoArray CanUnmarshallSystemReflectionMethodInfoArrayStructField()
		{
			StructSystemReflectionMethodInfoArray structSystemReflectionMethodInfoArray;
			SystemReflectionMethodInfoMarshallingTests.CanUnmarshallSystemReflectionMethodInfoArrayStructField_Injected(out structSystemReflectionMethodInfoArray);
			return structSystemReflectionMethodInfoArray;
		}

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodInfo CanUnmarshallScriptingMethodInfoObjectPtrToMethodInfo();

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodInfo CanUnmarshallScriptingMethodPtrToMethodInfo();

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodInfo[] CanUnmarshallScriptingArrayPtrToMethodInfoArray();

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodInfo[] CanUnmarshallArrayOfScriptingMethodInfoObjectPtrToMethodInfoArray();

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodInfo[] CanUnmarshallArrayOfScriptingMethodPtrToMethodInfoArray();

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallMethodInfoArgumentToScriptingMethodPtr_Injected(MethodInfo param, out ManagedSpanWrapper ret);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallSystemReflectionMethodInfoStructField_Injected([In] ref StructSystemReflectionMethodInfo param, out ManagedSpanWrapper ret);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] CanMarshallSystemReflectionMethodInfoArrayStructField_Injected([In] ref StructSystemReflectionMethodInfoArray param);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemReflectionMethodInfoStructField_Injected(out StructSystemReflectionMethodInfo ret);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemReflectionMethodInfoArrayStructField_Injected(out StructSystemReflectionMethodInfoArray ret);
	}
}
