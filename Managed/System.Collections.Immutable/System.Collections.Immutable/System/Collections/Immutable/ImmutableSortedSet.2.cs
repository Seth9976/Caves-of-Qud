using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable sorted set implementation.
	///
	/// NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableSortedSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<T>, IList<T>, ICollection<T>, ISet<T>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000837C File Offset: 0x0000657C
		internal ImmutableSortedSet([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000839F File Offset: 0x0000659F
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		/// <summary>Removes all elements from the immutable sorted set.</summary>
		/// <returns>An empty set with the elements removed.</returns>
		// Token: 0x060002FD RID: 765 RVA: 0x000083D1 File Offset: 0x000065D1
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		/// <summary>Gets the maximum value in the immutable sorted set, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000083F2 File Offset: 0x000065F2
		[Nullable(2)]
		public T Max
		{
			[NullableContext(2)]
			get
			{
				return this._root.Max;
			}
		}

		/// <summary>Gets the minimum value in the immutable sorted set, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000083FF File Offset: 0x000065FF
		[Nullable(2)]
		public T Min
		{
			[NullableContext(2)]
			get
			{
				return this._root.Min;
			}
		}

		/// <summary>Gets a value that indicates whether this immutable sorted set is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if this set is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000840C File Offset: 0x0000660C
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		/// <summary>Gets the number of elements in the immutable sorted set.</summary>
		/// <returns>The number of elements in the immutable sorted set.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00008419 File Offset: 0x00006619
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		/// <summary>Gets the comparer used to sort keys in the immutable sorted set.</summary>
		/// <returns>The comparer used to sort keys.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00008426 File Offset: 0x00006626
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000842E File Offset: 0x0000662E
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		/// <summary>Gets the element of the immutable sorted set at the given index.</summary>
		/// <param name="index">The index of the element to retrieve from the sorted set.</param>
		/// <returns>The element at the given index.</returns>
		// Token: 0x17000082 RID: 130
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		/// <summary>Gets a read-only reference of the element of the set at the given <paramref name="index" />.</summary>
		/// <param name="index">The 0-based index of the element in the set to return.</param>
		/// <returns>A read-only reference of the element at the given position.</returns>
		// Token: 0x06000305 RID: 773 RVA: 0x00008449 File Offset: 0x00006649
		public readonly ref T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		/// <summary>Creates a collection that has the same contents as this immutable sorted set that can be efficiently manipulated by using standard mutable interfaces.</summary>
		/// <returns>The sorted set builder.</returns>
		// Token: 0x06000306 RID: 774 RVA: 0x00008457 File Offset: 0x00006657
		[return: Nullable(new byte[] { 1, 0 })]
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		/// <summary>Adds the specified value to this immutable sorted set.</summary>
		/// <param name="value">The value to add.</param>
		/// <returns>A new set with the element added, or this set if the element is already in this set.</returns>
		// Token: 0x06000307 RID: 775 RVA: 0x00008460 File Offset: 0x00006660
		public ImmutableSortedSet<T> Add(T value)
		{
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		/// <summary>Removes the specified value from this immutable sorted set.</summary>
		/// <param name="value">The element to remove.</param>
		/// <returns>A new immutable sorted set with the element removed, or this set if the element was not found in the set.</returns>
		// Token: 0x06000308 RID: 776 RVA: 0x00008488 File Offset: 0x00006688
		public ImmutableSortedSet<T> Remove(T value)
		{
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		/// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
		/// <param name="equalValue">The value to search for.</param>
		/// <param name="actualValue">The value from the set that the search found, or the original value if the search yielded no match.</param>
		/// <returns>A value indicating whether the search was successful.</returns>
		// Token: 0x06000309 RID: 777 RVA: 0x000084B0 File Offset: 0x000066B0
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
			if (node.IsEmpty)
			{
				actualValue = equalValue;
				return false;
			}
			actualValue = node.Key;
			return true;
		}

		/// <summary>Creates an immutable sorted set that contains elements that exist both in this set and in the specified set.</summary>
		/// <param name="other">The set to intersect with this one.</param>
		/// <returns>A new immutable sorted set that contains any elements that exist in both sets.</returns>
		// Token: 0x0600030A RID: 778 RVA: 0x000084F0 File Offset: 0x000066F0
		public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = this.Clear();
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(t))
				{
					immutableSortedSet = immutableSortedSet.Add(t);
				}
			}
			return immutableSortedSet;
		}

		/// <summary>Removes a specified set of items from this immutable sorted set.</summary>
		/// <param name="other">The items to remove from this set.</param>
		/// <returns>A new set with the items removed; or the original set if none of the items were in the set.</returns>
		// Token: 0x0600030B RID: 779 RVA: 0x00008564 File Offset: 0x00006764
		public ImmutableSortedSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Remove(t, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		/// <summary>Creates an immutable sorted set that contains elements that exist either in this set or in a given sequence, but not both.</summary>
		/// <param name="other">The other sequence of items.</param>
		/// <returns>The new immutable sorted set.</returns>
		// Token: 0x0600030C RID: 780 RVA: 0x000085DC File Offset: 0x000067DC
		public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = ImmutableSortedSet.CreateRange<T>(this._comparer, other);
			ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
			foreach (T t in this)
			{
				if (!immutableSortedSet.Contains(t))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(t);
				}
			}
			foreach (T t2 in immutableSortedSet)
			{
				if (!this.Contains(t2))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(t2);
				}
			}
			return immutableSortedSet2;
		}

		/// <summary>Adds a given set of items to this immutable sorted set.</summary>
		/// <param name="other">The items to add.</param>
		/// <returns>The new set with the items added; or the original set if all the items were already in the set.</returns>
		// Token: 0x0600030D RID: 781 RVA: 0x000086A0 File Offset: 0x000068A0
		public ImmutableSortedSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet;
			if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out immutableSortedSet) && immutableSortedSet.KeyComparer == this.KeyComparer)
			{
				if (immutableSortedSet.IsEmpty)
				{
					return this;
				}
				if (this.IsEmpty)
				{
					return immutableSortedSet;
				}
				if (immutableSortedSet.Count > this.Count)
				{
					return immutableSortedSet.Union(this);
				}
			}
			int num;
			if (this.IsEmpty || (other.TryGetCount<T>(out num) && (float)(this.Count + num) * 0.15f > (float)this.Count))
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		/// <summary>Returns the immutable sorted set that has the specified key comparer.</summary>
		/// <param name="comparer">The comparer to check for.</param>
		/// <returns>The immutable sorted set that has the specified key comparer.</returns>
		// Token: 0x0600030E RID: 782 RVA: 0x00008734 File Offset: 0x00006934
		public ImmutableSortedSet<T> WithComparer([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (comparer == this._comparer)
			{
				return this;
			}
			ImmutableSortedSet<T> immutableSortedSet = new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer);
			return immutableSortedSet.Union(this);
		}

		/// <summary>Determines whether the current immutable sorted set and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the sets are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600030F RID: 783 RVA: 0x0000876C File Offset: 0x0000696C
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this == other)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count != sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			foreach (T t in sortedSet)
			{
				if (!this.Contains(t))
				{
					return false;
				}
				num++;
			}
			return num == this.Count;
		}

		/// <summary>Determines whether the current immutable sorted set is a proper (strict) subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000310 RID: 784 RVA: 0x00008804 File Offset: 0x00006A04
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return Enumerable.Any<T>(other);
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count >= sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T t in sortedSet)
			{
				if (this.Contains(t))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == this.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the current immutable sorted set is a proper superset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000311 RID: 785 RVA: 0x000088B0 File Offset: 0x00006AB0
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				num++;
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return this.Count > num;
		}

		/// <summary>Determines whether the current immutable sorted set is a subset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000312 RID: 786 RVA: 0x00008934 File Offset: 0x00006B34
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			int num = 0;
			foreach (T t in sortedSet)
			{
				if (this.Contains(t))
				{
					num++;
				}
			}
			return num == this.Count;
		}

		/// <summary>Determines whether the current immutable sorted set is a superset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000313 RID: 787 RVA: 0x000089B8 File Offset: 0x00006BB8
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current immutable sorted set and a specified collection share common elements.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000314 RID: 788 RVA: 0x00008A24 File Offset: 0x00006C24
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(t))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over this immutable sorted set in reverse order.</summary>
		/// <returns>An enumerator that iterates over the immutable sorted set in reverse order.</returns>
		// Token: 0x06000315 RID: 789 RVA: 0x00008A98 File Offset: 0x00006C98
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		/// <summary>Gets the position within this immutable sorted set that the specified value appears in.</summary>
		/// <param name="item">The value whose position is being sought.</param>
		/// <returns>The index of the specified <paramref name="item" /> in the sorted set, if <paramref name="item" /> is found. If <paramref name="item" /> is not found and is less than one or more elements in this set, this method returns a negative number that is the bitwise complement of the index of the first element that is larger than value. If <paramref name="item" /> is not found and is greater than any of the elements in the set, this method returns a negative number that is the bitwise complement of the index of the last element plus 1.</returns>
		// Token: 0x06000316 RID: 790 RVA: 0x00008AA5 File Offset: 0x00006CA5
		public int IndexOf(T item)
		{
			return this._root.IndexOf(item, this._comparer);
		}

		/// <summary>Determines whether this immutable sorted set contains the specified value.</summary>
		/// <param name="value">The value to check for.</param>
		/// <returns>
		///   <see langword="true" /> if the set contains the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000317 RID: 791 RVA: 0x00008AB9 File Offset: 0x00006CB9
		public bool Contains(T value)
		{
			return this._root.Contains(value, this._comparer);
		}

		/// <summary>Retrieves an empty immutable set that has the same sorting and ordering semantics as this instance.</summary>
		/// <returns>An empty set that has the same sorting and ordering semantics as this instance.</returns>
		// Token: 0x06000318 RID: 792 RVA: 0x00008ACD File Offset: 0x00006CCD
		IImmutableSet<T> IImmutableSet<T>.Clear()
		{
			return this.Clear();
		}

		/// <summary>Adds the specified element to this immutable set.</summary>
		/// <param name="value">The element to add.</param>
		/// <returns>A new set with the element added, or this set if the element is already in the set.</returns>
		// Token: 0x06000319 RID: 793 RVA: 0x00008AD5 File Offset: 0x00006CD5
		IImmutableSet<T> IImmutableSet<T>.Add(T value)
		{
			return this.Add(value);
		}

		/// <summary>Removes the specified element from this immutable set.</summary>
		/// <param name="value">The element to remove.</param>
		/// <returns>A new set with the specified element removed, or the current set if the element cannot be found in the set.</returns>
		// Token: 0x0600031A RID: 794 RVA: 0x00008ADE File Offset: 0x00006CDE
		IImmutableSet<T> IImmutableSet<T>.Remove(T value)
		{
			return this.Remove(value);
		}

		/// <summary>Creates an immutable set that contains elements that exist in both this set and the specified set.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>A new immutable set that contains any elements that exist in both sets.</returns>
		// Token: 0x0600031B RID: 795 RVA: 0x00008AE7 File Offset: 0x00006CE7
		IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		/// <summary>Removes the elements in the specified collection from the current immutable set.</summary>
		/// <param name="other">The items to remove from this set.</param>
		/// <returns>The new set with the items removed; or the original set if none of the items were in the set.</returns>
		// Token: 0x0600031C RID: 796 RVA: 0x00008AF0 File Offset: 0x00006CF0
		IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		/// <summary>Creates an immutable set that contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>A new set that contains the elements that are present only in the current set or in the specified collection, but not both.</returns>
		// Token: 0x0600031D RID: 797 RVA: 0x00008AF9 File Offset: 0x00006CF9
		IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		/// <summary>Creates a new immutable set that contains all elements that are present in either the current set or in the specified collection.</summary>
		/// <param name="other">The collection to add elements from.</param>
		/// <returns>A new immutable set with the items added; or the original set if all the items were already in the set.</returns>
		// Token: 0x0600031E RID: 798 RVA: 0x00008B02 File Offset: 0x00006D02
		IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		/// <summary>Adds an element to the current set and returns a value to indicate if the element was successfully added.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>
		///   <see langword="true" /> if the element is added to the set; <see langword="false" /> if the element is already in the set.</returns>
		// Token: 0x0600031F RID: 799 RVA: 0x00008B0B File Offset: 0x00006D0B
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes all elements in the specified collection from the current set.</summary>
		/// <param name="other">The collection of items to remove from the set.</param>
		// Token: 0x06000320 RID: 800 RVA: 0x00008B12 File Offset: 0x00006D12
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		/// <summary>Modifies the current set so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		// Token: 0x06000321 RID: 801 RVA: 0x00008B19 File Offset: 0x00006D19
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		/// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		// Token: 0x06000322 RID: 802 RVA: 0x00008B20 File Offset: 0x00006D20
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		/// <summary>Modifies the current set so that it contains all elements that are present in either the current set or the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		// Token: 0x06000323 RID: 803 RVA: 0x00008B27 File Offset: 0x00006D27
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns true, since immutable collections are always read-only. See the <see cref="T:System.Collections.Generic.ICollection`1" /> interface.</summary>
		/// <returns>A boolean value indicating whether the collection is read-only.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00008B2E File Offset: 0x00006D2E
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies the elements of the collection to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from collection. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000325 RID: 805 RVA: 0x00008B31 File Offset: 0x00006D31
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		/// <summary>Adds the specified value to the collection.</summary>
		/// <param name="item">The value to add.</param>
		// Token: 0x06000326 RID: 806 RVA: 0x00008B40 File Offset: 0x00006D40
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes all the items from the collection.</summary>
		// Token: 0x06000327 RID: 807 RVA: 0x00008B47 File Offset: 0x00006D47
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="item">The object to remove from the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000328 RID: 808 RVA: 0x00008B4E File Offset: 0x00006D4E
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>See the <see cref="T:System.Collections.Generic.IList`1" /> interface.</summary>
		/// <param name="index">The zero-based index of the item to access.</param>
		/// <returns>The element stored at the specified index.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00008B55 File Offset: 0x00006D55
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00008B5E File Offset: 0x00006D5E
		T IList<T>.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Inserts an item in the set at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the set.</param>
		// Token: 0x0600032B RID: 811 RVA: 0x00008B65 File Offset: 0x00006D65
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x0600032C RID: 812 RVA: 0x00008B6C File Offset: 0x00006D6C
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008B73 File Offset: 0x00006D73
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00008B76 File Offset: 0x00006D76
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>See <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>Object used for synchronizing access to the collection.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00008B79 File Offset: 0x00006D79
		[DebuggerBrowsable(0)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Returns true, since immutable collections are always thread-safe. See the <see cref="T:System.Collections.ICollection" /> interface.</summary>
		/// <returns>A boolean value indicating whether the collection is thread-safe.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00008B7C File Offset: 0x00006D7C
		[DebuggerBrowsable(0)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Adds an item to the set.</summary>
		/// <param name="value">The object to add to the set.</param>
		/// <exception cref="T:System.NotSupportedException">The set is read-only or has a fixed size.</exception>
		/// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
		// Token: 0x06000331 RID: 817 RVA: 0x00008B7F File Offset: 0x00006D7F
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes all items from the set.</summary>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		// Token: 0x06000332 RID: 818 RVA: 0x00008B86 File Offset: 0x00006D86
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the set contains a specific value.</summary>
		/// <param name="value">The object to locate in the set.</param>
		/// <returns>
		///   <see langword="true" /> if the object is found in the set; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000333 RID: 819 RVA: 0x00008B8D File Offset: 0x00006D8D
		bool IList.Contains(object value)
		{
			return this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the set.</summary>
		/// <param name="value">The object to locate in the set.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06000334 RID: 820 RVA: 0x00008B9B File Offset: 0x00006D9B
		int IList.IndexOf(object value)
		{
			return this.IndexOf((T)((object)value));
		}

		/// <summary>Inserts an item into the set at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the set.</param>
		/// <exception cref="T:System.NotSupportedException">The set is read-only or has a fixed size.</exception>
		// Token: 0x06000335 RID: 821 RVA: 0x00008BA9 File Offset: 0x00006DA9
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the first occurrence of a specific object from the set.</summary>
		/// <param name="value">The object to remove from the set.</param>
		/// <exception cref="T:System.NotSupportedException">The set is read-only or has a fixed size.</exception>
		// Token: 0x06000336 RID: 822 RVA: 0x00008BB0 File Offset: 0x00006DB0
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes the item at the specified index of the set.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The set is read-only or has a fixed size.</exception>
		// Token: 0x06000337 RID: 823 RVA: 0x00008BB7 File Offset: 0x00006DB7
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> at the specified index.</summary>
		/// <param name="index">The index.</param>
		/// <exception cref="T:System.NotSupportedException" />
		/// <returns>The <see cref="T:System.Object" />.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00008BBE File Offset: 0x00006DBE
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00008BCC File Offset: 0x00006DCC
		[Nullable(2)]
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Copies the elements of the set to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the set. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600033A RID: 826 RVA: 0x00008BD3 File Offset: 0x00006DD3
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600033B RID: 827 RVA: 0x00008BE4 File Offset: 0x00006DE4
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator object that can be used to iterate through the collection.</returns>
		// Token: 0x0600033C RID: 828 RVA: 0x00008C11 File Offset: 0x00006E11
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the immutable sorted set.</summary>
		/// <returns>An enumerator that can be used to iterate through the set.</returns>
		// Token: 0x0600033D RID: 829 RVA: 0x00008C1E File Offset: 0x00006E1E
		[NullableContext(0)]
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008C2C File Offset: 0x00006E2C
		private static bool TryCastToImmutableSortedSet(IEnumerable<T> sequence, [NotNullWhen(true)] out ImmutableSortedSet<T> other)
		{
			other = sequence as ImmutableSortedSet<T>;
			if (other != null)
			{
				return true;
			}
			ImmutableSortedSet<T>.Builder builder = sequence as ImmutableSortedSet<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008C5C File Offset: 0x00006E5C
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008C7C File Offset: 0x00006E7C
		private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T t in items.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Add(t, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008CF4 File Offset: 0x00006EF4
		private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, this._comparer);
			}
			return this.Clear();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008D1C File Offset: 0x00006F1C
		private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
		{
			Requires.NotNull<IEnumerable<T>>(addedItems, "addedItems");
			List<T> list;
			if (this.IsEmpty)
			{
				int num;
				if (addedItems.TryGetCount<T>(out num) && num == 0)
				{
					return this;
				}
				list = new List<T>(addedItems);
				if (list.Count == 0)
				{
					return this;
				}
			}
			else
			{
				list = new List<T>(this);
				list.AddRange(addedItems);
			}
			IComparer<T> keyComparer = this.KeyComparer;
			list.Sort(keyComparer);
			int num2 = 1;
			for (int i = 1; i < list.Count; i++)
			{
				if (keyComparer.Compare(list[i], list[i - 1]) != 0)
				{
					list[num2++] = list[i];
				}
			}
			list.RemoveRange(num2, list.Count - num2);
			ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.NodeTreeFromList(list.AsOrderedCollection<T>(), 0, list.Count);
			return this.Wrap(node);
		}

		// Token: 0x04000035 RID: 53
		private const float RefillOverIncrementalThreshold = 0.15f;

		/// <summary>Gets an empty immutable sorted set.</summary>
		// Token: 0x04000036 RID: 54
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x04000037 RID: 55
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x04000038 RID: 56
		private readonly IComparer<T> _comparer;

		/// <summary>Represents a sorted set that enables changes with little or no memory allocations, and efficiently manipulates or builds immutable sorted sets.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x02000075 RID: 117
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, ICollection<T>, ICollection
		{
			// Token: 0x060005D7 RID: 1495 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			/// <summary>Gets the number of elements in the immutable sorted set.</summary>
			/// <returns>The number of elements in this set.</returns>
			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000FA2B File Offset: 0x0000DC2B
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			/// <summary>Gets a value that indicates whether this instance is read-only.</summary>
			/// <returns>Always <see langword="false" />.</returns>
			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000FA38 File Offset: 0x0000DC38
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the element of the set at the given index.</summary>
			/// <param name="index">The 0-based index of the element in the set to return.</param>
			/// <returns>The element at the given position.</returns>
			// Token: 0x1700012A RID: 298
			public unsafe T this[int index]
			{
				get
				{
					return *this._root.ItemRef(index);
				}
			}

			/// <summary>Gets a read-only reference to the element of the set at the given <paramref name="index" />.</summary>
			/// <param name="index">The 0-based index of the element in the set to return.</param>
			/// <returns>A read-only reference to the element at the given position.</returns>
			// Token: 0x060005DB RID: 1499 RVA: 0x0000FA4E File Offset: 0x0000DC4E
			public readonly ref T ItemRef(int index)
			{
				return this._root.ItemRef(index);
			}

			/// <summary>Gets the maximum value in the immutable sorted set, as defined by the comparer.</summary>
			/// <returns>The maximum value in the set.</returns>
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000FA5C File Offset: 0x0000DC5C
			[Nullable(2)]
			public T Max
			{
				[NullableContext(2)]
				get
				{
					return this._root.Max;
				}
			}

			/// <summary>Gets the minimum value in the immutable sorted set, as defined by the comparer.</summary>
			/// <returns>The minimum value in the set.</returns>
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000FA69 File Offset: 0x0000DC69
			[Nullable(2)]
			public T Min
			{
				[NullableContext(2)]
				get
				{
					return this._root.Min;
				}
			}

			/// <summary>Gets or sets the object that is used to determine equality for the values in the immutable sorted set.</summary>
			/// <returns>The comparer that is used to determine equality for the values in the set.</returns>
			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000FA76 File Offset: 0x0000DC76
			// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000FA80 File Offset: 0x0000DC80
			public IComparer<T> KeyComparer
			{
				get
				{
					return this._comparer;
				}
				set
				{
					Requires.NotNull<IComparer<T>>(value, "value");
					if (value != this._comparer)
					{
						ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
						foreach (T t in this)
						{
							bool flag;
							node = node.Add(t, value, out flag);
						}
						this._immutable = null;
						this._comparer = value;
						this.Root = node;
					}
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000FB04 File Offset: 0x0000DD04
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000FB0C File Offset: 0x0000DD0C
			// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000FB14 File Offset: 0x0000DD14
			[Nullable(new byte[] { 1, 0 })]
			private ImmutableSortedSet<T>.Node Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			/// <summary>Adds an element to the current set and returns a value to indicate whether the element was successfully added.</summary>
			/// <param name="item">The element to add to the set.</param>
			/// <returns>
			///   <see langword="true" /> if the element is added to the set; <see langword="false" /> if the element is already in the set.</returns>
			// Token: 0x060005E3 RID: 1507 RVA: 0x0000FB3C File Offset: 0x0000DD3C
			public bool Add(T item)
			{
				bool flag;
				this.Root = this.Root.Add(item, this._comparer, out flag);
				return flag;
			}

			/// <summary>Removes the specified set of items from the current set.</summary>
			/// <param name="other">The collection of items to remove from the set.</param>
			// Token: 0x060005E4 RID: 1508 RVA: 0x0000FB64 File Offset: 0x0000DD64
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Remove(t, this._comparer, out flag);
				}
			}

			/// <summary>Modifies the current set so that it contains only elements that are also in a specified collection.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			// Token: 0x060005E5 RID: 1509 RVA: 0x0000FBCC File Offset: 0x0000DDCC
			public void IntersectWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						bool flag;
						node = node.Add(t, this._comparer, out flag);
					}
				}
				this.Root = node;
			}

			/// <summary>Determines whether the current set is a proper (strict) subset of a specified collection.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005E6 RID: 1510 RVA: 0x0000FC40 File Offset: 0x0000DE40
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			/// <summary>Determines whether the current set is a proper (strict) superset of a specified collection.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005E7 RID: 1511 RVA: 0x0000FC4E File Offset: 0x0000DE4E
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			/// <summary>Determines whether the current set is a subset of a specified collection.</summary>
			/// <param name="other">The collection is compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005E8 RID: 1512 RVA: 0x0000FC5C File Offset: 0x0000DE5C
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			/// <summary>Determines whether the current set is a superset of a specified collection.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005E9 RID: 1513 RVA: 0x0000FC6A File Offset: 0x0000DE6A
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			/// <summary>Determines whether the current set overlaps with the specified collection.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005EA RID: 1514 RVA: 0x0000FC78 File Offset: 0x0000DE78
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			/// <summary>Determines whether the current set and the specified collection contain the same elements.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			/// <returns>
			///   <see langword="true" /> if the current set is equal to <paramref name="other" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005EB RID: 1515 RVA: 0x0000FC86 File Offset: 0x0000DE86
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			/// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
			/// <param name="other">The collection to compare to the current set.</param>
			// Token: 0x060005EC RID: 1516 RVA: 0x0000FC94 File Offset: 0x0000DE94
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			/// <summary>Modifies the current set so that it contains all elements that are present in both the current set and in the specified collection.</summary>
			/// <param name="other">The collection to compare to the current state.</param>
			// Token: 0x060005ED RID: 1517 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T t in other)
				{
					bool flag;
					this.Root = this.Root.Add(t, this._comparer, out flag);
				}
			}

			/// <summary>Adds an element to the current set and returns a value to indicate whether the element was successfully added.</summary>
			/// <param name="item">The element to add to the set.</param>
			// Token: 0x060005EE RID: 1518 RVA: 0x0000FD18 File Offset: 0x0000DF18
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			/// <summary>Removes all elements from this set.</summary>
			// Token: 0x060005EF RID: 1519 RVA: 0x0000FD22 File Offset: 0x0000DF22
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			/// <summary>Determines whether the set contains the specified object.</summary>
			/// <param name="item">The object to locate in the set.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="item" /> is found in the set; otherwise, <see langword="false" />.</returns>
			// Token: 0x060005F0 RID: 1520 RVA: 0x0000FD2F File Offset: 0x0000DF2F
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x060005F1 RID: 1521 RVA: 0x0000FD43 File Offset: 0x0000DF43
			void ICollection<T>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			/// <summary>Removes the first occurrence of the specified object from the set.</summary>
			/// <param name="item">The object to remove from the set.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="item" /> was removed from the set; <see langword="false" /> if <paramref name="item" /> was not found in the set.</returns>
			// Token: 0x060005F2 RID: 1522 RVA: 0x0000FD54 File Offset: 0x0000DF54
			public bool Remove(T item)
			{
				bool flag;
				this.Root = this.Root.Remove(item, this._comparer, out flag);
				return flag;
			}

			/// <summary>Returns an enumerator that iterates through the set.</summary>
			/// <returns>A enumerator that can be used to iterate through the set.</returns>
			// Token: 0x060005F3 RID: 1523 RVA: 0x0000FD7C File Offset: 0x0000DF7C
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>A enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x060005F4 RID: 1524 RVA: 0x0000FD8A File Offset: 0x0000DF8A
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>A enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x060005F5 RID: 1525 RVA: 0x0000FD9C File Offset: 0x0000DF9C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates over the immutable sorted set in reverse order.</summary>
			/// <returns>An enumerator that iterates over the set in reverse order.</returns>
			// Token: 0x060005F6 RID: 1526 RVA: 0x0000FDA9 File Offset: 0x0000DFA9
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			/// <summary>Creates an immutable sorted set based on the contents of this instance.</summary>
			/// <returns>An immutable set.</returns>
			// Token: 0x060005F7 RID: 1527 RVA: 0x0000FDB6 File Offset: 0x0000DFB6
			public ImmutableSortedSet<T> ToImmutable()
			{
				if (this._immutable == null)
				{
					this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer);
				}
				return this._immutable;
			}

			/// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
			/// <param name="equalValue">The value for which to search.</param>
			/// <param name="actualValue">The value from the set that the search found, or the original value if the search yielded no match.</param>
			/// <returns>A value indicating whether the search was successful.</returns>
			// Token: 0x060005F8 RID: 1528 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
			public bool TryGetValue(T equalValue, out T actualValue)
			{
				ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
				if (!node.IsEmpty)
				{
					actualValue = node.Key;
					return true;
				}
				actualValue = equalValue;
				return false;
			}

			/// <summary>Copies the elements of the set to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the set. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x060005F9 RID: 1529 RVA: 0x0000FE1E File Offset: 0x0000E01E
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread-safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread-safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000FE2D File Offset: 0x0000E02D
			[DebuggerBrowsable(0)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000FE30 File Offset: 0x0000E030
			[DebuggerBrowsable(0)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			// Token: 0x040000E3 RID: 227
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x040000E4 RID: 228
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x040000E5 RID: 229
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x040000E6 RID: 230
			private int _version;

			// Token: 0x040000E7 RID: 231
			private object _syncRoot;
		}

		// Token: 0x02000076 RID: 118
		private class ReverseEnumerable : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x0000FE52 File Offset: 0x0000E052
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0000FE6C File Offset: 0x0000E06C
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0000FE79 File Offset: 0x0000E079
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000E8 RID: 232
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		/// <summary>Enumerates the contents of a binary tree.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x02000077 RID: 119
		[NullableContext(0)]
		[EditorBrowsable(2)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x060005FF RID: 1535 RVA: 0x0000FE84 File Offset: 0x0000E084
			internal Enumerator([Nullable(new byte[] { 1, 0 })] ImmutableSortedSet<T>.Node root, [Nullable(new byte[] { 2, 0 })] ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._reverse = reverse;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : (-1));
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
				{
					this._stack = ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>(root.Height));
				}
				this.PushNext(this._root);
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000FF21 File Offset: 0x0000E121
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.  
			///
			///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
			/// <returns>The element at the current position of the enumerator.</returns>
			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000FF29 File Offset: 0x0000E129
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			/// <summary>The current element.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000FF4A File Offset: 0x0000E14A
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Releases the resources used by the current instance of the <see cref="T:System.Collections.Immutable.ImmutableSortedSet`1.Enumerator" /> class.  
			///
			///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
			// Token: 0x06000603 RID: 1539 RVA: 0x0000FF58 File Offset: 0x0000E158
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedSet<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
					ImmutableSortedSet<T>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
					this._stack = null;
				}
			}

			/// <summary>Advances the enumerator to the next element of the immutable sorted set.  
			///
			///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the sorted set.</returns>
			// Token: 0x06000604 RID: 1540 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				if (stack.Count > 0)
				{
					ImmutableSortedSet<T>.Node value = stack.Pop().Value;
					this._current = value;
					this.PushNext(this._reverse ? value.Left : value.Right);
					return true;
				}
				this._current = null;
				return false;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the immutable sorted set.  
			///
			///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
			// Token: 0x06000605 RID: 1541 RVA: 0x00010018 File Offset: 0x0000E218
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : (-1));
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001006D File Offset: 0x0000E26D
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x00010098 File Offset: 0x0000E298
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x000100C0 File Offset: 0x0000E2C0
			private void PushNext(ImmutableSortedSet<T>.Node node)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedSet<T>.Node>(node));
					node = (this._reverse ? node.Right : node.Left);
				}
			}

			// Token: 0x040000E9 RID: 233
			private static readonly SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>();

			// Token: 0x040000EA RID: 234
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x040000EB RID: 235
			private readonly int _poolUserId;

			// Token: 0x040000EC RID: 236
			private readonly bool _reverse;

			// Token: 0x040000ED RID: 237
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x040000EE RID: 238
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x040000EF RID: 239
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x040000F0 RID: 240
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000078 RID: 120
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600060A RID: 1546 RVA: 0x0001011F File Offset: 0x0000E31F
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x00010130 File Offset: 0x0000E330
			private Node(T key, ImmutableSortedSet<T>.Node left, ImmutableSortedSet<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600060C RID: 1548 RVA: 0x000101A5 File Offset: 0x0000E3A5
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x000101B0 File Offset: 0x0000E3B0
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600060E RID: 1550 RVA: 0x000101B8 File Offset: 0x0000E3B8
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Left
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600060F RID: 1551 RVA: 0x000101C0 File Offset: 0x0000E3C0
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000610 RID: 1552 RVA: 0x000101C8 File Offset: 0x0000E3C8
			[Nullable(new byte[] { 2, 0 })]
			public ImmutableSortedSet<T>.Node Right
			{
				[return: Nullable(new byte[] { 2, 0 })]
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x000101D0 File Offset: 0x0000E3D0
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000612 RID: 1554 RVA: 0x000101D8 File Offset: 0x0000E3D8
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x000101E0 File Offset: 0x0000E3E0
			[Nullable(new byte[] { 2, 1 })]
			IBinaryTree<T> IBinaryTree<T>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000614 RID: 1556 RVA: 0x000101E8 File Offset: 0x0000E3E8
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000615 RID: 1557 RVA: 0x000101F0 File Offset: 0x0000E3F0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000616 RID: 1558 RVA: 0x000101F8 File Offset: 0x0000E3F8
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000617 RID: 1559 RVA: 0x00010200 File Offset: 0x0000E400
			[Nullable(2)]
			internal T Max
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._right.IsEmpty)
					{
						node = node._right;
					}
					return node._key;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000618 RID: 1560 RVA: 0x00010240 File Offset: 0x0000E440
			[Nullable(2)]
			internal T Min
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._left.IsEmpty)
					{
						node = node._left;
					}
					return node._key;
				}
			}

			// Token: 0x17000142 RID: 322
			internal T this[int index]
			{
				get
				{
					Requires.Range(index >= 0 && index < this.Count, "index", null);
					if (index < this._left._count)
					{
						return this._left[index];
					}
					if (index > this._left._count)
					{
						return this._right[index - this._left._count - 1];
					}
					return this._key;
				}
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x000102F4 File Offset: 0x0000E4F4
			internal readonly ref T ItemRef(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this._left._count)
				{
					return this._left.ItemRef(index);
				}
				if (index > this._left._count)
				{
					return this._right.ItemRef(index - this._left._count - 1);
				}
				return ref this._key;
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x00010366 File Offset: 0x0000E566
			[NullableContext(0)]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00010370 File Offset: 0x0000E570
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0001037D File Offset: 0x0000E57D
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0001038A File Offset: 0x0000E58A
			[NullableContext(0)]
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator([Nullable(new byte[] { 1, 0 })] ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x00010394 File Offset: 0x0000E594
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x00010420 File Offset: 0x0000E620
			internal void CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array.SetValue(t, arrayIndex++);
				}
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x000104B4 File Offset: 0x0000E6B4
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedSet<T>.Node(key, this, this, false);
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedSet<T>.Node node2 = this._right.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node2);
					}
				}
				else
				{
					if (num >= 0)
					{
						mutated = false;
						return this;
					}
					ImmutableSortedSet<T>.Node node3 = this._left.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node3, null);
					}
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedSet<T>.Node.MakeBalanced(node);
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x00010548 File Offset: 0x0000E748
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedSet<T>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableSortedSet<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedSet<T>.Node node3 = this._right.Remove(node2._key, comparer, out flag);
						node = node2.Mutate(this._left, node3);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedSet<T>.Node node4 = this._left.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(node4, null);
					}
				}
				else
				{
					ImmutableSortedSet<T>.Node node5 = this._right.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, node5);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedSet<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001068D File Offset: 0x0000E88D
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x000106AA File Offset: 0x0000E8AA
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x000106D4 File Offset: 0x0000E8D4
			[return: Nullable(new byte[] { 1, 0 })]
			internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return this;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, comparer);
				}
				return this._left.Search(key, comparer);
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x00010728 File Offset: 0x0000E928
			internal int IndexOf(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return -1;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this._left.Count;
				}
				if (num > 0)
				{
					int num2 = this._right.IndexOf(key, comparer);
					bool flag = num2 < 0;
					if (flag)
					{
						num2 = ~num2;
					}
					num2 = this._left.Count + 1 + num2;
					if (flag)
					{
						num2 = ~num2;
					}
					return num2;
				}
				return this._left.IndexOf(key, comparer);
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x000107A9 File Offset: 0x0000E9A9
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x000107B8 File Offset: 0x0000E9B8
			private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x000107FC File Offset: 0x0000E9FC
			private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x00010840 File Offset: 0x0000EA40
			private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node node = tree.Mutate(null, ImmutableSortedSet<T>.Node.RotateRight(tree._right));
				return ImmutableSortedSet<T>.Node.RotateLeft(node);
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x00010880 File Offset: 0x0000EA80
			private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node node = tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree._left), null);
				return ImmutableSortedSet<T>.Node.RotateRight(node);
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x000108C0 File Offset: 0x0000EAC0
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x000108E4 File Offset: 0x0000EAE4
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x000108FD File Offset: 0x0000EAFD
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x00010918 File Offset: 0x0000EB18
			private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedSet<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateLeft(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedSet<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateRight(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x0001097C File Offset: 0x0000EB7C
			[return: Nullable(new byte[] { 1, 0 })]
			internal static ImmutableSortedSet<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				if (length == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedSet<T>.Node node2 = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableSortedSet<T>.Node(items[start + num2], node, node2, true);
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x000109D0 File Offset: 0x0000EBD0
			private ImmutableSortedSet<T>.Node Mutate(ImmutableSortedSet<T>.Node left = null, ImmutableSortedSet<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedSet<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				this._count = 1 + this._left._count + this._right._count;
				return this;
			}

			// Token: 0x040000F1 RID: 241
			[Nullable(new byte[] { 1, 0 })]
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x040000F2 RID: 242
			private readonly T _key;

			// Token: 0x040000F3 RID: 243
			private bool _frozen;

			// Token: 0x040000F4 RID: 244
			private byte _height;

			// Token: 0x040000F5 RID: 245
			private int _count;

			// Token: 0x040000F6 RID: 246
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x040000F7 RID: 247
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
