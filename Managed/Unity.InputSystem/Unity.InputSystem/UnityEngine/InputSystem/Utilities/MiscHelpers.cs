using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013A RID: 314
	internal static class MiscHelpers
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x00054300 File Offset: 0x00052500
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00054323 File Offset: 0x00052523
		public static IEnumerable<TValue> EveryNth<TValue>(this IEnumerable<TValue> enumerable, int n, int start = 0)
		{
			int index = 0;
			foreach (TValue tvalue in enumerable)
			{
				if (index < start)
				{
					int num = index + 1;
					index = num;
				}
				else
				{
					if ((index - start) % n == 0)
					{
						yield return tvalue;
					}
					int num = index + 1;
					index = num;
				}
			}
			IEnumerator<TValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00054344 File Offset: 0x00052544
		public static int IndexOf<TValue>(this IEnumerable<TValue> enumerable, TValue value)
		{
			int num = 0;
			foreach (TValue tvalue in enumerable)
			{
				if (EqualityComparer<TValue>.Default.Equals(tvalue, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
