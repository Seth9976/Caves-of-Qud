using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CC RID: 1228
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x00089270 File Offset: 0x00087470
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00089290 File Offset: 0x00087490
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400157D RID: 5501
		private readonly ICollection<TValue> _collection;
	}
}
