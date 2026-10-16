using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A62 RID: 2658
	internal sealed class IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06005F7C RID: 24444 RVA: 0x00140FF6 File Offset: 0x0013F1F6
		public IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x00141013 File Offset: 0x0013F213
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._collection.ToArray();
			}
		}

		// Token: 0x0400395E RID: 14686
		private readonly IProducerConsumerCollection<T> _collection;
	}
}
