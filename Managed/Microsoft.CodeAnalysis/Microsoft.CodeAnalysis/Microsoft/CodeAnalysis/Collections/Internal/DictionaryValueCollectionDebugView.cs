using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B2 RID: 1202
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryValueCollectionDebugView<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06003033 RID: 12339 RVA: 0x00089F34 File Offset: 0x00088134
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x00089F54 File Offset: 0x00088154
		[DebuggerBrowsable(3)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040012A4 RID: 4772
		private readonly ICollection<TValue> _collection;
	}
}
