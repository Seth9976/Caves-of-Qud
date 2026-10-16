using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AC RID: 1196
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SegmentedGenericArraySortHelper<[Nullable(0)] TKey, [Nullable(2)] TValue> where TKey : IComparable<TKey>
	{
		// Token: 0x06003019 RID: 12313 RVA: 0x00088EB4 File Offset: 0x000870B4
		public static void Sort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, [Nullable(new byte[] { 2, 1 })] IComparer<TKey> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<TKey>.Default)
				{
					if (keys.Length > 1)
					{
						if (typeof(TKey) == typeof(double) || typeof(TKey) == typeof(float) || typeof(TKey) == typeof(float))
						{
							int num = SegmentedArraySortUtils.MoveNansToFront<TKey, TValue>(keys, values);
							if (num == keys.Length)
							{
								return;
							}
							keys = keys.Slice(num);
							values = values.Slice(num);
						}
						SegmentedGenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, 2 * (SegmentedArraySortUtils.Log2((uint)keys.Length) + 1));
					}
				}
				else
				{
					SegmentedArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, comparer);
				}
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

		// Token: 0x0600301A RID: 12314 RVA: 0x00088FA8 File Offset: 0x000871A8
		private unsafe static void SwapIfGreaterWithValues([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int i, int j)
		{
			ref TKey ptr = ref keys[i];
			if (ptr != null && SegmentedGenericArraySortHelper<TKey, TValue>.GreaterThan(ref ptr, keys[j]))
			{
				TKey tkey = ptr;
				*keys[i] = *keys[j];
				*keys[j] = tkey;
				TValue tvalue = *values[i];
				*values[i] = *values[j];
				*values[j] = tvalue;
			}
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x00089040 File Offset: 0x00087240
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

		// Token: 0x0600301C RID: 12316 RVA: 0x000890BC File Offset: 0x000872BC
		private static void IntroSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int depthLimit)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 1);
						return;
					}
					if (i == 3)
					{
						SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 1);
						SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 2);
						SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 1, 2);
						return;
					}
					SegmentedGenericArraySortHelper<TKey, TValue>.InsertionSort(keys.Slice(0, i), values.Slice(0, i));
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						SegmentedGenericArraySortHelper<TKey, TValue>.HeapSort(keys.Slice(0, i), values.Slice(0, i));
						return;
					}
					depthLimit--;
					int num = SegmentedGenericArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys.Slice(0, i), values.Slice(0, i));
					SegmentedGenericArraySortHelper<TKey, TValue>.IntroSort(keys.Slice(num + 1, i - (num + 1)), values.Slice(num + 1, i - (num + 1)), depthLimit);
					i = num;
				}
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x00089188 File Offset: 0x00087388
		private unsafe static int PickPivotAndPartition([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, num2);
			SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, num);
			SegmentedGenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, num2, num);
			TKey tkey = *keys[num2];
			SegmentedGenericArraySortHelper<TKey, TValue>.Swap(keys, values, num2, num - 1);
			int i = 0;
			int j = num - 1;
			while (i < j)
			{
				if (tkey == null)
				{
					while (i < num - 1 && *keys[++i] == null)
					{
					}
					while (j > 0)
					{
						if (*keys[--j] == null)
						{
							break;
						}
					}
				}
				else
				{
					while (SegmentedGenericArraySortHelper<TKey, TValue>.GreaterThan(ref tkey, keys[++i]))
					{
					}
					while (SegmentedGenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[--j]))
					{
					}
				}
				if (i >= j)
				{
					break;
				}
				SegmentedGenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, j);
			}
			if (i != num - 1)
			{
				SegmentedGenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, num - 1);
			}
			return i;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00089280 File Offset: 0x00087480
		private static void HeapSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				SegmentedGenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, length, 0);
			}
			for (int j = length; j > 1; j--)
			{
				SegmentedGenericArraySortHelper<TKey, TValue>.Swap(keys, values, 0, j - 1);
				SegmentedGenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, 0);
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000892D0 File Offset: 0x000874D0
		private unsafe static void DownHeap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values, int i, int n, int lo)
		{
			TKey tkey = *keys[lo + i - 1];
			TValue tvalue = *values[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && (*keys[lo + num - 1] == null || SegmentedGenericArraySortHelper<TKey, TValue>.LessThan(keys[lo + num - 1], keys[lo + num])))
				{
					num++;
				}
				if (*keys[lo + num - 1] == null || !SegmentedGenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[lo + num - 1]))
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

		// Token: 0x06003020 RID: 12320 RVA: 0x000893F8 File Offset: 0x000875F8
		private unsafe static void InsertionSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<TKey> keys, [Nullable(new byte[] { 0, 1 })] Span<TValue> values)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				TKey tkey = *keys[i + 1];
				TValue tvalue = *values[i + 1];
				int num = i;
				while (num >= 0 && (tkey == null || SegmentedGenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[num])))
				{
					*keys[num + 1] = *keys[num];
					*values[num + 1] = *values[num];
					num--;
				}
				*keys[num + 1] = tkey;
				*values[num + 1] = tvalue;
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000894BC File Offset: 0x000876BC
		[MethodImpl(256)]
		private static bool LessThan(ref TKey left, ref TKey right)
		{
			if (typeof(TKey) == typeof(byte))
			{
				return (byte)((object)left) < (byte)((object)right);
			}
			if (typeof(TKey) == typeof(sbyte))
			{
				return (sbyte)((object)left) < (sbyte)((object)right);
			}
			if (typeof(TKey) == typeof(ushort))
			{
				return (ushort)((object)left) < (ushort)((object)right);
			}
			if (typeof(TKey) == typeof(short))
			{
				return (short)((object)left) < (short)((object)right);
			}
			if (typeof(TKey) == typeof(uint))
			{
				return (uint)((object)left) < (uint)((object)right);
			}
			if (typeof(TKey) == typeof(int))
			{
				return (int)((object)left) < (int)((object)right);
			}
			if (typeof(TKey) == typeof(ulong))
			{
				return (ulong)((object)left) < (ulong)((object)right);
			}
			if (typeof(TKey) == typeof(long))
			{
				return (long)((object)left) < (long)((object)right);
			}
			if (typeof(TKey) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) < (UIntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) < (IntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			if (typeof(TKey) == typeof(double))
			{
				return (double)((object)left) < (double)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00089830 File Offset: 0x00087A30
		[MethodImpl(256)]
		private static bool GreaterThan(ref TKey left, ref TKey right)
		{
			if (typeof(TKey) == typeof(byte))
			{
				return (byte)((object)left) > (byte)((object)right);
			}
			if (typeof(TKey) == typeof(sbyte))
			{
				return (sbyte)((object)left) > (sbyte)((object)right);
			}
			if (typeof(TKey) == typeof(ushort))
			{
				return (ushort)((object)left) > (ushort)((object)right);
			}
			if (typeof(TKey) == typeof(short))
			{
				return (short)((object)left) > (short)((object)right);
			}
			if (typeof(TKey) == typeof(uint))
			{
				return (uint)((object)left) > (uint)((object)right);
			}
			if (typeof(TKey) == typeof(int))
			{
				return (int)((object)left) > (int)((object)right);
			}
			if (typeof(TKey) == typeof(ulong))
			{
				return (ulong)((object)left) > (ulong)((object)right);
			}
			if (typeof(TKey) == typeof(long))
			{
				return (long)((object)left) > (long)((object)right);
			}
			if (typeof(TKey) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) > (UIntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) > (IntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			if (typeof(TKey) == typeof(double))
			{
				return (double)((object)left) > (double)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			return left.CompareTo(right) > 0;
		}
	}
}
