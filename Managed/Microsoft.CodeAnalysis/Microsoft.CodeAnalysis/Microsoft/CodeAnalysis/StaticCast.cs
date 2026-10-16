using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000121 RID: 289
	internal static class StaticCast<[Nullable(2)] T>
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x0001D48A File Offset: 0x0001B68A
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> From<TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> from) where TDerived : class, T
		{
			return ImmutableArray<T>.CastUp<TDerived>(from);
		}
	}
}
