using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Web.Caching
{
	// Token: 0x0200067E RID: 1662
	internal sealed class CacheItemPriorityQueue
	{
		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x0600470D RID: 18189 RVA: 0x000C5D1D File Offset: 0x000C3F1D
		public int Count
		{
			get
			{
				return this.heapCount;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x0600470E RID: 18190 RVA: 0x000C5D25 File Offset: 0x000C3F25
		public int Size
		{
			get
			{
				return this.heapSize;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x0600470F RID: 18191 RVA: 0x000C5D2D File Offset: 0x000C3F2D
		public CacheItem[] Heap
		{
			get
			{
				return this.heap;
			}
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x000C5D35 File Offset: 0x000C3F35
		public CacheItemPriorityQueue()
		{
			this.queueLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x000C5D48 File Offset: 0x000C3F48
		private void ResizeHeap(int newSize)
		{
			CacheItem[] array = this.heap;
			Array.Resize<CacheItem>(ref this.heap, newSize);
			this.heapSize = newSize;
			if (array != null)
			{
				((IList)array).Clear();
			}
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x000C5D7C File Offset: 0x000C3F7C
		private CacheItem[] GetHeapWithGrow()
		{
			if (this.heap == null)
			{
				this.heap = new CacheItem[32];
				this.heapSize = 32;
				this.heapCount = 0;
				return this.heap;
			}
			if (this.heapCount >= this.heapSize)
			{
				this.ResizeHeap(this.heapSize <<= 1);
			}
			return this.heap;
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x000C5DE0 File Offset: 0x000C3FE0
		private CacheItem[] GetHeapWithShrink()
		{
			if (this.heap == null)
			{
				return null;
			}
			if (this.heapSize > 8192)
			{
				int num = this.heapSize >> 1;
				if (this.heapCount < num)
				{
					this.ResizeHeap(num + this.heapCount / 3);
				}
			}
			return this.heap;
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000C5E2C File Offset: 0x000C402C
		public void Enqueue(CacheItem item)
		{
			if (item == null)
			{
				return;
			}
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem[] heapWithGrow = this.GetHeapWithGrow();
				heapWithGrow[this.heapCount] = item;
				if (this.heapCount == 0)
				{
					item.PriorityQueueIndex = 0;
				}
				CacheItem[] array = heapWithGrow;
				int num = this.heapCount;
				this.heapCount = num + 1;
				this.BubbleUp(array, num);
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000C5EA0 File Offset: 0x000C40A0
		public CacheItem Dequeue()
		{
			CacheItem cacheItem;
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem[] heapWithShrink = this.GetHeapWithShrink();
				if (heapWithShrink == null || this.heapCount == 0)
				{
					cacheItem = null;
				}
				else
				{
					CacheItem cacheItem2 = heapWithShrink[0];
					int num = this.heapCount - 1;
					this.heapCount = num;
					int num2 = num;
					heapWithShrink[0] = heapWithShrink[num2];
					heapWithShrink[num2] = null;
					if (this.heapCount > 0)
					{
						this.BubbleDown(heapWithShrink, 0);
					}
					cacheItem = cacheItem2;
				}
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
			return cacheItem;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x000C5F20 File Offset: 0x000C4120
		public bool Update(CacheItem item)
		{
			if (item == null || item.PriorityQueueIndex <= 0 || item.PriorityQueueIndex >= this.heapCount - 1)
			{
				return false;
			}
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem cacheItem = this.heap[item.PriorityQueueIndex];
				if (cacheItem == null || string.Compare(cacheItem.Key, item.Key, StringComparison.Ordinal) != 0)
				{
					return false;
				}
				int priorityQueueIndex = item.PriorityQueueIndex;
				int num = this.BubbleUp(this.heap, priorityQueueIndex);
				if (num > -1 && num >= priorityQueueIndex)
				{
					this.BubbleDown(this.heap, num);
				}
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
			return true;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x000C5FCC File Offset: 0x000C41CC
		public CacheItem Peek()
		{
			CacheItem cacheItem;
			try
			{
				this.queueLock.EnterReadLock();
				if (this.heap == null || this.heapCount == 0)
				{
					cacheItem = null;
				}
				else
				{
					cacheItem = this.heap[0];
				}
			}
			finally
			{
				this.queueLock.ExitReadLock();
			}
			return cacheItem;
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000C6020 File Offset: 0x000C4220
		private int BubbleDown(CacheItem[] heap, int startIndex)
		{
			int num = startIndex;
			int num2 = startIndex + 1;
			int num3 = startIndex + 2;
			CacheItem cacheItem = heap[num];
			int num4 = ((num3 < this.heapCount && heap[num3].ExpiresAt < heap[num2].ExpiresAt) ? 2 : 1);
			for (;;)
			{
				int num5 = num;
				num2 = (num << 1) + 1;
				num3 = num2 + 1;
				if (this.heapCount > num2 && heap[num].ExpiresAt > heap[num2].ExpiresAt)
				{
					num = num2;
				}
				if (this.heapCount > num3 && heap[num].ExpiresAt > heap[num3].ExpiresAt)
				{
					num = num3;
				}
				if (num == num5)
				{
					break;
				}
				CacheItem cacheItem2 = heap[num];
				heap[num] = heap[num5];
				heap[num].PriorityQueueIndex = num;
				heap[num5] = cacheItem2;
				cacheItem2.PriorityQueueIndex = num5;
			}
			cacheItem.PriorityQueueIndex = num;
			return num;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x000C60D8 File Offset: 0x000C42D8
		private int BubbleUp(CacheItem[] heap, int startIndex)
		{
			if (this.heapCount <= 1)
			{
				return -1;
			}
			int num = this.heapCount - 1;
			if (startIndex < 0 || startIndex > num)
			{
				return -1;
			}
			int i = startIndex;
			int num2 = i - 1 >> 1;
			CacheItem cacheItem = heap[i];
			while (i > 0)
			{
				CacheItem cacheItem2 = heap[num2];
				if (heap[i].ExpiresAt >= cacheItem2.ExpiresAt)
				{
					break;
				}
				heap[i] = cacheItem2;
				cacheItem2.PriorityQueueIndex = i;
				i = num2;
				num2 = i - 1 >> 1;
			}
			heap[i] = cacheItem;
			cacheItem.PriorityQueueIndex = i;
			return i;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DEBUG")]
		private void InitDebugMode()
		{
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DEBUG")]
		private void AddSequenceEntry(CacheItem item, EDSequenceEntryType type)
		{
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DEBUG")]
		public void OnItemDisable(CacheItem i)
		{
		}

		// Token: 0x04002587 RID: 9607
		private const int INITIAL_HEAP_SIZE = 32;

		// Token: 0x04002588 RID: 9608
		private const int HEAP_RESIZE_THRESHOLD = 8192;

		// Token: 0x04002589 RID: 9609
		private CacheItem[] heap;

		// Token: 0x0400258A RID: 9610
		private int heapSize;

		// Token: 0x0400258B RID: 9611
		private int heapCount;

		// Token: 0x0400258C RID: 9612
		private ReaderWriterLockSlim queueLock;
	}
}
