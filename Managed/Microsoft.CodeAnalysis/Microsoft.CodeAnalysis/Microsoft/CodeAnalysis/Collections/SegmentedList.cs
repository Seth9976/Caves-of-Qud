using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections.Internal;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A8 RID: 1192
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	internal class SegmentedList<[Nullable(2)] T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06002FAF RID: 12207 RVA: 0x0008676E File Offset: 0x0008496E
		public SegmentedList()
		{
			this._items = SegmentedList<T>.s_emptyArray;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00086781 File Offset: 0x00084981
		public SegmentedList(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = SegmentedList<T>.s_emptyArray;
				return;
			}
			this._items = new SegmentedArray<T>(capacity);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000867B0 File Offset: 0x000849B0
		public SegmentedList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count == 0)
				{
					this._items = SegmentedList<T>.s_emptyArray;
					return;
				}
				this._items = new SegmentedArray<T>(count);
				T[][] array = (T[][])this._items.SyncRoot;
				if (array != null && array.Length == 1)
				{
					collection2.CopyTo(array[0], 0);
					this._size = count;
					return;
				}
			}
			else
			{
				this._items = SegmentedList<T>.s_emptyArray;
			}
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0008686C File Offset: 0x00084A6C
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x0008687C File Offset: 0x00084A7C
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						SegmentedArray<T> segmentedArray = new SegmentedArray<T>(value);
						if (this._size > 0)
						{
							SegmentedArray.Copy<T>(this._items, segmentedArray, this._size);
						}
						this._items = segmentedArray;
						return;
					}
					this._items = SegmentedList<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000868E1 File Offset: 0x00084AE1
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000868E9 File Offset: 0x00084AE9
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000868EC File Offset: 0x00084AEC
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000868EF File Offset: 0x00084AEF
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000868F2 File Offset: 0x00084AF2
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000868F5 File Offset: 0x00084AF5
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		public unsafe T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return *this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				*this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x0008694C File Offset: 0x00084B4C
		[NullableContext(2)]
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x00086979 File Offset: 0x00084B79
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x00086988 File Offset: 0x00084B88
		[Nullable(2)]
		object IList.Item
		{
			[NullableContext(2)]
			get
			{
				return this[index];
			}
			[NullableContext(2)]
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException<object>(value, typeof(T));
				}
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000869D0 File Offset: 0x00084BD0
		[MethodImpl(256)]
		public unsafe void Add(T item)
		{
			this._version++;
			SegmentedArray<T> items = this._items;
			int size = this._size;
			if (size < items.Length)
			{
				this._size = size + 1;
				*items[size] = item;
				return;
			}
			this.AddWithResize(item);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00086A24 File Offset: 0x00084C24
		[MethodImpl(8)]
		private unsafe void AddWithResize(T item)
		{
			int size = this._size;
			this.EnsureCapacity(size + 1);
			this._size = size + 1;
			*this._items[size] = item;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00086A5C File Offset: 0x00084C5C
		[NullableContext(2)]
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(item, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00086AAC File Offset: 0x00084CAC
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00086ABB File Offset: 0x00084CBB
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00086AC3 File Offset: 0x00084CC3
		public int BinarySearch(int index, int count, T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return SegmentedArray.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00086AFB File Offset: 0x00084CFB
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00086B0C File Offset: 0x00084D0C
		public int BinarySearch(T item, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00086B20 File Offset: 0x00084D20
		[MethodImpl(256)]
		public void Clear()
		{
			this._version++;
			int size = this._size;
			this._size = 0;
			if (size > 0)
			{
				SegmentedArray.Clear<T>(this._items, 0, size);
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00086B5A File Offset: 0x00084D5A
		public bool Contains(T item)
		{
			return this._size != 0 && this.IndexOf(item) != -1;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00086B73 File Offset: 0x00084D73
		[NullableContext(2)]
		bool IList.Contains(object item)
		{
			return SegmentedList<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00086B8C File Offset: 0x00084D8C
		public unsafe SegmentedList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			SegmentedList<TOutput> segmentedList = new SegmentedList<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				*segmentedList._items[i] = converter.Invoke(*this._items[i]);
			}
			segmentedList._size = this._size;
			return segmentedList;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00086BF5 File Offset: 0x00084DF5
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00086C00 File Offset: 0x00084E00
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				SegmentedArray.Copy<T>(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00086C50 File Offset: 0x00084E50
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			SegmentedArray.Copy<T>(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00086C74 File Offset: 0x00084E74
		public void CopyTo(T[] array, int arrayIndex)
		{
			SegmentedArray.Copy<T>(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00086C8C File Offset: 0x00084E8C
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00086CE0 File Offset: 0x00084EE0
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00086CF0 File Offset: 0x00084EF0
		[return: Nullable(2)]
		public unsafe T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match.Invoke(*this._items[i]))
				{
					return *this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00086D4C File Offset: 0x00084F4C
		public unsafe SegmentedList<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			SegmentedList<T> segmentedList = new SegmentedList<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match.Invoke(*this._items[i]))
				{
					segmentedList.Add(*this._items[i]);
				}
			}
			return segmentedList;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00086DAB File Offset: 0x00084FAB
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x00086DBB File Offset: 0x00084FBB
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00086DD0 File Offset: 0x00084FD0
		public unsafe int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex > this._size - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match.Invoke(*this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x00086E38 File Offset: 0x00085038
		[return: Nullable(2)]
		public unsafe T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match.Invoke(*this._items[i]))
				{
					return *this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00086E96 File Offset: 0x00085096
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x00086EAD File Offset: 0x000850AD
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00086EBC File Offset: 0x000850BC
		public unsafe int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
			}
			else if (startIndex >= this._size)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match.Invoke(*this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x00086F34 File Offset: 0x00085134
		public unsafe void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && version == this._version)
			{
				action.Invoke(*this._items[num]);
				num++;
			}
			if (version != this._version)
			{
				ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x00086F91 File Offset: 0x00085191
		[NullableContext(0)]
		public SegmentedList<T>.Enumerator GetEnumerator()
		{
			return new SegmentedList<T>.Enumerator(this);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x00086F99 File Offset: 0x00085199
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new SegmentedList<T>.Enumerator(this);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x00086FA6 File Offset: 0x000851A6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SegmentedList<T>.Enumerator(this);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x00086FB4 File Offset: 0x000851B4
		public SegmentedList<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			SegmentedList<T> segmentedList = new SegmentedList<T>(count);
			SegmentedArray.Copy<T>(this._items, index, segmentedList._items, 0, count);
			segmentedList._size = count;
			return segmentedList;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0008700A File Offset: 0x0008520A
		public int IndexOf(T item)
		{
			return SegmentedArray.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0008701F File Offset: 0x0008521F
		[NullableContext(2)]
		int IList.IndexOf(object item)
		{
			if (SegmentedList<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x00087037 File Offset: 0x00085237
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return SegmentedArray.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0008705C File Offset: 0x0008525C
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			return SegmentedArray.IndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x00087090 File Offset: 0x00085290
		public unsafe void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				SegmentedArray.Copy<T>(this._items, index, this._items, index + 1, this._size - index);
			}
			*this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00087124 File Offset: 0x00085324
		[NullableContext(2)]
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException<object>(item, typeof(T));
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0008716C File Offset: 0x0008536C
		public unsafe void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						SegmentedArray.Copy<T>(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						SegmentedArray.Copy<T>(this._items, 0, this._items, index, index);
						SegmentedArray.Copy<T>(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						SegmentedList<T> segmentedList = collection2 as SegmentedList<T>;
						if (segmentedList != null)
						{
							SegmentedArray.Copy<T>(segmentedList._items, 0, this._items, index, segmentedList.Count);
						}
						else if (collection2 is SegmentedArray<T>)
						{
							SegmentedArray<T> segmentedArray = (SegmentedArray<T>)collection2;
							SegmentedArray.Copy<T>(segmentedArray, 0, this._items, index, segmentedArray.Length);
						}
						else
						{
							int num = index;
							foreach (T t in collection2)
							{
								*this._items[num++] = t;
							}
						}
					}
					this._size += count;
				}
			}
			else
			{
				foreach (T t2 in collection)
				{
					this.Insert(index++, t2);
				}
			}
			this._version++;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0008731C File Offset: 0x0008551C
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0008733D File Offset: 0x0008553D
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x00087358 File Offset: 0x00085558
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return SegmentedArray.LastIndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000873C4 File Offset: 0x000855C4
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000873E7 File Offset: 0x000855E7
		[NullableContext(2)]
		void IList.Remove(object item)
		{
			if (SegmentedList<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x00087400 File Offset: 0x00085600
		public unsafe int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = 0;
			while (num < this._size && !match.Invoke(*this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match.Invoke(*this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					*this._items[num++] = *this._items[i++];
				}
			}
			SegmentedArray.Clear<T>(this._items, num, this._size - num);
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000874E8 File Offset: 0x000856E8
		public unsafe void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this._size--;
			if (index < this._size)
			{
				SegmentedArray.Copy<T>(this._items, index + 1, this._items, index, this._size - index);
			}
			*this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x0008755C File Offset: 0x0008575C
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				this._size -= count;
				if (index < this._size)
				{
					SegmentedArray.Copy<T>(this._items, index + count, this._items, index, this._size - index);
				}
				this._version++;
				SegmentedArray.Clear<T>(this._items, this._size, count);
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000875E7 File Offset: 0x000857E7
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000875F8 File Offset: 0x000857F8
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				SegmentedArray.Reverse<T>(this._items, index, count);
			}
			this._version++;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x0008764A File Offset: 0x0008584A
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0008765A File Offset: 0x0008585A
		public void Sort([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0008766C File Offset: 0x0008586C
		public void Sort(int index, int count, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				SegmentedArray.Sort<T>(this._items, index, count, comparer);
			}
			this._version++;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000876BF File Offset: 0x000858BF
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (this._size > 1)
			{
				SegmentedArray.Sort<T>(this._items, 0, this._size, Comparer<T>.Create(comparison));
			}
			this._version++;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000876FC File Offset: 0x000858FC
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			SegmentedArray.Copy<T>(this._items, array, this._size);
			return array;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x00087738 File Offset: 0x00085938
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00087774 File Offset: 0x00085974
		public unsafe bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match.Invoke(*this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001298 RID: 4760
		private const int DefaultCapacity = 4;

		// Token: 0x04001299 RID: 4761
		private const int MaxArrayLength = 2146435071;

		// Token: 0x0400129A RID: 4762
		[Nullable(new byte[] { 0, 1 })]
		internal SegmentedArray<T> _items;

		// Token: 0x0400129B RID: 4763
		internal int _size;

		// Token: 0x0400129C RID: 4764
		private int _version;

		// Token: 0x0400129D RID: 4765
		[Nullable(new byte[] { 0, 1 })]
		private static readonly SegmentedArray<T> s_emptyArray = new SegmentedArray<T>(0);

		// Token: 0x0200075F RID: 1887
		[Nullable(0)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06003E1B RID: 15899 RVA: 0x000AF3BC File Offset: 0x000AD5BC
			internal Enumerator(SegmentedList<T> list)
			{
				this._list = list;
				this._index = 0;
				this._version = list._version;
				this._current = default(T);
			}

			// Token: 0x06003E1C RID: 15900 RVA: 0x000AF3E4 File Offset: 0x000AD5E4
			public void Dispose()
			{
			}

			// Token: 0x06003E1D RID: 15901 RVA: 0x000AF3E8 File Offset: 0x000AD5E8
			public unsafe bool MoveNext()
			{
				SegmentedList<T> list = this._list;
				if (this._version == list._version && this._index < list._size)
				{
					this._current = *list._items[this._index];
					this._index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06003E1E RID: 15902 RVA: 0x000AF44A File Offset: 0x000AD64A
			private bool MoveNextRare()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = this._list._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x1700112D RID: 4397
			// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000AF484 File Offset: 0x000AD684
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700112E RID: 4398
			// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000AF48C File Offset: 0x000AD68C
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					if (this._index == 0 || this._index == this._list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this.Current;
				}
			}

			// Token: 0x06003E21 RID: 15905 RVA: 0x000AF4BB File Offset: 0x000AD6BB
			void IEnumerator.Reset()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04001D28 RID: 7464
			private readonly SegmentedList<T> _list;

			// Token: 0x04001D29 RID: 7465
			private int _index;

			// Token: 0x04001D2A RID: 7466
			private readonly int _version;

			// Token: 0x04001D2B RID: 7467
			[Nullable(2)]
			private T _current;
		}
	}
}
