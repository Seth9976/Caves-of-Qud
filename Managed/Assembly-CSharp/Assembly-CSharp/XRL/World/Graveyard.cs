using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x0200097B RID: 2427
	public class Graveyard
	{
		// Token: 0x060066BC RID: 26300 RVA: 0x001FF600 File Offset: 0x001FD800
		public static Graveyard Get()
		{
			Graveyard graveyard;
			if (!Graveyard.Pool.TryDequeue(out graveyard))
			{
				graveyard = new Graveyard(256);
			}
			return graveyard;
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x001FF627 File Offset: 0x001FD827
		public static void Return(Graveyard Graveyard)
		{
			Graveyard.ReleaseObjects();
			Graveyard.Pool.Enqueue(Graveyard);
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x001FF63A File Offset: 0x001FD83A
		public static void Return(ref Graveyard Graveyard)
		{
			Graveyard.Return(Graveyard);
			Graveyard = null;
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x001FF646 File Offset: 0x001FD846
		public Graveyard(int Capacity)
			: this(Capacity, Capacity)
		{
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x001FF650 File Offset: 0x001FD850
		public Graveyard(int Capacity, int MaxCount)
		{
			this.Objects = new RingDeque<GameObject>(Capacity);
			this.MaxCount = MaxCount;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x001FF66C File Offset: 0x001FD86C
		public void ReleaseObjects()
		{
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				this.Objects[i].Pool();
			}
			this.Objects.Clear();
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x001FF6B0 File Offset: 0x001FD8B0
		public void ReleaseObjects(int Count)
		{
			int count = this.Objects.Count;
			if (Count >= count)
			{
				this.ReleaseObjects();
				return;
			}
			for (int i = 0; i < Count; i++)
			{
				this.Objects.Dequeue().Pool();
			}
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001FF6F0 File Offset: 0x001FD8F0
		public void Add(GameObject Object)
		{
			if (Object.IsInvalid())
			{
				return;
			}
			if (this.Objects.Count >= this.MaxCount)
			{
				this.Objects.Dequeue().Pool();
			}
			Object.Flags |= 4;
			this.Objects.Enqueue(Object);
		}

		// Token: 0x04002F57 RID: 12119
		private static RingDeque<Graveyard> Pool = new RingDeque<Graveyard>(4);

		// Token: 0x04002F58 RID: 12120
		public RingDeque<GameObject> Objects;

		// Token: 0x04002F59 RID: 12121
		public int MaxCount;
	}
}
