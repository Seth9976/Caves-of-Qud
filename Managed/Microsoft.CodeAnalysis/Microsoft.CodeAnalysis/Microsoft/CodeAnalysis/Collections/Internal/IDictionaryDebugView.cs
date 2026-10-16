using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004B0 RID: 1200
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class IDictionaryDebugView<K, [Nullable(2)] V>
	{
		// Token: 0x0600302F RID: 12335 RVA: 0x00089E9C File Offset: 0x0008809C
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x00089EBC File Offset: 0x000880BC
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		[DebuggerBrowsable(3)]
		public KeyValuePair<K, V>[] Items
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x040012A2 RID: 4770
		private readonly IDictionary<K, V> _dict;
	}
}
