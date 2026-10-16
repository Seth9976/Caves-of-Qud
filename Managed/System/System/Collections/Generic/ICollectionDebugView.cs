using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x0600279F RID: 10143 RVA: 0x0008918E File Offset: 0x0008738E
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000891AC File Offset: 0x000873AC
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

		// Token: 0x0400157A RID: 5498
		private readonly ICollection<T> _collection;
	}
}
