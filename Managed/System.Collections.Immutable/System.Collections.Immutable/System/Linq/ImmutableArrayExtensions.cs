using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	/// <summary>LINQ extension method overrides that offer greater efficiency for <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> than the standard LINQ methods  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArrayExtensions
	{
		/// <summary>Projects each element of a sequence into a new form.</summary>
		/// <param name="immutableArray">The immutable array to select items from.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <typeparam name="TResult">The type of the result element.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of source.</returns>
		// Token: 0x06000032 RID: 50 RVA: 0x00002434 File Offset: 0x00000634
		public static IEnumerable<TResult> Select<[Nullable(2)] T, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TResult> selector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return Enumerable.Select<T, TResult>(immutableArray.array, selector);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />,             flattens the resulting sequences into one sequence, and invokes a result             selector function on each element therein.</summary>
		/// <param name="immutableArray">The immutable array.</param>
		/// <param name="collectionSelector">A transform function to apply to each element of the input sequence.</param>
		/// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="immutableArray" />.</typeparam>
		/// <typeparam name="TCollection">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result             of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each             element of <paramref name="immutableArray" /> and then mapping each of those sequence elements and their             corresponding source element to a result element.</returns>
		// Token: 0x06000033 RID: 51 RVA: 0x0000244C File Offset: 0x0000064C
		public static IEnumerable<TResult> SelectMany<[Nullable(2)] TSource, [Nullable(2)] TCollection, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (collectionSelector == null || resultSelector == null)
			{
				return Enumerable.SelectMany<TSource, TCollection, TResult>(immutableArray, collectionSelector, resultSelector);
			}
			if (immutableArray.Length != 0)
			{
				return immutableArray.SelectManyIterator<TSource, TCollection, TResult>(collectionSelector, resultSelector);
			}
			return Enumerable.Empty<TResult>();
		}

		/// <summary>Filters a sequence of values based on a predicate.</summary>
		/// <param name="immutableArray">The array to filter.</param>
		/// <param name="predicate">The condition to use for filtering the array content.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements that meet the condition.</returns>
		// Token: 0x06000034 RID: 52 RVA: 0x0000248D File Offset: 0x0000068D
		public static IEnumerable<T> Where<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return Enumerable.Where<T>(immutableArray.array, predicate);
		}

		/// <summary>Gets a value indicating whether the array contains any elements.</summary>
		/// <param name="immutableArray">The array to check for elements.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the array contains an elements; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000035 RID: 53 RVA: 0x000024A2 File Offset: 0x000006A2
		[NullableContext(2)]
		public static bool Any<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			return immutableArray.Length > 0;
		}

		/// <summary>Gets a value indicating whether the array contains any elements that match a specified condition.</summary>
		/// <param name="immutableArray">The array to check for elements.</param>
		/// <param name="predicate">The delegate that defines the condition to match to an element.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if an element matches the specified condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000036 RID: 54 RVA: 0x000024B0 File Offset: 0x000006B0
		public static bool Any<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate.Invoke(t))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a value indicating whether all elements in this array match a given condition.</summary>
		/// <param name="immutableArray">The array to check for matches.</param>
		/// <param name="predicate">The predicate.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> if every element of the source sequence passes the test in the specified predicate; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000037 RID: 55 RVA: 0x000024F8 File Offset: 0x000006F8
		public static bool All<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (!predicate.Invoke(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether two sequences are equal according to an equality comparer.</summary>
		/// <param name="immutableArray">The array to use for comparison.</param>
		/// <param name="items">The items to use for comparison.</param>
		/// <param name="comparer">The comparer to use to check for equality.</param>
		/// <typeparam name="TDerived">The type of element in the compared array.</typeparam>
		/// <typeparam name="TBase">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> to indicate the sequences are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x00002540 File Offset: 0x00000740
		[NullableContext(0)]
		public static bool SequenceEqual<TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			for (int i = 0; i < immutableArray.Length; i++)
			{
				if (!comparer.Equals(immutableArray.array[i], (TBase)((object)items.array[i])))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether two sequences are equal according to an equality comparer.</summary>
		/// <param name="immutableArray">The array to use for comparison.</param>
		/// <param name="items">The items to use for comparison.</param>
		/// <param name="comparer">The comparer to use to check for equality.</param>
		/// <typeparam name="TDerived">The type of element in the compared array.</typeparam>
		/// <typeparam name="TBase">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> to indicate the sequences are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000039 RID: 57 RVA: 0x000025C8 File Offset: 0x000007C8
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, IEnumerable<TDerived> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			Requires.NotNull<IEnumerable<TDerived>>(items, "items");
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			int num = 0;
			int length = immutableArray.Length;
			foreach (TDerived tderived in items)
			{
				if (num == length)
				{
					return false;
				}
				if (!comparer.Equals(immutableArray[num], (TBase)((object)tderived)))
				{
					return false;
				}
				num++;
			}
			return num == length;
		}

		/// <summary>Determines whether two sequences are equal according to an equality comparer.</summary>
		/// <param name="immutableArray">The array to use for comparison.</param>
		/// <param name="items">The items to use for comparison.</param>
		/// <param name="predicate">The comparer to use to check for equality.</param>
		/// <typeparam name="TDerived">The type of element in the compared array.</typeparam>
		/// <typeparam name="TBase">The type of element contained by the collection.</typeparam>
		/// <returns>
		///   <see langword="true" /> to indicate the sequences are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600003A RID: 58 RVA: 0x0000265C File Offset: 0x0000085C
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items, Func<TBase, TBase, bool> predicate) where TDerived : TBase
		{
			Requires.NotNull<Func<TBase, TBase, bool>>(predicate, "predicate");
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			int i = 0;
			int length = immutableArray.Length;
			while (i < length)
			{
				if (!predicate.Invoke(immutableArray[i], (TBase)((object)items[i])))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		/// <summary>Applies a function to a sequence of elements in a cumulative way.</summary>
		/// <param name="immutableArray">The collection to apply the function to.</param>
		/// <param name="func">A function to be invoked on each element, in a cumulative way.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The final value after the cumulative function has been applied to all elements.</returns>
		// Token: 0x0600003B RID: 59 RVA: 0x000026E0 File Offset: 0x000008E0
		[NullableContext(2)]
		public static T Aggregate<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, T, T> func)
		{
			Requires.NotNull<Func<T, T, T>>(func, "func");
			if (immutableArray.Length == 0)
			{
				return default(T);
			}
			T t = immutableArray[0];
			int i = 1;
			int length = immutableArray.Length;
			while (i < length)
			{
				t = func.Invoke(t, immutableArray[i]);
				i++;
			}
			return t;
		}

		/// <summary>Applies a function to a sequence of elements in a cumulative way.</summary>
		/// <param name="immutableArray">The collection to apply the function to.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">A function to be invoked on each element, in a cumulative way.</param>
		/// <typeparam name="TAccumulate">The type of the accumulated value.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The final accumulator value.</returns>
		// Token: 0x0600003C RID: 60 RVA: 0x0000273C File Offset: 0x0000093C
		public static TAccumulate Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
		{
			Requires.NotNull<Func<TAccumulate, T, TAccumulate>>(func, "func");
			TAccumulate taccumulate = seed;
			foreach (T t in immutableArray.array)
			{
				taccumulate = func.Invoke(taccumulate, t);
			}
			return taccumulate;
		}

		/// <summary>Applies a function to a sequence of elements in a cumulative way.</summary>
		/// <param name="immutableArray">The collection to apply the function to.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">A function to be invoked on each element, in a cumulative way.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result type.</param>
		/// <typeparam name="TAccumulate">The type of the accumulated value.</typeparam>
		/// <typeparam name="TResult">The type of result returned by the result selector.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The final accumulator value.</returns>
		// Token: 0x0600003D RID: 61 RVA: 0x0000277D File Offset: 0x0000097D
		public static TResult Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] TResult, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Requires.NotNull<Func<TAccumulate, TResult>>(resultSelector, "resultSelector");
			return resultSelector.Invoke(immutableArray.Aggregate(seed, func));
		}

		/// <summary>Returns the element at a specified index in the array.</summary>
		/// <param name="immutableArray">The array to find an element in.</param>
		/// <param name="index">The index for the element to retrieve.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The item at the specified index.</returns>
		// Token: 0x0600003E RID: 62 RVA: 0x00002798 File Offset: 0x00000998
		public static T ElementAt<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			return immutableArray[index];
		}

		/// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
		/// <param name="immutableArray">The array to find an element in.</param>
		/// <param name="index">The index for the element to retrieve.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The item at the specified index, or the default value if the index is not found.</returns>
		// Token: 0x0600003F RID: 63 RVA: 0x000027A4 File Offset: 0x000009A4
		[NullableContext(2)]
		public static T ElementAtOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, int index)
		{
			if (index < 0 || index >= immutableArray.Length)
			{
				return default(T);
			}
			return immutableArray[index];
		}

		/// <summary>Returns the first element in a sequence that satisfies a specified condition.</summary>
		/// <param name="immutableArray">The array to get an item from.</param>
		/// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">If the array is empty.</exception>
		/// <returns>The first item in the list if it meets the condition specified by <paramref name="predicate" />.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x000027D4 File Offset: 0x000009D4
		public static T First<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate.Invoke(t))
				{
					return t;
				}
			}
			return Enumerable.First<T>(Enumerable.Empty<T>());
		}

		/// <summary>Returns the first element in an array.</summary>
		/// <param name="immutableArray">The array to get an item from.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">If the array is empty.</exception>
		/// <returns>The first item in the array.</returns>
		// Token: 0x06000041 RID: 65 RVA: 0x0000281E File Offset: 0x00000A1E
		public static T First<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return Enumerable.First<T>(immutableArray.array);
			}
			return immutableArray[0];
		}

		/// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="immutableArray">The array to retrieve items from.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The first item in the list, if found; otherwise the default value for the item type.</returns>
		// Token: 0x06000042 RID: 66 RVA: 0x00002840 File Offset: 0x00000A40
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.array.Length == 0)
			{
				return default(T);
			}
			return immutableArray.array[0];
		}

		/// <summary>Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <param name="immutableArray">The array to retrieve elements from.</param>
		/// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The first item in the list, if found; otherwise the default value for the item type.</returns>
		// Token: 0x06000043 RID: 67 RVA: 0x0000286C File Offset: 0x00000A6C
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate.Invoke(t))
				{
					return t;
				}
			}
			return default(T);
		}

		/// <summary>Returns the last element of the array.</summary>
		/// <param name="immutableArray">The array to retrieve items from.</param>
		/// <typeparam name="T">The type of element contained by the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The collection is empty.</exception>
		/// <returns>The last element in the array.</returns>
		// Token: 0x06000044 RID: 68 RVA: 0x000028B5 File Offset: 0x00000AB5
		public static T Last<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return Enumerable.Last<T>(immutableArray.array);
			}
			return immutableArray[immutableArray.Length - 1];
		}

		/// <summary>Returns the last element of a sequence that satisfies a specified condition.</summary>
		/// <param name="immutableArray">The array to retrieve elements from.</param>
		/// <param name="predicate">The delegate that defines the conditions of the element to retrieve.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The collection is empty.</exception>
		/// <returns>The last element of the array that satisfies the <paramref name="predicate" /> condition.</returns>
		// Token: 0x06000045 RID: 69 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static T Last<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate.Invoke(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return Enumerable.Last<T>(Enumerable.Empty<T>());
		}

		/// <summary>Returns the last element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="immutableArray">The array to retrieve items from.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The last element of a sequence, or a default value if the sequence contains no elements.</returns>
		// Token: 0x06000046 RID: 70 RVA: 0x0000292F File Offset: 0x00000B2F
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return Enumerable.LastOrDefault<T>(immutableArray.array);
		}

		/// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <param name="immutableArray">The array to retrieve an element from.</param>
		/// <param name="predicate">The delegate that defines the conditions of the element to search for.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The last element of a sequence, or a default value if the sequence contains no elements.</returns>
		// Token: 0x06000047 RID: 71 RVA: 0x00002944 File Offset: 0x00000B44
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate.Invoke(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return default(T);
		}

		/// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <param name="immutableArray">The array to retrieve the element from.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The element in the sequence.</returns>
		// Token: 0x06000048 RID: 72 RVA: 0x00002992 File Offset: 0x00000B92
		public static T Single<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return Enumerable.Single<T>(immutableArray.array);
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <param name="immutableArray">The immutable array to return a single element from.</param>
		/// <param name="predicate">The function to test whether an element should be returned.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x06000049 RID: 73 RVA: 0x000029A8 File Offset: 0x00000BA8
		public static T Single<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T t = default(T);
			foreach (T t2 in immutableArray.array)
			{
				if (predicate.Invoke(t2))
				{
					if (!flag)
					{
						Enumerable.Single<byte>(ImmutableArray.TwoElementArray);
					}
					flag = false;
					t = t2;
				}
			}
			if (flag)
			{
				Enumerable.Single<T>(Enumerable.Empty<T>());
			}
			return t;
		}

		/// <summary>Returns the only element of the array, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <param name="immutableArray">The array.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains more than one element.</exception>
		/// <returns>The element in the array, or the default value if the array is empty.</returns>
		// Token: 0x0600004A RID: 74 RVA: 0x00002A14 File Offset: 0x00000C14
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return Enumerable.SingleOrDefault<T>(immutableArray.array);
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <param name="immutableArray">The array to get the element from.</param>
		/// <param name="predicate">The condition the element must satisfy.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
		/// <returns>The element if it satisfies the specified condition; otherwise the default element.</returns>
		// Token: 0x0600004B RID: 75 RVA: 0x00002A28 File Offset: 0x00000C28
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T t = default(T);
			foreach (T t2 in immutableArray.array)
			{
				if (predicate.Invoke(t2))
				{
					if (!flag)
					{
						Enumerable.Single<byte>(ImmutableArray.TwoElementArray);
					}
					flag = false;
					t = t2;
				}
			}
			return t;
		}

		/// <summary>Creates a dictionary based on the contents of this array.</summary>
		/// <param name="immutableArray">The array to create a dictionary from.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The newly initialized dictionary.</returns>
		// Token: 0x0600004C RID: 76 RVA: 0x00002A86 File Offset: 0x00000C86
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector)
		{
			return immutableArray.ToDictionary<TKey, T>(keySelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a dictionary based on the contents of this array.</summary>
		/// <param name="immutableArray">The array to create a dictionary from.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="elementSelector">The element selector.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TElement">The type of the element.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The newly initialized dictionary.</returns>
		// Token: 0x0600004D RID: 77 RVA: 0x00002A94 File Offset: 0x00000C94
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
		{
			return immutableArray.ToDictionary<TKey, TElement, T>(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a dictionary based on the contents of this array.</summary>
		/// <param name="immutableArray">The array to create a dictionary from.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="comparer">The comparer to initialize the dictionary with.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The newly initialized dictionary.</returns>
		// Token: 0x0600004E RID: 78 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Dictionary<TKey, T> dictionary = new Dictionary<TKey, T>(immutableArray.Length, comparer);
			foreach (T t in immutableArray)
			{
				dictionary.Add(keySelector.Invoke(t), t);
			}
			return dictionary;
		}

		/// <summary>Creates a dictionary based on the contents of this array.</summary>
		/// <param name="immutableArray">The array to create a dictionary from.</param>
		/// <param name="keySelector">The key selector.</param>
		/// <param name="elementSelector">The element selector.</param>
		/// <param name="comparer">The comparer to initialize the dictionary with.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TElement">The type of the element.</typeparam>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The newly initialized dictionary.</returns>
		// Token: 0x0600004F RID: 79 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<T, TElement>>(elementSelector, "elementSelector");
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(immutableArray.Length, comparer);
			foreach (T t in immutableArray.array)
			{
				dictionary.Add(keySelector.Invoke(t), elementSelector.Invoke(t));
			}
			return dictionary;
		}

		/// <summary>Copies the contents of this array to a mutable array.</summary>
		/// <param name="immutableArray">The immutable array to copy into a mutable one.</param>
		/// <typeparam name="T">The type of element contained by the collection.</typeparam>
		/// <returns>The newly instantiated array.</returns>
		// Token: 0x06000050 RID: 80 RVA: 0x00002B57 File Offset: 0x00000D57
		public static T[] ToArray<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (immutableArray.array.Length == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			return (T[])immutableArray.array.Clone();
		}

		/// <summary>Returns the first element in the collection.</summary>
		/// <param name="builder">The builder to retrieve an item from.</param>
		/// <typeparam name="T">The type of items in the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">If the array is empty.</exception>
		/// <returns>The first item in the list.</returns>
		// Token: 0x06000051 RID: 81 RVA: 0x00002B84 File Offset: 0x00000D84
		public static T First<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[0];
		}

		/// <summary>Returns the first element in the collection, or the default value if the collection is empty.</summary>
		/// <param name="builder">The builder to retrieve an element from.</param>
		/// <typeparam name="T">The type of item in the builder.</typeparam>
		/// <returns>The first item in the list, if found; otherwise the default value for the item type.</returns>
		// Token: 0x06000052 RID: 82 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[0];
		}

		/// <summary>Returns the last element in the collection.</summary>
		/// <param name="builder">The builder to retrieve elements from.</param>
		/// <typeparam name="T">The type of item in the builder.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The collection is empty.</exception>
		/// <returns>The last element in the builder.</returns>
		// Token: 0x06000053 RID: 83 RVA: 0x00002BD9 File Offset: 0x00000DD9
		public static T Last<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[builder.Count - 1];
		}

		/// <summary>Returns the last element in the collection, or the default value if the collection is empty.</summary>
		/// <param name="builder">The builder to retrieve an element from.</param>
		/// <typeparam name="T">The type of item in the builder.</typeparam>
		/// <returns>The last element of a sequence, or a default value if the sequence contains no elements.</returns>
		// Token: 0x06000054 RID: 84 RVA: 0x00002C04 File Offset: 0x00000E04
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[builder.Count - 1];
		}

		/// <summary>Returns a value indicating whether this collection contains any elements.</summary>
		/// <param name="builder">The builder to check for matches.</param>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the array builder contains any elements; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000055 RID: 85 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static bool Any<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			return builder.Count > 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C52 File Offset: 0x00000E52
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource item in immutableArray.array)
			{
				foreach (TCollection tcollection in collectionSelector.Invoke(item))
				{
					yield return resultSelector.Invoke(item, tcollection);
				}
				IEnumerator<TCollection> enumerator = null;
				item = default(TSource);
			}
			TSource[] array = null;
			yield break;
			yield break;
		}
	}
}
