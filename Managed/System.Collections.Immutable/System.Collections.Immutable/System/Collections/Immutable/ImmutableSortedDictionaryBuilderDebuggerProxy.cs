using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000825A File Offset: 0x0000645A
		public ImmutableSortedDictionaryBuilderDebuggerProxy(ImmutableSortedDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00008274 File Offset: 0x00006474
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		[DebuggerBrowsable(3)]
		public KeyValuePair<TKey, TValue>[] Contents
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get
			{
				if (this._contents == null)
				{
					this._contents = this._map.ToArray<KeyValuePair<TKey, TValue>>(this._map.Count);
				}
				return this._contents;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000034 RID: 52
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
