using System;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000044 RID: 68
	internal abstract class AsyncEnumerableSorter<TElement>
	{
		// Token: 0x06000306 RID: 774
		internal abstract UniTask ComputeKeysAsync(TElement[] elements, int count);

		// Token: 0x06000307 RID: 775
		internal abstract int CompareKeys(int index1, int index2);

		// Token: 0x06000308 RID: 776 RVA: 0x0000B9BC File Offset: 0x00009BBC
		internal async UniTask<int[]> SortAsync(TElement[] elements, int count)
		{
			await this.ComputeKeysAsync(elements, count);
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = i;
			}
			this.QuickSort(array, 0, count - 1);
			return array;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BA10 File Offset: 0x00009C10
		private void QuickSort(int[] map, int left, int right)
		{
			do
			{
				int num = left;
				int num2 = right;
				int num3 = map[num + (num2 - num >> 1)];
				do
				{
					if (num < map.Length)
					{
						if (this.CompareKeys(num3, map[num]) > 0)
						{
							num++;
							continue;
						}
					}
					while (num2 >= 0 && this.CompareKeys(num3, map[num2]) < 0)
					{
						num2--;
					}
					if (num > num2)
					{
						break;
					}
					if (num < num2)
					{
						int num4 = map[num];
						map[num] = map[num2];
						map[num2] = num4;
					}
					num++;
					num2--;
				}
				while (num <= num2);
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(map, left, num2);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(map, num, right);
					}
					right = num2;
				}
			}
			while (left < right);
		}
	}
}
