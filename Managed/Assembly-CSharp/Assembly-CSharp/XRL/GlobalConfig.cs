using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL
{
	// Token: 0x020004CB RID: 1227
	[HasModSensitiveStaticCache]
	public static class GlobalConfig
	{
		// Token: 0x060034F4 RID: 13556 RVA: 0x00100028 File Offset: 0x000FE228
		public static void LoadGlobalConfig()
		{
			GlobalConfig.IntSettings = new Dictionary<string, int>();
			GlobalConfig.BoolSettings = new Dictionary<string, bool>();
			GlobalConfig.FloatSettings = new Dictionary<string, float>();
			NameValueBag nameValueBag = new NameValueBag(DataManager.FilePath("GlobalConfig.json"));
			nameValueBag.Load();
			GlobalConfig.StringSettings = nameValueBag.Bag;
			ModManager.ForEachFile("GlobalConfig.json", delegate(string fileName)
			{
				NameValueBag nameValueBag2 = new NameValueBag(fileName);
				nameValueBag2.Load();
				foreach (string text in nameValueBag2.Bag.Keys)
				{
					if (GlobalConfig.StringSettings.ContainsKey(text))
					{
						GlobalConfig.StringSettings[text] = nameValueBag2.Bag[text];
					}
					else
					{
						GlobalConfig.StringSettings.Add(text, nameValueBag2.Bag[text]);
					}
				}
			}, false, false);
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x001000A0 File Offset: 0x000FE2A0
		public static string GetStringSetting(string Name, string Default = null)
		{
			if (GlobalConfig.StringSettings == null)
			{
				GlobalConfig.LoadGlobalConfig();
				if (GlobalConfig.StringSettings == null)
				{
					return Default;
				}
			}
			string text;
			if (GlobalConfig.StringSettings.TryGetValue(Name, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x001000D4 File Offset: 0x000FE2D4
		public static bool GetBoolSetting(string Name, bool Default = false)
		{
			if (GlobalConfig.BoolSettings == null || GlobalConfig.StringSettings == null)
			{
				GlobalConfig.LoadGlobalConfig();
				if (GlobalConfig.BoolSettings == null)
				{
					return Default;
				}
			}
			bool flag;
			if (GlobalConfig.BoolSettings.TryGetValue(Name, out flag))
			{
				return flag;
			}
			string text;
			if (GlobalConfig.StringSettings != null && GlobalConfig.StringSettings.TryGetValue(Name, out text))
			{
				try
				{
					bool flag2 = bool.Parse(text);
					GlobalConfig.BoolSettings.Add(Name, flag2);
					return flag2;
				}
				catch (Exception)
				{
				}
				return Default;
			}
			return Default;
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x00100154 File Offset: 0x000FE354
		public static int GetIntSetting(string Name, int Default = 0)
		{
			if (GlobalConfig.IntSettings == null)
			{
				GlobalConfig.LoadGlobalConfig();
				if (GlobalConfig.IntSettings == null)
				{
					return Default;
				}
			}
			int num;
			if (GlobalConfig.IntSettings.TryGetValue(Name, out num))
			{
				return num;
			}
			if (GlobalConfig.StringSettings.ContainsKey(Name))
			{
				try
				{
					int num2 = int.Parse(GlobalConfig.StringSettings[Name]);
					GlobalConfig.IntSettings.Add(Name, num2);
					return num2;
				}
				catch (Exception)
				{
				}
				return Default;
			}
			return Default;
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x001001CC File Offset: 0x000FE3CC
		public static float GetFloatSetting(string Name, float Default = 0f)
		{
			if (GlobalConfig.FloatSettings == null)
			{
				GlobalConfig.LoadGlobalConfig();
				if (GlobalConfig.FloatSettings == null)
				{
					return Default;
				}
			}
			float num;
			if (GlobalConfig.FloatSettings.TryGetValue(Name, out num))
			{
				return num;
			}
			if (GlobalConfig.StringSettings.ContainsKey(Name))
			{
				try
				{
					float num2 = Convert.ToSingle(GlobalConfig.StringSettings[Name]);
					GlobalConfig.FloatSettings.Add(Name, num2);
					return num2;
				}
				catch (Exception)
				{
				}
				return Default;
			}
			return Default;
		}

		// Token: 0x04001ACE RID: 6862
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, string> StringSettings;

		// Token: 0x04001ACF RID: 6863
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, int> IntSettings;

		// Token: 0x04001AD0 RID: 6864
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, bool> BoolSettings;

		// Token: 0x04001AD1 RID: 6865
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, float> FloatSettings;
	}
}
