using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	internal interface IOrderedCollection<[Nullable(2)] out T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FB RID: 251
		int Count { get; }

		// Token: 0x1700002E RID: 46
		T this[int index] { get; }
	}
}
