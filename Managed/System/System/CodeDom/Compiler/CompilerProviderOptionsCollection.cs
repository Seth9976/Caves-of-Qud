using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035E RID: 862
	[ConfigurationCollection(typeof(CompilerProviderOption), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "providerOption")]
	internal sealed class CompilerProviderOptionsCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C89 RID: 7305 RVA: 0x000673D6 File Offset: 0x000655D6
		protected override ConfigurationElement CreateNewElement()
		{
			return new CompilerProviderOption();
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000673DD File Offset: 0x000655DD
		public CompilerProviderOption Get(int index)
		{
			return (CompilerProviderOption)base.BaseGet(index);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000673EB File Offset: 0x000655EB
		public CompilerProviderOption Get(string name)
		{
			return (CompilerProviderOption)base.BaseGet(name);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000673F9 File Offset: 0x000655F9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CompilerProviderOption)element).Name;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00067292 File Offset: 0x00065492
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00067408 File Offset: 0x00065608
		public string[] AllKeys
		{
			get
			{
				int count = base.Count;
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0006743F File Offset: 0x0006563F
		protected override string ElementName
		{
			get
			{
				return "providerOption";
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00067446 File Offset: 0x00065646
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOptionsCollection.properties;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x00067450 File Offset: 0x00065650
		public Dictionary<string, string> ProviderOptions
		{
			get
			{
				int count = base.Count;
				if (count == 0)
				{
					return null;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(count);
				for (int i = 0; i < count; i++)
				{
					CompilerProviderOption compilerProviderOption = this[i];
					dictionary.Add(compilerProviderOption.Name, compilerProviderOption.Value);
				}
				return dictionary;
			}
		}

		// Token: 0x170005C4 RID: 1476
		public CompilerProviderOption this[int index]
		{
			get
			{
				return (CompilerProviderOption)base.BaseGet(index);
			}
		}

		// Token: 0x170005C5 RID: 1477
		public CompilerProviderOption this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					CompilerProviderOption compilerProviderOption = (CompilerProviderOption)obj;
					if (compilerProviderOption.Name == name)
					{
						return compilerProviderOption;
					}
				}
				return null;
			}
		}

		// Token: 0x04000EA9 RID: 3753
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
