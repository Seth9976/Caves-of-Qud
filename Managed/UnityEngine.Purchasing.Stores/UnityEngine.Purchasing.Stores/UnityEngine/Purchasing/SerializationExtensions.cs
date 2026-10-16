using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000040 RID: 64
	internal static class SerializationExtensions
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00004C79 File Offset: 0x00002E79
		public static string TryGetString(this Dictionary<string, object> dic, string key)
		{
			if (dic.ContainsKey(key) && dic[key] != null)
			{
				return dic[key].ToString();
			}
			return null;
		}
	}
}
