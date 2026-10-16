using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000115 RID: 277
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 0, 1, 1 })]
	internal class CachingIdentityFactory<TKey, [Nullable(2)] TValue> : CachingBase<CachingIdentityFactory<TKey, TValue>.Entry> where TKey : class
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x0001BCDE File Offset: 0x00019EDE
		public CachingIdentityFactory(int size, Func<TKey, TValue> valueFactory)
			: base(size)
		{
			this._valueFactory = valueFactory;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001BCEE File Offset: 0x00019EEE
		public CachingIdentityFactory(int size, Func<TKey, TValue> valueFactory, ObjectPool<CachingIdentityFactory<TKey, TValue>> pool)
			: this(size, valueFactory)
		{
			this._pool = pool;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001BD00 File Offset: 0x00019F00
		public void Add(TKey key, TValue value)
		{
			int num = RuntimeHelpers.GetHashCode(key) & this.mask;
			this.entries[num].key = key;
			this.entries[num].value = value;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001BD44 File Offset: 0x00019F44
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			int num = RuntimeHelpers.GetHashCode(key) & this.mask;
			CachingIdentityFactory<TKey, TValue>.Entry[] entries = this.entries;
			if (entries[num].key == key)
			{
				value = entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		public TValue GetOrMakeValue(TKey key)
		{
			int num = RuntimeHelpers.GetHashCode(key) & this.mask;
			CachingIdentityFactory<TKey, TValue>.Entry[] entries = this.entries;
			if (entries[num].key == key)
			{
				return entries[num].value;
			}
			TValue tvalue = this._valueFactory.Invoke(key);
			entries[num].key = key;
			entries[num].value = tvalue;
			return tvalue;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001BE15 File Offset: 0x0001A015
		public static ObjectPool<CachingIdentityFactory<TKey, TValue>> CreatePool(int size, Func<TKey, TValue> valueFactory)
		{
			return new ObjectPool<CachingIdentityFactory<TKey, TValue>>((ObjectPool<CachingIdentityFactory<TKey, TValue>> pool) => new CachingIdentityFactory<TKey, TValue>(size, valueFactory, pool), Environment.ProcessorCount * 2);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001BE41 File Offset: 0x0001A041
		public void Free()
		{
			ObjectPool<CachingIdentityFactory<TKey, TValue>> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x040002C1 RID: 705
		private readonly Func<TKey, TValue> _valueFactory;

		// Token: 0x040002C2 RID: 706
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private readonly ObjectPool<CachingIdentityFactory<TKey, TValue>> _pool;

		// Token: 0x0200053F RID: 1343
		[Nullable(0)]
		internal struct Entry
		{
			// Token: 0x040014E8 RID: 5352
			internal TKey key;

			// Token: 0x040014E9 RID: 5353
			internal TValue value;
		}
	}
}
