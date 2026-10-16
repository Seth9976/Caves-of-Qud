using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleJSON;

namespace XRL.UI
{
	// Token: 0x020014CA RID: 5322
	public static class HotkeyFavorites
	{
		// Token: 0x0600D084 RID: 53380 RVA: 0x004D16F2 File Offset: 0x004CF8F2
		public static Dictionary<string, List<int>> LoadForUpgrade()
		{
			HotkeyFavorites.Load();
			return HotkeyFavorites.commandToKey;
		}

		// Token: 0x0600D085 RID: 53381 RVA: 0x004D1700 File Offset: 0x004CF900
		private static void Load()
		{
			HotkeyFavorites.commandToKey = new Dictionary<string, List<int>>();
			try
			{
				string text = DataManager.SavePath("HotkeyFavorites.json");
				if (File.Exists(text))
				{
					string text2 = "";
					using (StreamReader streamReader = new StreamReader(text))
					{
						text2 = streamReader.ReadToEnd();
					}
					if (text2 != "")
					{
						foreach (object obj in (JSON.Parse(text2) as JSONClass))
						{
							KeyValuePair<string, JSONNode> keyValuePair = (KeyValuePair<string, JSONNode>)obj;
							string key = keyValuePair.Key;
							string text3 = keyValuePair.Value;
							List<int> list = new List<int>();
							if (!string.IsNullOrEmpty(text3))
							{
								foreach (string text4 in text3.Split(',', StringSplitOptions.None))
								{
									list.Add(Convert.ToInt32(text4));
								}
							}
							HotkeyFavorites.commandToKey.Add(key, list);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("HotkeyFavorites::Load", ex);
			}
		}

		// Token: 0x0600D086 RID: 53382 RVA: 0x004D1840 File Offset: 0x004CFA40
		private static void Save()
		{
			try
			{
				string text = DataManager.SavePath("HotkeyFavorites.json");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("{");
				int num = 0;
				foreach (KeyValuePair<string, List<int>> keyValuePair in HotkeyFavorites.commandToKey)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int i = 0; i < keyValuePair.Value.Count; i++)
					{
						stringBuilder2.Append(keyValuePair.Value[i]);
						if (i != keyValuePair.Value.Count - 1)
						{
							stringBuilder2.Append(",");
						}
					}
					StringBuilder stringBuilder3 = stringBuilder;
					string[] array = new string[5];
					array[0] = " \"";
					array[1] = keyValuePair.Key;
					array[2] = "\":\"";
					int num2 = 3;
					StringBuilder stringBuilder4 = stringBuilder2;
					array[num2] = ((stringBuilder4 != null) ? stringBuilder4.ToString() : null);
					array[4] = "\"";
					stringBuilder3.Append(string.Concat(array));
					if (num != HotkeyFavorites.commandToKey.Keys.Count - 1)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.AppendLine();
					num++;
				}
				stringBuilder.AppendLine("}");
				File.WriteAllText(text, stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("HotkeyFavorites::Save", ex);
			}
		}

		// Token: 0x0600D087 RID: 53383 RVA: 0x004D19C8 File Offset: 0x004CFBC8
		public static List<int> GetFavorites(string command)
		{
			if (HotkeyFavorites.commandToKey == null)
			{
				HotkeyFavorites.Load();
			}
			List<int> list;
			if (HotkeyFavorites.commandToKey.TryGetValue(command, out list))
			{
				return list;
			}
			return null;
		}

		// Token: 0x0600D088 RID: 53384 RVA: 0x004D19F4 File Offset: 0x004CFBF4
		public static void AddFavorite(string command, int key)
		{
			if (HotkeyFavorites.commandToKey == null)
			{
				HotkeyFavorites.Load();
			}
			List<int> list;
			if (HotkeyFavorites.commandToKey.TryGetValue(command, out list))
			{
				list.Insert(0, key);
			}
			else
			{
				List<int> list2 = new List<int>();
				HotkeyFavorites.commandToKey.Add(command, list2);
				list2.Add(key);
			}
			HotkeyFavorites.Save();
		}

		// Token: 0x04005237 RID: 21047
		public static Dictionary<string, List<int>> commandToKey;
	}
}
