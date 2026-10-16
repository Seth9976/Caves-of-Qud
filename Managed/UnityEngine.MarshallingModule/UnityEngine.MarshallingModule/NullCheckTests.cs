using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class NullCheckTests
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00002C9C File Offset: 0x00000E9C
		public unsafe static void StringParameterNullAllowed(string param)
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
				NullCheckTests.StringParameterNullAllowed_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public unsafe static void StringParameterNullNotAllowed([NotNull] string param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
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
				NullCheckTests.StringParameterNullNotAllowed_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002D54 File Offset: 0x00000F54
		public unsafe static void ArrayParameterNullAllowed(int[] param)
		{
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				NullCheckTests.ArrayParameterNullAllowed_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002D8C File Offset: 0x00000F8C
		public unsafe static void ArrayParameterNullNotAllowed([NotNull] int[] param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			Span<int> span = new Span<int>(param);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				NullCheckTests.ArrayParameterNullNotAllowed_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002DD4 File Offset: 0x00000FD4
		[NativeThrows]
		public static void ObjectParameterNullAllowed(MarshallingTestObject param)
		{
			NullCheckTests.ObjectParameterNullAllowed_Injected(Object.MarshalledUnityObject.Marshal<MarshallingTestObject>(param));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002DEC File Offset: 0x00000FEC
		public static void ObjectParameterNullNotAllowed([NotNull] MarshallingTestObject param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MarshallingTestObject>(param);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			NullCheckTests.ObjectParameterNullNotAllowed_Injected(intPtr);
		}

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WritableObjectParameterNullAllowed([Writable] MarshallingTestObject param);

		// Token: 0x060000B8 RID: 184 RVA: 0x00002E24 File Offset: 0x00001024
		public static void WritableObjectParameterNullNotAllowed([Writable] [NotNull] MarshallingTestObject param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			NullCheckTests.WritableObjectParameterNullNotAllowed_Injected(param);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002E48 File Offset: 0x00001048
		[NativeThrows]
		public static void IntPtrObjectParameterNullAllowed(MyIntPtrObject param)
		{
			NullCheckTests.IntPtrObjectParameterNullAllowed_Injected((param == null) ? ((IntPtr)0) : MyIntPtrObject.BindingsMarshaller.ConvertToNative(param));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002E6C File Offset: 0x0000106C
		public static void IntPtrObjectParameterNullNotAllowed([NotNull] MyIntPtrObject param)
		{
			if (param == null)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			IntPtr intPtr = MyIntPtrObject.BindingsMarshaller.ConvertToNative(param);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(param, "param");
			}
			NullCheckTests.IntPtrObjectParameterNullNotAllowed_Injected(intPtr);
		}

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StringParameterNullAllowed_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StringParameterNullNotAllowed_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ArrayParameterNullAllowed_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ArrayParameterNullNotAllowed_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjectParameterNullAllowed_Injected(IntPtr param);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjectParameterNullNotAllowed_Injected(IntPtr param);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WritableObjectParameterNullNotAllowed_Injected([Writable] MarshallingTestObject param);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IntPtrObjectParameterNullAllowed_Injected(IntPtr param);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IntPtrObjectParameterNullNotAllowed_Injected(IntPtr param);
	}
}
