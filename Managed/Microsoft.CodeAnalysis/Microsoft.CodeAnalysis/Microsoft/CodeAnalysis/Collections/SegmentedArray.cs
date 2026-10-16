using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Collections.Internal;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A5 RID: 1189
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SegmentedArray
	{
		// Token: 0x06002F2F RID: 12079 RVA: 0x00084B30 File Offset: 0x00082D30
		[NullableContext(2)]
		internal static void Clear<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length)
		{
			foreach (Memory<T> memory in array.GetSegments<T>(index, length))
			{
				memory.Span.Clear();
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x00084B70 File Offset: 0x00082D70
		[NullableContext(2)]
		internal static void Copy<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> sourceArray, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> destinationArray, int length)
		{
			if (length == 0)
			{
				return;
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length > sourceArray.Length)
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcArray, "sourceArray");
			}
			if (length > destinationArray.Length)
			{
				throw new ArgumentException(SR.Arg_LongerThanDestArray, "destinationArray");
			}
			foreach (ValueTuple<Memory<T>, Memory<T>> valueTuple in SegmentedArray.GetSegments<T>(sourceArray, destinationArray, length))
			{
				Memory<T> item = valueTuple.Item1;
				Memory<T> item2 = valueTuple.Item2;
				item.CopyTo(item2);
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x00084BFC File Offset: 0x00082DFC
		public static void Copy<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> sourceArray, Array destinationArray, int length)
		{
			if (destinationArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.destinationArray);
			}
			if (length == 0)
			{
				return;
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length > sourceArray.Length)
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcArray, "sourceArray");
			}
			if (length > destinationArray.Length)
			{
				throw new ArgumentException(SR.Arg_LongerThanDestArray, "destinationArray");
			}
			int num = 0;
			SegmentedArray.SegmentEnumerator<T> enumerator = sourceArray.GetSegments<T>(0, length).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ArraySegment<T> arraySegment;
				if (!MemoryMarshal.TryGetArray<T>(enumerator.Current, ref arraySegment))
				{
					throw new NotSupportedException();
				}
				Array.Copy(arraySegment.Array, arraySegment.Offset, destinationArray, num, arraySegment.Count);
				num += arraySegment.Count;
			}
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x00084CB8 File Offset: 0x00082EB8
		[NullableContext(2)]
		public static void Copy<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> sourceArray, int sourceIndex, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> destinationArray, int destinationIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			if ((ulong)(sourceIndex + length) > (ulong)((long)sourceArray.Length))
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcArray, "sourceArray");
			}
			if ((ulong)(destinationIndex + length) > (ulong)((long)destinationArray.Length))
			{
				throw new ArgumentException(SR.Arg_LongerThanDestArray, "destinationArray");
			}
			if (length == 0)
			{
				return;
			}
			if (sourceArray.SyncRoot == destinationArray.SyncRoot && sourceIndex + length > destinationIndex)
			{
				SegmentedArray.CopyOverlapped<T>(sourceArray, sourceIndex, destinationIndex, length);
				return;
			}
			foreach (ValueTuple<Memory<T>, Memory<T>> valueTuple in SegmentedArray.GetSegmentsUnaligned<T>(sourceArray, sourceIndex, destinationArray, destinationIndex, length))
			{
				Memory<T> item = valueTuple.Item1;
				Memory<T> item2 = valueTuple.Item2;
				item.CopyTo(item2);
			}
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x00084DA4 File Offset: 0x00082FA4
		[NullableContext(2)]
		[MethodImpl(8)]
		private static void CopyOverlapped<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int sourceIndex, int destinationIndex, int length)
		{
			SegmentedArray.UnalignedSegmentEnumerable<T> segmentsUnaligned = SegmentedArray.GetSegmentsUnaligned<T>(array, sourceIndex, array, destinationIndex, length);
			if (sourceIndex < destinationIndex)
			{
				foreach (ValueTuple<Memory<T>, Memory<T>> valueTuple in segmentsUnaligned.Reverse())
				{
					Memory<T> item = valueTuple.Item1;
					Memory<T> item2 = valueTuple.Item2;
					item.CopyTo(item2);
				}
				return;
			}
			foreach (ValueTuple<Memory<T>, Memory<T>> valueTuple2 in segmentsUnaligned)
			{
				Memory<T> item3 = valueTuple2.Item1;
				Memory<T> item4 = valueTuple2.Item2;
				item3.CopyTo(item4);
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x00084E30 File Offset: 0x00083030
		public static void Copy<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			if (destinationArray == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.destinationArray);
			}
			if (typeof(T[]) != destinationArray.GetType() && destinationArray.Rank != 1)
			{
				throw new RankException(SR.Rank_MustMatch);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			int lowerBound = destinationArray.GetLowerBound(0);
			if (destinationIndex < lowerBound || destinationIndex - lowerBound < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_ArrayLB);
			}
			destinationIndex -= lowerBound;
			if ((ulong)(sourceIndex + length) > (ulong)((long)sourceArray.Length))
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcArray, "sourceArray");
			}
			if ((UIntPtr)(destinationIndex + length) > (UIntPtr)destinationArray.LongLength)
			{
				throw new ArgumentException(SR.Arg_LongerThanDestArray, "destinationArray");
			}
			int num = 0;
			SegmentedArray.SegmentEnumerator<T> enumerator = sourceArray.GetSegments<T>(0, length).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ArraySegment<T> arraySegment;
				if (!MemoryMarshal.TryGetArray<T>(enumerator.Current, ref arraySegment))
				{
					throw new NotSupportedException();
				}
				Array.Copy(arraySegment.Array, arraySegment.Offset, destinationArray, num, arraySegment.Count);
				num += arraySegment.Count;
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00084F5F File Offset: 0x0008315F
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value)
		{
			return SegmentedArray.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00084F71 File Offset: 0x00083171
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return SegmentedArray.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00084F83 File Offset: 0x00083183
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value)
		{
			return SegmentedArray.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00084F8F File Offset: 0x0008318F
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return SegmentedArraySortHelper<T>.BinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00084FC0 File Offset: 0x000831C0
		public static int IndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value)
		{
			return SegmentedArray.IndexOf<T>(array, value, 0, array.Length);
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00084FD1 File Offset: 0x000831D1
		public static int IndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value, int startIndex)
		{
			return SegmentedArray.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00084FE4 File Offset: 0x000831E4
		public static int IndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value, int startIndex, int count)
		{
			if (startIndex > array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count > array.Length - startIndex)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex;
			SegmentedArray.SegmentEnumerator<T> enumerator = array.GetSegments<T>(startIndex, count).GetEnumerator();
			while (enumerator.MoveNext())
			{
				ArraySegment<T> arraySegment;
				if (!MemoryMarshal.TryGetArray<T>(enumerator.Current, ref arraySegment))
				{
					throw new NotSupportedException();
				}
				int num2 = Array.IndexOf<T>(arraySegment.Array, value, arraySegment.Offset, arraySegment.Count);
				if (num2 >= 0)
				{
					return num2 + num - arraySegment.Offset;
				}
				num += arraySegment.Count;
			}
			return -1;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00085084 File Offset: 0x00083284
		public static int LastIndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value)
		{
			return SegmentedArray.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0008509D File Offset: 0x0008329D
		public static int LastIndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value, int startIndex)
		{
			return SegmentedArray.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000850B8 File Offset: 0x000832B8
		public unsafe static int LastIndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, T value, int startIndex, int count)
		{
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
				if (count != 0)
				{
					ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
				}
				return -1;
			}
			if (startIndex >= array.Length)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (EqualityComparer<T>.Default.Equals(*array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x00085133 File Offset: 0x00083333
		[NullableContext(2)]
		public static void Reverse<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array)
		{
			SegmentedArray.Reverse<T>(array, 0, array.Length);
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x00085144 File Offset: 0x00083344
		[NullableContext(2)]
		public unsafe static void Reverse<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length <= 1)
			{
				return;
			}
			int num = index;
			int num2 = index + length - 1;
			do
			{
				T t = *array[num];
				*array[num] = *array[num2];
				*array[num2] = t;
				num++;
				num2--;
			}
			while (num < num2);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000851C4 File Offset: 0x000833C4
		[NullableContext(2)]
		public static void Sort<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array)
		{
			if (array.Length > 1)
			{
				SegmentedArraySortHelper<T>.Sort(new SegmentedArraySegment<T>(array, 0, array.Length), null);
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000851E4 File Offset: 0x000833E4
		[NullableContext(2)]
		public static void Sort<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length)
		{
			SegmentedArray.Sort<T>(array, index, length, null);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000851EF File Offset: 0x000833EF
		[NullableContext(2)]
		public static void Sort<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			SegmentedArray.Sort<T>(array, 0, array.Length, comparer);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x00085200 File Offset: 0x00083400
		[NullableContext(2)]
		public static void Sort<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (length < 0)
			{
				ThrowHelper.ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum();
			}
			if (array.Length - index < length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (length > 1)
			{
				SegmentedArraySortHelper<T>.Sort(new SegmentedArraySegment<T>(array, index, length), comparer);
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x00085238 File Offset: 0x00083438
		public static void Sort<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (array.Length > 1)
			{
				SegmentedArraySortHelper<T>.Sort(new SegmentedArraySegment<T>(array, 0, array.Length), comparison);
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x00085262 File Offset: 0x00083462
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static SegmentedArray.SegmentEnumerable<T> GetSegments<T>([Nullable(new byte[] { 0, 1 })] this SegmentedArray<T> array, int offset, int length)
		{
			return new SegmentedArray.SegmentEnumerable<T>(array, offset, length);
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0008526C File Offset: 0x0008346C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static SegmentedArray.AlignedSegmentEnumerable<T> GetSegments<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int length)
		{
			return new SegmentedArray.AlignedSegmentEnumerable<T>(first, second, length);
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00085276 File Offset: 0x00083476
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static SegmentedArray.AlignedSegmentEnumerable<T> GetSegmentsAligned<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, int firstOffset, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int secondOffset, int length)
		{
			return new SegmentedArray.AlignedSegmentEnumerable<T>(first, firstOffset, second, secondOffset, length);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00085283 File Offset: 0x00083483
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static SegmentedArray.UnalignedSegmentEnumerable<T> GetSegmentsUnaligned<T>([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, int firstOffset, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int secondOffset, int length)
		{
			return new SegmentedArray.UnalignedSegmentEnumerable<T>(first, firstOffset, second, secondOffset, length);
		}

		// Token: 0x02000753 RID: 1875
		[NullableContext(0)]
		private readonly struct AlignedSegmentEnumerable<[Nullable(2)] T>
		{
			// Token: 0x06003DD9 RID: 15833 RVA: 0x000AE6B0 File Offset: 0x000AC8B0
			public AlignedSegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int length)
			{
				this = new SegmentedArray.AlignedSegmentEnumerable<T>(first, 0, second, 0, length);
			}

			// Token: 0x06003DDA RID: 15834 RVA: 0x000AE6BD File Offset: 0x000AC8BD
			public AlignedSegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, int firstOffset, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int secondOffset, int length)
			{
				this._first = first;
				this._firstOffset = firstOffset;
				this._second = second;
				this._secondOffset = secondOffset;
				this._length = length;
			}

			// Token: 0x06003DDB RID: 15835 RVA: 0x000AE6E4 File Offset: 0x000AC8E4
			[return: Nullable(new byte[] { 0, 1 })]
			public SegmentedArray.AlignedSegmentEnumerator<T> GetEnumerator()
			{
				return new SegmentedArray.AlignedSegmentEnumerator<T>((T[][])this._first.SyncRoot, this._firstOffset, (T[][])this._second.SyncRoot, this._secondOffset, this._length);
			}

			// Token: 0x04001CF6 RID: 7414
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _first;

			// Token: 0x04001CF7 RID: 7415
			private readonly int _firstOffset;

			// Token: 0x04001CF8 RID: 7416
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _second;

			// Token: 0x04001CF9 RID: 7417
			private readonly int _secondOffset;

			// Token: 0x04001CFA RID: 7418
			private readonly int _length;
		}

		// Token: 0x02000754 RID: 1876
		[Nullable(0)]
		private struct AlignedSegmentEnumerator<[Nullable(2)] T>
		{
			// Token: 0x06003DDC RID: 15836 RVA: 0x000AE720 File Offset: 0x000AC920
			public AlignedSegmentEnumerator(T[][] firstSegments, int firstOffset, T[][] secondSegments, int secondOffset, int length)
			{
				this._firstSegments = firstSegments;
				this._firstOffset = firstOffset;
				this._secondSegments = secondSegments;
				this._secondOffset = secondOffset;
				this._length = length;
				this._completed = 0;
				this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
			}

			// Token: 0x17001119 RID: 4377
			// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000AE76E File Offset: 0x000AC96E
			[TupleElementNames(new string[] { "first", "second" })]
			[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
			public ValueTuple<Memory<T>, Memory<T>> Current
			{
				[return: TupleElementNames(new string[] { "first", "second" })]
				[return: Nullable(new byte[] { 0, 0, 1, 0, 1 })]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003DDE RID: 15838 RVA: 0x000AE778 File Offset: 0x000AC978
			public bool MoveNext()
			{
				if (this._completed == this._length)
				{
					this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
					return false;
				}
				if (this._completed == 0)
				{
					int num = this._firstOffset >> SegmentedArrayHelper.GetSegmentShift<T>();
					int num2 = this._secondOffset >> SegmentedArrayHelper.GetSegmentShift<T>();
					int num3 = this._firstOffset & SegmentedArrayHelper.GetOffsetMask<T>();
					T[] array = this._firstSegments[num];
					T[] array2 = this._secondSegments[num2];
					int num4 = Math.Min(array.Length - num3, this._length);
					this._current = new ValueTuple<Memory<T>, Memory<T>>(MemoryExtensions.AsMemory<T>(array).Slice(num3, num4), MemoryExtensions.AsMemory<T>(array2).Slice(num3, num4));
					this._completed = num4;
					return true;
				}
				T[] array3 = this._firstSegments[this._completed + this._firstOffset >> SegmentedArrayHelper.GetSegmentShift<T>()];
				T[] array4 = this._secondSegments[this._completed + this._secondOffset >> SegmentedArrayHelper.GetSegmentShift<T>()];
				int num5 = Math.Min(SegmentedArrayHelper.GetSegmentSize<T>(), this._length - this._completed);
				this._current = new ValueTuple<Memory<T>, Memory<T>>(MemoryExtensions.AsMemory<T>(array3).Slice(0, num5), MemoryExtensions.AsMemory<T>(array4).Slice(0, num5));
				this._completed += num5;
				return true;
			}

			// Token: 0x04001CFB RID: 7419
			private readonly T[][] _firstSegments;

			// Token: 0x04001CFC RID: 7420
			private readonly int _firstOffset;

			// Token: 0x04001CFD RID: 7421
			private readonly T[][] _secondSegments;

			// Token: 0x04001CFE RID: 7422
			private readonly int _secondOffset;

			// Token: 0x04001CFF RID: 7423
			private readonly int _length;

			// Token: 0x04001D00 RID: 7424
			private int _completed;

			// Token: 0x04001D01 RID: 7425
			[TupleElementNames(new string[] { "first", "second" })]
			[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
			private ValueTuple<Memory<T>, Memory<T>> _current;
		}

		// Token: 0x02000755 RID: 1877
		[NullableContext(0)]
		private readonly struct UnalignedSegmentEnumerable<[Nullable(2)] T>
		{
			// Token: 0x06003DDF RID: 15839 RVA: 0x000AE8D9 File Offset: 0x000ACAD9
			public UnalignedSegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int length)
			{
				this = new SegmentedArray.UnalignedSegmentEnumerable<T>(first, 0, second, 0, length);
			}

			// Token: 0x06003DE0 RID: 15840 RVA: 0x000AE8E6 File Offset: 0x000ACAE6
			public UnalignedSegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> first, int firstOffset, [Nullable(new byte[] { 0, 1 })] SegmentedArray<T> second, int secondOffset, int length)
			{
				this._first = first;
				this._firstOffset = firstOffset;
				this._second = second;
				this._secondOffset = secondOffset;
				this._length = length;
			}

			// Token: 0x06003DE1 RID: 15841 RVA: 0x000AE90D File Offset: 0x000ACB0D
			[return: Nullable(new byte[] { 0, 1 })]
			public SegmentedArray.UnalignedSegmentEnumerator<T> GetEnumerator()
			{
				return new SegmentedArray.UnalignedSegmentEnumerator<T>((T[][])this._first.SyncRoot, this._firstOffset, (T[][])this._second.SyncRoot, this._secondOffset, this._length);
			}

			// Token: 0x06003DE2 RID: 15842 RVA: 0x000AE946 File Offset: 0x000ACB46
			public SegmentedArray.UnalignedSegmentEnumerable<T>.ReverseEnumerable Reverse()
			{
				return new SegmentedArray.UnalignedSegmentEnumerable<T>.ReverseEnumerable(this);
			}

			// Token: 0x04001D02 RID: 7426
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _first;

			// Token: 0x04001D03 RID: 7427
			private readonly int _firstOffset;

			// Token: 0x04001D04 RID: 7428
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _second;

			// Token: 0x04001D05 RID: 7429
			private readonly int _secondOffset;

			// Token: 0x04001D06 RID: 7430
			private readonly int _length;

			// Token: 0x020007FB RID: 2043
			public readonly struct ReverseEnumerable
			{
				// Token: 0x060040CA RID: 16586 RVA: 0x000B48C4 File Offset: 0x000B2AC4
				public ReverseEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray.UnalignedSegmentEnumerable<T> enumerable)
				{
					this._enumerable = enumerable;
				}

				// Token: 0x060040CB RID: 16587 RVA: 0x000B48D0 File Offset: 0x000B2AD0
				[return: Nullable(new byte[] { 0, 1 })]
				public SegmentedArray.UnalignedSegmentEnumerator<T>.Reverse GetEnumerator()
				{
					return new SegmentedArray.UnalignedSegmentEnumerator<T>.Reverse((T[][])this._enumerable._first.SyncRoot, this._enumerable._firstOffset, (T[][])this._enumerable._second.SyncRoot, this._enumerable._secondOffset, this._enumerable._length);
				}

				// Token: 0x060040CC RID: 16588 RVA: 0x000B492D File Offset: 0x000B2B2D
				[return: Nullable(new byte[] { 0, 1 })]
				public SegmentedArray.UnalignedSegmentEnumerable<T> Reverse()
				{
					return this._enumerable;
				}

				// Token: 0x04001E74 RID: 7796
				[Nullable(new byte[] { 0, 1 })]
				private readonly SegmentedArray.UnalignedSegmentEnumerable<T> _enumerable;
			}
		}

		// Token: 0x02000756 RID: 1878
		[Nullable(0)]
		private struct UnalignedSegmentEnumerator<[Nullable(2)] T>
		{
			// Token: 0x06003DE3 RID: 15843 RVA: 0x000AE954 File Offset: 0x000ACB54
			public UnalignedSegmentEnumerator(T[][] firstSegments, int firstOffset, T[][] secondSegments, int secondOffset, int length)
			{
				this._firstSegments = firstSegments;
				this._firstOffset = firstOffset;
				this._secondSegments = secondSegments;
				this._secondOffset = secondOffset;
				this._length = length;
				this._completed = 0;
				this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
			}

			// Token: 0x1700111A RID: 4378
			// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000AE9A2 File Offset: 0x000ACBA2
			[TupleElementNames(new string[] { "first", "second" })]
			[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
			public ValueTuple<Memory<T>, Memory<T>> Current
			{
				[return: TupleElementNames(new string[] { "first", "second" })]
				[return: Nullable(new byte[] { 0, 0, 1, 0, 1 })]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003DE5 RID: 15845 RVA: 0x000AE9AC File Offset: 0x000ACBAC
			public bool MoveNext()
			{
				if (this._completed == this._length)
				{
					this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
					return false;
				}
				int num = this._completed + this._firstOffset >> SegmentedArrayHelper.GetSegmentShift<T>();
				int num2 = this._completed + this._secondOffset >> SegmentedArrayHelper.GetSegmentShift<T>();
				int num3 = (this._completed + this._firstOffset) & SegmentedArrayHelper.GetOffsetMask<T>();
				int num4 = (this._completed + this._secondOffset) & SegmentedArrayHelper.GetOffsetMask<T>();
				T[] array = this._firstSegments[num];
				T[] array2 = this._secondSegments[num2];
				int num5 = array.Length - num3;
				int num6 = array2.Length - num4;
				int num7 = Math.Min(Math.Min(num5, num6), this._length - this._completed);
				this._current = new ValueTuple<Memory<T>, Memory<T>>(MemoryExtensions.AsMemory<T>(array).Slice(num3, num7), MemoryExtensions.AsMemory<T>(array2).Slice(num4, num7));
				this._completed += num7;
				return true;
			}

			// Token: 0x04001D07 RID: 7431
			private readonly T[][] _firstSegments;

			// Token: 0x04001D08 RID: 7432
			private readonly int _firstOffset;

			// Token: 0x04001D09 RID: 7433
			private readonly T[][] _secondSegments;

			// Token: 0x04001D0A RID: 7434
			private readonly int _secondOffset;

			// Token: 0x04001D0B RID: 7435
			private readonly int _length;

			// Token: 0x04001D0C RID: 7436
			private int _completed;

			// Token: 0x04001D0D RID: 7437
			[TupleElementNames(new string[] { "first", "second" })]
			[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
			private ValueTuple<Memory<T>, Memory<T>> _current;

			// Token: 0x020007FC RID: 2044
			[Nullable(0)]
			public struct Reverse
			{
				// Token: 0x060040CD RID: 16589 RVA: 0x000B4938 File Offset: 0x000B2B38
				public Reverse(T[][] firstSegments, int firstOffset, T[][] secondSegments, int secondOffset, int length)
				{
					this._firstSegments = firstSegments;
					this._firstOffset = firstOffset;
					this._secondSegments = secondSegments;
					this._secondOffset = secondOffset;
					this._length = length;
					this._completed = 0;
					this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
				}

				// Token: 0x170011C2 RID: 4546
				// (get) Token: 0x060040CE RID: 16590 RVA: 0x000B4986 File Offset: 0x000B2B86
				[TupleElementNames(new string[] { "first", "second" })]
				[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
				public ValueTuple<Memory<T>, Memory<T>> Current
				{
					[return: TupleElementNames(new string[] { "first", "second" })]
					[return: Nullable(new byte[] { 0, 0, 1, 0, 1 })]
					get
					{
						return this._current;
					}
				}

				// Token: 0x060040CF RID: 16591 RVA: 0x000B4990 File Offset: 0x000B2B90
				public bool MoveNext()
				{
					if (this._completed == this._length)
					{
						this._current = new ValueTuple<Memory<T>, Memory<T>>(Memory<T>.Empty, Memory<T>.Empty);
						return false;
					}
					int num = this._firstOffset + this._length - this._completed - 1 >> SegmentedArrayHelper.GetSegmentShift<T>();
					int num2 = this._secondOffset + this._length - this._completed - 1 >> SegmentedArrayHelper.GetSegmentShift<T>();
					int num3 = (this._firstOffset + this._length - this._completed - 1) & SegmentedArrayHelper.GetOffsetMask<T>();
					int num4 = (this._secondOffset + this._length - this._completed - 1) & SegmentedArrayHelper.GetOffsetMask<T>();
					T[] array = this._firstSegments[num];
					T[] array2 = this._secondSegments[num2];
					int num5 = num3 + 1;
					int num6 = num4 + 1;
					int num7 = Math.Min(Math.Min(num5, num6), this._length - this._completed);
					this._current = new ValueTuple<Memory<T>, Memory<T>>(MemoryExtensions.AsMemory<T>(array).Slice(num3 - num7 + 1, num7), MemoryExtensions.AsMemory<T>(array2).Slice(num4 - num7 + 1, num7));
					this._completed += num7;
					return true;
				}

				// Token: 0x04001E75 RID: 7797
				private readonly T[][] _firstSegments;

				// Token: 0x04001E76 RID: 7798
				private readonly int _firstOffset;

				// Token: 0x04001E77 RID: 7799
				private readonly T[][] _secondSegments;

				// Token: 0x04001E78 RID: 7800
				private readonly int _secondOffset;

				// Token: 0x04001E79 RID: 7801
				private readonly int _length;

				// Token: 0x04001E7A RID: 7802
				private int _completed;

				// Token: 0x04001E7B RID: 7803
				[TupleElementNames(new string[] { "first", "second" })]
				[Nullable(new byte[] { 0, 0, 1, 0, 1 })]
				private ValueTuple<Memory<T>, Memory<T>> _current;
			}
		}

		// Token: 0x02000757 RID: 1879
		[NullableContext(0)]
		private readonly struct SegmentEnumerable<[Nullable(2)] T>
		{
			// Token: 0x06003DE6 RID: 15846 RVA: 0x000AEAB2 File Offset: 0x000ACCB2
			public SegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array)
			{
				this._array = array;
				this._offset = 0;
				this._length = array.Length;
			}

			// Token: 0x06003DE7 RID: 15847 RVA: 0x000AEACF File Offset: 0x000ACCCF
			public SegmentEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int offset, int length)
			{
				if (offset < 0 || length < 0 || offset + length > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this._array = array;
				this._offset = offset;
				this._length = length;
			}

			// Token: 0x06003DE8 RID: 15848 RVA: 0x000AEAFF File Offset: 0x000ACCFF
			[return: Nullable(new byte[] { 0, 1 })]
			public SegmentedArray.SegmentEnumerator<T> GetEnumerator()
			{
				return new SegmentedArray.SegmentEnumerator<T>((T[][])this._array.SyncRoot, this._offset, this._length);
			}

			// Token: 0x06003DE9 RID: 15849 RVA: 0x000AEB22 File Offset: 0x000ACD22
			public SegmentedArray.SegmentEnumerable<T>.ReverseEnumerable Reverse()
			{
				return new SegmentedArray.SegmentEnumerable<T>.ReverseEnumerable(this);
			}

			// Token: 0x04001D0E RID: 7438
			[Nullable(new byte[] { 0, 1 })]
			private readonly SegmentedArray<T> _array;

			// Token: 0x04001D0F RID: 7439
			private readonly int _offset;

			// Token: 0x04001D10 RID: 7440
			private readonly int _length;

			// Token: 0x020007FD RID: 2045
			public readonly struct ReverseEnumerable
			{
				// Token: 0x060040D0 RID: 16592 RVA: 0x000B4ABE File Offset: 0x000B2CBE
				public ReverseEnumerable([Nullable(new byte[] { 0, 1 })] SegmentedArray.SegmentEnumerable<T> enumerable)
				{
					this._enumerable = enumerable;
				}

				// Token: 0x060040D1 RID: 16593 RVA: 0x000B4AC7 File Offset: 0x000B2CC7
				[return: Nullable(new byte[] { 0, 1 })]
				public SegmentedArray.SegmentEnumerator<T>.Reverse GetEnumerator()
				{
					return new SegmentedArray.SegmentEnumerator<T>.Reverse((T[][])this._enumerable._array.SyncRoot, this._enumerable._offset, this._enumerable._length);
				}

				// Token: 0x060040D2 RID: 16594 RVA: 0x000B4AF9 File Offset: 0x000B2CF9
				[return: Nullable(new byte[] { 0, 1 })]
				public SegmentedArray.SegmentEnumerable<T> Reverse()
				{
					return this._enumerable;
				}

				// Token: 0x04001E7C RID: 7804
				[Nullable(new byte[] { 0, 1 })]
				private readonly SegmentedArray.SegmentEnumerable<T> _enumerable;
			}
		}

		// Token: 0x02000758 RID: 1880
		[Nullable(0)]
		private struct SegmentEnumerator<[Nullable(2)] T>
		{
			// Token: 0x06003DEA RID: 15850 RVA: 0x000AEB2F File Offset: 0x000ACD2F
			public SegmentEnumerator(T[][] segments, int offset, int length)
			{
				this._segments = segments;
				this._offset = offset;
				this._length = length;
				this._completed = 0;
				this._current = Memory<T>.Empty;
			}

			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000AEB58 File Offset: 0x000ACD58
			[Nullable(new byte[] { 0, 1 })]
			public Memory<T> Current
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003DEC RID: 15852 RVA: 0x000AEB60 File Offset: 0x000ACD60
			public bool MoveNext()
			{
				if (this._completed == this._length)
				{
					this._current = Memory<T>.Empty;
					return false;
				}
				if (this._completed == 0)
				{
					int num = this._offset >> SegmentedArrayHelper.GetSegmentShift<T>();
					int num2 = this._offset & SegmentedArrayHelper.GetOffsetMask<T>();
					T[] array = this._segments[num];
					int num3 = array.Length - num2;
					this._current = MemoryExtensions.AsMemory<T>(array).Slice(num2, Math.Min(num3, this._length));
					this._completed = this._current.Length;
					return true;
				}
				T[] array2 = this._segments[this._completed + this._offset >> SegmentedArrayHelper.GetSegmentShift<T>()];
				this._current = MemoryExtensions.AsMemory<T>(array2).Slice(0, Math.Min(SegmentedArrayHelper.GetSegmentSize<T>(), this._length - this._completed));
				this._completed += this._current.Length;
				return true;
			}

			// Token: 0x04001D11 RID: 7441
			private readonly T[][] _segments;

			// Token: 0x04001D12 RID: 7442
			private readonly int _offset;

			// Token: 0x04001D13 RID: 7443
			private readonly int _length;

			// Token: 0x04001D14 RID: 7444
			private int _completed;

			// Token: 0x04001D15 RID: 7445
			[Nullable(new byte[] { 0, 1 })]
			private Memory<T> _current;

			// Token: 0x020007FE RID: 2046
			[Nullable(0)]
			public struct Reverse
			{
				// Token: 0x060040D3 RID: 16595 RVA: 0x000B4B01 File Offset: 0x000B2D01
				public Reverse(T[][] segments, int offset, int length)
				{
					this._segments = segments;
					this._offset = offset;
					this._length = length;
					this._completed = 0;
					this._current = Memory<T>.Empty;
				}

				// Token: 0x170011C3 RID: 4547
				// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000B4B2A File Offset: 0x000B2D2A
				[Nullable(new byte[] { 0, 1 })]
				public Memory<T> Current
				{
					[return: Nullable(new byte[] { 0, 1 })]
					get
					{
						return this._current;
					}
				}

				// Token: 0x060040D5 RID: 16597 RVA: 0x000B4B34 File Offset: 0x000B2D34
				public bool MoveNext()
				{
					if (this._completed == this._length)
					{
						this._current = Memory<T>.Empty;
						return false;
					}
					if (this._completed == 0)
					{
						int num = this._offset >> SegmentedArrayHelper.GetSegmentShift<T>();
						int num2 = this._offset & SegmentedArrayHelper.GetOffsetMask<T>();
						T[] array = this._segments[num];
						int num3 = array.Length - num2;
						this._current = MemoryExtensions.AsMemory<T>(array).Slice(num2, Math.Min(num3, this._length));
						this._completed = this._current.Length;
						return true;
					}
					T[] array2 = this._segments[this._completed + this._offset >> SegmentedArrayHelper.GetSegmentShift<T>()];
					this._current = MemoryExtensions.AsMemory<T>(array2).Slice(0, Math.Min(SegmentedArrayHelper.GetSegmentSize<T>(), this._length - this._completed));
					this._completed += this._current.Length;
					return true;
				}

				// Token: 0x04001E7D RID: 7805
				private readonly T[][] _segments;

				// Token: 0x04001E7E RID: 7806
				private readonly int _offset;

				// Token: 0x04001E7F RID: 7807
				private readonly int _length;

				// Token: 0x04001E80 RID: 7808
				private int _completed;

				// Token: 0x04001E81 RID: 7809
				[Nullable(new byte[] { 0, 1 })]
				private Memory<T> _current;
			}
		}
	}
}
