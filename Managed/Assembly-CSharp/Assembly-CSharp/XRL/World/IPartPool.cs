using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x0200096A RID: 2410
	public class IPartPool
	{
		// Token: 0x06006393 RID: 25491 RVA: 0x001E8B32 File Offset: 0x001E6D32
		public IPartPool()
		{
			this.InternalPool = new RingDeque<IPart>();
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x001E8B45 File Offset: 0x001E6D45
		public IPartPool(int Capacity)
		{
			this.InternalPool = new RingDeque<IPart>(Capacity);
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06006395 RID: 25493 RVA: 0x001E8B59 File Offset: 0x001E6D59
		public int Count
		{
			get
			{
				return this.InternalPool.Count;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x001E8B66 File Offset: 0x001E6D66
		public int Capacity
		{
			get
			{
				return this.InternalPool.Capacity;
			}
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x001E8B73 File Offset: 0x001E6D73
		public bool TryGet(out IPart Part)
		{
			return this.InternalPool.TryDequeue(out Part);
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x001E8B81 File Offset: 0x001E6D81
		public void Return(IPart Part)
		{
			this.InternalPool.Enqueue(Part);
		}

		// Token: 0x04002ED5 RID: 11989
		private RingDeque<IPart> InternalPool;
	}
}
