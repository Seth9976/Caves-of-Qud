using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 0, 1, 1 })]
	internal class ImmutableDictionaryDebuggerProxy<TKey, [Nullable(2)] TValue> : ImmutableEnumerableDebuggerProxy<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x000061EA File Offset: 0x000043EA
		public ImmutableDictionaryDebuggerProxy(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary)
		{
		}
	}
}
