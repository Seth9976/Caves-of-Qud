using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004A9 RID: 1193
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SegmentedArraySortHelper<[Nullable(2)] T>
	{
		// Token: 0x06002FF8 RID: 12280 RVA: 0x000877C8 File Offset: 0x000859C8
		public static void Sort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				SegmentedArraySortHelper<T>.IntrospectiveSort(keys, new Comparison<T>(comparer.Compare));
			}
			catch (IndexOutOfRangeException)
			{
				ThrowHelper.ThrowArgumentException_BadComparer(comparer);
			}
			catch (Exception ex)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, ex);
			}
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00087824 File Offset: 0x00085A24
		public static int BinarySearch([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			int num;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				num = SegmentedArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
			}
			catch (Exception ex)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x00087868 File Offset: 0x00085A68
		internal static void Sort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer)
		{
			try
			{
				SegmentedArraySortHelper<T>.IntrospectiveSort(keys, comparer);
			}
			catch (IndexOutOfRangeException)
			{
				ThrowHelper.ThrowArgumentException_BadComparer(comparer);
			}
			catch (Exception ex)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, ex);
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000878B0 File Offset: 0x00085AB0
		internal unsafe static int InternalBinarySearch([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(*array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x00087900 File Offset: 0x00085B00
		private unsafe static void SwapIfGreater([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer, int i, int j)
		{
			if (comparer.Invoke(*keys[i], *keys[j]) > 0)
			{
				T t = *keys[i];
				*keys[i] = *keys[j];
				*keys[j] = t;
			}
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x00087968 File Offset: 0x00085B68
		[MethodImpl(256)]
		private unsafe static void Swap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> a, int i, int j)
		{
			T t = *a[i];
			*a[i] = *a[j];
			*a[j] = t;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000879AB File Offset: 0x00085BAB
		internal static void IntrospectiveSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer)
		{
			if (keys.Length > 1)
			{
				SegmentedArraySortHelper<T>.IntroSort(keys, 2 * (SegmentedArraySortUtils.Log2((uint)keys.Length) + 1), comparer);
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000879D0 File Offset: 0x00085BD0
		private static void IntroSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, int depthLimit, Comparison<T> comparer)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 1);
						return;
					}
					if (i == 3)
					{
						SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 1);
						SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 2);
						SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 1, 2);
						return;
					}
					SegmentedArraySortHelper<T>.InsertionSort(keys.Slice(0, i), comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						SegmentedArraySortHelper<T>.HeapSort(keys.Slice(0, i), comparer);
						return;
					}
					depthLimit--;
					int num = SegmentedArraySortHelper<T>.PickPivotAndPartition(keys.Slice(0, i), comparer);
					SegmentedArraySortHelper<T>.IntroSort(keys.Slice(num + 1, i - (num + 1)), depthLimit, comparer);
					i = num;
				}
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00087A74 File Offset: 0x00085C74
		private unsafe static int PickPivotAndPartition([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, num2);
			SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, num);
			SegmentedArraySortHelper<T>.SwapIfGreater(keys, comparer, num2, num);
			T t = *keys[num2];
			SegmentedArraySortHelper<T>.Swap(keys, num2, num - 1);
			int i = 0;
			int num3 = num - 1;
			while (i < num3)
			{
				while (comparer.Invoke(*keys[++i], t) < 0)
				{
				}
				while (comparer.Invoke(t, *keys[--num3]) < 0)
				{
				}
				if (i >= num3)
				{
					break;
				}
				SegmentedArraySortHelper<T>.Swap(keys, i, num3);
			}
			if (i != num - 1)
			{
				SegmentedArraySortHelper<T>.Swap(keys, i, num - 1);
			}
			return i;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x00087B28 File Offset: 0x00085D28
		private static void HeapSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				SegmentedArraySortHelper<T>.DownHeap(keys, i, length, 0, comparer);
			}
			for (int j = length; j > 1; j--)
			{
				SegmentedArraySortHelper<T>.Swap(keys, 0, j - 1);
				SegmentedArraySortHelper<T>.DownHeap(keys, 1, j - 1, 0, comparer);
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x00087B78 File Offset: 0x00085D78
		private unsafe static void DownHeap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, int i, int n, int lo, Comparison<T> comparer)
		{
			T t = *keys[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && comparer.Invoke(*keys[lo + num - 1], *keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Invoke(t, *keys[lo + num - 1]) >= 0)
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = t;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x00087C28 File Offset: 0x00085E28
		private unsafe static void InsertionSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, Comparison<T> comparer)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				T t = *keys[i + 1];
				int num = i;
				while (num >= 0 && comparer.Invoke(t, *keys[num]) < 0)
				{
					*keys[num + 1] = *keys[num];
					num--;
				}
				*keys[num + 1] = t;
			}
		}
	}
}
