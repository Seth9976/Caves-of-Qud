using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A98 RID: 2712
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006128 RID: 24872 RVA: 0x001451E4 File Offset: 0x001433E4
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x00145204 File Offset: 0x00143404
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

		// Token: 0x040039E5 RID: 14821
		private readonly ICollection<TValue> _collection;
	}
}
