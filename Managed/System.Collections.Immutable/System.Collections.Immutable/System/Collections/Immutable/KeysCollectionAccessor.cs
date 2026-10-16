using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal sealed class KeysCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
	{
		// Token: 0x0600036B RID: 875 RVA: 0x000091BA File Offset: 0x000073BA
		internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary, dictionary.Keys)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000091C9 File Offset: 0x000073C9
		public override bool Contains(TKey item)
		{
			return base.Dictionary.ContainsKey(item);
		}
	}
}
