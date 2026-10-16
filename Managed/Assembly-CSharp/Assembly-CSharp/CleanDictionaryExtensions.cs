using System;
using System.Collections.Generic;

// Token: 0x02000178 RID: 376
public static class CleanDictionaryExtensions
{
	// Token: 0x06000E87 RID: 3719 RVA: 0x0005CE5C File Offset: 0x0005B05C
	public static bool SameAs<K, V>(this Dictionary<K, V> dict1, Dictionary<K, V> dict2)
	{
		if (dict1.Count != dict2.Count)
		{
			return false;
		}
		foreach (K k in dict1.Keys)
		{
			if (dict2.ContainsKey(k))
			{
				V v = dict2[k];
				if (v.Equals(dict1[k]))
				{
					continue;
				}
			}
			return false;
		}
		return true;
	}
}
