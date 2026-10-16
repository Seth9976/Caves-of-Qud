using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021E RID: 542
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00045910 File Offset: 0x00043B10
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00045918 File Offset: 0x00043B18
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}
