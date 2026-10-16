using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	internal interface IHashKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003A4 RID: 932
		IEqualityComparer<TKey> KeyComparer { get; }
	}
}
