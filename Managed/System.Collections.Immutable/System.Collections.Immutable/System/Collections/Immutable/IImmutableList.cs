using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents a list of elements that cannot be modified.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	public interface IImmutableList<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		/// <summary>Creates  a list with all the items removed, but with the same sorting and ordering semantics as this list.</summary>
		/// <returns>An empty list that has the same sorting and ordering semantics as this instance.</returns>
		// Token: 0x0600007B RID: 123
		IImmutableList<T> Clear();

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Immutable.IImmutableList`1" />. This value can be null for reference types.</param>
		/// <param name="index">The zero-based starting indexes of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="equalityComparer">The equality comparer to use to locate <paramref name="item" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that starts at <paramref name="index" /> and contains <paramref name="count" /> number of elements if found; otherwise -1.</returns>
		// Token: 0x0600007C RID: 124
		int IndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		/// <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.Immutable.IImmutableList`1" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="item">The object to locate in the list. The value can be <see langword="null" /> for reference types.</param>
		/// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="equalityComparer">The equality comparer to match <paramref name="item" />.</param>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x0600007D RID: 125
		int LastIndexOf(T item, int index, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		/// <summary>Makes a copy of the list, and adds the specified object to the end of the copied list.</summary>
		/// <param name="value">The object to add to the list.</param>
		/// <returns>A new list with the object added.</returns>
		// Token: 0x0600007E RID: 126
		IImmutableList<T> Add(T value);

		/// <summary>Makes a copy of the list and adds the specified objects to the end of the copied list.</summary>
		/// <param name="items">The objects to add to the list.</param>
		/// <returns>A new list with the elements added.</returns>
		// Token: 0x0600007F RID: 127
		IImmutableList<T> AddRange(IEnumerable<T> items);

		/// <summary>Inserts the specified element at the specified index in the immutable list.</summary>
		/// <param name="index">The zero-based index at which to insert the value.</param>
		/// <param name="element">The object to insert.</param>
		/// <returns>A new immutable list that includes the specified element.</returns>
		// Token: 0x06000080 RID: 128
		IImmutableList<T> Insert(int index, T element);

		/// <summary>Inserts the specified elements at the specified index in the immutable list.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted.</param>
		/// <param name="items">The elements to insert.</param>
		/// <returns>A new immutable list that includes the specified elements.</returns>
		// Token: 0x06000081 RID: 129
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		/// <summary>Removes the first occurrence of a specified object from this immutable list.</summary>
		/// <param name="value">The object to remove from the list.</param>
		/// <param name="equalityComparer">The equality comparer to use to locate <paramref name="value" />.</param>
		/// <returns>A new list with the specified object removed.</returns>
		// Token: 0x06000082 RID: 130
		IImmutableList<T> Remove(T value, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		/// <summary>Removes all the elements that match the conditions defined by the specified predicate.</summary>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <returns>A new immutable list with the elements removed.</returns>
		// Token: 0x06000083 RID: 131
		IImmutableList<T> RemoveAll(Predicate<T> match);

		/// <summary>Removes the specified object from the list.</summary>
		/// <param name="items">The objects to remove from the list.</param>
		/// <param name="equalityComparer">The equality comparer to use to determine if <paramref name="items" /> match any objects in the list.</param>
		/// <returns>A new immutable list with the specified objects removed, if <paramref name="items" /> matched objects in the list.</returns>
		// Token: 0x06000084 RID: 132
		IImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.Immutable.IImmutableList`1" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <returns>A new immutable list with the elements removed.</returns>
		// Token: 0x06000085 RID: 133
		IImmutableList<T> RemoveRange(int index, int count);

		/// <summary>Removes the element at the specified index of the immutable list.</summary>
		/// <param name="index">The index of the element to remove.</param>
		/// <returns>A new list with the element removed.</returns>
		// Token: 0x06000086 RID: 134
		IImmutableList<T> RemoveAt(int index);

		/// <summary>Replaces an element in the list at a given position with the specified element.</summary>
		/// <param name="index">The position in the list of the element to replace.</param>
		/// <param name="value">The element to replace the old element with.</param>
		/// <returns>A new list that contains the new element, even if the element at the specified location is the same as the new element.</returns>
		// Token: 0x06000087 RID: 135
		IImmutableList<T> SetItem(int index, T value);

		/// <summary>Returns a new list with the first matching element in the list replaced with the specified element.</summary>
		/// <param name="oldValue">The element to be replaced.</param>
		/// <param name="newValue">The element to replace the first occurrence of <paramref name="oldValue" /> with.</param>
		/// <param name="equalityComparer">The equality comparer to use for matching <paramref name="oldValue" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> does not exist in the list.</exception>
		/// <returns>A new list that contains <paramref name="newValue" />, even if <paramref name="oldvalue" /> is the same as <paramref name="newValue" />.</returns>
		// Token: 0x06000088 RID: 136
		IImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer);
	}
}
