using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000AB6 RID: 2742
	internal sealed class CollectionDebugView<T>
	{
		// Token: 0x0600621B RID: 25115 RVA: 0x00148095 File Offset: 0x00146295
		public CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x001480B4 File Offset: 0x001462B4
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

		// Token: 0x04003A26 RID: 14886
		private readonly ICollection<T> _collection;
	}
}
