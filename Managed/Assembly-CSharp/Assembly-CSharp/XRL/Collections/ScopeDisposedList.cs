using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace XRL.Collections
{
	// Token: 0x020005D2 RID: 1490
	public class ScopeDisposedList<T> : PooledList<T>
	{
		// Token: 0x06003FD2 RID: 16338 RVA: 0x0013991C File Offset: 0x00137B1C
		public static ScopeDisposedList<T> GetFromPool()
		{
			ScopeDisposedList<T> scopeDisposedList;
			if (ScopeDisposedList<T>.UnusedPool.TryTake(out scopeDisposedList))
			{
				scopeDisposedList.Reuse();
				return scopeDisposedList;
			}
			return new ScopeDisposedList<T>();
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x00139944 File Offset: 0x00137B44
		public static ScopeDisposedList<T> GetFromPoolFilledWith(IEnumerable<T> items)
		{
			ScopeDisposedList<T> fromPool = ScopeDisposedList<T>.GetFromPool();
			fromPool.AddRange(items);
			return fromPool;
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x00139952 File Offset: 0x00137B52
		public static ScopeDisposedList<T> GetFromPoolFilledWith(IReadOnlyCollection<T> items)
		{
			ScopeDisposedList<T> fromPool = ScopeDisposedList<T>.GetFromPool();
			fromPool.AddRange(items);
			return fromPool;
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x00139960 File Offset: 0x00137B60
		public static ScopeDisposedList<T> GetFromPoolFilledWith(IReadOnlyList<T> items)
		{
			ScopeDisposedList<T> fromPool = ScopeDisposedList<T>.GetFromPool();
			fromPool.AddRange(items);
			return fromPool;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0013996E File Offset: 0x00137B6E
		protected void Reuse()
		{
			if (!this.Pooled)
			{
				throw new Exception("Attempt to reuse ScopeDisposedList not in pool");
			}
			this.Pooled = false;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0013998A File Offset: 0x00137B8A
		public override void Dispose()
		{
			if (this.Pooled)
			{
				return;
			}
			base.Dispose();
			this.Pooled = true;
			ScopeDisposedList<T>.UnusedPool.Add(this);
		}

		// Token: 0x04001F4E RID: 8014
		protected static ConcurrentBag<ScopeDisposedList<T>> UnusedPool = new ConcurrentBag<ScopeDisposedList<T>>();

		// Token: 0x04001F4F RID: 8015
		protected bool Pooled;
	}
}
