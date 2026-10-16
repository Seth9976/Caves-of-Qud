using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableSetWithInsertionOrder<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005FFB File Offset: 0x000041FB
		private ImmutableSetWithInsertionOrder(ImmutableDictionary<T, uint> map, uint nextElementValue)
		{
			this._map = map;
			this._nextElementValue = nextElementValue;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006011 File Offset: 0x00004211
		public int Count
		{
			get
			{
				return this._map.Count;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000601E File Offset: 0x0000421E
		public bool Contains(T value)
		{
			return this._map.ContainsKey(value);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000602C File Offset: 0x0000422C
		public ImmutableSetWithInsertionOrder<T> Add(T value)
		{
			if (this._map.ContainsKey(value))
			{
				return this;
			}
			return new ImmutableSetWithInsertionOrder<T>(this._map.Add(value, this._nextElementValue), this._nextElementValue + 1U);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006060 File Offset: 0x00004260
		public ImmutableSetWithInsertionOrder<T> Remove(T value)
		{
			ImmutableDictionary<T, uint> immutableDictionary = this._map.Remove(value);
			if (immutableDictionary == this._map)
			{
				return this;
			}
			if (this.Count != 1)
			{
				return new ImmutableSetWithInsertionOrder<T>(immutableDictionary, this._nextElementValue);
			}
			return ImmutableSetWithInsertionOrder<T>.Empty;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000060A0 File Offset: 0x000042A0
		public IEnumerable<T> InInsertionOrder
		{
			get
			{
				return Enumerable.Select<KeyValuePair<T, uint>, T>(Enumerable.OrderBy<KeyValuePair<T, uint>, uint>(this._map, (KeyValuePair<T, uint> kv) => kv.Value), (KeyValuePair<T, uint> kv) => kv.Key);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000060FB File Offset: 0x000042FB
		public override string ToString()
		{
			return "{" + string.Join<T>(", ", this) + "}";
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006117 File Offset: 0x00004317
		public IEnumerator<T> GetEnumerator()
		{
			return this._map.Keys.GetEnumerator();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006129 File Offset: 0x00004329
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._map.Keys.GetEnumerator();
		}

		// Token: 0x04000071 RID: 113
		public static readonly ImmutableSetWithInsertionOrder<T> Empty = new ImmutableSetWithInsertionOrder<T>(ImmutableDictionary.Create<T, uint>(), 0U);

		// Token: 0x04000072 RID: 114
		private readonly ImmutableDictionary<T, uint> _map;

		// Token: 0x04000073 RID: 115
		private readonly uint _nextElementValue;
	}
}
