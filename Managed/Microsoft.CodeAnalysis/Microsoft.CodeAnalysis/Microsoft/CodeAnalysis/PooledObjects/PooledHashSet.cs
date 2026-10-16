using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029E RID: 670
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class PooledHashSet<[Nullable(2)] T> : HashSet<T>
	{
		// Token: 0x06001C62 RID: 7266 RVA: 0x00053AD7 File Offset: 0x00051CD7
		private PooledHashSet(ObjectPool<PooledHashSet<T>> pool, IEqualityComparer<T> equalityComparer)
			: base(equalityComparer)
		{
			this._pool = pool;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00053AE7 File Offset: 0x00051CE7
		public void Free()
		{
			base.Clear();
			ObjectPool<PooledHashSet<T>> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00053B00 File Offset: 0x00051D00
		public static ObjectPool<PooledHashSet<T>> CreatePool(IEqualityComparer<T> equalityComparer)
		{
			ObjectPool<PooledHashSet<T>> pool = null;
			pool = new ObjectPool<PooledHashSet<T>>(() => new PooledHashSet<T>(pool, equalityComparer), 128);
			return pool;
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00053B36 File Offset: 0x00051D36
		public static PooledHashSet<T> GetInstance()
		{
			return PooledHashSet<T>.s_poolInstance.Allocate();
		}

		// Token: 0x04000D4E RID: 3406
		private readonly ObjectPool<PooledHashSet<T>> _pool;

		// Token: 0x04000D4F RID: 3407
		private static readonly ObjectPool<PooledHashSet<T>> s_poolInstance = PooledHashSet<T>.CreatePool(EqualityComparer<T>.Default);
	}
}
