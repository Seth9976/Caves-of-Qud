using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	[NativeType("Modules/Marshalling/MarshallingTests.h")]
	internal class ValueTypeSpanTests
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00003B9C File Offset: 0x00001D9C
		[NativeThrows]
		public unsafe static void ParameterIntReadOnlySpan(ReadOnlySpan<int> param)
		{
			ReadOnlySpan<int> readOnlySpan = param;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				ValueTypeSpanTests.ParameterIntReadOnlySpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003BD0 File Offset: 0x00001DD0
		[NativeThrows]
		public unsafe static void ParameterIntSpan(Span<int> param)
		{
			Span<int> span = param;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ValueTypeSpanTests.ParameterIntSpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003C04 File Offset: 0x00001E04
		[NativeThrows]
		public unsafe static void ParameterBoolReadOnlySpan(ReadOnlySpan<bool> param)
		{
			ReadOnlySpan<bool> readOnlySpan = param;
			fixed (bool* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				ValueTypeSpanTests.ParameterBoolReadOnlySpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003C38 File Offset: 0x00001E38
		[NativeThrows]
		public unsafe static void ParameterCharReadOnlySpan(ReadOnlySpan<char> param)
		{
			ReadOnlySpan<char> readOnlySpan = param;
			fixed (char* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				ValueTypeSpanTests.ParameterCharReadOnlySpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003C6C File Offset: 0x00001E6C
		[NativeThrows]
		public unsafe static void ParameterEnumReadOnlySpan(ReadOnlySpan<SomeEnum> param)
		{
			ReadOnlySpan<SomeEnum> readOnlySpan = param;
			fixed (SomeEnum* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				ValueTypeSpanTests.ParameterEnumReadOnlySpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003CA0 File Offset: 0x00001EA0
		[NativeThrows]
		public unsafe static void ParameterBlittableCornerCaseStructReadOnlySpan(ReadOnlySpan<BlittableCornerCases> param)
		{
			ReadOnlySpan<BlittableCornerCases> readOnlySpan = param;
			fixed (BlittableCornerCases* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				ValueTypeSpanTests.ParameterBlittableCornerCaseStructReadOnlySpan_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public static Span<int> ReturnsArrayRefWritableAsSpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsArrayRefWritableAsSpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToSpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static Span<int> ReturnsCoreVectorRefAsSpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsCoreVectorRefAsSpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToSpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003D14 File Offset: 0x00001F14
		public static Span<int> ReturnsScriptingSpanAsSpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsScriptingSpanAsSpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToSpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003D34 File Offset: 0x00001F34
		public static ReadOnlySpan<int> ReturnsArrayRefWritableAsReadOnlySpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsArrayRefWritableAsReadOnlySpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToReadOnlySpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003D54 File Offset: 0x00001F54
		public static ReadOnlySpan<int> ReturnsCoreVectorRefAsReadOnlySpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsCoreVectorRefAsReadOnlySpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToReadOnlySpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003D74 File Offset: 0x00001F74
		public static ReadOnlySpan<int> ReturnsArrayRefAsReadOnlySpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsArrayRefAsReadOnlySpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToReadOnlySpan<int>(managedSpanWrapper);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003D94 File Offset: 0x00001F94
		public static ReadOnlySpan<int> ReturnsScriptingReadOnlySpanAsSpan(int val1, int val2, int val3)
		{
			ManagedSpanWrapper managedSpanWrapper;
			ValueTypeSpanTests.ReturnsScriptingReadOnlySpanAsSpan_Injected(val1, val2, val3, out managedSpanWrapper);
			return ManagedSpanWrapper.ToReadOnlySpan<int>(managedSpanWrapper);
		}

		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntReadOnlySpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A2 RID: 418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterIntSpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterBoolReadOnlySpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterCharReadOnlySpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterEnumReadOnlySpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterBlittableCornerCaseStructReadOnlySpan_Injected(ref ManagedSpanWrapper param);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsArrayRefWritableAsSpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsCoreVectorRefAsSpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsScriptingSpanAsSpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsArrayRefWritableAsReadOnlySpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsCoreVectorRefAsReadOnlySpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsArrayRefAsReadOnlySpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReturnsScriptingReadOnlySpanAsSpan_Injected(int val1, int val2, int val3, out ManagedSpanWrapper ret);
	}
}
