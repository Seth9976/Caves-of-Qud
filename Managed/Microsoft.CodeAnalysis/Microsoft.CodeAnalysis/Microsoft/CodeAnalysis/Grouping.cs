using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011B RID: 283
	[NullableContext(1)]
	[Nullable(0)]
	internal class Grouping<TKey, [Nullable(2)] TElement> : IGrouping<TKey, TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001C023 File Offset: 0x0001A223
		public TKey Key { get; }

		// Token: 0x060009CC RID: 2508 RVA: 0x0001C02B File Offset: 0x0001A22B
		public Grouping(TKey key, IEnumerable<TElement> elements)
		{
			this.Key = key;
			this._elements = elements;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001C041 File Offset: 0x0001A241
		public Grouping([Nullable(new byte[] { 0, 1, 1, 1 })] KeyValuePair<TKey, IEnumerable<TElement>> pair)
			: this(pair.Key, pair.Value)
		{
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001C057 File Offset: 0x0001A257
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001C064 File Offset: 0x0001A264
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002C7 RID: 711
		private readonly IEnumerable<TElement> _elements;
	}
}
