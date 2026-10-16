using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x02000588 RID: 1416
	internal class CapabilitiesResult : HttpBrowserCapabilities
	{
		// Token: 0x06003BD5 RID: 15317 RVA: 0x0009DD57 File Offset: 0x0009BF57
		internal CapabilitiesResult(IDictionary items)
		{
			base.Capabilities = items;
			base.Capabilities["browsers"] = new ArrayList();
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x0009DD7B File Offset: 0x0009BF7B
		internal void AddCapabilities(string name, string value)
		{
			base.Capabilities[name] = value;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x0009DD8C File Offset: 0x0009BF8C
		internal void AddMatchingBrowserId(string id)
		{
			ArrayList arrayList = base.Capabilities["browsers"] as ArrayList;
			if (arrayList != null && !arrayList.Contains(id))
			{
				arrayList.Add(id);
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0009DDC4 File Offset: 0x0009BFC4
		internal virtual string Replace(string item)
		{
			if (item.IndexOf('$') > -1)
			{
				MatchCollection matchCollection = Regex.Matches(item, "\\$\\{(?'Capability'\\w*)\\}");
				if (matchCollection.Count == 0)
				{
					return item;
				}
				for (int i = 0; i <= matchCollection.Count - 1; i++)
				{
					if (matchCollection[i].Success)
					{
						string text = matchCollection[i].Result("${Capability}");
						item = item.Replace("${" + text + "}", this[text]);
					}
				}
			}
			if (item.IndexOf('%') > -1)
			{
				MatchCollection matchCollection2 = Regex.Matches(item, "\\%\\{(?'Capability'\\w*)\\}");
				if (matchCollection2.Count == 0)
				{
					return item;
				}
				for (int j = 0; j <= matchCollection2.Count - 1; j++)
				{
					if (matchCollection2[j].Success)
					{
						string text2 = matchCollection2[j].Result("${Capability}");
						item = item.Replace("%{" + text2 + "}", this[text2]);
					}
				}
			}
			return item;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x0009DEC4 File Offset: 0x0009C0C4
		public StringCollection Keys
		{
			get
			{
				string[] array = new string[base.Capabilities.Keys.Count];
				base.Capabilities.Keys.CopyTo(array, 0);
				Array.Sort<string>(array);
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(array);
				return stringCollection;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x0009DF0B File Offset: 0x0009C10B
		public string UserAgent
		{
			get
			{
				return this[""];
			}
		}
	}
}
