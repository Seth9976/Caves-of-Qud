using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x0200049E RID: 1182
	internal sealed class BlockingCollectionDebugView<T>
	{
		// Token: 0x060025DF RID: 9695 RVA: 0x00084873 File Offset: 0x00082A73
		public BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._blockingCollection = collection;
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00084890 File Offset: 0x00082A90
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._blockingCollection.ToArray();
			}
		}

		// Token: 0x040014E5 RID: 5349
		private readonly BlockingCollection<T> _blockingCollection;
	}
}
