using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/Marshalling/SystemTypeMarshallingTests.h")]
	[ExcludeFromDocs]
	internal static class SystemTypeMarshallingTests
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00002EE4 File Offset: 0x000010E4
		public static string CanMarshallSystemTypeArgumentToScriptingClassPtr(Type param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemTypeMarshallingTests.CanMarshallSystemTypeArgumentToScriptingClassPtr_Injected(param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002F14 File Offset: 0x00001114
		public static string CanMarshallSystemTypeStructField(StructSystemType param)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemTypeMarshallingTests.CanMarshallSystemTypeStructField_Injected(ref param, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002F48 File Offset: 0x00001148
		public static string[] CanMarshallSystemTypeArrayStructField(StructSystemTypeArray param)
		{
			return SystemTypeMarshallingTests.CanMarshallSystemTypeArrayStructField_Injected(ref param);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002F5C File Offset: 0x0000115C
		public static StructSystemType CanUnmarshallSystemTypeStructField()
		{
			StructSystemType structSystemType;
			SystemTypeMarshallingTests.CanUnmarshallSystemTypeStructField_Injected(out structSystemType);
			return structSystemType;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002F74 File Offset: 0x00001174
		public static StructSystemTypeArray CanUnmarshallSystemTypeArrayStructField()
		{
			StructSystemTypeArray structSystemTypeArray;
			SystemTypeMarshallingTests.CanUnmarshallSystemTypeArrayStructField_Injected(out structSystemTypeArray);
			return structSystemTypeArray;
		}

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanUnmarshallArrayOfSystemTypeArgumentToDynamicArrayOfScriptingSystemTypeObjectPtr(Type[] param);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanUnmarshallArrayOfSystemTypeArgumentToDynamicArrayOfUnityType(Type[] param);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] CanUnmarshallArrayOfSystemTypeArgumentToDynamicArrayOfScriptingClassPtr(Type[] param);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type CanUnmarshallScriptingSystemTypeObjectPtrToSystemType();

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type CanUnmarshallUnityTypeToSystemType();

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type CanUnmarshallScriptingClassPtrToSystemType();

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type[] CanUnmarshallScriptingArrayPtrToSystemTypeArray();

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type[] CanUnmarshallArrayOfScriptingSystemTypeObjectPtrToSystemTypeArray();

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type[] CanUnmarshallArrayOfUnityTypeToSystemTypeArray();

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type[] CanUnmarshallArrayOfScriptingClassPtrToSystemTypeArray();

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallSystemTypeArgumentToScriptingClassPtr_Injected(Type param, out ManagedSpanWrapper ret);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanMarshallSystemTypeStructField_Injected([In] ref StructSystemType param, out ManagedSpanWrapper ret);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] CanMarshallSystemTypeArrayStructField_Injected([In] ref StructSystemTypeArray param);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemTypeStructField_Injected(out StructSystemType ret);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CanUnmarshallSystemTypeArrayStructField_Injected(out StructSystemTypeArray ret);
	}
}
