using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableArrayBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000547C File Offset: 0x0000367C
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			this._builder = builder;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005496 File Offset: 0x00003696
		[DebuggerBrowsable(3)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
