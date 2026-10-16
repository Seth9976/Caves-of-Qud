using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableListExtensions
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00005FD9 File Offset: 0x000041D9
		internal static ImmutableList<T> ToImmutableListOrEmpty<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] this T[] items)
		{
			if (items == null)
			{
				return ImmutableList.Create<T>();
			}
			return ImmutableList.Create<T>(items);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005FEA File Offset: 0x000041EA
		internal static ImmutableList<T> ToImmutableListOrEmpty<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] this IEnumerable<T> items)
		{
			if (items == null)
			{
				return ImmutableList.Create<T>();
			}
			return ImmutableList.CreateRange<T>(items);
		}
	}
}
