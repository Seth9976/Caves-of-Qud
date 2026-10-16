using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200093D RID: 2365
	public abstract class PooledEvent<T> : CachedEvent, IDisposable where T : PooledEvent<T>, new()
	{
		// Token: 0x06005B43 RID: 23363 RVA: 0x001B95CC File Offset: 0x001B77CC
		public PooledEvent()
		{
			this.ID = PooledEvent<T>.ID;
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x001B95DF File Offset: 0x001B77DF
		public static int CountPool()
		{
			return PooledEvent<T>.Pool.Count;
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x001B95EB File Offset: 0x001B77EB
		public static void ResetPool()
		{
			while (PooledEvent<T>.PoolCounter > 0)
			{
				PooledEvent<T>.Pool[--PooledEvent<T>.PoolCounter].Reset();
			}
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x001B9618 File Offset: 0x001B7818
		public static void ResetTo(ref T E)
		{
			MinEvent.ResetTo<T>(E, PooledEvent<T>.Pool, ref PooledEvent<T>.PoolCounter);
			E = default(T);
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x001B9638 File Offset: 0x001B7838
		public void Dispose()
		{
			if (this.Disposed)
			{
				return;
			}
			while (PooledEvent<T>.PoolCounter > 0)
			{
				T t = PooledEvent<T>.Pool[--PooledEvent<T>.PoolCounter];
				t.Reset();
				t.Disposed = true;
				if (this == t)
				{
					break;
				}
			}
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x001B968E File Offset: 0x001B788E
		public static T FromPool()
		{
			T t = MinEvent.FromPool<T>(PooledEvent<T>.Pool, ref PooledEvent<T>.PoolCounter, 8192);
			t.Disposed = false;
			return t;
		}

		// Token: 0x04002D0E RID: 11534
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(T), null, new MinEvent.EventPoolCount(PooledEvent<T>.CountPool), new MinEvent.EventPoolReset(PooledEvent<T>.ResetPool));

		// Token: 0x04002D0F RID: 11535
		protected static readonly List<T> Pool = new List<T>();

		// Token: 0x04002D10 RID: 11536
		protected static int PoolCounter;

		// Token: 0x04002D11 RID: 11537
		protected bool Disposed;
	}
}
