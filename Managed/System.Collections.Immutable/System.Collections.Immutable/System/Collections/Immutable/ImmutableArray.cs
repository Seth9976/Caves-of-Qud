using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides methods for creating an array that is immutable; meaning it cannot be changed once it is created.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArray
	{
		/// <summary>Creates an empty immutable array.</summary>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An empty immutable array.</returns>
		// Token: 0x060000FD RID: 253 RVA: 0x00003C77 File Offset: 0x00001E77
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>()
		{
			return ImmutableArray<T>.Empty;
		}

		/// <summary>Creates an immutable array that contains the specified object.</summary>
		/// <param name="item">The object to store in the array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified object.</returns>
		// Token: 0x060000FE RID: 254 RVA: 0x00003C80 File Offset: 0x00001E80
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item)
		{
			T[] array = new T[] { item };
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates an immutable array that contains the specified objects.</summary>
		/// <param name="item1">The first object to store in the array.</param>
		/// <param name="item2">The second object to store in the array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified objects.</returns>
		// Token: 0x060000FF RID: 255 RVA: 0x00003CA4 File Offset: 0x00001EA4
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2)
		{
			T[] array = new T[] { item1, item2 };
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates an immutable array that contains the specified objects.</summary>
		/// <param name="item1">The first object to store in the array.</param>
		/// <param name="item2">The second object to store in the array.</param>
		/// <param name="item3">The third object to store in the array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified objects.</returns>
		// Token: 0x06000100 RID: 256 RVA: 0x00003CD0 File Offset: 0x00001ED0
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3)
		{
			T[] array = new T[] { item1, item2, item3 };
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates an immutable array that contains the specified objects.</summary>
		/// <param name="item1">The first object to store in the array.</param>
		/// <param name="item2">The second object to store in the array.</param>
		/// <param name="item3">The third object to store in the array.</param>
		/// <param name="item4">The fourth object to store in the array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified objects.</returns>
		// Token: 0x06000101 RID: 257 RVA: 0x00003D04 File Offset: 0x00001F04
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3, T item4)
		{
			T[] array = new T[] { item1, item2, item3, item4 };
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates a new <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> populated with the specified items.</summary>
		/// <param name="items">The elements to add to the array.</param>
		/// <typeparam name="T">The type of element stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified items.</returns>
		// Token: 0x06000102 RID: 258 RVA: 0x00003D40 File Offset: 0x00001F40
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			IImmutableArray immutableArray = items as IImmutableArray;
			if (immutableArray != null)
			{
				Array array = immutableArray.Array;
				if (array == null)
				{
					throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
				}
				return new ImmutableArray<T>((T[])array);
			}
			else
			{
				int num;
				if (items.TryGetCount<T>(out num))
				{
					return new ImmutableArray<T>(items.ToArray<T>(num));
				}
				return new ImmutableArray<T>(Enumerable.ToArray<T>(items));
			}
		}

		/// <summary>Creates an immutable array from the specified array of objects.</summary>
		/// <param name="items">The array of objects to populate the array with.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the array of items.</returns>
		// Token: 0x06000103 RID: 259 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[] { 2, 1 })] params T[] items)
		{
			if (items == null || items.Length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[items.Length];
			Array.Copy(items, array, items.Length);
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates an immutable array with specified objects from another array.</summary>
		/// <param name="items">The source array of objects.</param>
		/// <param name="start">The index of the first element to copy from <paramref name="items" />.</param>
		/// <param name="length">The number of elements from <paramref name="items" /> to include in this immutable array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified objects from the source array.</returns>
		// Token: 0x06000104 RID: 260 RVA: 0x00003DDC File Offset: 0x00001FDC
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T[] items, int start, int length)
		{
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			T[] array = new T[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = items[start + i];
			}
			return new ImmutableArray<T>(array);
		}

		/// <summary>Creates an immutable array with the specified objects from another immutable array.</summary>
		/// <param name="items">The source array of objects.</param>
		/// <param name="start">The index of the first element to copy from <paramref name="items" />.</param>
		/// <param name="length">The number of elements from <paramref name="items" /> to include in this immutable array.</param>
		/// <typeparam name="T">The type of elements stored in the array.</typeparam>
		/// <returns>An immutable array that contains the specified objects from the source array.</returns>
		// Token: 0x06000105 RID: 261 RVA: 0x00003E60 File Offset: 0x00002060
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int start, int length)
		{
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			if (start == 0 && length == items.Length)
			{
				return items;
			}
			T[] array = new T[length];
			Array.Copy(items.array, start, array, 0, length);
			return new ImmutableArray<T>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct.</summary>
		/// <param name="items">The source array to initialize the resulting array with.</param>
		/// <param name="selector">The function to apply to each element from the source array.</param>
		/// <typeparam name="TSource">The type of element stored in the source array.</typeparam>
		/// <typeparam name="TResult">The type of element to store in the target array.</typeparam>
		/// <returns>An immutable array that contains the specified items.</returns>
		// Token: 0x06000106 RID: 262 RVA: 0x00003EE4 File Offset: 0x000020E4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TResult> selector)
		{
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector.Invoke(items[i]);
			}
			return new ImmutableArray<TResult>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct.</summary>
		/// <param name="items">The source array to initialize the resulting array with.</param>
		/// <param name="start">The index of the first element in the source array to include in the resulting array.</param>
		/// <param name="length">The number of elements from the source array to include in the resulting array.</param>
		/// <param name="selector">The function to apply to each element from the source array included in the resulting array.</param>
		/// <typeparam name="TSource">The type of element stored in the source array.</typeparam>
		/// <typeparam name="TResult">The type of element to store in the target array.</typeparam>
		/// <returns>An immutable array that contains the specified items.</returns>
		// Token: 0x06000107 RID: 263 RVA: 0x00003F40 File Offset: 0x00002140
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TResult> selector)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector.Invoke(items[i + start]);
			}
			return new ImmutableArray<TResult>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct.</summary>
		/// <param name="items">The source array to initialize the resulting array with.</param>
		/// <param name="selector">The function to apply to each element from the source array.</param>
		/// <param name="arg">An argument to be passed to the selector mapping function.</param>
		/// <typeparam name="TSource">The type of element stored in the source array.</typeparam>
		/// <typeparam name="TArg">The type of argument to pass to the selector mapping function.</typeparam>
		/// <typeparam name="TResult">The type of element to store in the target array.</typeparam>
		/// <returns>An immutable array that contains the specified items.</returns>
		// Token: 0x06000108 RID: 264 RVA: 0x00003FD0 File Offset: 0x000021D0
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector.Invoke(items[i], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct.</summary>
		/// <param name="items">The source array to initialize the resulting array with.</param>
		/// <param name="start">The index of the first element in the source array to include in the resulting array.</param>
		/// <param name="length">The number of elements from the source array to include in the resulting array.</param>
		/// <param name="selector">The function to apply to each element from the source array included in the resulting array.</param>
		/// <param name="arg">An argument to be passed to the selector mapping function.</param>
		/// <typeparam name="TSource">The type of element stored in the source array.</typeparam>
		/// <typeparam name="TArg">The type of argument to be passed to the selector mapping function.</typeparam>
		/// <typeparam name="TResult">The type of element to be stored in the target array.</typeparam>
		/// <returns>An immutable array that contains the specified items.</returns>
		// Token: 0x06000109 RID: 265 RVA: 0x0000402C File Offset: 0x0000222C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector.Invoke(items[i + start], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		/// <summary>Creates a mutable array that can be converted to an <see cref="T:System.Collections.Immutable.ImmutableArray" /> without allocating new memory.</summary>
		/// <typeparam name="T">The type of elements stored in the builder.</typeparam>
		/// <returns>A mutable array of the specified type that can be efficiently converted to an immutable array.</returns>
		// Token: 0x0600010A RID: 266 RVA: 0x000040C0 File Offset: 0x000022C0
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableArray.Create<T>().ToBuilder();
		}

		/// <summary>Creates a mutable array that can be converted to an <see cref="T:System.Collections.Immutable.ImmutableArray" /> without allocating new memory.</summary>
		/// <param name="initialCapacity">The initial capacity of the builder.</param>
		/// <typeparam name="T">The type of elements stored in the builder.</typeparam>
		/// <returns>A mutable array of the specified type that can be efficiently converted to an immutable array.</returns>
		// Token: 0x0600010B RID: 267 RVA: 0x000040DA File Offset: 0x000022DA
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>(int initialCapacity)
		{
			return new ImmutableArray<T>.Builder(initialCapacity);
		}

		/// <summary>Creates an immutable array from the specified collection.</summary>
		/// <param name="items">The collection of objects to copy to the immutable array.</param>
		/// <typeparam name="TSource">The type of elements contained in <paramref name="items" />.</typeparam>
		/// <returns>An immutable array that contains the specified collection of objects.</returns>
		// Token: 0x0600010C RID: 268 RVA: 0x000040E2 File Offset: 0x000022E2
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this IEnumerable<TSource> items)
		{
			if (items is ImmutableArray<TSource>)
			{
				return (ImmutableArray<TSource>)items;
			}
			return ImmutableArray.CreateRange<TSource>(items);
		}

		/// <summary>Creates an immutable array from the current contents of the builder's array.</summary>
		/// <param name="builder">The builder to create the immutable array from.</param>
		/// <typeparam name="TSource">The type of elements contained in the immutable array.</typeparam>
		/// <returns>An immutable array that contains the current contents of the builder's array.</returns>
		// Token: 0x0600010D RID: 269 RVA: 0x000040F9 File Offset: 0x000022F9
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this ImmutableArray<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		/// <summary>Searches the sorted immutable array for a specified element using the default comparer and returns the zero-based index of the element, if it's found.</summary>
		/// <param name="array">The sorted array to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of element stored in the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IComparable" /> or the search encounters an element that does not implement <see cref="T:System.IComparable" />.</exception>
		/// <returns>The zero-based index of the item in the array, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.ICollection`1.Count" />.</returns>
		// Token: 0x0600010E RID: 270 RVA: 0x0000410C File Offset: 0x0000230C
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.array, value);
		}

		/// <summary>Searches a sorted immutable array for a specified element and returns the zero-based index of the element, if it's found.</summary>
		/// <param name="array">The sorted array to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The comparer implementation to use when comparing elements, or null to use the default comparer.</param>
		/// <typeparam name="T">The type of element stored in the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> does not implement <see cref="T:System.IComparable" /> or the search encounters an element that does not implement <see cref="T:System.IComparable" />.</exception>
		/// <returns>The zero-based index of the item in the array, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.ICollection`1.Count" />.</returns>
		// Token: 0x0600010F RID: 271 RVA: 0x0000411A File Offset: 0x0000231A
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, value, comparer);
		}

		/// <summary>Searches a sorted immutable array for a specified element and returns the zero-based index of the element, if it's found.</summary>
		/// <param name="array">The sorted array to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <typeparam name="T">The type of element stored in the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IComparable" /> or the search encounters an element that does not implement <see cref="T:System.IComparable" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.
		///
		/// -or-
		///
		/// <paramref name="length" /> is less than zero.</exception>
		/// <returns>The zero-based index of the item in the array, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.ICollection`1.Count" />.</returns>
		// Token: 0x06000110 RID: 272 RVA: 0x00004129 File Offset: 0x00002329
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.array, index, length, value);
		}

		/// <summary>Searches a sorted immutable array for a specified element and returns the zero-based index of the element.</summary>
		/// <param name="array">The sorted array to search.</param>
		/// <param name="index">The starting index of the range to search.</param>
		/// <param name="length">The length of the range to search.</param>
		/// <param name="value">The object to search for.</param>
		/// <param name="comparer">The comparer to use when comparing elements for equality or <see langword="null" /> to use the default comparer.</param>
		/// <typeparam name="T">The type of element stored in the array.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> does not implement <see cref="T:System.IComparable" /> or the search encounters an element that does not implement <see cref="T:System.IComparable" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.
		///
		/// -or-
		///
		/// <paramref name="comparer" /> is <see langword="null" />, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.
		///
		/// -or-
		///
		/// <paramref name="length" /> is less than zero.</exception>
		/// <returns>The zero-based index of the item in the array, if item is found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.Generic.ICollection`1.Count" />.</returns>
		// Token: 0x06000111 RID: 273 RVA: 0x00004139 File Offset: 0x00002339
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, int index, int length, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, index, length, value, comparer);
		}

		// Token: 0x04000019 RID: 25
		internal static readonly byte[] TwoElementArray = new byte[2];
	}
}
