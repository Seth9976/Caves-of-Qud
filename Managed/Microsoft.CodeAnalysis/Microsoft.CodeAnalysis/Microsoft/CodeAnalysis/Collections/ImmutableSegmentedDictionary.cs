using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A2 RID: 1186
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableSegmentedDictionary
	{
		// Token: 0x06002ED0 RID: 11984 RVA: 0x00083E7E File Offset: 0x0008207E
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x00083E85 File Offset: 0x00082085
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.WithComparer(keyComparer);
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x00083E94 File Offset: 0x00082094
		public static ImmutableSegmentedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSegmentedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00083EB0 File Offset: 0x000820B0
		public static ImmutableSegmentedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableSegmentedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00083ECB File Offset: 0x000820CB
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x00083ED8 File Offset: 0x000820D8
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.WithComparer(keyComparer).AddRange(items);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00083EF9 File Offset: 0x000820F9
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> ToImmutableSegmentedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return items.ToImmutableSegmentedDictionary<TKey, TValue>(null);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00083F02 File Offset: 0x00082102
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> ToImmutableSegmentedDictionary<TKey, [Nullable(2)] TValue>(this ImmutableSegmentedDictionary<TKey, TValue>.Builder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00083F18 File Offset: 0x00082118
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> ToImmutableSegmentedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (items is ImmutableSegmentedDictionary<TKey, TValue>)
			{
				return ((ImmutableSegmentedDictionary<TKey, TValue>)items).WithComparer(keyComparer);
			}
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.WithComparer(keyComparer).AddRange(items);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00083F5F File Offset: 0x0008215F
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> ToImmutableSegmentedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSegmentedDictionary<TSource, TKey, TValue>(keySelector, elementSelector, null);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x00083F6C File Offset: 0x0008216C
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TValue> ToImmutableSegmentedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			return ImmutableSegmentedDictionary<TKey, TValue>.Empty.WithComparer(keyComparer).AddRange(Enumerable.Select<TSource, KeyValuePair<TKey, TValue>>(source, (TSource element) => new KeyValuePair<TKey, TValue>(keySelector.Invoke(element), elementSelector.Invoke(element))));
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00083FE6 File Offset: 0x000821E6
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TSource> ToImmutableSegmentedDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToImmutableSegmentedDictionary<TSource, TKey, TSource>(keySelector, (TSource x) => x, null);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x0008400F File Offset: 0x0008220F
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public static ImmutableSegmentedDictionary<TKey, TSource> ToImmutableSegmentedDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableSegmentedDictionary<TSource, TKey, TSource>(keySelector, (TSource x) => x, keyComparer);
		}
	}
}
