using System;
using UnityEngine;
using XRL.UI;

namespace XRL
{
	// Token: 0x020004E9 RID: 1257
	public static class Prefs
	{
		// Token: 0x0600364E RID: 13902 RVA: 0x00104C6F File Offset: 0x00102E6F
		public static void Init()
		{
			Debug.Log("Loading user prefs");
			Prefs.Bag = new NameValueBag(DataManager.SyncedPath("UserPrefs.json"));
			Prefs.Bag.Load();
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x00104C99 File Offset: 0x00102E99
		public static bool HasString(string Name)
		{
			if (Prefs.Bag == null)
			{
				Prefs.Init();
			}
			return Prefs.Bag.Bag.ContainsKey(Name);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00104CB8 File Offset: 0x00102EB8
		public static string GetString(string Name, string Default = null)
		{
			if (Prefs.Bag == null)
			{
				Prefs.Init();
			}
			NameValueBag bag = Prefs.Bag;
			lock (bag)
			{
				string text;
				if (Prefs.Bag.Bag.TryGetValue(Name, out text))
				{
					return text;
				}
			}
			return Default;
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x00104D18 File Offset: 0x00102F18
		public static void SetString(string Name, string Value)
		{
			if (Prefs.Bag == null)
			{
				Prefs.Init();
			}
			NameValueBag bag = Prefs.Bag;
			lock (bag)
			{
				Prefs.Bag.SetValue(Name, Value, true);
			}
		}

		// Token: 0x04001B32 RID: 6962
		public static NameValueBag Bag;
	}
}
