using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AA RID: 1194
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SegmentedGenericArraySortHelper<[Nullable(0)] T> where T : IComparable<T>
	{
		// Token: 0x06003004 RID: 12292 RVA: 0x00087CA8 File Offset: 0x00085EA8
		public static void Sort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					if (keys.Length > 1)
					{
						if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(float))
						{
							int num = SegmentedArraySortUtils.MoveNansToFront<T, byte>(keys, default(Span<byte>));
							if (num == keys.Length)
							{
								return;
							}
							keys = keys.Slice(num);
						}
						SegmentedGenericArraySortHelper<T>.IntroSort(keys, 2 * (SegmentedArraySortUtils.Log2((uint)keys.Length) + 1));
					}
				}
				else
				{
					SegmentedArraySortHelper<T>.IntrospectiveSort(keys, new Comparison<T>(comparer.Compare));
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

		// Token: 0x06003005 RID: 12293 RVA: 0x00087DA4 File Offset: 0x00085FA4
		public static int BinarySearch([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			int num;
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					num = SegmentedGenericArraySortHelper<T>.BinarySearch(array, index, length, value);
				}
				else
				{
					num = SegmentedArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
				}
			}
			catch (Exception ex)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x00087DF8 File Offset: 0x00085FF8
		private unsafe static int BinarySearch([Nullable(new byte[] { 0, 1 })] SegmentedArray<T> array, int index, int length, T value)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3;
				if (*array[num2] == null)
				{
					num3 = ((value == null) ? 0 : (-1));
				}
				else
				{
					num3 = array[num2].CompareTo(value);
				}
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

		// Token: 0x06003007 RID: 12295 RVA: 0x00087E68 File Offset: 0x00086068
		[MethodImpl(256)]
		private static void SwapIfGreater(ref T i, ref T j)
		{
			if (i != null && SegmentedGenericArraySortHelper<T>.GreaterThan(ref i, ref j))
			{
				SegmentedGenericArraySortHelper<T>.Swap(ref i, ref j);
			}
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00087E88 File Offset: 0x00086088
		[MethodImpl(256)]
		private static void Swap(ref T i, ref T j)
		{
			T t = i;
			i = j;
			j = t;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00087EB0 File Offset: 0x000860B0
		private static void IntroSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, int depthLimit)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						SegmentedGenericArraySortHelper<T>.SwapIfGreater(keys[0], keys[1]);
						return;
					}
					if (i == 3)
					{
						ref T ptr = ref keys[2];
						ref T ptr2 = ref keys[1];
						ref T ptr3 = ref keys[0];
						SegmentedGenericArraySortHelper<T>.SwapIfGreater(ref ptr3, ref ptr2);
						SegmentedGenericArraySortHelper<T>.SwapIfGreater(ref ptr3, ref ptr);
						SegmentedGenericArraySortHelper<T>.SwapIfGreater(ref ptr2, ref ptr);
						return;
					}
					SegmentedGenericArraySortHelper<T>.InsertionSort(keys.Slice(0, i));
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						SegmentedGenericArraySortHelper<T>.HeapSort(keys.Slice(0, i));
						return;
					}
					depthLimit--;
					int num = SegmentedGenericArraySortHelper<T>.PickPivotAndPartition(keys.Slice(0, i));
					SegmentedGenericArraySortHelper<T>.IntroSort(keys.Slice(num + 1, i - (num + 1)), depthLimit);
					i = num;
				}
			}
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00087F70 File Offset: 0x00086170
		private unsafe static int PickPivotAndPartition([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys)
		{
			int num = 0;
			int num2 = keys.Length - 1;
			int num3 = keys.Length - 1 >> 1;
			SegmentedGenericArraySortHelper<T>.SwapIfGreater(keys[num], keys[num3]);
			SegmentedGenericArraySortHelper<T>.SwapIfGreater(keys[num], keys[num2]);
			SegmentedGenericArraySortHelper<T>.SwapIfGreater(keys[num3], keys[num2]);
			int num4 = keys.Length - 2;
			T t = *keys[num3];
			SegmentedGenericArraySortHelper<T>.Swap(keys[num3], keys[num4]);
			int i = num;
			int j = num4;
			while (i < j)
			{
				if (t == null)
				{
					while (i < num4 && *keys[++i] == null)
					{
					}
					while (j > num)
					{
						if (*keys[--j] == null)
						{
							break;
						}
					}
				}
				else
				{
					while (i < num4 && SegmentedGenericArraySortHelper<T>.GreaterThan(ref t, keys[++i]))
					{
					}
					while (j > num && SegmentedGenericArraySortHelper<T>.LessThan(ref t, keys[--j]))
					{
					}
				}
				if (i >= j)
				{
					break;
				}
				SegmentedGenericArraySortHelper<T>.Swap(keys[i], keys[j]);
			}
			if (i != num4)
			{
				SegmentedGenericArraySortHelper<T>.Swap(keys[i], keys[num4]);
			}
			return i;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000880D4 File Offset: 0x000862D4
		private static void HeapSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				SegmentedGenericArraySortHelper<T>.DownHeap(keys, i, length, 0);
			}
			for (int j = length; j > 1; j--)
			{
				SegmentedGenericArraySortHelper<T>.Swap(keys[0], keys[j - 1]);
				SegmentedGenericArraySortHelper<T>.DownHeap(keys, 1, j - 1, 0);
			}
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00088130 File Offset: 0x00086330
		private unsafe static void DownHeap([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys, int i, int n, int lo)
		{
			T t = *keys[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && (*keys[lo + num - 1] == null || SegmentedGenericArraySortHelper<T>.LessThan(keys[lo + num - 1], keys[lo + num])))
				{
					num++;
				}
				if (*keys[lo + num - 1] == null || !SegmentedGenericArraySortHelper<T>.LessThan(ref t, keys[lo + num - 1]))
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = t;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00088204 File Offset: 0x00086404
		private unsafe static void InsertionSort([Nullable(new byte[] { 0, 1 })] SegmentedArraySegment<T> keys)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				T t = *keys[i + 1];
				int num = i;
				while (num >= 0 && (t == null || SegmentedGenericArraySortHelper<T>.LessThan(ref t, keys[num])))
				{
					*keys[num + 1] = *keys[num];
					num--;
				}
				*keys[num + 1] = t;
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00088288 File Offset: 0x00086488
		[MethodImpl(256)]
		private static bool LessThan(ref T left, ref T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) < (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) < (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) < (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) < (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) < (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) < (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) < (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) < (long)((object)right);
			}
			if (typeof(T) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) < (UIntPtr)((object)right);
			}
			if (typeof(T) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) < (IntPtr)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) < (double)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			return left.CompareTo(right) < 0;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000885FC File Offset: 0x000867FC
		[MethodImpl(256)]
		private static bool GreaterThan(ref T left, ref T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) > (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) > (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) > (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) > (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) > (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) > (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) > (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) > (long)((object)right);
			}
			if (typeof(T) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) > (UIntPtr)((object)right);
			}
			if (typeof(T) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) > (IntPtr)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) > (double)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			return left.CompareTo(right) > 0;
		}
	}
}
