using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000118 RID: 280
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal class ConcurrentCache<TKey, [Nullable(2)] TValue> : CachingBase<ConcurrentCache<TKey, TValue>.Entry>
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
		public ConcurrentCache(int size, IEqualityComparer<TKey> keyComparer)
			: base(size)
		{
			this._keyComparer = keyComparer;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001BEF1 File Offset: 0x0001A0F1
		public ConcurrentCache(int size)
			: this(size, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001BF00 File Offset: 0x0001A100
		public bool TryAdd(TKey key, TValue value)
		{
			int hashCode = this._keyComparer.GetHashCode(key);
			int num = hashCode & this.mask;
			ConcurrentCache<TKey, TValue>.Entry entry = this.entries[num];
			if (entry != null && entry.hash == hashCode && this._keyComparer.Equals(entry.key, key))
			{
				return false;
			}
			this.entries[num] = new ConcurrentCache<TKey, TValue>.Entry(hashCode, key, value);
			return true;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001BF60 File Offset: 0x0001A160
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			int hashCode = this._keyComparer.GetHashCode(key);
			int num = hashCode & this.mask;
			ConcurrentCache<TKey, TValue>.Entry entry = this.entries[num];
			if (entry != null && entry.hash == hashCode && this._keyComparer.Equals(entry.key, key))
			{
				value = entry.value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x040002C5 RID: 709
		private readonly IEqualityComparer<TKey> _keyComparer;

		// Token: 0x02000541 RID: 1345
		[Nullable(0)]
		internal class Entry
		{
			// Token: 0x060033BF RID: 13247 RVA: 0x0009240B File Offset: 0x0009060B
			internal Entry(int hash, TKey key, TValue value)
			{
				this.hash = hash;
				this.key = key;
				this.value = value;
			}

			// Token: 0x040014EC RID: 5356
			internal readonly int hash;

			// Token: 0x040014ED RID: 5357
			internal readonly TKey key;

			// Token: 0x040014EE RID: 5358
			internal readonly TValue value;
		}
	}
}
