using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AB RID: 1195
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SegmentedArraySortHelper<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06003010 RID: 12304 RVA: 0x00088970 File Offset: 0x00086B70
		public static void Sort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> comparer)
		{
			try
			{
				SegmentedArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, comparer ?? Comparer<TKey>.Default);
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

		// Token: 0x06003011 RID: 12305 RVA: 0x000889C0 File Offset: 0x00086BC0
		private unsafe static void SwapIfGreaterWithValues([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, IComparer<TKey> comparer, int i, int j)
		{
			if (comparer.Compare(*keys[i], *keys[j]) > 0)
			{
				TKey tkey = *keys[i];
				*keys[i] = *keys[j];
				*keys[j] = tkey;
				TValue tvalue = *values[i];
				*values[i] = *values[j];
				*values[j] = tvalue;
			}
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00088A64 File Offset: 0x00086C64
		[MethodImpl(256)]
		private unsafe static void Swap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int i, int j)
		{
			TKey tkey = *keys[i];
			*keys[i] = *keys[j];
			*keys[j] = tkey;
			TValue tvalue = *values[i];
			*values[i] = *values[j];
			*values[j] = tvalue;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x00088ADD File Offset: 0x00086CDD
		internal static void IntrospectiveSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, IComparer<TKey> comparer)
		{
			if (keys.Length > 1)
			{
				SegmentedArraySortHelper<TKey, TValue>.IntroSort(keys, values, 2 * (SegmentedArraySortUtils.Log2((uint)keys.Length) + 1), comparer);
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00088B04 File Offset: 0x00086D04
		private static void IntroSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int depthLimit, IComparer<TKey> comparer)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 1);
						return;
					}
					if (i == 3)
					{
						SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 1);
						SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 2);
						SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 1, 2);
						return;
					}
					SegmentedArraySortHelper<TKey, TValue>.InsertionSort(keys.Slice(0, i), values.Slice(0, i), comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						SegmentedArraySortHelper<TKey, TValue>.HeapSort(keys.Slice(0, i), values.Slice(0, i), comparer);
						return;
					}
					depthLimit--;
					int num = SegmentedArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys.Slice(0, i), values.Slice(0, i), comparer);
					SegmentedArraySortHelper<TKey, TValue>.IntroSort(keys.Slice(num + 1, i - (num + 1)), values.Slice(num + 1, i - (num + 1)), depthLimit, comparer);
					i = num;
				}
			}
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x00088BD8 File Offset: 0x00086DD8
		private unsafe static int PickPivotAndPartition([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, IComparer<TKey> comparer)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, num2);
			SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, num);
			SegmentedArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, num2, num);
			TKey tkey = *keys[num2];
			SegmentedArraySortHelper<TKey, TValue>.Swap(keys, values, num2, num - 1);
			int i = 0;
			int num3 = num - 1;
			while (i < num3)
			{
				while (comparer.Compare(*keys[++i], tkey) < 0)
				{
				}
				while (comparer.Compare(tkey, *keys[--num3]) < 0)
				{
				}
				if (i >= num3)
				{
					break;
				}
				SegmentedArraySortHelper<TKey, TValue>.Swap(keys, values, i, num3);
			}
			if (i != num - 1)
			{
				SegmentedArraySortHelper<TKey, TValue>.Swap(keys, values, i, num - 1);
			}
			return i;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00088C94 File Offset: 0x00086E94
		private static void HeapSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, IComparer<TKey> comparer)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				SegmentedArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, length, 0, comparer);
			}
			for (int j = length; j > 1; j--)
			{
				SegmentedArraySortHelper<TKey, TValue>.Swap(keys, values, 0, j - 1);
				SegmentedArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, 0, comparer);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00088CE8 File Offset: 0x00086EE8
		private unsafe static void DownHeap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int i, int n, int lo, IComparer<TKey> comparer)
		{
			TKey tkey = *keys[lo + i - 1];
			TValue tvalue = *values[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(*keys[lo + num - 1], *keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(tkey, *keys[lo + num - 1]) >= 0)
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				*values[lo + i - 1] = *values[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = tkey;
			*values[lo + i - 1] = tvalue;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00088DF0 File Offset: 0x00086FF0
		private unsafe static void InsertionSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, IComparer<TKey> comparer)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				TKey tkey = *keys[i + 1];
				TValue tvalue = *values[i + 1];
				int num = i;
				while (num >= 0 && comparer.Compare(tkey, *keys[num]) < 0)
				{
					*keys[num + 1] = *keys[num];
					*values[num + 1] = *values[num];
					num--;
				}
				*keys[num + 1] = tkey;
				*values[num + 1] = tvalue;
			}
		}
	}
}
