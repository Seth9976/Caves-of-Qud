using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedSetBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00008DF3 File Offset: 0x00006FF3
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008E0D File Offset: 0x0000700D
		[DebuggerBrowsable(3)]
		public T[] Contents
		{
			get
			{
				return this._set.ToArray<T>(this._set.Count);
			}
		}

		// Token: 0x04000039 RID: 57
		private readonly ImmutableSortedSet<T>.Builder _set;
	}
}
