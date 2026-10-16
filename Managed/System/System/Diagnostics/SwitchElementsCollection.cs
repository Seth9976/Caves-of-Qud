using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000229 RID: 553
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x170002A1 RID: 673
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0000390E File Offset: 0x00001B0E
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00046BD8 File Offset: 0x00044DD8
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00046BDF File Offset: 0x00044DDF
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}
	}
}
