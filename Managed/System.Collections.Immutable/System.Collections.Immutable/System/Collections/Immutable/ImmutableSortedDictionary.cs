using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableSortedDictionary`2" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedDictionary
	{
		/// <summary>Creates an empty immutable sorted dictionary.</summary>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable sorted dictionary.</returns>
		// Token: 0x06000299 RID: 665 RVA: 0x00007886 File Offset: 0x00005A86
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		/// <summary>Creates an empty immutable sorted dictionary that uses the specified key comparer.</summary>
		/// <param name="keyComparer">The implementation to use to determine the equality of keys in the dictionary.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable sorted dictionary.</returns>
		// Token: 0x0600029A RID: 666 RVA: 0x0000788D File Offset: 0x00005A8D
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		/// <summary>Creates an empty immutable sorted dictionary that uses the specified key and value comparers.</summary>
		/// <param name="keyComparer">The implementation to use to determine the equality of keys in the dictionary.</param>
		/// <param name="valueComparer">The implementation to use to determine the equality of values in the dictionary.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable sorted dictionary.</returns>
		// Token: 0x0600029B RID: 667 RVA: 0x0000789A File Offset: 0x00005A9A
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		/// <summary>Creates an immutable sorted dictionary that contains the specified items and uses the default comparer.</summary>
		/// <param name="items">The items to add to the sorted dictionary before it's immutable.</param>
		/// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the specified items.</returns>
		// Token: 0x0600029C RID: 668 RVA: 0x000078A8 File Offset: 0x00005AA8
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		/// <summary>Creates a new immutable sorted dictionary from the specified range of items with the specified key comparer.</summary>
		/// <param name="keyComparer">The comparer implementation to use to evaluate keys for equality and sorting.</param>
		/// <param name="items">The items to add to the sorted dictionary.</param>
		/// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
		/// <returns>The new immutable sorted dictionary that contains the specified items and uses the specified key comparer.</returns>
		// Token: 0x0600029D RID: 669 RVA: 0x000078B5 File Offset: 0x00005AB5
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		/// <summary>Creates a new immutable sorted dictionary from the specified range of items with the specified key and value comparers.</summary>
		/// <param name="keyComparer">The comparer implementation to use to compare keys for equality and sorting.</param>
		/// <param name="valueComparer">The comparer implementation to use to compare values for equality.</param>
		/// <param name="items">The items to add to the sorted dictionary before it's immutable.</param>
		/// <typeparam name="TKey">The type of keys stored in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the specified items and uses the specified comparers.</returns>
		// Token: 0x0600029E RID: 670 RVA: 0x000078C8 File Offset: 0x00005AC8
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		/// <summary>Creates a new immutable sorted dictionary builder.</summary>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The immutable collection builder.</returns>
		// Token: 0x0600029F RID: 671 RVA: 0x000078DC File Offset: 0x00005ADC
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		/// <summary>Creates a new immutable sorted dictionary builder.</summary>
		/// <param name="keyComparer">The key comparer.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The immutable collection builder.</returns>
		// Token: 0x060002A0 RID: 672 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		/// <summary>Creates a new immutable sorted dictionary builder.</summary>
		/// <param name="keyComparer">The key comparer.</param>
		/// <param name="valueComparer">The value comparer.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The immutable collection builder.</returns>
		// Token: 0x060002A1 RID: 673 RVA: 0x000078F5 File Offset: 0x00005AF5
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable sorted dictionary of its contents by using the specified key and value comparers.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <param name="keyComparer">The key comparer to use for the dictionary.</param>
		/// <param name="valueComparer">The value comparer to use for the dictionary.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x060002A2 RID: 674 RVA: 0x00007904 File Offset: 0x00005B04
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(Enumerable.Select<TSource, KeyValuePair<TKey, TValue>>(source, (TSource element) => new KeyValuePair<TKey, TValue>(keySelector.Invoke(element), elementSelector.Invoke(element))));
		}

		/// <summary>Creates an immutable sorted dictionary from the current contents of the builder's dictionary.</summary>
		/// <param name="builder">The builder to create the immutable sorted dictionary from.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the current contents in the builder's dictionary.</returns>
		// Token: 0x060002A3 RID: 675 RVA: 0x00007974 File Offset: 0x00005B74
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>(this ImmutableSortedDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable sorted dictionary of its contents by using the specified key comparer.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <param name="keyComparer">The key comparer to use for the dictionary.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x060002A4 RID: 676 RVA: 0x00007987 File Offset: 0x00005B87
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary<TSource, TKey, TValue>(keySelector, elementSelector, keyComparer, null);
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable sorted dictionary of its contents.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x060002A5 RID: 677 RVA: 0x00007993 File Offset: 0x00005B93
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary<TSource, TKey, TValue>(keySelector, elementSelector, null, null);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable sorted dictionary of its contents by using the specified key and value comparers.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <param name="keyComparer">The key comparer to use when building the immutable dictionary.</param>
		/// <param name="valueComparer">The value comparer to use for the immutable dictionary.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x060002A6 RID: 678 RVA: 0x000079A0 File Offset: 0x00005BA0
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = source as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable dictionary of its contents by using the specified key comparer.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <param name="keyComparer">The key comparer to use when building the immutable dictionary.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x060002A7 RID: 679 RVA: 0x000079DD File Offset: 0x00005BDD
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary<TKey, TValue>(keyComparer, null);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable sorted dictionary of its contents.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable sorted dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x060002A8 RID: 680 RVA: 0x000079E7 File Offset: 0x00005BE7
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary<TKey, TValue>(null, null);
		}
	}
}
