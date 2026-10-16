using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029D RID: 669
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	internal sealed class PooledDictionary<K, [Nullable(2)] V> : Dictionary<K, V>
	{
		// Token: 0x06001C5B RID: 7259 RVA: 0x00053A39 File Offset: 0x00051C39
		private PooledDictionary(ObjectPool<PooledDictionary<K, V>> pool, IEqualityComparer<K> keyComparer)
			: base(keyComparer)
		{
			this._pool = pool;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00053A49 File Offset: 0x00051C49
		public ImmutableDictionary<K, V> ToImmutableDictionaryAndFree()
		{
			ImmutableDictionary<K, V> immutableDictionary = this.ToImmutableDictionary<K, V>(base.Comparer);
			this.Free();
			return immutableDictionary;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00053A5D File Offset: 0x00051C5D
		public ImmutableDictionary<K, V> ToImmutableDictionary()
		{
			return this.ToImmutableDictionary<K, V>(base.Comparer);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00053A6B File Offset: 0x00051C6B
		public void Free()
		{
			base.Clear();
			ObjectPool<PooledDictionary<K, V>> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00053A84 File Offset: 0x00051C84
		public static ObjectPool<PooledDictionary<K, V>> CreatePool(IEqualityComparer<K> keyComparer)
		{
			ObjectPool<PooledDictionary<K, V>> pool = null;
			pool = new ObjectPool<PooledDictionary<K, V>>(() => new PooledDictionary<K, V>(pool, keyComparer), 128);
			return pool;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00053ABA File Offset: 0x00051CBA
		public static PooledDictionary<K, V> GetInstance()
		{
			return PooledDictionary<K, V>.s_poolInstance.Allocate();
		}

		// Token: 0x04000D4C RID: 3404
		private readonly ObjectPool<PooledDictionary<K, V>> _pool;

		// Token: 0x04000D4D RID: 3405
		private static readonly ObjectPool<PooledDictionary<K, V>> s_poolInstance = PooledDictionary<K, V>.CreatePool(EqualityComparer<K>.Default);
	}
}
