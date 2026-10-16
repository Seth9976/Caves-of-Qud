using System;
using UnityEngine.Purchasing.MiniJSON;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000005 RID: 5
	public class MiniJson
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020BE File Offset: 0x000002BE
		public static string JsonEncode(object json)
		{
			return Json.Serialize(json);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C6 File Offset: 0x000002C6
		public static object JsonDecode(string json)
		{
			return Json.Deserialize(json);
		}
	}
}
