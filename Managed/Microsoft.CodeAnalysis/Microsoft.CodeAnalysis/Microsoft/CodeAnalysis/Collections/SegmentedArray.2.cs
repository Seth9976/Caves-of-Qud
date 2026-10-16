using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Collections.Internal;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A6 RID: 1190
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct SegmentedArray<[Nullable(2)] T> : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable, IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x00085290 File Offset: 0x00083490
		private static int SegmentSize
		{
			[MethodImpl(768)]
			get
			{
				return SegmentedArrayHelper.GetSegmentSize<T>();
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x00085297 File Offset: 0x00083497
		private static int SegmentShift
		{
			[MethodImpl(768)]
			get
			{
				return SegmentedArrayHelper.GetSegmentShift<T>();
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x0008529E File Offset: 0x0008349E
		private static int OffsetMask
		{
			[MethodImpl(768)]
			get
			{
				return SegmentedArrayHelper.GetOffsetMask<T>();
			}
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000852A8 File Offset: 0x000834A8
		public SegmentedArray(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				this._items = Array.Empty<T[]>();
				this._length = 0;
				return;
			}
			this._items = new T[length + SegmentedArray<T>.SegmentSize - 1 >> SegmentedArray<T>.SegmentShift][];
			for (int i = 0; i < this._items.Length - 1; i++)
			{
				this._items[i] = new T[SegmentedArray<T>.SegmentSize];
			}
			int num = length - (this._items.Length - 1 << SegmentedArray<T>.SegmentShift);
			this._items[this._items.Length - 1] = new T[num];
			this._length = length;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x00085352 File Offset: 0x00083552
		private SegmentedArray(int length, T[][] items)
		{
			this._length = length;
			this._items = items;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x00085362 File Offset: 0x00083562
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x00085365 File Offset: 0x00083565
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x00085368 File Offset: 0x00083568
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x0008536B File Offset: 0x0008356B
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x00085373 File Offset: 0x00083573
		public object SyncRoot
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		public ref T this[int index]
		{
			[MethodImpl(768)]
			get
			{
				return ref this._items[index >> SegmentedArray<T>.SegmentShift][index & SegmentedArray<T>.OffsetMask];
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x0008539A File Offset: 0x0008359A
		int ICollection.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000853A2 File Offset: 0x000835A2
		int ICollection<T>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x000853AA File Offset: 0x000835AA
		int IReadOnlyCollection<T>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000853B2 File Offset: 0x000835B2
		unsafe T IReadOnlyList<T>.Item
		{
			get
			{
				return *this[index];
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x000853C0 File Offset: 0x000835C0
		// (set) Token: 0x06002F5A RID: 12122 RVA: 0x000853CE File Offset: 0x000835CE
		unsafe T IList<T>.Item
		{
			get
			{
				return *this[index];
			}
			set
			{
				*this[index] = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000853DD File Offset: 0x000835DD
		// (set) Token: 0x06002F5C RID: 12124 RVA: 0x000853F0 File Offset: 0x000835F0
		[Nullable(2)]
		unsafe object IList.Item
		{
			[NullableContext(2)]
			get
			{
				return *this[index];
			}
			[NullableContext(2)]
			set
			{
				*this[index] = (T)((object)value);
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x00085404 File Offset: 0x00083604
		public object Clone()
		{
			T[][] array = (T[][])this._items.Clone();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T[])array[i].Clone();
			}
			return new SegmentedArray<T>(this.Length, array);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x00085454 File Offset: 0x00083654
		public void CopyTo(Array array, int index)
		{
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i].CopyTo(array, index + i * SegmentedArray<T>.SegmentSize);
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x0008548C File Offset: 0x0008368C
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this._items.Length; i++)
			{
				this._items[i].CopyTo(array, arrayIndex + i * SegmentedArray<T>.SegmentSize);
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000854C3 File Offset: 0x000836C3
		[NullableContext(0)]
		public SegmentedArray<T>.Enumerator GetEnumerator()
		{
			return new SegmentedArray<T>.Enumerator(this);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000854D0 File Offset: 0x000836D0
		[NullableContext(2)]
		int IList.Add(object value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000854DC File Offset: 0x000836DC
		void ICollection<T>.Add(T value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000854E8 File Offset: 0x000836E8
		void IList.Clear()
		{
			T[][] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				items[i].Clear();
			}
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00085512 File Offset: 0x00083712
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x00085520 File Offset: 0x00083720
		[NullableContext(2)]
		bool IList.Contains(object value)
		{
			T[][] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x00085550 File Offset: 0x00083750
		bool ICollection<T>.Contains(T value)
		{
			T[][] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x00085580 File Offset: 0x00083780
		[NullableContext(2)]
		int IList.IndexOf(object value)
		{
			for (int i = 0; i < this._items.Length; i++)
			{
				int num = this._items[i].IndexOf(value);
				if (num >= 0)
				{
					return num + i * SegmentedArray<T>.SegmentSize;
				}
			}
			return -1;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000855C0 File Offset: 0x000837C0
		int IList<T>.IndexOf(T value)
		{
			for (int i = 0; i < this._items.Length; i++)
			{
				int num = this._items[i].IndexOf(value);
				if (num >= 0)
				{
					return num + i * SegmentedArray<T>.SegmentSize;
				}
			}
			return -1;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000855FE File Offset: 0x000837FE
		[NullableContext(2)]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0008560A File Offset: 0x0008380A
		void IList<T>.Insert(int index, T value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00085616 File Offset: 0x00083816
		[NullableContext(2)]
		void IList.Remove(object value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x00085622 File Offset: 0x00083822
		bool ICollection<T>.Remove(T value)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0008562E File Offset: 0x0008382E
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0008563A File Offset: 0x0008383A
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException(SR.NotSupported_FixedSizeCollection);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x00085646 File Offset: 0x00083846
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x00085653 File Offset: 0x00083853
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x00085660 File Offset: 0x00083860
		unsafe int IStructuralComparable.CompareTo([Nullable(2)] object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (other is SegmentedArray<T>)
			{
				SegmentedArray<T> segmentedArray = (SegmentedArray<T>)other;
				if (this.Length == segmentedArray.Length)
				{
					for (int i = 0; i < this.Length; i++)
					{
						int num = comparer.Compare(*this[i], *segmentedArray[i]);
						if (num != 0)
						{
							return num;
						}
					}
					return 0;
				}
			}
			throw new ArgumentException(SR.ArgumentException_OtherNotArrayOfCorrectLength, "other");
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000856E4 File Offset: 0x000838E4
		unsafe bool IStructuralEquatable.Equals([Nullable(2)] object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (!(other is SegmentedArray<T>))
			{
				return false;
			}
			SegmentedArray<T> segmentedArray = (SegmentedArray<T>)other;
			if (this._items == segmentedArray._items)
			{
				return true;
			}
			if (this.Length != segmentedArray.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Length; i++)
			{
				if (!comparer.Equals(*this[i], *segmentedArray[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0008576C File Offset: 0x0008396C
		unsafe int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = ((this.Length >= 8) ? (this.Length - 8) : 0); i < this.Length; i++)
			{
				num = num * -1521134295 + comparer.GetHashCode(*this[i]);
			}
			return num;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000857CE File Offset: 0x000839CE
		[NullableContext(0)]
		internal SegmentedArray<T>.TestAccessor GetTestAccessor()
		{
			return new SegmentedArray<T>.TestAccessor(this);
		}

		// Token: 0x0400128A RID: 4746
		private readonly int _length;

		// Token: 0x0400128B RID: 4747
		private readonly T[][] _items;

		// Token: 0x02000759 RID: 1881
		[Nullable(0)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06003DED RID: 15853 RVA: 0x000AEC57 File Offset: 0x000ACE57
			public Enumerator([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array)
			{
				this._items = array._items;
				this._nextItemSegment = 0;
				this._nextItemIndex = 0;
				this._current = default(T);
			}

			// Token: 0x1700111C RID: 4380
			// (get) Token: 0x06003DEE RID: 15854 RVA: 0x000AEC7F File Offset: 0x000ACE7F
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700111D RID: 4381
			// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000AEC87 File Offset: 0x000ACE87
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003DF0 RID: 15856 RVA: 0x000AEC94 File Offset: 0x000ACE94
			public void Dispose()
			{
			}

			// Token: 0x06003DF1 RID: 15857 RVA: 0x000AEC98 File Offset: 0x000ACE98
			public bool MoveNext()
			{
				if (this._items.Length == 0)
				{
					return false;
				}
				if (this._nextItemIndex == this._items[this._nextItemSegment].Length)
				{
					if (this._nextItemSegment == this._items.Length - 1)
					{
						return false;
					}
					this._nextItemSegment++;
					this._nextItemIndex = 0;
				}
				this._current = this._items[this._nextItemSegment][this._nextItemIndex];
				this._nextItemIndex++;
				return true;
			}

			// Token: 0x06003DF2 RID: 15858 RVA: 0x000AED1D File Offset: 0x000ACF1D
			public void Reset()
			{
				this._nextItemSegment = 0;
				this._nextItemIndex = 0;
				this._current = default(T);
			}

			// Token: 0x04001D16 RID: 7446
			private readonly T[][] _items;

			// Token: 0x04001D17 RID: 7447
			private int _nextItemSegment;

			// Token: 0x04001D18 RID: 7448
			private int _nextItemIndex;

			// Token: 0x04001D19 RID: 7449
			private T _current;
		}

		// Token: 0x0200075A RID: 1882
		[Nullable(0)]
		internal readonly struct TestAccessor
		{
			// Token: 0x06003DF3 RID: 15859 RVA: 0x000AED39 File Offset: 0x000ACF39
			public TestAccessor([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array)
			{
				this._array = array;
			}

			// Token: 0x1700111E RID: 4382
			// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000AED42 File Offset: 0x000ACF42
			public static int SegmentSize
			{
				get
				{
					return SegmentedArray<T>.SegmentSize;
				}
			}

			// Token: 0x1700111F RID: 4383
			// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000AED49 File Offset: 0x000ACF49
			public T[][] Items
			{
				get
				{
					return this._array._items;
				}
			}

			// Token: 0x04001D1A RID: 7450
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _array;
		}
	}
}
