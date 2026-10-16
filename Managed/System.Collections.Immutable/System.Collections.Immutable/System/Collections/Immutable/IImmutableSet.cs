using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents a set of elements that can only be modified by creating a new instance of the set.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of element stored in the set.</typeparam>
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	public interface IImmutableSet<[Nullable(2)] T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		/// <summary>Retrieves an empty immutable set that has the same sorting and ordering semantics as this instance.</summary>
		/// <returns>An empty set that has the same sorting and ordering semantics as this instance.</returns>
		// Token: 0x060000A2 RID: 162
		IImmutableSet<T> Clear();

		/// <summary>Determines whether this immutable set contains a specified element.</summary>
		/// <param name="value">The element to locate in the set.</param>
		/// <returns>
		///   <see langword="true" /> if the set contains the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000A3 RID: 163
		bool Contains(T value);

		/// <summary>Adds the specified element to this immutable set.</summary>
		/// <param name="value">The element to add.</param>
		/// <returns>A new set with the element added, or this set if the element is already in the set.</returns>
		// Token: 0x060000A4 RID: 164
		IImmutableSet<T> Add(T value);

		/// <summary>Removes the specified element from this immutable set.</summary>
		/// <param name="value">The element to remove.</param>
		/// <returns>A new set with the specified element removed, or the current set if the element cannot be found in the set.</returns>
		// Token: 0x060000A5 RID: 165
		IImmutableSet<T> Remove(T value);

		/// <summary>Determines whether the set contains a specified value.</summary>
		/// <param name="equalValue">The value to search for.</param>
		/// <param name="actualValue">The matching value from the set, if found, or <c>equalvalue</c> if there are no matches.</param>
		/// <returns>
		///   <see langword="true" /> if a matching value was found; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000A6 RID: 166
		bool TryGetValue(T equalValue, out T actualValue);

		/// <summary>Creates an immutable set that contains only elements that exist in this set and the specified set.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Immutable.IImmutableSet`1" />.</param>
		/// <returns>A new immutable set that contains elements that exist in both sets.</returns>
		// Token: 0x060000A7 RID: 167
		IImmutableSet<T> Intersect(IEnumerable<T> other);

		/// <summary>Removes the elements in the specified collection from the current immutable set.</summary>
		/// <param name="other">The collection of items to remove from this set.</param>
		/// <returns>A new set with the items removed; or the original set if none of the items were in the set.</returns>
		// Token: 0x060000A8 RID: 168
		IImmutableSet<T> Except(IEnumerable<T> other);

		/// <summary>Creates an immutable set that contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>A new set that contains the elements that are present only in the current set or in the specified collection, but not both.</returns>
		// Token: 0x060000A9 RID: 169
		IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

		/// <summary>Creates a new immutable set that contains all elements that are present in either the current set or in the specified collection.</summary>
		/// <param name="other">The collection to add elements from.</param>
		/// <returns>A new immutable set with the items added; or the original set if all the items were already in the set.</returns>
		// Token: 0x060000AA RID: 170
		IImmutableSet<T> Union(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the sets are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AB RID: 171
		bool SetEquals(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set is a proper (strict) subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper subset of the specified collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AC RID: 172
		bool IsProperSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set is a proper (strict) superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper superset of the specified collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AD RID: 173
		bool IsProperSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set is a subset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a subset of the specified collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AE RID: 174
		bool IsSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set is a superset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a superset of the specified collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AF RID: 175
		bool IsSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current immutable set overlaps with the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set and the specified collection share at least one common element; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000B0 RID: 176
		bool Overlaps(IEnumerable<T> other);
	}
}
