using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B1 RID: 1201
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryKeyCollectionDebugView<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x06003031 RID: 12337 RVA: 0x00089EE8 File Offset: 0x000880E8
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x00089F08 File Offset: 0x00088108
		[DebuggerBrowsable(3)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040012A3 RID: 4771
		private readonly ICollection<TKey> _collection;
	}
}
