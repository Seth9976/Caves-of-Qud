using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrderedMultiDictionary<K, [Nullable(2)] V> : IEnumerable<KeyValuePair<K, SetWithInsertionOrder<V>>>, IEnumerable
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006B93 File Offset: 0x00004D93
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public IEnumerable<K> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x1700003C RID: 60
		public SetWithInsertionOrder<V> this[K k]
		{
			get
			{
				SetWithInsertionOrder<V> setWithInsertionOrder;
				if (!this._dictionary.TryGetValue(k, ref setWithInsertionOrder))
				{
					return new SetWithInsertionOrder<V>();
				}
				return setWithInsertionOrder;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006BCC File Offset: 0x00004DCC
		public OrderedMultiDictionary()
		{
			this._dictionary = new Dictionary<K, SetWithInsertionOrder<V>>();
			this._keys = new List<K>();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006BEC File Offset: 0x00004DEC
		public void Add(K k, V v)
		{
			SetWithInsertionOrder<V> setWithInsertionOrder;
			if (!this._dictionary.TryGetValue(k, ref setWithInsertionOrder))
			{
				this._keys.Add(k);
				setWithInsertionOrder = new SetWithInsertionOrder<V>();
			}
			setWithInsertionOrder.Add(v);
			this._dictionary[k] = setWithInsertionOrder;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006C30 File Offset: 0x00004E30
		public void AddRange(K k, IEnumerable<V> values)
		{
			foreach (V v in values)
			{
				this.Add(k, v);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006C7C File Offset: 0x00004E7C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006C84 File Offset: 0x00004E84
		[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
		public IEnumerator<KeyValuePair<K, SetWithInsertionOrder<V>>> GetEnumerator()
		{
			foreach (K k in this._keys)
			{
				yield return new KeyValuePair<K, SetWithInsertionOrder<V>>(k, this._dictionary[k]);
			}
			List<K>.Enumerator enumerator = default(List<K>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000082 RID: 130
		private readonly Dictionary<K, SetWithInsertionOrder<V>> _dictionary;

		// Token: 0x04000083 RID: 131
		private readonly List<K> _keys;
	}
}
