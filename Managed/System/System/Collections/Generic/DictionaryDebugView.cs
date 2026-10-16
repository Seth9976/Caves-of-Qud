using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004F3 RID: 1267
	internal sealed class DictionaryDebugView<K, V>
	{
		// Token: 0x06002961 RID: 10593 RVA: 0x0008E87C File Offset: 0x0008CA7C
		public DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x0008E89C File Offset: 0x0008CA9C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001604 RID: 5636
		private readonly IDictionary<K, V> _dict;
	}
}
