using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class ExceptionTypeTests
	{
		// Token: 0x06000126 RID: 294 RVA: 0x000031C0 File Offset: 0x000013C0
		[NativeThrows]
		public unsafe static void NullReferenceException(string nativeFormat, string values)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(nativeFormat, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = nativeFormat.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(values, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = values.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ExceptionTypeTests.NullReferenceException_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003248 File Offset: 0x00001448
		[NativeThrows]
		public unsafe static void ArgumentNullException(string argumentName)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(argumentName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = argumentName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ExceptionTypeTests.ArgumentNullException_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000329C File Offset: 0x0000149C
		[NativeThrows]
		public unsafe static void ArgumentException(string nativeFormat, string values)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(nativeFormat, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = nativeFormat.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(values, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = values.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ExceptionTypeTests.ArgumentException_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003324 File Offset: 0x00001524
		[NativeThrows]
		public unsafe static void InvalidOperationException(string nativeFormat, string values)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(nativeFormat, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = nativeFormat.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(values, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = values.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ExceptionTypeTests.InvalidOperationException_Injected(ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000033AC File Offset: 0x000015AC
		[NativeThrows]
		public unsafe static void IndexOutOfRangeException(string nativeFormat, int index)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(nativeFormat, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = nativeFormat.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ExceptionTypeTests.IndexOutOfRangeException_Injected(ref managedSpanWrapper, index);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NullReferenceException_Injected(ref ManagedSpanWrapper nativeFormat, ref ManagedSpanWrapper values);

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ArgumentNullException_Injected(ref ManagedSpanWrapper argumentName);

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ArgumentException_Injected(ref ManagedSpanWrapper nativeFormat, ref ManagedSpanWrapper values);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvalidOperationException_Injected(ref ManagedSpanWrapper nativeFormat, ref ManagedSpanWrapper values);

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IndexOutOfRangeException_Injected(ref ManagedSpanWrapper nativeFormat, int index);
	}
}
