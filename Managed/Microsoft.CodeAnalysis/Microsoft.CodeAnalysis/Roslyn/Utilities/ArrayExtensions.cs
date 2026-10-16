using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ArrayExtensions
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004214 File Offset: 0x00002414
		internal static T[] Copy<[Nullable(2)] T>(this T[] array, int start, int length)
		{
			if (start + length > array.Length)
			{
				length = array.Length - start;
			}
			T[] array2 = new T[length];
			Array.Copy(array, start, array2, 0, length);
			return array2;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004244 File Offset: 0x00002444
		internal static T[] InsertAt<[Nullable(2)] T>(this T[] array, int position, T item)
		{
			T[] array2 = new T[array.Length + 1];
			if (position > 0)
			{
				Array.Copy(array, array2, position);
			}
			if (position < array.Length)
			{
				Array.Copy(array, position, array2, position + 1, array.Length - position);
			}
			array2[position] = item;
			return array2;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004287 File Offset: 0x00002487
		internal static T[] Append<[Nullable(2)] T>(this T[] array, T item)
		{
			return array.InsertAt(array.Length, item);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004294 File Offset: 0x00002494
		internal static T[] InsertAt<[Nullable(2)] T>(this T[] array, int position, T[] items)
		{
			T[] array2 = new T[array.Length + items.Length];
			if (position > 0)
			{
				Array.Copy(array, array2, position);
			}
			if (position < array.Length)
			{
				Array.Copy(array, position, array2, position + items.Length, array.Length - position);
			}
			items.CopyTo(array2, position);
			return array2;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000042DB File Offset: 0x000024DB
		internal static T[] Append<[Nullable(2)] T>(this T[] array, T[] items)
		{
			return array.InsertAt<T>(array.Length, items);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000042E7 File Offset: 0x000024E7
		internal static T[] RemoveAt<[Nullable(2)] T>(this T[] array, int position)
		{
			return array.RemoveAt<T>(position, 1);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000042F4 File Offset: 0x000024F4
		internal static T[] RemoveAt<[Nullable(2)] T>(this T[] array, int position, int length)
		{
			if (position + length > array.Length)
			{
				length = array.Length - position;
			}
			T[] array2 = new T[array.Length - length];
			if (position > 0)
			{
				Array.Copy(array, array2, position);
			}
			if (position < array2.Length)
			{
				Array.Copy(array, position + length, array2, position, array2.Length - position);
			}
			return array2;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004340 File Offset: 0x00002540
		internal static T[] ReplaceAt<[Nullable(2)] T>(this T[] array, int position, T item)
		{
			T[] array2 = new T[array.Length];
			Array.Copy(array, array2, array.Length);
			array2[position] = item;
			return array2;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004369 File Offset: 0x00002569
		internal static T[] ReplaceAt<[Nullable(2)] T>(this T[] array, int position, int length, T[] items)
		{
			return array.RemoveAt<T>(position, length).InsertAt<T>(position, items);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000437A File Offset: 0x0000257A
		internal static void ReverseContents<[Nullable(2)] T>(this T[] array)
		{
			array.ReverseContents<T>(0, array.Length);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004388 File Offset: 0x00002588
		internal static void ReverseContents<[Nullable(2)] T>(this T[] array, int start, int count)
		{
			int num = start + count - 1;
			int i = start;
			int num2 = num;
			while (i < num2)
			{
				T t = array[i];
				array[i] = array[num2];
				array[num2] = t;
				i++;
				num2--;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000043CC File Offset: 0x000025CC
		internal static int BinarySearch(this int[] array, int value)
		{
			int i = 0;
			int num = array.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = array[num2];
				if (num3 == value)
				{
					return num2;
				}
				if (num3 > value)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return ~i;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000440C File Offset: 0x0000260C
		internal static int BinarySearchUpperBound(this int[] array, int value)
		{
			int i = 0;
			int num = array.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				if (array[num2] > value)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return i;
		}
	}
}
