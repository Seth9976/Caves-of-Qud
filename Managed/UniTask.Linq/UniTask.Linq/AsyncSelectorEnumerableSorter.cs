using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000046 RID: 70
	internal class AsyncSelectorEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000BB8A File Offset: 0x00009D8A
		internal AsyncSelectorEnumerableSorter(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TElement> next)
		{
			this.keySelector = keySelector;
			this.comparer = comparer;
			this.descending = descending;
			this.next = next;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		internal override async UniTask ComputeKeysAsync(TElement[] elements, int count)
		{
			this.keys = new TKey[count];
			for (int i = 0; i < count; i++)
			{
				TKey[] array = this.keys;
				int num = i;
				TKey tkey = await this.keySelector(elements[i]);
				array[num] = tkey;
				array = null;
			}
			if (this.next != null)
			{
				await this.next.ComputeKeysAsync(elements, count);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000BC04 File Offset: 0x00009E04
		internal override int CompareKeys(int index1, int index2)
		{
			int num = this.comparer.Compare(this.keys[index1], this.keys[index2]);
			if (num == 0)
			{
				if (this.next == null)
				{
					return index1 - index2;
				}
				return this.next.CompareKeys(index1, index2);
			}
			else
			{
				if (!this.descending)
				{
					return num;
				}
				return -num;
			}
		}

		// Token: 0x0400010B RID: 267
		private readonly Func<TElement, UniTask<TKey>> keySelector;

		// Token: 0x0400010C RID: 268
		private readonly IComparer<TKey> comparer;

		// Token: 0x0400010D RID: 269
		private readonly bool descending;

		// Token: 0x0400010E RID: 270
		private readonly AsyncEnumerableSorter<TElement> next;

		// Token: 0x0400010F RID: 271
		private TKey[] keys;
	}
}
