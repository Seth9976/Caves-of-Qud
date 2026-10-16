using System;
using System.Collections.Generic;

namespace Mono
{
	// Token: 0x020001A8 RID: 424
	internal class MergeSort<T>
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00017C22 File Offset: 0x00015E22
		private MergeSort(T[] elements, IComparer<T> comparer)
		{
			this.elements = elements;
			this.buffer = new T[elements.Length];
			Array.Copy(this.elements, this.buffer, elements.Length);
			this.comparer = comparer;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00017C5A File Offset: 0x00015E5A
		public static void Sort(T[] source, IComparer<T> comparer)
		{
			MergeSort<T>.Sort(source, 0, source.Length, comparer);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017C67 File Offset: 0x00015E67
		public static void Sort(T[] source, int start, int length, IComparer<T> comparer)
		{
			new MergeSort<T>(source, comparer).Sort(start, length);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017C77 File Offset: 0x00015E77
		private void Sort(int start, int length)
		{
			this.TopDownSplitMerge(this.buffer, this.elements, start, length);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017C90 File Offset: 0x00015E90
		private void TopDownSplitMerge(T[] a, T[] b, int start, int end)
		{
			if (end - start < 2)
			{
				return;
			}
			int num = (end + start) / 2;
			this.TopDownSplitMerge(b, a, start, num);
			this.TopDownSplitMerge(b, a, num, end);
			this.TopDownMerge(a, b, start, num, end);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00017CD0 File Offset: 0x00015ED0
		private void TopDownMerge(T[] a, T[] b, int start, int middle, int end)
		{
			int num = start;
			int num2 = middle;
			for (int i = start; i < end; i++)
			{
				if (num < middle && (num2 >= end || this.comparer.Compare(a[num], a[num2]) <= 0))
				{
					b[i] = a[num++];
				}
				else
				{
					b[i] = a[num2++];
				}
			}
		}

		// Token: 0x04000237 RID: 567
		private readonly T[] elements;

		// Token: 0x04000238 RID: 568
		private readonly T[] buffer;

		// Token: 0x04000239 RID: 569
		private readonly IComparer<T> comparer;
	}
}
