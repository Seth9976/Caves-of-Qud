using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Collections.Immutable
{
	/// <summary>Represents an array that is immutable; meaning it cannot be changed once it is created.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of element stored by the array.</typeparam>
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NonVersionable]
	public struct ImmutableArray<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, IList<T>, ICollection<T>, IEquatable<ImmutableArray<T>>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable, IImmutableList<T>
	{
		/// <summary>Gets or sets the element at the specified index in the read-only list.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown from the setter.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>The element at the specified index in the read-only list.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004158 File Offset: 0x00002358
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000417B File Offset: 0x0000237B
		T IList<T>.Item
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value indicating whether this instance is read only.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is read only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004182 File Offset: 0x00002382
		[DebuggerBrowsable(0)]
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>Number of items in the collection.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004188 File Offset: 0x00002388
		[DebuggerBrowsable(0)]
		int ICollection<T>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000041AC File Offset: 0x000023AC
		[DebuggerBrowsable(0)]
		int IReadOnlyCollection<T>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		/// <summary>Gets the element at the specified index.</summary>
		/// <param name="index">The index.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>The element.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000041D0 File Offset: 0x000023D0
		T IReadOnlyList<T>.Item
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		/// <summary>Creates a new read-only span over this immutable array.</summary>
		/// <returns>The read-only span representation of this immutable array.</returns>
		// Token: 0x06000119 RID: 281 RVA: 0x000041F3 File Offset: 0x000023F3
		[return: Nullable(new byte[] { 0, 1 })]
		public ReadOnlySpan<T> AsSpan()
		{
			return new ReadOnlySpan<T>(this.array);
		}

		/// <summary>Creates a new read-only memory region over this immutable array.</summary>
		/// <returns>The read-only memory representation of this immutable array.</returns>
		// Token: 0x0600011A RID: 282 RVA: 0x00004200 File Offset: 0x00002400
		[return: Nullable(new byte[] { 0, 1 })]
		public ReadOnlyMemory<T> AsMemory()
		{
			return new ReadOnlyMemory<T>(this.array);
		}

		/// <summary>Searches the array for the specified item.</summary>
		/// <param name="item">The item to search for.</param>
		/// <returns>The zero-based index position of the item if it is found, or -1 if it is not.</returns>
		// Token: 0x0600011B RID: 283 RVA: 0x00004210 File Offset: 0x00002410
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>The zero-based index position of the item if it is found, or -1 if it is not.</returns>
		// Token: 0x0600011C RID: 284 RVA: 0x0000423C File Offset: 0x0000243C
		public int IndexOf(T item, int startIndex, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		/// <summary>Searches the array for the specified item.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <returns>The zero-based index position of the item if it is found, or -1 if it is not.</returns>
		// Token: 0x0600011D RID: 285 RVA: 0x00004264 File Offset: 0x00002464
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <param name="count">The number of elements to search.</param>
		/// <returns>The zero-based index position of the item if it is found, or -1 if it is not.</returns>
		// Token: 0x0600011E RID: 286 RVA: 0x0000428F File Offset: 0x0000248F
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <param name="count">The number of elements to search.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>The zero-based index position of the item if it is found, or -1 if it is not.</returns>
		// Token: 0x0600011F RID: 287 RVA: 0x000042A0 File Offset: 0x000024A0
		public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (count == 0 && startIndex == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex + count <= immutableArray.Length, "count", null);
			equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.IndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (equalityComparer.Equals(immutableArray.array[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Searches the array for the specified item; starting at the end of the array.</summary>
		/// <param name="item">The item to search for.</param>
		/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
		// Token: 0x06000120 RID: 288 RVA: 0x0000434C File Offset: 0x0000254C
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item; starting at the end of the array.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
		// Token: 0x06000121 RID: 289 RVA: 0x00004388 File Offset: 0x00002588
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0 && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item; starting at the end of the array.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <param name="count">The number of elements to search.</param>
		/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
		// Token: 0x06000122 RID: 290 RVA: 0x000043BB File Offset: 0x000025BB
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		/// <summary>Searches the array for the specified item; starting at the end of the array.</summary>
		/// <param name="item">The item to search for.</param>
		/// <param name="startIndex">The index at which to begin the search.</param>
		/// <param name="count">The number of elements to search.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
		// Token: 0x06000123 RID: 291 RVA: 0x000043CC File Offset: 0x000025CC
		public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (startIndex == 0 && count == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
			equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.LastIndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (equalityComparer.Equals(item, immutableArray.array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Determines whether the specified item exists in the array.</summary>
		/// <param name="item">The item to search for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified item was found in the array; otherwise <see langword="false" />.</returns>
		// Token: 0x06000124 RID: 292 RVA: 0x00004476 File Offset: 0x00002676
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		/// <summary>Returns a new array with the specified value inserted at the specified position.</summary>
		/// <param name="index">The 0-based index into the array at which the new item should be added.</param>
		/// <param name="item">The item to insert at the start of the array.</param>
		/// <returns>A new array with the item inserted at the specified index.</returns>
		// Token: 0x06000125 RID: 293 RVA: 0x00004488 File Offset: 0x00002688
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Insert(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			T[] array = new T[immutableArray.Length + 1];
			array[index] = item;
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + 1, immutableArray.Length - index);
			}
			return new ImmutableArray<T>(array);
		}

		/// <summary>Inserts the specified values at the specified index.</summary>
		/// <param name="index">The index at which to insert the value.</param>
		/// <param name="items">The elements to insert.</param>
		/// <returns>A new immutable array with the items inserted at the specified index.</returns>
		// Token: 0x06000126 RID: 294 RVA: 0x00004524 File Offset: 0x00002724
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			int count = ImmutableExtensions.GetCount<T>(ref items);
			if (count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + count];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + count, immutableArray.Length - index);
			}
			if (!items.TryCopyTo<T>(array, index))
			{
				int num = index;
				foreach (T t in items)
				{
					array[num++] = t;
				}
			}
			return new ImmutableArray<T>(array);
		}

		/// <summary>Inserts the specified values at the specified index.</summary>
		/// <param name="index">The index at which to insert the value.</param>
		/// <param name="items">The elements to insert.</param>
		/// <returns>A new immutable array with the items inserted at the specified index.</returns>
		// Token: 0x06000127 RID: 295 RVA: 0x00004624 File Offset: 0x00002824
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> InsertRange(int index, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.IsEmpty)
			{
				return items;
			}
			if (items.IsEmpty)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + items.Length];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + items.Length, immutableArray.Length - index);
			}
			Array.Copy(items.array, 0, array, index, items.Length);
			return new ImmutableArray<T>(array);
		}

		/// <summary>Returns a copy of the original array with the specified item added to the end.</summary>
		/// <param name="item">The item to be added to the end of the array.</param>
		/// <returns>A new array with the specified item added to the end.</returns>
		// Token: 0x06000128 RID: 296 RVA: 0x000046E4 File Offset: 0x000028E4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Add(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			return immutableArray.Insert(immutableArray.Length, item);
		}

		/// <summary>Returns a copy of the original array with the specified elements added to the end of the array.</summary>
		/// <param name="items">The elements to add to the array.</param>
		/// <returns>A new array with the elements added.</returns>
		// Token: 0x06000129 RID: 297 RVA: 0x00004718 File Offset: 0x00002918
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		/// <summary>Returns a copy of the original array with the specified elements added to the end of the array.</summary>
		/// <param name="items">The elements to add to the array.</param>
		/// <returns>A new array with the elements added.</returns>
		// Token: 0x0600012A RID: 298 RVA: 0x0000473C File Offset: 0x0000293C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The index of the item to replace.</param>
		/// <param name="item">The item to add to the list.</param>
		/// <returns>The new array that contains <paramref name="item" /> at the specified index.</returns>
		// Token: 0x0600012B RID: 299 RVA: 0x00004760 File Offset: 0x00002960
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> SetItem(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			T[] array = new T[immutableArray.Length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			array[index] = item;
			return new ImmutableArray<T>(array);
		}

		/// <summary>Finds the first element in the array equal to the specified value and replaces the value with the specified new value.</summary>
		/// <param name="oldValue">The value to find and replace in the array.</param>
		/// <param name="newValue">The value to replace the <c>oldvalue</c> with.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is not found in the array.</exception>
		/// <returns>A new array that contains <paramref name="newValue" /> even if the new and old values are the same.</returns>
		// Token: 0x0600012C RID: 300 RVA: 0x000047C5 File Offset: 0x000029C5
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		/// <summary>Finds the first element in the array equal to the specified value and replaces the value with the specified new value.</summary>
		/// <param name="oldValue">The value to find and replace in the array.</param>
		/// <param name="newValue">The value to replace the <c>oldvalue</c> with.</param>
		/// <param name="equalityComparer">The equality comparer to use to compare values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is not found in the array.</exception>
		/// <returns>A new array that contains <paramref name="newValue" /> even if the new and old values are the same.</returns>
		// Token: 0x0600012D RID: 301 RVA: 0x000047D4 File Offset: 0x000029D4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Replace(T oldValue, T newValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			int num = immutableArray.IndexOf(oldValue, 0, immutableArray.Length, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return immutableArray.SetItem(num, newValue);
		}

		/// <summary>Returns an array with the first occurrence of the specified element removed from the array. If no match is found, the current array is returned.</summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>A new array with the item removed.</returns>
		// Token: 0x0600012E RID: 302 RVA: 0x00004817 File Offset: 0x00002A17
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		/// <summary>Returns an array with the first occurrence of the specified element removed from the array.  
		///
		///  If no match is found, the current array is returned.</summary>
		/// <param name="item">The item to remove.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>A new array with the specified item removed.</returns>
		// Token: 0x0600012F RID: 303 RVA: 0x00004828 File Offset: 0x00002A28
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Remove(T item, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			int num = immutableArray.IndexOf(item, 0, immutableArray.Length, equalityComparer);
			if (num >= 0)
			{
				return immutableArray.RemoveAt(num);
			}
			return immutableArray;
		}

		/// <summary>Returns an array with the element at the specified position removed.</summary>
		/// <param name="index">The 0-based index of the element to remove from the returned array.</param>
		/// <returns>A new array with the item at the specified index removed.</returns>
		// Token: 0x06000130 RID: 304 RVA: 0x00004863 File Offset: 0x00002A63
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		/// <summary>Returns an array with the elements at the specified position removed.</summary>
		/// <param name="index">The 0-based index of the starting element to remove from the array.</param>
		/// <param name="length">The number of elements to remove from the array.</param>
		/// <returns>The new array with the specified elements removed.</returns>
		// Token: 0x06000131 RID: 305 RVA: 0x00004870 File Offset: 0x00002A70
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(int index, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.Range(length >= 0 && index + length <= immutableArray.Length, "length", null);
			if (length == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - length];
			Array.Copy(immutableArray.array, array, index);
			Array.Copy(immutableArray.array, index + length, array, index, immutableArray.Length - index - length);
			return new ImmutableArray<T>(array);
		}

		/// <summary>Removes the specified items from this array.</summary>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <returns>A new array with the elements removed.</returns>
		// Token: 0x06000132 RID: 306 RVA: 0x0000490D File Offset: 0x00002B0D
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		/// <summary>Removes the specified items from this array.</summary>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>A new array with the elements removed.</returns>
		// Token: 0x06000133 RID: 307 RVA: 0x0000491C File Offset: 0x00002B1C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEnumerable<T>>(items, "items");
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (T t in items)
			{
				int num = immutableArray.IndexOf(t, 0, immutableArray.Length, equalityComparer);
				while (num >= 0 && !sortedSet.Add(num) && num + 1 < immutableArray.Length)
				{
					num = immutableArray.IndexOf(t, num + 1, equalityComparer);
				}
			}
			return immutableArray.RemoveAtRange(sortedSet);
		}

		/// <summary>Removes the specified values from this list.</summary>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <returns>A new list with the elements removed.</returns>
		// Token: 0x06000134 RID: 308 RVA: 0x000049C4 File Offset: 0x00002BC4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		/// <summary>Removes the specified items from this list.</summary>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>A new array with the elements removed.</returns>
		// Token: 0x06000135 RID: 309 RVA: 0x000049D4 File Offset: 0x00002BD4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			Requires.NotNull<T[]>(items.array, "items");
			if (items.IsEmpty)
			{
				immutableArray.ThrowNullRefIfNotInitialized();
				return immutableArray;
			}
			if (items.Length == 1)
			{
				return immutableArray.Remove(items[0], equalityComparer);
			}
			return immutableArray.RemoveRange(items.array, equalityComparer);
		}

		/// <summary>Removes all the items from the array that meet the specified condition.</summary>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <returns>A new array with items that meet the specified condition removed.</returns>
		// Token: 0x06000136 RID: 310 RVA: 0x00004A34 File Offset: 0x00002C34
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Predicate<T>>(match, "match");
			if (immutableArray.IsEmpty)
			{
				return immutableArray;
			}
			List<int> list = null;
			for (int i = 0; i < immutableArray.array.Length; i++)
			{
				if (match.Invoke(immutableArray.array[i]))
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			if (list == null)
			{
				return immutableArray;
			}
			return immutableArray.RemoveAtRange(list);
		}

		/// <summary>Returns an array with all the elements removed.</summary>
		/// <returns>An array with all of the elements removed.</returns>
		// Token: 0x06000137 RID: 311 RVA: 0x00004AAB File Offset: 0x00002CAB
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		/// <summary>Sorts the elements in the immutable array using the default comparer.</summary>
		/// <returns>A new immutable array that contains the items in this array, in sorted order.</returns>
		// Token: 0x06000138 RID: 312 RVA: 0x00004AB4 File Offset: 0x00002CB4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> using             the specified <see cref="T:System.Comparison`1" />.</summary>
		/// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparison" /> is null.</exception>
		/// <returns>The sorted list.</returns>
		// Token: 0x06000139 RID: 313 RVA: 0x00004ADC File Offset: 0x00002CDC
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(Comparer<T>.Create(comparison));
		}

		/// <summary>Sorts the elements in the immutable array using the specified comparer.</summary>
		/// <param name="comparer">The implementation to use when comparing elements, or <see langword="null" /> to use the default comparer.</param>
		/// <returns>A new immutable array that contains the items in this array, in sorted order.</returns>
		// Token: 0x0600013A RID: 314 RVA: 0x00004B08 File Offset: 0x00002D08
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		/// <summary>Sorts the specified elements in the immutable array using the specified comparer.</summary>
		/// <param name="index">The index of the first element to sort.</param>
		/// <param name="count">The number of elements to include in the sort.</param>
		/// <param name="comparer">The implementation to use when comparing elements, or <see langword="null" /> to use the default comparer.</param>
		/// <returns>A new immutable array that contains the items in this array, in sorted order.</returns>
		// Token: 0x0600013B RID: 315 RVA: 0x00004B2C File Offset: 0x00002D2C
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0 && index + count <= immutableArray.Length, "count", null);
			if (count > 1)
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				bool flag = false;
				for (int i = index + 1; i < index + count; i++)
				{
					if (comparer.Compare(immutableArray.array[i - 1], immutableArray.array[i]) > 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					T[] array = new T[immutableArray.Length];
					Array.Copy(immutableArray.array, array, immutableArray.Length);
					Array.Sort<T>(array, index, count, comparer);
					return new ImmutableArray<T>(array);
				}
			}
			return immutableArray;
		}

		/// <summary>Filters the elements of this array to those assignable to the specified type.</summary>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> that contains elements from the input sequence of type of <paramref name="TResult" />.</returns>
		// Token: 0x0600013C RID: 316 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public IEnumerable<TResult> OfType<[Nullable(2)] TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return Enumerable.OfType<TResult>(immutableArray.array);
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="index">The index of the location to insert the item.</param>
		/// <param name="item">The item to insert.</param>
		// Token: 0x0600013D RID: 317 RVA: 0x00004C2E File Offset: 0x00002E2E
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="index">The index.</param>
		// Token: 0x0600013E RID: 318 RVA: 0x00004C35 File Offset: 0x00002E35
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="item">The item to add to the end of the array.</param>
		// Token: 0x0600013F RID: 319 RVA: 0x00004C3C File Offset: 0x00002E3C
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		// Token: 0x06000140 RID: 320 RVA: 0x00004C43 File Offset: 0x00002E43
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="item">The object to remove from the array.</param>
		/// <returns>Throws <see cref="T:System.NotSupportedException" /> in all cases.</returns>
		// Token: 0x06000141 RID: 321 RVA: 0x00004C4A File Offset: 0x00002E4A
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns an array with all the elements removed.</summary>
		/// <returns>An array with all the elements removed.</returns>
		// Token: 0x06000142 RID: 322 RVA: 0x00004C54 File Offset: 0x00002E54
		IImmutableList<T> IImmutableList<T>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		/// <summary>Returns a copy of the original array with the specified item added to the end.</summary>
		/// <param name="value">The value to add to the end of the array.</param>
		/// <returns>A new array with the specified item added to the end.</returns>
		// Token: 0x06000143 RID: 323 RVA: 0x00004C7C File Offset: 0x00002E7C
		IImmutableList<T> IImmutableList<T>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		/// <summary>Returns a copy of the original array with the specified elements added to the end of the array.</summary>
		/// <param name="items">The elements to add to the end of the array.</param>
		/// <returns>A new array with the elements added to the end.</returns>
		// Token: 0x06000144 RID: 324 RVA: 0x00004CA4 File Offset: 0x00002EA4
		IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		/// <summary>Returns a new array with the specified value inserted at the specified position.</summary>
		/// <param name="index">The 0-based index into the array at which the new item should be added.</param>
		/// <param name="element">The item to insert at the start of the array.</param>
		/// <returns>A new array with the specified value inserted.</returns>
		// Token: 0x06000145 RID: 325 RVA: 0x00004CCC File Offset: 0x00002ECC
		IImmutableList<T> IImmutableList<T>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		/// <summary>Inserts the specified values at the specified index.</summary>
		/// <param name="index">The index at which to insert the value.</param>
		/// <param name="items">The elements to insert.</param>
		/// <returns>A new array with the specified values inserted.</returns>
		// Token: 0x06000146 RID: 326 RVA: 0x00004CF8 File Offset: 0x00002EF8
		IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		/// <summary>Returns an array with the first occurrence of the specified element removed from the array; if no match is found, the current array is returned.</summary>
		/// <param name="value">The value to remove from the array.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>A new array with the value removed.</returns>
		// Token: 0x06000147 RID: 327 RVA: 0x00004D24 File Offset: 0x00002F24
		IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		/// <summary>Removes all the items from the array that meet the specified condition.</summary>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <returns>A new array with items that meet the specified condition removed.</returns>
		// Token: 0x06000148 RID: 328 RVA: 0x00004D50 File Offset: 0x00002F50
		IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		/// <summary>Removes the specified items from this array.</summary>
		/// <param name="items">The items to remove if matches are found in this list.</param>
		/// <param name="equalityComparer">The equality comparer to use in the search.</param>
		/// <returns>A new array with the elements removed.</returns>
		// Token: 0x06000149 RID: 329 RVA: 0x00004D78 File Offset: 0x00002F78
		IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		/// <summary>Returns an array with the elements at the specified position removed.</summary>
		/// <param name="index">The 0-based index of the starting element to remove from the array.</param>
		/// <param name="count">The number of elements to remove from the array.</param>
		/// <returns>The new array with the specified elements removed.</returns>
		// Token: 0x0600014A RID: 330 RVA: 0x00004DA4 File Offset: 0x00002FA4
		IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		/// <summary>Returns an array with the element at the specified position removed.</summary>
		/// <param name="index">The 0-based index of the element to remove from the returned array.</param>
		/// <returns>A new array with the specified item removed.</returns>
		// Token: 0x0600014B RID: 331 RVA: 0x00004DD0 File Offset: 0x00002FD0
		IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The index of the item to replace.</param>
		/// <param name="value">The value to add to the list.</param>
		/// <returns>The new array that contains <paramref name="item" /> at the specified index.</returns>
		// Token: 0x0600014C RID: 332 RVA: 0x00004DF8 File Offset: 0x00002FF8
		IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		/// <summary>Finds the first element in the array equal to the specified value and replaces the value with the specified new value.</summary>
		/// <param name="oldValue">The value to find and replace in the array.</param>
		/// <param name="newValue">The value to replace the <c>oldvalue</c> with.</param>
		/// <param name="equalityComparer">The equality comparer to use to compare values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is not found in the array.</exception>
		/// <returns>A new array that contains <paramref name="newValue" /> even if the new and old values are the same.</returns>
		// Token: 0x0600014D RID: 333 RVA: 0x00004E24 File Offset: 0x00003024
		IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="value">The value to add to the array.</param>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		/// <returns>Throws <see cref="T:System.NotSupportedException" /> in all cases.</returns>
		// Token: 0x0600014E RID: 334 RVA: 0x00004E4E File Offset: 0x0000304E
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		// Token: 0x0600014F RID: 335 RVA: 0x00004E55 File Offset: 0x00003055
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="value">The value to check for.</param>
		/// <returns>Throws <see cref="T:System.NotSupportedException" /> in all cases.</returns>
		// Token: 0x06000150 RID: 336 RVA: 0x00004E5C File Offset: 0x0000305C
		bool IList.Contains(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Contains((T)((object)value));
		}

		/// <summary>Gets the value at the specified index.</summary>
		/// <param name="value">The value to return the index of.</param>
		/// <returns>The value of the element at the specified index.</returns>
		// Token: 0x06000151 RID: 337 RVA: 0x00004E84 File Offset: 0x00003084
		int IList.IndexOf(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.IndexOf((T)((object)value));
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="index">Index that indicates where to insert the item.</param>
		/// <param name="value">The value to insert.</param>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		// Token: 0x06000152 RID: 338 RVA: 0x00004EAC File Offset: 0x000030AC
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a value indicating whether this instance is fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004EB3 File Offset: 0x000030B3
		[DebuggerBrowsable(0)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether this instance is read only.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is read only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004EB6 File Offset: 0x000030B6
		[DebuggerBrowsable(0)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the size of the array.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004EBC File Offset: 0x000030BC
		[DebuggerBrowsable(0)]
		int ICollection.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		/// <summary>See the <see cref="T:System.Collections.ICollection" /> interface. Always returns <see langword="true" /> since since immutable collections are thread-safe.</summary>
		/// <returns>Boolean value determining whether the collection is thread-safe.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004EDE File Offset: 0x000030DE
		[DebuggerBrowsable(0)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the sync root.</summary>
		/// <returns>An object for synchronizing access to the collection.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004EE1 File Offset: 0x000030E1
		[DebuggerBrowsable(0)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="value">The value to remove from the array.</param>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		// Token: 0x06000158 RID: 344 RVA: 0x00004EE8 File Offset: 0x000030E8
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Throws <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <param name="index">The index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Thrown in all cases.</exception>
		// Token: 0x06000159 RID: 345 RVA: 0x00004EEF File Offset: 0x000030EF
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> at the specified index.</summary>
		/// <param name="index">The index.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown from the setter.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>The object at the specified index.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004EF8 File Offset: 0x000030F8
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004F20 File Offset: 0x00003120
		[Nullable(2)]
		object IList.Item
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Copies this array to another array starting at the specified index.</summary>
		/// <param name="array">The array to copy this array to.</param>
		/// <param name="index">The index in the destination array to start the copy operation.</param>
		// Token: 0x0600015C RID: 348 RVA: 0x00004F28 File Offset: 0x00003128
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		/// <summary>Determines whether this array is structurally equal to the specified array.</summary>
		/// <param name="other">The array to compare with the current instance.</param>
		/// <param name="comparer">An object that determines whether the current instance and other are structurally equal.</param>
		/// <returns>
		///   <see langword="true" /> if the two arrays are structurally equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600015D RID: 349 RVA: 0x00004F58 File Offset: 0x00003158
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return true;
					}
					if (immutableArray.array == null)
					{
						return false;
					}
				}
			}
			IStructuralEquatable structuralEquatable = immutableArray.array;
			return structuralEquatable.Equals(array, comparer);
		}

		/// <summary>Returns a hash code for the current instance.</summary>
		/// <param name="comparer">An object that computes the hash code of the current object.</param>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x0600015E RID: 350 RVA: 0x00004FB0 File Offset: 0x000031B0
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			IStructuralEquatable structuralEquatable = immutableArray.array;
			if (structuralEquatable == null)
			{
				return immutableArray.GetHashCode();
			}
			return structuralEquatable.GetHashCode(comparer);
		}

		/// <summary>Determines whether the current collection element precedes, occurs in the same position as, or follows another element in the sort order.</summary>
		/// <param name="other">The element to compare with the current instance.</param>
		/// <param name="comparer">The object used to compare members of the current array with the corresponding members of other array.</param>
		/// <exception cref="T:System.ArgumentException">The arrays are not the same length.</exception>
		/// <returns>An integer that indicates whether the current element precedes, is in the same position or follows the other element.</returns>
		// Token: 0x0600015F RID: 351 RVA: 0x00004FE4 File Offset: 0x000031E4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return 0;
					}
					if ((immutableArray.array == null) ^ (array == null))
					{
						throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
					}
				}
			}
			if (array == null)
			{
				throw new ArgumentException(SR.ArrayLengthsNotEqual, "other");
			}
			IStructuralComparable structuralComparable = immutableArray.array;
			if (structuralComparable == null)
			{
				throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
			}
			return structuralComparable.CompareTo(array, comparer);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005078 File Offset: 0x00003278
		private ImmutableArray<T> RemoveAtRange(ICollection<int> indicesToRemove)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<ICollection<int>>(indicesToRemove, "indicesToRemove");
			if (indicesToRemove.Count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - indicesToRemove.Count];
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			foreach (int num4 in indicesToRemove)
			{
				int num5 = ((num3 == -1) ? num4 : (num4 - num3 - 1));
				Array.Copy(immutableArray.array, num + num2, array, num, num5);
				num2++;
				num += num5;
				num3 = num4;
			}
			Array.Copy(immutableArray.array, num + num2, array, num, immutableArray.Length - (num + num2));
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005150 File Offset: 0x00003350
		internal ImmutableArray([Nullable(new byte[] { 2, 1 })] T[] items)
		{
			this.array = items;
		}

		/// <summary>Returns a value that indicates if two arrays are equal.</summary>
		/// <param name="left">The array to the left of the operator.</param>
		/// <param name="right">The array to the right of the operator.</param>
		/// <returns>
		///   <see langword="true" /> if the arrays are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000162 RID: 354 RVA: 0x00005159 File Offset: 0x00003359
		[NonVersionable]
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two arrays are not equal.</summary>
		/// <param name="left">The array to the left of the operator.</param>
		/// <param name="right">The array to the right of the operator.</param>
		/// <returns>
		///   <see langword="true" /> if the arrays are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000163 RID: 355 RVA: 0x00005163 File Offset: 0x00003363
		[NonVersionable]
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates if two arrays are equal.</summary>
		/// <param name="left">The array to the left of the operator.</param>
		/// <param name="right">The array to the right of the operator.</param>
		/// <returns>
		///   <see langword="true" /> if the arrays are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000164 RID: 356 RVA: 0x00005170 File Offset: 0x00003370
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		/// <summary>Checks for inequality between two array.</summary>
		/// <param name="left">The object to the left of the operator.</param>
		/// <param name="right">The object to the right of the operator.</param>
		/// <returns>
		///   <see langword="true" /> if the two arrays are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000165 RID: 357 RVA: 0x00005194 File Offset: 0x00003394
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? left, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		/// <summary>Gets the element at the specified index in the immutable array.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index in the immutable array.</returns>
		// Token: 0x1700003A RID: 58
		public T this[int index]
		{
			[NonVersionable]
			get
			{
				return this.array[index];
			}
		}

		/// <summary>Gets a read-only reference to the element at the specified <paramref name="index" /> in the read-only list.</summary>
		/// <param name="index">The zero-based index of the element to get a reference to.</param>
		/// <returns>A read-only reference to the element at the specified <paramref name="index" /> in the read-only list.</returns>
		// Token: 0x06000167 RID: 359 RVA: 0x000051C8 File Offset: 0x000033C8
		public readonly ref T ItemRef(int index)
		{
			return ref this.array[index];
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000051D8 File Offset: 0x000033D8
		[DebuggerBrowsable(0)]
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this.array.Length == 0;
			}
		}

		/// <summary>Gets the number of elements in the array.</summary>
		/// <returns>The number of elements in the array.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000051E4 File Offset: 0x000033E4
		[DebuggerBrowsable(0)]
		public int Length
		{
			[NonVersionable]
			get
			{
				return this.array.Length;
			}
		}

		/// <summary>Gets a value indicating whether this array was declared but not initialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> is <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000051EE File Offset: 0x000033EE
		[DebuggerBrowsable(0)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> is empty or is not initialized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> is <see langword="null" /> or <see cref="F:System.Collections.Immutable.ImmutableArray`1.Empty" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000051FC File Offset: 0x000033FC
		[DebuggerBrowsable(0)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005224 File Offset: 0x00003424
		[Nullable(2)]
		[DebuggerBrowsable(0)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000522C File Offset: 0x0000342C
		[DebuggerBrowsable(0)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format(CultureInfo.CurrentCulture, "Length = {0}", immutableArray.Length);
				}
				return "Uninitialized";
			}
		}

		/// <summary>Copies the contents of this array to the specified array.</summary>
		/// <param name="destination">The array to copy to.</param>
		// Token: 0x0600016E RID: 366 RVA: 0x0000526C File Offset: 0x0000346C
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, destination, immutableArray.Length);
		}

		/// <summary>Copies the contents of this array to the specified array starting at the specified destination index.</summary>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="destinationIndex">The index in <paramref name="array" /> where copying begins.</param>
		// Token: 0x0600016F RID: 367 RVA: 0x0000529C File Offset: 0x0000349C
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		/// <summary>Copies the specified items in this array to the specified array at the specified starting index.</summary>
		/// <param name="sourceIndex">The index of this array where copying begins.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="destinationIndex">The index in <paramref name="array" /> where copying begins.</param>
		/// <param name="length">The number of elements to copy from this array.</param>
		// Token: 0x06000170 RID: 368 RVA: 0x000052CC File Offset: 0x000034CC
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		/// <summary>Creates a mutable array that has the same contents as this array and can be efficiently mutated across multiple operations using standard mutable interfaces.</summary>
		/// <returns>The new builder with the same contents as this array.</returns>
		// Token: 0x06000171 RID: 369 RVA: 0x000052F8 File Offset: 0x000034F8
		public ImmutableArray<T>.Builder ToBuilder()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return new ImmutableArray<T>.Builder();
			}
			ImmutableArray<T>.Builder builder = new ImmutableArray<T>.Builder(immutableArray.Length);
			builder.AddRange(immutableArray);
			return builder;
		}

		/// <summary>Returns an enumerator that iterates through the contents of the array.</summary>
		/// <returns>An enumerator.</returns>
		// Token: 0x06000172 RID: 370 RVA: 0x00005330 File Offset: 0x00003530
		[NullableContext(0)]
		[MethodImpl(256)]
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		// Token: 0x06000173 RID: 371 RVA: 0x00005358 File Offset: 0x00003558
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		/// <summary>Determines if this array is equal to the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this array.</param>
		/// <returns>
		///   <see langword="true" /> if this array is equal to <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000174 RID: 372 RVA: 0x00005384 File Offset: 0x00003584
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			IImmutableArray immutableArray = obj as IImmutableArray;
			return immutableArray != null && this.array == immutableArray.Array;
		}

		/// <summary>Indicates whether specified array is equal to this array.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is equal to this array; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000175 RID: 373 RVA: 0x000053AB File Offset: 0x000035AB
		[NonVersionable]
		public bool Equals([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct based on the contents of an existing instance, allowing a covariant static cast to efficiently reuse the existing array.</summary>
		/// <param name="items">The array to initialize the array with. No copy is made.</param>
		/// <typeparam name="TDerived">The type of array element to return.</typeparam>
		/// <returns>An immutable array instance with elements cast to the new type.</returns>
		// Token: 0x06000176 RID: 374 RVA: 0x000053BC File Offset: 0x000035BC
		[NullableContext(0)]
		public static ImmutableArray<T> CastUp<[Nullable(2)] TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : class, T
		{
			T[] array = items.array;
			return new ImmutableArray<T>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> struct by casting the underlying array to an array of type <typeparamref name="TOther" />.</summary>
		/// <typeparam name="TOther">The type of array element to return.</typeparam>
		/// <exception cref="T:System.InvalidCastException">The cast is illegal.</exception>
		/// <returns>An immutable array instance with elements cast to the new type.</returns>
		// Token: 0x06000177 RID: 375 RVA: 0x000053D6 File Offset: 0x000035D6
		[NullableContext(0)]
		public ImmutableArray<TOther> CastArray<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		/// <summary>Returns a new immutable array that contains the elements of this array cast to a different type.</summary>
		/// <typeparam name="TOther">The type of array element to return.</typeparam>
		/// <returns>An immutable array that contains the elements of this array, cast to a different type. If the cast fails, returns an array whose <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</returns>
		// Token: 0x06000178 RID: 376 RVA: 0x000053E8 File Offset: 0x000035E8
		[NullableContext(0)]
		public ImmutableArray<TOther> As<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		/// <summary>Returns an enumerator that iterates through the array.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>An enumerator that can be used to iterate through the array.</returns>
		// Token: 0x06000179 RID: 377 RVA: 0x000053FC File Offset: 0x000035FC
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		/// <summary>Returns an enumerator that iterates through the immutable array.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Immutable.ImmutableArray`1.IsDefault" /> property returns <see langword="true" />.</exception>
		/// <returns>An enumerator that iterates through the immutable array.</returns>
		// Token: 0x0600017A RID: 378 RVA: 0x00005424 File Offset: 0x00003624
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000544A File Offset: 0x0000364A
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005455 File Offset: 0x00003655
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
			}
		}

		/// <summary>Gets an empty immutable array.</summary>
		// Token: 0x0400001A RID: 26
		[Nullable(new byte[] { 0, 1 })]
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x0400001B RID: 27
		[Nullable(new byte[] { 2, 1 })]
		[DebuggerBrowsable(3)]
		internal T[] array;

		/// <summary>A writable array accessor that can be converted into an <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> instance without allocating extra memory.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x02000059 RID: 89
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
		{
			// Token: 0x060003FD RID: 1021 RVA: 0x0000A62C File Offset: 0x0000882C
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x0000A659 File Offset: 0x00008859
			internal Builder()
				: this(8)
			{
			}

			/// <summary>Gets or sets the length of the internal array. When set, the internal array is reallocated to the given capacity if it is not already the specified length.</summary>
			/// <returns>The length of the internal array.</returns>
			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000A662 File Offset: 0x00008862
			// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000A66C File Offset: 0x0000886C
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
				set
				{
					if (value < this._count)
					{
						throw new ArgumentException(SR.CapacityMustBeGreaterThanOrEqualToCount, "value");
					}
					if (value != this._elements.Length)
					{
						if (value > 0)
						{
							T[] array = new T[value];
							if (this._count > 0)
							{
								Array.Copy(this._elements, array, this._count);
							}
							this._elements = array;
							return;
						}
						this._elements = ImmutableArray<T>.Empty.array;
					}
				}
			}

			/// <summary>Gets or sets the number of items in the array.</summary>
			/// <returns>The number of items in the array.</returns>
			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000A6DB File Offset: 0x000088DB
			// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000A6E4 File Offset: 0x000088E4
			public int Count
			{
				get
				{
					return this._count;
				}
				set
				{
					Requires.Range(value >= 0, "value", null);
					if (value < this._count)
					{
						if (this._count - value > 64)
						{
							Array.Clear(this._elements, value, this._count - value);
						}
						else
						{
							for (int i = value; i < this.Count; i++)
							{
								this._elements[i] = default(T);
							}
						}
					}
					else if (value > this._count)
					{
						this.EnsureCapacity(value);
					}
					this._count = value;
				}
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0000A76D File Offset: 0x0000896D
			private static void ThrowIndexOutOfRangeException()
			{
				throw new IndexOutOfRangeException();
			}

			/// <summary>Gets or sets the item at the specified index.</summary>
			/// <param name="index">The index of the item to get or set.</param>
			/// <exception cref="T:System.IndexOutOfRangeException">The specified index is not in the array.</exception>
			/// <returns>The item at the specified index.</returns>
			// Token: 0x170000B5 RID: 181
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			/// <summary>Gets a read-only reference to the element at the specified index.</summary>
			/// <param name="index">The item index.</param>
			/// <exception cref="T:System.IndexOutOfRangeException">
			///   <paramref name="index" /> is greater or equal to the array count.</exception>
			/// <returns>The read-only reference to the element at the specified index.</returns>
			// Token: 0x06000406 RID: 1030 RVA: 0x0000A7AD File Offset: 0x000089AD
			public readonly ref T ItemRef(int index)
			{
				if (index >= this.Count)
				{
					ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
				}
				return ref this._elements[index];
			}

			/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000A7CB File Offset: 0x000089CB
			bool ICollection<T>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Returns an immutable array that contains the current contents of this <see cref="T:System.Collections.Immutable.ImmutableArray`1.Builder" />.</summary>
			/// <returns>An immutable array that contains the current contents of this <see cref="T:System.Collections.Immutable.ImmutableArray`1.Builder" />.</returns>
			// Token: 0x06000408 RID: 1032 RVA: 0x0000A7CE File Offset: 0x000089CE
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<T> ToImmutable()
			{
				return new ImmutableArray<T>(this.ToArray());
			}

			/// <summary>Extracts the internal array as an <see cref="T:System.Collections.Immutable.ImmutableArray`1" /> and replaces it              with a zero length array.</summary>
			/// <exception cref="T:System.InvalidOperationException">When <see cref="P:System.Collections.Immutable.ImmutableArray`1.Builder.Count" /> doesn't              equal <see cref="P:System.Collections.Immutable.ImmutableArray`1.Builder.Capacity" />.</exception>
			/// <returns>An immutable array containing the elements of the builder.</returns>
			// Token: 0x06000409 RID: 1033 RVA: 0x0000A7DC File Offset: 0x000089DC
			[return: Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException(SR.CapacityMustEqualCountOnMove);
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			/// <summary>Removes all items from the array.</summary>
			// Token: 0x0600040A RID: 1034 RVA: 0x0000A826 File Offset: 0x00008A26
			public void Clear()
			{
				this.Count = 0;
			}

			/// <summary>Inserts an item in the array at the specified index.</summary>
			/// <param name="index">The zero-based index at which to insert the item.</param>
			/// <param name="item">The object to insert into the array.</param>
			// Token: 0x0600040B RID: 1035 RVA: 0x0000A830 File Offset: 0x00008A30
			public void Insert(int index, T item)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				this.EnsureCapacity(this.Count + 1);
				if (index < this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + 1, this.Count - index);
				}
				this._count++;
				this._elements[index] = item;
			}

			/// <summary>Adds the specified item to the array.</summary>
			/// <param name="item">The object to add to the array.</param>
			// Token: 0x0600040C RID: 1036 RVA: 0x0000A8AC File Offset: 0x00008AAC
			public void Add(T item)
			{
				int num = this._count + 1;
				this.EnsureCapacity(num);
				this._elements[this._count] = item;
				this._count = num;
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			// Token: 0x0600040D RID: 1037 RVA: 0x0000A8E4 File Offset: 0x00008AE4
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int num;
				if (items.TryGetCount<T>(out num))
				{
					this.EnsureCapacity(this.Count + num);
					if (items.TryCopyTo<T>(this._elements, this._count))
					{
						this._count += num;
						return;
					}
				}
				foreach (T t in items)
				{
					this.Add(t);
				}
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			// Token: 0x0600040E RID: 1038 RVA: 0x0000A974 File Offset: 0x00008B74
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			/// <summary>Adds the specified items that derive from the type currently in the array, to the end of the array.</summary>
			/// <param name="items">The items to add to end of the array.</param>
			/// <typeparam name="TDerived">The type that derives from the type of item already in the array.</typeparam>
			// Token: 0x0600040F RID: 1039 RVA: 0x0000A9B4 File Offset: 0x00008BB4
			public void AddRange<[Nullable(0)] TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			/// <param name="length">The number of elements from the source array to add.</param>
			// Token: 0x06000410 RID: 1040 RVA: 0x0000A9F4 File Offset: 0x00008BF4
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0 && length <= items.Length, "length", null);
				int count = this.Count;
				this.Count += length;
				Array.Copy(items, 0, this._elements, count, length);
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			// Token: 0x06000411 RID: 1041 RVA: 0x0000AA4B File Offset: 0x00008C4B
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			/// <param name="length">The number of elements from the source array to add.</param>
			// Token: 0x06000412 RID: 1042 RVA: 0x0000AA5B File Offset: 0x00008C5B
			public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			/// <summary>Adds the specified items that derive from the type currently in the array, to the end of the array.</summary>
			/// <param name="items">The items to add to the end of the array.</param>
			/// <typeparam name="TDerived">The type that derives from the type of item already in the array.</typeparam>
			// Token: 0x06000413 RID: 1043 RVA: 0x0000AA84 File Offset: 0x00008C84
			[NullableContext(0)]
			public void AddRange<TDerived>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			/// <summary>Adds the specified items to the end of the array.</summary>
			/// <param name="items">The items to add to the array.</param>
			// Token: 0x06000414 RID: 1044 RVA: 0x0000AA9A File Offset: 0x00008C9A
			public void AddRange([Nullable(new byte[] { 1, 0 })] ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			/// <summary>Adds the specified items that derive from the type currently in the array, to the end of the array.</summary>
			/// <param name="items">The items to add to the end of the array.</param>
			/// <typeparam name="TDerived">The type that derives from the type of item already in the array.</typeparam>
			// Token: 0x06000415 RID: 1045 RVA: 0x0000AAB9 File Offset: 0x00008CB9
			public void AddRange<[Nullable(0)] TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			/// <summary>Removes the specified element.</summary>
			/// <param name="element">The item to remove.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="element" /> was found and removed; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000416 RID: 1046 RVA: 0x0000AAD8 File Offset: 0x00008CD8
			public bool Remove(T element)
			{
				int num = this.IndexOf(element);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			/// <summary>Removes the item at the specified index from the array.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x06000417 RID: 1047 RVA: 0x0000AAFC File Offset: 0x00008CFC
			public void RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this.Count - 1)
				{
					Array.Copy(this._elements, index + 1, this._elements, index, this.Count - index - 1);
				}
				int count = this.Count;
				this.Count = count - 1;
			}

			/// <summary>Determines whether the array contains a specific value.</summary>
			/// <param name="item">The object to locate in the array.</param>
			/// <returns>
			///   <see langword="true" /> if the object is found; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000418 RID: 1048 RVA: 0x0000AB5E File Offset: 0x00008D5E
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			/// <summary>Creates a new array with the current contents of this <see cref="T:System.Collections.Immutable.ImmutableArray`1.Builder" />.</summary>
			/// <returns>A new array with the contents of this <see cref="T:System.Collections.Immutable.ImmutableArray`1.Builder" />.</returns>
			// Token: 0x06000419 RID: 1049 RVA: 0x0000AB70 File Offset: 0x00008D70
			public T[] ToArray()
			{
				if (this.Count == 0)
				{
					return ImmutableArray<T>.Empty.array;
				}
				T[] array = new T[this.Count];
				Array.Copy(this._elements, array, this.Count);
				return array;
			}

			/// <summary>Copies the current contents to the specified array.</summary>
			/// <param name="array">The array to copy to.</param>
			/// <param name="index">The index to start the copy operation.</param>
			// Token: 0x0600041A RID: 1050 RVA: 0x0000ABB0 File Offset: 0x00008DB0
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "index", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x0000AC00 File Offset: 0x00008E00
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int num = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, num);
				}
			}

			/// <summary>Determines the index of a specific item in the array.</summary>
			/// <param name="item">The item to locate in the array.</param>
			/// <returns>The index of <paramref name="item" /> if it's found in the list; otherwise, -1.</returns>
			// Token: 0x0600041C RID: 1052 RVA: 0x0000AC35 File Offset: 0x00008E35
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the index of the specified item.</summary>
			/// <param name="item">The item to locate in the array.</param>
			/// <param name="startIndex">The starting position of the search.</param>
			/// <returns>The index of <paramref name="item" /> if it's found in the list; otherwise, -1.</returns>
			// Token: 0x0600041D RID: 1053 RVA: 0x0000AC4A File Offset: 0x00008E4A
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the index of the specified item.</summary>
			/// <param name="item">The item to locate in the array.</param>
			/// <param name="startIndex">The starting position of the search.</param>
			/// <param name="count">The number of elements to search.</param>
			/// <returns>The index of <paramref name="item" /> if it's found in the list; otherwise, -1.</returns>
			// Token: 0x0600041E RID: 1054 RVA: 0x0000AC61 File Offset: 0x00008E61
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the index for the specified item.</summary>
			/// <param name="item">The item to locate in the array.</param>
			/// <param name="startIndex">The index at which to begin the search.</param>
			/// <param name="count">The starting position of the search.</param>
			/// <param name="equalityComparer">The equality comparer to use in the search.</param>
			/// <returns>The index of <paramref name="item" /> if it's found in the list; otherwise, -1.</returns>
			// Token: 0x0600041F RID: 1055 RVA: 0x0000AC74 File Offset: 0x00008E74
			public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex + count <= this.Count, "count", null);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.IndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (equalityComparer.Equals(this._elements[i], item))
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Determines the 0-based index of the last occurrence of the specified item in this array.</summary>
			/// <param name="item">The item to search for.</param>
			/// <returns>The 0-based index where the item was found; or -1 if it could not be found.</returns>
			// Token: 0x06000420 RID: 1056 RVA: 0x0000AD10 File Offset: 0x00008F10
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the 0-based index of the last occurrence of the specified item in this array.</summary>
			/// <param name="item">The item to search for.</param>
			/// <param name="startIndex">The starting position of the search.</param>
			/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
			// Token: 0x06000421 RID: 1057 RVA: 0x0000AD36 File Offset: 0x00008F36
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the 0-based index of the last occurrence of the specified item in this array.</summary>
			/// <param name="item">The item to search for.</param>
			/// <param name="startIndex">The starting position of the search.</param>
			/// <param name="count">The number of elements to search.</param>
			/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
			// Token: 0x06000422 RID: 1058 RVA: 0x0000AD70 File Offset: 0x00008F70
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			/// <summary>Determines the 0-based index of the last occurrence of the specified item in this array.</summary>
			/// <param name="item">The item to search for.</param>
			/// <param name="startIndex">The starting position of the search.</param>
			/// <param name="count">The number of elements to search.</param>
			/// <param name="equalityComparer">The equality comparer to use in the search.</param>
			/// <returns>The 0-based index into the array where the item was found; or -1 if it could not be found.</returns>
			// Token: 0x06000423 RID: 1059 RVA: 0x0000AD80 File Offset: 0x00008F80
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
				equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.LastIndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i >= startIndex - count + 1; i--)
				{
					if (equalityComparer.Equals(item, this._elements[i]))
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Reverses the order of elements in the collection.</summary>
			// Token: 0x06000424 RID: 1060 RVA: 0x0000AE1C File Offset: 0x0000901C
			public void Reverse()
			{
				int i = 0;
				int num = this._count - 1;
				T[] elements = this._elements;
				while (i < num)
				{
					T t = elements[i];
					elements[i] = elements[num];
					elements[num] = t;
					i++;
					num--;
				}
			}

			/// <summary>Sorts the contents of the array.</summary>
			// Token: 0x06000425 RID: 1061 RVA: 0x0000AE67 File Offset: 0x00009067
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			/// <summary>Sorts the elements in the entire array using the specified <see cref="T:System.Comparison`1" />.</summary>
			/// <param name="comparison">The <see cref="T:System.Comparison`1" /> to use when comparing elements.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="comparison" /> is null.</exception>
			// Token: 0x06000426 RID: 1062 RVA: 0x0000AE89 File Offset: 0x00009089
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, Comparer<T>.Create(comparison));
				}
			}

			/// <summary>Sorts the contents of the array.</summary>
			/// <param name="comparer">The comparer to use for sorting. If comparer is <see langword="null" />, the default comparer for the elements type in the array is used.</param>
			// Token: 0x06000427 RID: 1063 RVA: 0x0000AEB7 File Offset: 0x000090B7
			public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			/// <summary>Sorts the contents of the array.</summary>
			/// <param name="index">The starting index for the sort.</param>
			/// <param name="count">The number of elements to include in the sort.</param>
			/// <param name="comparer">The comparer to use for sorting. If comparer is <see langword="null" />, the default comparer for the elements type in the array is used.</param>
			// Token: 0x06000428 RID: 1064 RVA: 0x0000AED8 File Offset: 0x000090D8
			public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			/// <summary>Gets an object that can be used to iterate through the collection.</summary>
			/// <returns>An object that can be used to iterate through the collection.</returns>
			// Token: 0x06000429 RID: 1065 RVA: 0x0000AF29 File Offset: 0x00009129
			public IEnumerator<T> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num + 1)
				{
					yield return this[i];
					num = i;
				}
				yield break;
			}

			/// <summary>Returns an enumerator that iterates through the array.</summary>
			/// <returns>An enumerator that iterates through the array.</returns>
			// Token: 0x0600042A RID: 1066 RVA: 0x0000AF38 File Offset: 0x00009138
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the array.</summary>
			/// <returns>An enumerator that iterates through the array.</returns>
			// Token: 0x0600042B RID: 1067 RVA: 0x0000AF40 File Offset: 0x00009140
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000AF48 File Offset: 0x00009148
			private void AddRange<TDerived>(TDerived[] items, int length) where TDerived : T
			{
				this.EnsureCapacity(this.Count + length);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x04000076 RID: 118
			private T[] _elements;

			// Token: 0x04000077 RID: 119
			private int _count;
		}

		/// <summary>An array enumerator.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x0200005A RID: 90
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x0600042D RID: 1069 RVA: 0x0000AFA5 File Offset: 0x000091A5
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			/// <summary>Gets the current item.</summary>
			/// <returns>The current item.</returns>
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000AFB5 File Offset: 0x000091B5
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			/// <summary>Advances to the next value in the array.</summary>
			/// <returns>
			///   <see langword="true" /> if another item exists in the array; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600042F RID: 1071 RVA: 0x0000AFC8 File Offset: 0x000091C8
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x04000078 RID: 120
			private readonly T[] _array;

			// Token: 0x04000079 RID: 121
			private int _index;
		}

		// Token: 0x0200005B RID: 91
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000430 RID: 1072 RVA: 0x0000AFF0 File Offset: 0x000091F0
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000B006 File Offset: 0x00009206
			public T Current
			{
				get
				{
					if (this._index < this._array.Length)
					{
						return this._array[this._index];
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000B02F File Offset: 0x0000922F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0000B03C File Offset: 0x0000923C
			public bool MoveNext()
			{
				int num = this._index + 1;
				int num2 = this._array.Length;
				if (num <= num2)
				{
					this._index = num;
					return num < num2;
				}
				return false;
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x0000B06C File Offset: 0x0000926C
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0000B075 File Offset: 0x00009275
			public void Dispose()
			{
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0000B077 File Offset: 0x00009277
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x0400007A RID: 122
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x0400007B RID: 123
			private readonly T[] _array;

			// Token: 0x0400007C RID: 124
			private int _index;
		}
	}
}
