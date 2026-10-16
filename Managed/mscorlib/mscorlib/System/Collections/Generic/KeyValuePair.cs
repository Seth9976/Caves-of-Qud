using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x02000AA1 RID: 2721
	public static class KeyValuePair
	{
		// Token: 0x0600613E RID: 24894 RVA: 0x00145271 File Offset: 0x00143471
		public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0014527C File Offset: 0x0014347C
		internal static string PairToString(object key, object value)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (key != null)
			{
				stringBuilder.Append(key);
			}
			stringBuilder.Append(", ");
			if (value != null)
			{
				stringBuilder.Append(value);
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}
	}
}
