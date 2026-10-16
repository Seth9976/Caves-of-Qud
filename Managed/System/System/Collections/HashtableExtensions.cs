using System;

namespace System.Collections
{
	// Token: 0x02000497 RID: 1175
	internal static class HashtableExtensions
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x0008336F File Offset: 0x0008156F
		public static bool TryGetValue<T>(this Hashtable table, object key, out T value)
		{
			if (table.ContainsKey(key))
			{
				value = (T)((object)table[key]);
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
