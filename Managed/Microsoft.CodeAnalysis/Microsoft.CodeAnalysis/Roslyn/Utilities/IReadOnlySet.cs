using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	internal interface IReadOnlySet<[Nullable(2)] T>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000193 RID: 403
		int Count { get; }

		// Token: 0x06000194 RID: 404
		bool Contains(T item);
	}
}
