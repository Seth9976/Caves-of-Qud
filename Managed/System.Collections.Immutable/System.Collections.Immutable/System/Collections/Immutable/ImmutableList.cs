using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableList`1" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableList
	{
		/// <summary>Creates an empty immutable list.</summary>
		/// <typeparam name="T">The type of items to be stored in the .</typeparam>
		/// <returns>An empty immutable list.</returns>
		// Token: 0x0600020E RID: 526 RVA: 0x00006B0E File Offset: 0x00004D0E
		public static ImmutableList<T> Create<[Nullable(2)] T>()
		{
			return ImmutableList<T>.Empty;
		}

		/// <summary>Creates a new immutable list that contains the specified item.</summary>
		/// <param name="item">The item to prepopulate the list with.</param>
		/// <typeparam name="T">The type of items in the .</typeparam>
		/// <returns>A new  that contains the specified item.</returns>
		// Token: 0x0600020F RID: 527 RVA: 0x00006B15 File Offset: 0x00004D15
		public static ImmutableList<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		/// <summary>Creates a new immutable list that contains the specified items.</summary>
		/// <param name="items">The items to add to the list.</param>
		/// <typeparam name="T">The type of items in the .</typeparam>
		/// <returns>An immutable list that contains the specified items.</returns>
		// Token: 0x06000210 RID: 528 RVA: 0x00006B22 File Offset: 0x00004D22
		public static ImmutableList<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		/// <summary>Creates a new immutable list that contains the specified array of items.</summary>
		/// <param name="items">An array that contains the items to prepopulate the list with.</param>
		/// <typeparam name="T">The type of items in the .</typeparam>
		/// <returns>A new immutable list that contains the specified items.</returns>
		// Token: 0x06000211 RID: 529 RVA: 0x00006B2F File Offset: 0x00004D2F
		public static ImmutableList<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		/// <summary>Creates a new immutable list builder.</summary>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The immutable collection builder.</returns>
		// Token: 0x06000212 RID: 530 RVA: 0x00006B3C File Offset: 0x00004D3C
		public static ImmutableList<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		/// <summary>Enumerates a sequence and produces an immutable list of its contents.</summary>
		/// <param name="source">The sequence to enumerate.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <returns>An immutable list that contains the items in the specified sequence.</returns>
		// Token: 0x06000213 RID: 531 RVA: 0x00006B48 File Offset: 0x00004D48
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		/// <summary>Creates an immutable list from the current contents of the builder's collection.</summary>
		/// <param name="builder">The builder to create the immutable list from.</param>
		/// <typeparam name="TSource">The type of the elements in the list.</typeparam>
		/// <returns>An immutable list that contains the current contents in the builder's collection.</returns>
		// Token: 0x06000214 RID: 532 RVA: 0x00006B6C File Offset: 0x00004D6C
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this ImmutableList<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableList<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		/// <summary>Replaces the first equal element in the list with the specified element.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="oldValue">The element to replace.</param>
		/// <param name="newValue">The element to replace the old element with.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> does not exist in the list.</exception>
		/// <returns>The new list -- even if the value being replaced is equal to the new value for that position.</returns>
		// Token: 0x06000215 RID: 533 RVA: 0x00006B7F File Offset: 0x00004D7F
		public static IImmutableList<T> Replace<[Nullable(2)] T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		/// <summary>Removes the specified value from this list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="value">The value to remove.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>A new immutable list with the element removed, or this list if the element is not in this list.</returns>
		// Token: 0x06000216 RID: 534 RVA: 0x00006B99 File Offset: 0x00004D99
		public static IImmutableList<T> Remove<[Nullable(2)] T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		/// <summary>Removes the specified values from this list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>A new immutable list with the elements removed.</returns>
		// Token: 0x06000217 RID: 535 RVA: 0x00006BB2 File Offset: 0x00004DB2
		public static IImmutableList<T> RemoveRange<[Nullable(2)] T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the list. The value can be null for reference types.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the first occurrence of item within the range of elements in the list that extends from index to the last element, if found; otherwise, -1.</returns>
		// Token: 0x06000218 RID: 536 RVA: 0x00006BCB File Offset: 0x00004DCB
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the first occurrence of item within the range of elements in the immutable list that extends from index to the last element, if found; otherwise, -1.</returns>
		// Token: 0x06000219 RID: 537 RVA: 0x00006BEB File Offset: 0x00004DEB
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the immutable list that extends from the specified index to the last element.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the first occurrence of item within the range of elements in the Immutable list that extends from index to the last element, if found; otherwise, -1.</returns>
		// Token: 0x0600021A RID: 538 RVA: 0x00006C07 File Offset: 0x00004E07
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the immutable list that extends from the specified index to the last element.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the first occurrence of item within the range of elements in the Immutable list that extends from index to the last element, if found; otherwise, -1.</returns>
		// Token: 0x0600021B RID: 539 RVA: 0x00006C29 File Offset: 0x00004E29
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire immutable list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the last occurrence of item within the entire the Immutable list, if found; otherwise, -1.</returns>
		// Token: 0x0600021C RID: 540 RVA: 0x00006C44 File Offset: 0x00004E44
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire immutable list.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the last occurrence of item within the entire the Immutable list, if found; otherwise, -1.</returns>
		// Token: 0x0600021D RID: 541 RVA: 0x00006C75 File Offset: 0x00004E75
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the immutable list that extends from the first element to the specified index.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the last occurrence of item within the range of elements in the Immutable list that extends from the first element to index, if found; otherwise, -1.</returns>
		// Token: 0x0600021E RID: 542 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the immutable list that extends from the first element to the specified index.</summary>
		/// <param name="list">The list to search.</param>
		/// <param name="item">The object to locate in the Immutable list. The value can be null for reference types.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <returns>The zero-based index of the last occurrence of item within the range of elements in the Immutable list that extends from the first element to index, if found; otherwise, -1.</returns>
		// Token: 0x0600021F RID: 543 RVA: 0x00006CCC File Offset: 0x00004ECC
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}
