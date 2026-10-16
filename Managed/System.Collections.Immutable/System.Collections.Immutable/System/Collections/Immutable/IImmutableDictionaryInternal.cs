using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001E RID: 30
	[NullableContext(2)]
	internal interface IImmutableDictionaryInternal<TKey, TValue>
	{
		// Token: 0x0600007A RID: 122
		[NullableContext(1)]
		bool ContainsValue(TValue value);
	}
}
