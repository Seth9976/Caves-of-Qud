using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A93 RID: 2707
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06006119 RID: 24857 RVA: 0x00145103 File Offset: 0x00143303
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x00145120 File Offset: 0x00143320
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040039E2 RID: 14818
		private readonly ICollection<T> _collection;
	}
}
