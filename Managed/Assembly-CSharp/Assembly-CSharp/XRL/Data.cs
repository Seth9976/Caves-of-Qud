using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

namespace XRL
{
	// Token: 0x020004D6 RID: 1238
	public static class Data
	{
		// Token: 0x0600350A RID: 13578 RVA: 0x00100274 File Offset: 0x000FE474
		private static void CheckInit()
		{
			if (Data.TextData == null)
			{
				Data.TextData = new Dictionary<string, string>();
				using (StreamReader streamingAssetsStreamReader = DataManager.GetStreamingAssetsStreamReader("Text.txt"))
				{
					foreach (object obj in (JSON.Parse(streamingAssetsStreamReader.ReadToEnd()) as JSONClass))
					{
						KeyValuePair<string, JSONNode> keyValuePair = (KeyValuePair<string, JSONNode>)obj;
						Data.TextData.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x00100320 File Offset: 0x000FE520
		public static bool HasText(string Name)
		{
			Data.CheckInit();
			return Data.TextData.ContainsKey(Name);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x00100332 File Offset: 0x000FE532
		public static string GetText(string Name)
		{
			Data.CheckInit();
			return Data.TextData[Name];
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x00100344 File Offset: 0x000FE544
		public static bool TryGetText(string Name, out string Result)
		{
			Data.CheckInit();
			return Data.TextData.TryGetValue(Name, out Result);
		}

		// Token: 0x04001AD4 RID: 6868
		private static Dictionary<string, string> TextData;
	}
}
