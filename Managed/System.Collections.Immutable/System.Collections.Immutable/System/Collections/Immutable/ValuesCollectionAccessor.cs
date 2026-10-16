using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal sealed class ValuesCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x000091D7 File Offset: 0x000073D7
		internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
			: base(dictionary, dictionary.Values)
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000091E8 File Offset: 0x000073E8
		public override bool Contains(TValue item)
		{
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = base.Dictionary as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.ContainsValue(item);
			}
			IImmutableDictionaryInternal<TKey, TValue> immutableDictionaryInternal = base.Dictionary as IImmutableDictionaryInternal<TKey, TValue>;
			if (immutableDictionaryInternal != null)
			{
				return immutableDictionaryInternal.ContainsValue(item);
			}
			throw new NotSupportedException();
		}
	}
}
