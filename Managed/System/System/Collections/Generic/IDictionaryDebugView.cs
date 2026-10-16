using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CA RID: 1226
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x060027A1 RID: 10145 RVA: 0x000891D8 File Offset: 0x000873D8
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000891F8 File Offset: 0x000873F8
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

		// Token: 0x0400157B RID: 5499
		private readonly IDictionary<K, V> _dict;
	}
}
