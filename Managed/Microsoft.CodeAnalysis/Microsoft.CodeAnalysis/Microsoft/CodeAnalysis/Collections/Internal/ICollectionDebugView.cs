using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AF RID: 1199
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ICollectionDebugView<[Nullable(2)] T>
	{
		// Token: 0x0600302D RID: 12333 RVA: 0x00089E4F File Offset: 0x0008804F
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x00089E70 File Offset: 0x00088070
		[DebuggerBrowsable(3)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040012A1 RID: 4769
		private readonly ICollection<T> _collection;
	}
}
