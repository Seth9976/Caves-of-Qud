using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableDictionary`2" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableDictionary
	{
		/// <summary>Creates an empty immutable dictionary.</summary>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable dictionary.</returns>
		// Token: 0x06000180 RID: 384 RVA: 0x000054A3 File Offset: 0x000036A3
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		/// <summary>Creates an empty immutable dictionary that uses the specified key comparer.</summary>
		/// <param name="keyComparer">The implementation to use to determine the equality of keys in the dictionary.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable dictionary.</returns>
		// Token: 0x06000181 RID: 385 RVA: 0x000054AA File Offset: 0x000036AA
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		/// <summary>Creates an empty immutable dictionary that uses the specified key and value comparers.</summary>
		/// <param name="keyComparer">The implementation to use to determine the equality of keys in the dictionary.</param>
		/// <param name="valueComparer">The implementation to use to determine the equality of values in the dictionary.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>An empty immutable dictionary.</returns>
		// Token: 0x06000182 RID: 386 RVA: 0x000054B7 File Offset: 0x000036B7
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		/// <summary>Creates a new immutable dictionary that contains the specified items.</summary>
		/// <param name="items">The items used to populate the dictionary before it's immutable.</param>
		/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
		/// <returns>A new immutable dictionary that contains the specified items.</returns>
		// Token: 0x06000183 RID: 387 RVA: 0x000054C5 File Offset: 0x000036C5
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		/// <summary>Creates a new immutable dictionary that contains the specified items and uses the specified key comparer.</summary>
		/// <param name="keyComparer">The comparer implementation to use to compare keys for equality.</param>
		/// <param name="items">The items to add to the dictionary before it's immutable.</param>
		/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
		/// <returns>A new immutable dictionary that contains the specified items and uses the specified comparer.</returns>
		// Token: 0x06000184 RID: 388 RVA: 0x000054D2 File Offset: 0x000036D2
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		/// <summary>Creates a new immutable dictionary that contains the specified items and uses the specified key comparer.</summary>
		/// <param name="keyComparer">The comparer implementation to use to compare keys for equality.</param>
		/// <param name="valueComparer">The comparer implementation to use to compare values for equality.</param>
		/// <param name="items">The items to add to the dictionary before it's immutable.</param>
		/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
		/// <returns>A new immutable dictionary that contains the specified items and uses the specified comparer.</returns>
		// Token: 0x06000185 RID: 389 RVA: 0x000054E5 File Offset: 0x000036E5
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		/// <summary>Creates a new immutable dictionary builder.</summary>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The new builder.</returns>
		// Token: 0x06000186 RID: 390 RVA: 0x000054F9 File Offset: 0x000036F9
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary.Create<TKey, TValue>().ToBuilder();
		}

		/// <summary>Creates a new immutable dictionary builder.</summary>
		/// <param name="keyComparer">The key comparer.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The new builder.</returns>
		// Token: 0x06000187 RID: 391 RVA: 0x00005505 File Offset: 0x00003705
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		/// <summary>Creates a new immutable dictionary builder.</summary>
		/// <param name="keyComparer">The key comparer.</param>
		/// <param name="valueComparer">The value comparer.</param>
		/// <typeparam name="TKey">The type of keys stored by the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of values stored by the dictionary.</typeparam>
		/// <returns>The new builder.</returns>
		// Token: 0x06000188 RID: 392 RVA: 0x00005512 File Offset: 0x00003712
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable dictionary of its contents by using the specified key and value comparers.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <param name="keyComparer">The key comparer to use for the dictionary.</param>
		/// <param name="valueComparer">The value comparer to use for the dictionary.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x06000189 RID: 393 RVA: 0x00005520 File Offset: 0x00003720
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(Enumerable.Select<TSource, KeyValuePair<TKey, TValue>>(source, (TSource element) => new KeyValuePair<TKey, TValue>(keySelector.Invoke(element), elementSelector.Invoke(element))));
		}

		/// <summary>Creates an immutable dictionary from the current contents of the builder's dictionary.</summary>
		/// <param name="builder">The builder to create the immutable dictionary from.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the current contents in the builder's dictionary.</returns>
		// Token: 0x0600018A RID: 394 RVA: 0x00005590 File Offset: 0x00003790
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>(this ImmutableDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable dictionary of its contents by using the specified key comparer.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <param name="keyComparer">The key comparer to use for the dictionary.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x0600018B RID: 395 RVA: 0x000055A3 File Offset: 0x000037A3
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary<TSource, TKey, TValue>(keySelector, elementSelector, keyComparer, null);
		}

		/// <summary>Constructs an immutable dictionary from an existing collection of elements, applying a transformation function to the source keys.</summary>
		/// <param name="source">The source collection used to generate the immutable dictionary.</param>
		/// <param name="keySelector">The function used to transform keys for the immutable dictionary.</param>
		/// <typeparam name="TSource">The type of element in the source collection.</typeparam>
		/// <typeparam name="TKey">The type of key in the resulting immutable dictionary.</typeparam>
		/// <returns>The immutable dictionary that contains elements from <paramref name="source" />, with keys transformed by applying <paramref name="keySelector" />.</returns>
		// Token: 0x0600018C RID: 396 RVA: 0x000055AF File Offset: 0x000037AF
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToImmutableDictionary<TSource, TKey, TSource>(keySelector, (TSource v) => v, null, null);
		}

		/// <summary>Constructs an immutable dictionary based on some transformation of a sequence.</summary>
		/// <param name="source">The source collection used to generate the immutable dictionary.</param>
		/// <param name="keySelector">The function used to transform keys for the immutable dictionary.</param>
		/// <param name="keyComparer">The key comparer to use for the dictionary.</param>
		/// <typeparam name="TSource">The type of element in the source collection.</typeparam>
		/// <typeparam name="TKey">The type of key in the resulting immutable dictionary.</typeparam>
		/// <returns>The immutable dictionary that contains elements from <paramref name="source" />, with keys transformed by applying <paramref name="keySelector" />.</returns>
		// Token: 0x0600018D RID: 397 RVA: 0x000055D9 File Offset: 0x000037D9
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary<TSource, TKey, TSource>(keySelector, (TSource v) => v, keyComparer, null);
		}

		/// <summary>Enumerates and transforms a sequence, and produces an immutable dictionary of its contents.</summary>
		/// <param name="source">The sequence to enumerate to generate the dictionary.</param>
		/// <param name="keySelector">The function that will produce the key for the dictionary from each sequence element.</param>
		/// <param name="elementSelector">The function that will produce the value for the dictionary from each sequence element.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys in the resulting dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the resulting dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the items in the specified sequence.</returns>
		// Token: 0x0600018E RID: 398 RVA: 0x00005603 File Offset: 0x00003803
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableDictionary<TSource, TKey, TValue>(keySelector, elementSelector, null, null);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable dictionary of its contents by using the specified key and value comparers.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <param name="keyComparer">The key comparer to use when building the immutable dictionary.</param>
		/// <param name="valueComparer">The value comparer to use for the immutable dictionary.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x0600018F RID: 399 RVA: 0x00005610 File Offset: 0x00003810
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableDictionary<TKey, TValue> immutableDictionary = source as ImmutableDictionary<TKey, TValue>;
			if (immutableDictionary != null)
			{
				return immutableDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable dictionary of its contents by using the specified key comparer.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <param name="keyComparer">The key comparer to use when building the immutable dictionary.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x06000190 RID: 400 RVA: 0x0000564D File Offset: 0x0000384D
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary<TKey, TValue>(keyComparer, null);
		}

		/// <summary>Enumerates a sequence of key/value pairs and produces an immutable dictionary of its contents.</summary>
		/// <param name="source">The sequence of key/value pairs to enumerate.</param>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		/// <returns>An immutable dictionary that contains the key/value pairs in the specified sequence.</returns>
		// Token: 0x06000191 RID: 401 RVA: 0x00005657 File Offset: 0x00003857
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 0, 1, 1 })] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableDictionary<TKey, TValue>(null, null);
		}

		/// <summary>Determines whether the specified immutable dictionary contains the specified key/value pair.</summary>
		/// <param name="map">The immutable dictionary to search.</param>
		/// <param name="key">The key to locate in the immutable dictionary.</param>
		/// <param name="value">The value to locate on the specified key, if the key is found.</param>
		/// <typeparam name="TKey">The type of the keys in the immutable dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the immutable dictionary.</typeparam>
		/// <returns>
		///   <see langword="true" /> if this map contains the specified key/value pair; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000192 RID: 402 RVA: 0x00005661 File Offset: 0x00003861
		public static bool Contains<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(map, "map");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return map.Contains(new KeyValuePair<TKey, TValue>(key, value));
		}

		/// <summary>Gets the value for a given key if a matching key exists in the dictionary.</summary>
		/// <param name="dictionary">The dictionary to retrieve the value from.</param>
		/// <param name="key">The key to search for.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <returns>The value for the key, or <c>default(TValue)</c> if no matching key was found.</returns>
		// Token: 0x06000193 RID: 403 RVA: 0x00005688 File Offset: 0x00003888
		[return: Nullable(2)]
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		/// <summary>Gets the value for a given key if a matching key exists in the dictionary.</summary>
		/// <param name="dictionary">The dictionary to retrieve the value from.</param>
		/// <param name="key">The key to search for.</param>
		/// <param name="defaultValue">The default value to return if no matching key is found in the dictionary.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <returns>The value for the key, or <paramref name="defaultValue" /> if no matching key was found.</returns>
		// Token: 0x06000194 RID: 404 RVA: 0x000056A8 File Offset: 0x000038A8
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			TValue tvalue;
			if (dictionary.TryGetValue(key, ref tvalue))
			{
				return tvalue;
			}
			return defaultValue;
		}
	}
}
