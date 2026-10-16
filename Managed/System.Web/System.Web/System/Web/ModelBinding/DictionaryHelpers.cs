using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000514 RID: 1300
	internal static class DictionaryHelpers
	{
		// Token: 0x06003996 RID: 14742 RVA: 0x0009A31E File Offset: 0x0009851E
		public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(prefix, out tvalue))
			{
				yield return new KeyValuePair<string, TValue>(prefix, tvalue);
			}
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					char c = key[prefix.Length];
					if (c == '.' || c == '[')
					{
						yield return keyValuePair;
					}
				}
			}
			IEnumerator<KeyValuePair<string, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0009A335 File Offset: 0x00098535
		public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			return DictionaryHelpers.FindKeysWithPrefix<TValue>(dictionary, prefix).Any<KeyValuePair<string, TValue>>();
		}
	}
}
