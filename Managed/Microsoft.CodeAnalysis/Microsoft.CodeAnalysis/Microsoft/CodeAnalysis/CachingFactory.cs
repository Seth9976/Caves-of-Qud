using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000114 RID: 276
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 0, 1, 1 })]
	internal class CachingFactory<TKey, [Nullable(2)] TValue> : CachingBase<CachingFactory<TKey, TValue>.Entry>
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0001BB7C File Offset: 0x00019D7C
		public CachingFactory(int size, Func<TKey, TValue> valueFactory, Func<TKey, int> keyHash, Func<TKey, TValue, bool> keyValueEquality)
			: base(size)
		{
			this._size = size;
			this._valueFactory = valueFactory;
			this._keyHash = keyHash;
			this._keyValueEquality = keyValueEquality;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		public void Add(TKey key, TValue value)
		{
			int keyHash = this.GetKeyHash(key);
			int num = keyHash & this.mask;
			this.entries[num].hash = keyHash;
			this.entries[num].value = value;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			int keyHash = this.GetKeyHash(key);
			int num = keyHash & this.mask;
			CachingFactory<TKey, TValue>.Entry[] entries = this.entries;
			if (entries[num].hash == keyHash)
			{
				TValue value2 = entries[num].value;
				if (this._keyValueEquality.Invoke(key, value2))
				{
					value = value2;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001BC4C File Offset: 0x00019E4C
		public TValue GetOrMakeValue(TKey key)
		{
			int keyHash = this.GetKeyHash(key);
			int num = keyHash & this.mask;
			CachingFactory<TKey, TValue>.Entry[] entries = this.entries;
			if (entries[num].hash == keyHash)
			{
				TValue value = entries[num].value;
				if (this._keyValueEquality.Invoke(key, value))
				{
					return value;
				}
			}
			TValue tvalue = this._valueFactory.Invoke(key);
			entries[num].hash = keyHash;
			entries[num].value = tvalue;
			return tvalue;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001BCC9 File Offset: 0x00019EC9
		private int GetKeyHash(TKey key)
		{
			return this._keyHash.Invoke(key) | this._size;
		}

		// Token: 0x040002BD RID: 701
		private readonly int _size;

		// Token: 0x040002BE RID: 702
		private readonly Func<TKey, TValue> _valueFactory;

		// Token: 0x040002BF RID: 703
		private readonly Func<TKey, int> _keyHash;

		// Token: 0x040002C0 RID: 704
		private readonly Func<TKey, TValue, bool> _keyValueEquality;

		// Token: 0x0200053E RID: 1342
		[NullableContext(0)]
		internal struct Entry
		{
			// Token: 0x040014E6 RID: 5350
			internal int hash;

			// Token: 0x040014E7 RID: 5351
			[Nullable(1)]
			internal TValue value;
		}
	}
}
