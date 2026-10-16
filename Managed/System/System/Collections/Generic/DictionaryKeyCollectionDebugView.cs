using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CB RID: 1227
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x00089224 File Offset: 0x00087424
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00089244 File Offset: 0x00087444
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400157C RID: 5500
		private readonly ICollection<TKey> _collection;
	}
}
