using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200062B RID: 1579
	internal class BestFitAllocator
	{
		// Token: 0x06002E87 RID: 11911 RVA: 0x000A7EC0 File Offset: 0x000A60C0
		public BestFitAllocator(uint size)
		{
			this.totalSize = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000A7F0D File Offset: 0x000A610D
		public uint totalSize { get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000A7F18 File Offset: 0x000A6118
		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000A7F30 File Offset: 0x000A6130
		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc alloc;
			if (flag)
			{
				alloc = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				alloc = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return alloc;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000A8070 File Offset: 0x000A6270
		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0U;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block block2 = this.m_FirstAvailableBlock;
				BestFitAllocator.Block block3 = null;
				while (block2 != null && block2.start < block.start)
				{
					block3 = block2;
					block2 = block2.nextAvailable;
				}
				bool flag4 = block3 == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = block3;
					block.nextAvailable = block3.nextAvailable;
					block3.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000A822C File Offset: 0x000A642C
		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000A82DC File Offset: 0x000A64DC
		internal HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			for (BestFitAllocator.Block block = this.m_FirstBlock; block != null; block = block.next)
			{
				bool allocated = block.allocated;
				if (allocated)
				{
					heapStatistics.numAllocs += 1U;
					heapStatistics.allocatedSize += block.size;
				}
				else
				{
					heapStatistics.freeSize += block.size;
					heapStatistics.availableBlocksCount += 1U;
					heapStatistics.largestAvailableBlock = Math.Max(heapStatistics.largestAvailableBlock, block.size);
				}
				heapStatistics.blockCount += 1U;
			}
			heapStatistics.totalSize = this.totalSize;
			heapStatistics.highWatermark = this.m_HighWatermark;
			bool flag = heapStatistics.freeSize > 0U;
			if (flag)
			{
				heapStatistics.fragmentation = (float)((heapStatistics.freeSize - heapStatistics.largestAvailableBlock) / heapStatistics.freeSize) * 100f;
			}
			return heapStatistics;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000A83D0 File Offset: 0x000A65D0
		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block block = this.m_FirstAvailableBlock;
			BestFitAllocator.Block block2 = null;
			uint num = uint.MaxValue;
			while (block != null)
			{
				bool flag = block.size >= size && num > block.size;
				if (flag)
				{
					block2 = block;
					num = block.size;
				}
				block = block.nextAvailable;
			}
			return block2;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000A842C File Offset: 0x000A662C
		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block block2 = this.m_BlockPool.Get();
			block2.next = block.next;
			block2.nextAvailable = block.nextAvailable;
			block2.prev = block;
			block2.prevAvailable = block;
			block2.start = block.start + size;
			block2.end = block.end;
			bool flag = block2.next != null;
			if (flag)
			{
				block2.next.prev = block2;
			}
			bool flag2 = block2.nextAvailable != null;
			if (flag2)
			{
				block2.nextAvailable.prevAvailable = block2;
			}
			block.next = block2;
			block.nextAvailable = block2;
			block.end = block2.start;
		}

		// Token: 0x0400156A RID: 5482
		private BestFitAllocator.Block m_FirstBlock;

		// Token: 0x0400156B RID: 5483
		private BestFitAllocator.Block m_FirstAvailableBlock;

		// Token: 0x0400156C RID: 5484
		private BestFitAllocator.BlockPool m_BlockPool = new BestFitAllocator.BlockPool();

		// Token: 0x0400156D RID: 5485
		private uint m_HighWatermark;

		// Token: 0x0200062C RID: 1580
		private class BlockPool : LinkedPool<BestFitAllocator.Block>
		{
			// Token: 0x06002E90 RID: 11920 RVA: 0x000A84E4 File Offset: 0x000A66E4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static BestFitAllocator.Block CreateBlock()
			{
				return new BestFitAllocator.Block();
			}

			// Token: 0x06002E91 RID: 11921 RVA: 0x00002152 File Offset: 0x00000352
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void ResetBlock(BestFitAllocator.Block block)
			{
			}

			// Token: 0x06002E92 RID: 11922 RVA: 0x000A84FB File Offset: 0x000A66FB
			public BlockPool()
				: base(new Func<BestFitAllocator.Block>(BestFitAllocator.BlockPool.CreateBlock), new Action<BestFitAllocator.Block>(BestFitAllocator.BlockPool.ResetBlock), 10000)
			{
			}
		}

		// Token: 0x0200062D RID: 1581
		private class Block : LinkedPoolItem<BestFitAllocator.Block>
		{
			// Token: 0x17000B4D RID: 2893
			// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000A8524 File Offset: 0x000A6724
			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}

			// Token: 0x0400156E RID: 5486
			public uint start;

			// Token: 0x0400156F RID: 5487
			public uint end;

			// Token: 0x04001570 RID: 5488
			public BestFitAllocator.Block prev;

			// Token: 0x04001571 RID: 5489
			public BestFitAllocator.Block next;

			// Token: 0x04001572 RID: 5490
			public BestFitAllocator.Block prevAvailable;

			// Token: 0x04001573 RID: 5491
			public BestFitAllocator.Block nextAvailable;

			// Token: 0x04001574 RID: 5492
			public bool allocated;
		}
	}
}
