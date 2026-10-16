using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000222 RID: 546
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x1700028C RID: 652
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000460DB File Offset: 0x000442DB
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000460E2 File Offset: 0x000442E2
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000460F4 File Offset: 0x000442F4
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}
	}
}
