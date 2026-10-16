using System;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000606 RID: 1542
	internal class DefaultImplicitResourceProvider : IImplicitResourceProvider
	{
		// Token: 0x0600427B RID: 17019 RVA: 0x000AD24A File Offset: 0x000AB44A
		internal DefaultImplicitResourceProvider(IResourceProvider resourceProvider)
		{
			this._resourceProvider = resourceProvider;
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000AD25C File Offset: 0x000AB45C
		public virtual object GetObject(ImplicitResourceKey entry, CultureInfo culture)
		{
			string text = DefaultImplicitResourceProvider.ConstructFullKey(entry);
			return this._resourceProvider.GetObject(text, culture);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000AD27D File Offset: 0x000AB47D
		public virtual ICollection GetImplicitResourceKeys(string keyPrefix)
		{
			this.EnsureGetPageResources();
			if (this._implicitResources == null)
			{
				return null;
			}
			return (ICollection)this._implicitResources[keyPrefix];
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000AD2A0 File Offset: 0x000AB4A0
		internal void EnsureGetPageResources()
		{
			if (this._attemptedGetPageResources)
			{
				return;
			}
			this._attemptedGetPageResources = true;
			IResourceReader resourceReader = this._resourceProvider.ResourceReader;
			if (resourceReader == null)
			{
				return;
			}
			this._implicitResources = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in resourceReader)
			{
				ImplicitResourceKey implicitResourceKey = DefaultImplicitResourceProvider.ParseFullKey((string)((DictionaryEntry)obj).Key);
				if (implicitResourceKey != null)
				{
					ArrayList arrayList = (ArrayList)this._implicitResources[implicitResourceKey.KeyPrefix];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						this._implicitResources[implicitResourceKey.KeyPrefix] = arrayList;
					}
					arrayList.Add(implicitResourceKey);
				}
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000AD374 File Offset: 0x000AB574
		private static ImplicitResourceKey ParseFullKey(string key)
		{
			string text = string.Empty;
			if (key.IndexOf(':') > 0)
			{
				string[] array = key.Split(':', StringSplitOptions.None);
				if (array.Length > 2)
				{
					return null;
				}
				text = array[0];
				key = array[1];
			}
			int num = key.IndexOf('.');
			if (num <= 0)
			{
				return null;
			}
			string text2 = key.Substring(0, num);
			string text3 = key.Substring(num + 1);
			return new ImplicitResourceKey
			{
				Filter = text,
				KeyPrefix = text2,
				Property = text3
			};
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000AD3F0 File Offset: 0x000AB5F0
		private static string ConstructFullKey(ImplicitResourceKey entry)
		{
			string text = entry.KeyPrefix + "." + entry.Property;
			if (entry.Filter.Length > 0)
			{
				text = entry.Filter + ":" + text;
			}
			return text;
		}

		// Token: 0x040023B2 RID: 9138
		private IResourceProvider _resourceProvider;

		// Token: 0x040023B3 RID: 9139
		private IDictionary _implicitResources;

		// Token: 0x040023B4 RID: 9140
		private bool _attemptedGetPageResources;
	}
}
