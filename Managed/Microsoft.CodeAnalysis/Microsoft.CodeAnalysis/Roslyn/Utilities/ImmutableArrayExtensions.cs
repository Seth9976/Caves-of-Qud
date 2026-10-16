using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableArrayExtensions
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00005F5F File Offset: 0x0000415F
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> ToImmutableArrayOrEmpty<T>([Nullable(new byte[] { 2, 1 })] this IEnumerable<T> items)
		{
			if (items != null)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005F70 File Offset: 0x00004170
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> ToImmutableArrayOrEmpty<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> items)
		{
			if (!items.IsDefault)
			{
				return items;
			}
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005F84 File Offset: 0x00004184
		internal static int BinarySearch<[Nullable(2)] TElement, [Nullable(2)] TValue>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TElement> array, TValue value, Func<TElement, TValue, int> comparer)
		{
			int i = 0;
			int num = array.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Invoke(array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 > 0)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return ~i;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005FD0 File Offset: 0x000041D0
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<TDerived> CastDown<[Nullable(2)] TOriginal, TDerived>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TOriginal> array) where TDerived : class, TOriginal
		{
			return array.CastArray<TDerived>();
		}
	}
}
