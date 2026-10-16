using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableListBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00007582 File Offset: 0x00005782
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000759C File Offset: 0x0000579C
		[DebuggerBrowsable(3)]
		public T[] Contents
		{
			get
			{
				if (this._cachedContents == null)
				{
					this._cachedContents = this._list.ToArray<T>(this._list.Count);
				}
				return this._cachedContents;
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04000029 RID: 41
		private T[] _cachedContents;
	}
}
