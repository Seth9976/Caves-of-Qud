using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004F2 RID: 1266
	internal sealed class CollectionDebugView<T>
	{
		// Token: 0x0600295F RID: 10591 RVA: 0x0008E830 File Offset: 0x0008CA30
		public CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x0008E850 File Offset: 0x0008CA50
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

		// Token: 0x04001603 RID: 5635
		private readonly ICollection<T> _collection;
	}
}
