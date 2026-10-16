using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	internal interface ISortKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003A5 RID: 933
		IComparer<TKey> KeyComparer { get; }
	}
}
