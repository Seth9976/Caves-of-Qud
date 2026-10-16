using System;
using System.Collections.Generic;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026F RID: 623
	internal static class SortHelpers
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x00078BB4 File Offset: 0x00076DB4
		public static Dictionary<TKey, List<TValue>> BucketSort<TValue, TKey>(this IEnumerable<TValue> inputs, SortHelpers.KeySelector<TValue, TKey> keySelector)
		{
			Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();
			foreach (TValue tvalue in inputs)
			{
				TKey tkey = keySelector(tvalue);
				List<TValue> list;
				if (!dictionary.TryGetValue(tkey, out list))
				{
					list = new List<TValue>();
					dictionary.Add(tkey, list);
				}
				list.Add(tvalue);
			}
			return dictionary;
		}

		// Token: 0x02001675 RID: 5749
		// (Invoke) Token: 0x0600D822 RID: 55330
		internal delegate TKey KeySelector<TValue, TKey>(TValue value);
	}
}
