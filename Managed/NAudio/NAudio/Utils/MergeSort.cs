using System;
using System.Collections.Generic;

namespace NAudio.Utils
{
	// Token: 0x02000114 RID: 276
	internal class MergeSort
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000132E0 File Offset: 0x000114E0
		private static void Sort<T>(IList<T> list, int lowIndex, int highIndex, IComparer<T> comparer)
		{
			if (lowIndex >= highIndex)
			{
				return;
			}
			int num = (lowIndex + highIndex) / 2;
			MergeSort.Sort<T>(list, lowIndex, num, comparer);
			MergeSort.Sort<T>(list, num + 1, highIndex, comparer);
			int num2 = num;
			int num3 = num + 1;
			while (lowIndex <= num2 && num3 <= highIndex)
			{
				if (comparer.Compare(list[lowIndex], list[num3]) <= 0)
				{
					lowIndex++;
				}
				else
				{
					T t = list[num3];
					for (int i = num3 - 1; i >= lowIndex; i--)
					{
						list[i + 1] = list[i];
					}
					list[lowIndex] = t;
					lowIndex++;
					num2++;
					num3++;
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001337B File Offset: 0x0001157B
		public static void Sort<T>(IList<T> list) where T : IComparable<T>
		{
			MergeSort.Sort<T>(list, 0, list.Count - 1, Comparer<T>.Default);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00013391 File Offset: 0x00011591
		public static void Sort<T>(IList<T> list, IComparer<T> comparer)
		{
			MergeSort.Sort<T>(list, 0, list.Count - 1, comparer);
		}
	}
}
