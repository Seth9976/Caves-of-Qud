using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200062E RID: 1582
	internal class GPUBufferAllocator
	{
		// Token: 0x06002E95 RID: 11925 RVA: 0x000A854C File Offset: 0x000A674C
		public GPUBufferAllocator(uint maxSize)
		{
			this.m_Low = new BestFitAllocator(maxSize);
			this.m_High = new BestFitAllocator(maxSize);
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000A8570 File Offset: 0x000A6770
		public Alloc Allocate(uint size, bool shortLived)
		{
			bool flag = !shortLived;
			Alloc alloc;
			if (flag)
			{
				alloc = this.m_Low.Allocate(size);
			}
			else
			{
				alloc = this.m_High.Allocate(size);
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
			}
			alloc.shortLived = shortLived;
			bool flag2 = this.HighLowCollide() && alloc.size > 0U;
			Alloc alloc2;
			if (flag2)
			{
				this.Free(alloc);
				alloc2 = default(Alloc);
			}
			else
			{
				alloc2 = alloc;
			}
			return alloc2;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000A8604 File Offset: 0x000A6804
		public void Free(Alloc alloc)
		{
			bool flag = !alloc.shortLived;
			if (flag)
			{
				this.m_Low.Free(alloc);
			}
			else
			{
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
				this.m_High.Free(alloc);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000A8660 File Offset: 0x000A6860
		public bool isEmpty
		{
			get
			{
				return this.m_Low.highWatermark == 0U && this.m_High.highWatermark == 0U;
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000A8690 File Offset: 0x000A6890
		public HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			heapStatistics.subAllocators = new HeapStatistics[]
			{
				this.m_Low.GatherStatistics(),
				this.m_High.GatherStatistics()
			};
			heapStatistics.largestAvailableBlock = uint.MaxValue;
			for (int i = 0; i < 2; i++)
			{
				heapStatistics.numAllocs += heapStatistics.subAllocators[i].numAllocs;
				heapStatistics.totalSize = Math.Max(heapStatistics.totalSize, heapStatistics.subAllocators[i].totalSize);
				heapStatistics.allocatedSize += heapStatistics.subAllocators[i].allocatedSize;
				heapStatistics.largestAvailableBlock = Math.Min(heapStatistics.largestAvailableBlock, heapStatistics.subAllocators[i].largestAvailableBlock);
				heapStatistics.availableBlocksCount += heapStatistics.subAllocators[i].availableBlocksCount;
				heapStatistics.blockCount += heapStatistics.subAllocators[i].blockCount;
				heapStatistics.highWatermark = Math.Max(heapStatistics.highWatermark, heapStatistics.subAllocators[i].highWatermark);
				heapStatistics.fragmentation = Math.Max(heapStatistics.fragmentation, heapStatistics.subAllocators[i].fragmentation);
			}
			heapStatistics.freeSize = heapStatistics.totalSize - heapStatistics.allocatedSize;
			return heapStatistics;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000A880C File Offset: 0x000A6A0C
		private bool HighLowCollide()
		{
			return this.m_Low.highWatermark + this.m_High.highWatermark > this.m_Low.totalSize;
		}

		// Token: 0x04001575 RID: 5493
		private BestFitAllocator m_Low;

		// Token: 0x04001576 RID: 5494
		private BestFitAllocator m_High;
	}
}
