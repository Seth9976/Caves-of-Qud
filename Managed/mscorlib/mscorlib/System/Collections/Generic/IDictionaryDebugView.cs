using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000A96 RID: 2710
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06006124 RID: 24868 RVA: 0x0014514C File Offset: 0x0014334C
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06006125 RID: 24869 RVA: 0x0014516C File Offset: 0x0014336C
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

		// Token: 0x040039E3 RID: 14819
		private readonly IDictionary<K, V> _dict;
	}
}
