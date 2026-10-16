using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.MiniJSON
{
	// Token: 0x02000008 RID: 8
	public static class MiniJsonExtensions
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002254 File Offset: 0x00000454
		public static Dictionary<string, object> GetHash(this Dictionary<string, object> dic, string key)
		{
			return (Dictionary<string, object>)dic[key];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002264 File Offset: 0x00000464
		public static T GetEnum<T>(this Dictionary<string, object> dic, string key)
		{
			if (dic.ContainsKey(key))
			{
				return (T)((object)Enum.Parse(typeof(T), dic[key].ToString(), true));
			}
			return default(T);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A5 File Offset: 0x000004A5
		public static string GetString(this Dictionary<string, object> dic, string key, string defaultValue = "")
		{
			if (dic.ContainsKey(key))
			{
				return dic[key].ToString();
			}
			return defaultValue;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022BE File Offset: 0x000004BE
		public static long GetLong(this Dictionary<string, object> dic, string key)
		{
			if (dic.ContainsKey(key))
			{
				return long.Parse(dic[key].ToString());
			}
			return 0L;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022E0 File Offset: 0x000004E0
		public static List<string> GetStringList(this Dictionary<string, object> dic, string key)
		{
			if (dic.ContainsKey(key))
			{
				List<string> list = new List<string>();
				foreach (object obj in ((List<object>)dic[key]))
				{
					list.Add(obj.ToString());
				}
				return list;
			}
			return new List<string>();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002354 File Offset: 0x00000554
		public static bool GetBool(this Dictionary<string, object> dic, string key)
		{
			return dic.ContainsKey(key) && bool.Parse(dic[key].ToString());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002374 File Offset: 0x00000574
		public static T Get<T>(this Dictionary<string, object> dic, string key)
		{
			if (dic.ContainsKey(key))
			{
				return (T)((object)dic[key]);
			}
			return default(T);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023A0 File Offset: 0x000005A0
		public static string toJson(this Dictionary<string, object> obj)
		{
			return MiniJson.JsonEncode(obj);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023A8 File Offset: 0x000005A8
		public static string toJson(this Dictionary<string, string> obj)
		{
			return MiniJson.JsonEncode(obj);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023B0 File Offset: 0x000005B0
		public static string toJson(this string[] array)
		{
			List<object> list = new List<object>();
			foreach (string text in array)
			{
				list.Add(text);
			}
			return MiniJson.JsonEncode(list);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023E4 File Offset: 0x000005E4
		public static List<object> ArrayListFromJson(this string json)
		{
			return MiniJson.JsonDecode(json) as List<object>;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023F1 File Offset: 0x000005F1
		public static Dictionary<string, object> HashtableFromJson(this string json)
		{
			return MiniJson.JsonDecode(json) as Dictionary<string, object>;
		}
	}
}
