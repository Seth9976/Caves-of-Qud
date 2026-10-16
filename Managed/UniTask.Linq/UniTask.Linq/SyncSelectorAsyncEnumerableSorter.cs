using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x02000045 RID: 69
	internal class SyncSelectorAsyncEnumerableSorter<TElement, TKey> : AsyncEnumerableSorter<TElement>
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		internal SyncSelectorAsyncEnumerableSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, AsyncEnumerableSorter<TElement> next)
		{
			this.keySelector = keySelector;
			this.comparer = comparer;
			this.descending = descending;
			this.next = next;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000BADC File Offset: 0x00009CDC
		internal override async UniTask ComputeKeysAsync(TElement[] elements, int count)
		{
			this.keys = new TKey[count];
			for (int i = 0; i < count; i++)
			{
				this.keys[i] = this.keySelector(elements[i]);
			}
			if (this.next != null)
			{
				await this.next.ComputeKeysAsync(elements, count);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000BB30 File Offset: 0x00009D30
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

		// Token: 0x04000106 RID: 262
		private readonly Func<TElement, TKey> keySelector;

		// Token: 0x04000107 RID: 263
		private readonly IComparer<TKey> comparer;

		// Token: 0x04000108 RID: 264
		private readonly bool descending;

		// Token: 0x04000109 RID: 265
		private readonly AsyncEnumerableSorter<TElement> next;

		// Token: 0x0400010A RID: 266
		private TKey[] keys;
	}
}
