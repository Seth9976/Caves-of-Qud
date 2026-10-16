using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200036D RID: 877
	[VisibleToOtherModules]
	internal readonly ref struct ManagedSpanWrapper
	{
		// Token: 0x060028CB RID: 10443 RVA: 0x0004EC06 File Offset: 0x0004CE06
		public unsafe ManagedSpanWrapper(void* begin, int length)
		{
			this.begin = begin;
			this.length = length;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0004EC18 File Offset: 0x0004CE18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> ToSpan<T>(ManagedSpanWrapper spanWrapper)
		{
			return new Span<T>(spanWrapper.begin, spanWrapper.length);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0004EC3C File Offset: 0x0004CE3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<T> ToReadOnlySpan<T>(ManagedSpanWrapper spanWrapper)
		{
			return new ReadOnlySpan<T>(spanWrapper.begin, spanWrapper.length);
		}

		// Token: 0x04000B8F RID: 2959
		public unsafe readonly void* begin;

		// Token: 0x04000B90 RID: 2960
		public readonly int length;
	}
}
