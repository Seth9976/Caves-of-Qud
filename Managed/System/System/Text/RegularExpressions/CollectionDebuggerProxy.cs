using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001ED RID: 493
	internal sealed class CollectionDebuggerProxy<T>
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x00035852 File Offset: 0x00033A52
		public CollectionDebuggerProxy(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00035870 File Offset: 0x00033A70
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

		// Token: 0x040007FC RID: 2044
		private readonly ICollection<T> _collection;
	}
}
