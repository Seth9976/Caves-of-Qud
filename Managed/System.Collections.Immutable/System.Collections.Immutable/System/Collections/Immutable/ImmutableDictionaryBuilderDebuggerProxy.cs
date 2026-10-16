using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableDictionaryBuilderDebuggerProxy<TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000061A4 File Offset: 0x000043A4
		public ImmutableDictionaryBuilderDebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, "map");
			this._map = map;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000061BE File Offset: 0x000043BE
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

		// Token: 0x04000022 RID: 34
		private readonly ImmutableDictionary<TKey, TValue>.Builder _map;

		// Token: 0x04000023 RID: 35
		private KeyValuePair<TKey, TValue>[] _contents;
	}
}
