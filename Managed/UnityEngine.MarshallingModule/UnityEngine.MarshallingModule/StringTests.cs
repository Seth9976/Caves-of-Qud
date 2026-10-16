using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class StringTests
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002224 File Offset: 0x00000424
		public unsafe static void SetTestOutString(string testString)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(testString, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = testString.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.SetTestOutString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002278 File Offset: 0x00000478
		[NativeThrows]
		public unsafe static void ParameterICallString(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterICallString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022CC File Offset: 0x000004CC
		[NativeThrows]
		public unsafe static void ParameterICallNullString(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterICallNullString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002320 File Offset: 0x00000520
		[NativeThrows]
		public unsafe static void ParameterCoreString(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterCoreString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002374 File Offset: 0x00000574
		[NativeThrows]
		public unsafe static void ParameterConstCharPtr(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterConstCharPtr_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023C8 File Offset: 0x000005C8
		[NativeThrows]
		public unsafe static void ParameterConstCharPtrNull(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterConstCharPtrNull_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000241C File Offset: 0x0000061C
		[NativeThrows]
		public unsafe static void ParameterConstCharPtrEmptyString(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterConstCharPtrEmptyString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600001E RID: 30
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterCoreStringVector(string[] param);

		// Token: 0x0600001F RID: 31
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterCoreStringDynamicArray(string[] param);

		// Token: 0x06000020 RID: 32 RVA: 0x00002470 File Offset: 0x00000670
		[NativeThrows]
		public static void ParameterStructCoreString(StructCoreString param)
		{
			StringTests.ParameterStructCoreString_Injected(ref param);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002484 File Offset: 0x00000684
		[NativeThrows]
		public static void ParameterStructCoreStringVector(StructCoreStringVector param)
		{
			StringTests.ParameterStructCoreStringVector_Injected(ref param);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002498 File Offset: 0x00000698
		[NativeThrows]
		public static StructCoreString TestCoreStringViaProxy(StructCoreString param)
		{
			StructCoreString structCoreString;
			StringTests.TestCoreStringViaProxy_Injected(ref param, out structCoreString);
			return structCoreString;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B0 File Offset: 0x000006B0
		public static string ReturnCoreString()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ReturnCoreString_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024E0 File Offset: 0x000006E0
		public static string ReturnCoreStringRef()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ReturnCoreStringRef_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002510 File Offset: 0x00000710
		public static string ReturnConstCharPtr()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ReturnConstCharPtr_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] ReturnCoreStringVector();

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] ReturnCoreStringDynamicArray();

		// Token: 0x06000028 RID: 40
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] ReturnNullStringDynamicArray();

		// Token: 0x06000029 RID: 41 RVA: 0x00002540 File Offset: 0x00000740
		public static StructCoreString ReturnStructCoreString()
		{
			StructCoreString structCoreString;
			StringTests.ReturnStructCoreString_Injected(out structCoreString);
			return structCoreString;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002558 File Offset: 0x00000758
		[NativeConditional("FOO")]
		public static string FalseConditional()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.FalseConditional_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002588 File Offset: 0x00000788
		public static StructCoreStringVector ReturnStructCoreStringVector()
		{
			StructCoreStringVector structCoreStringVector;
			StringTests.ReturnStructCoreStringVector_Injected(out structCoreStringVector);
			return structCoreStringVector;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025A0 File Offset: 0x000007A0
		[NativeThrows]
		public static void ParameterOutString(out string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ParameterOutString_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025D0 File Offset: 0x000007D0
		[NativeThrows]
		public static void ParameterOutStringInNull(out string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ParameterOutStringInNull_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002600 File Offset: 0x00000800
		[NativeThrows]
		public static void ParameterOutStringNotSet(out string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				StringTests.ParameterOutStringNotSet_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002630 File Offset: 0x00000830
		[NativeThrows]
		public unsafe static void ParameterRefString(ref string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterRefString_Injected(ref managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				char* ptr = null;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002690 File Offset: 0x00000890
		[NativeThrows]
		public unsafe static void ParameterRefStringInNull(ref string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterRefStringInNull_Injected(ref managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				char* ptr = null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026F0 File Offset: 0x000008F0
		[NativeThrows]
		public unsafe static void ParameterRefStringNotSet(ref string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				StringTests.ParameterRefStringNotSet_Injected(ref managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				param = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				char* ptr = null;
			}
		}

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTestOutString_Injected(ref ManagedSpanWrapper testString);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterICallString_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterICallNullString_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterCoreString_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterConstCharPtr_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterConstCharPtrNull_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterConstCharPtrEmptyString_Injected(ref ManagedSpanWrapper param);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructCoreString_Injected([In] ref StructCoreString param);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructCoreStringVector_Injected([In] ref StructCoreStringVector param);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TestCoreStringViaProxy_Injected([In] ref StructCoreString param, out StructCoreString ret);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnCoreString_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnCoreStringRef_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnConstCharPtr_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructCoreString_Injected(out StructCoreString ret);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FalseConditional_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnStructCoreStringVector_Injected(out StructCoreStringVector ret);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterOutString_Injected(out ManagedSpanWrapper param);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterOutStringInNull_Injected(out ManagedSpanWrapper param);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterOutStringNotSet_Injected(out ManagedSpanWrapper param);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterRefString_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterRefStringInNull_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterRefStringNotSet_Injected(ref ManagedSpanWrapper param);
	}
}
