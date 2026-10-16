using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x02000A5D RID: 2653
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06005F51 RID: 24401 RVA: 0x001409B0 File Offset: 0x0013EBB0
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dictionary = dictionary;
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x001409D0 File Offset: 0x0013EBD0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dictionary.Count];
				this._dictionary.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04003955 RID: 14677
		private readonly IDictionary<K, V> _dictionary;
	}
}
