using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044F RID: 1103
	internal class XmlDataSourceNodeDescriptor : ICustomTypeDescriptor, IXPathNavigable
	{
		// Token: 0x0600333E RID: 13118 RVA: 0x0008759F File Offset: 0x0008579F
		public XmlDataSourceNodeDescriptor(XmlNode node)
		{
			this.node = node;
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000875AE File Offset: 0x000857AE
		public XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000875B6 File Offset: 0x000857B6
		public AttributeCollection GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000875BD File Offset: 0x000857BD
		public string GetClassName()
		{
			return "XmlDataSourceNodeDescriptor";
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public string GetComponentName()
		{
			return null;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public TypeConverter GetConverter()
		{
			return null;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public EventDescriptor GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public EventDescriptorCollection GetEvents()
		{
			return null;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public EventDescriptorCollection GetEvents(Attribute[] arr)
		{
			return null;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000875C4 File Offset: 0x000857C4
		public PropertyDescriptorCollection GetProperties()
		{
			if (this.node.Attributes != null)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.node.Attributes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new XmlDataSourcePropertyDescriptor(this.node.Attributes[i].Name, this.node.IsReadOnly);
				}
				return new PropertyDescriptorCollection(array);
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x00087637 File Offset: 0x00085837
		public PropertyDescriptorCollection GetProperties(Attribute[] arr)
		{
			return this.GetProperties();
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0008763F File Offset: 0x0008583F
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlDataSourcePropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0008764C File Offset: 0x0008584C
		public XPathNavigator CreateNavigator()
		{
			return this.node.CreateNavigator();
		}

		// Token: 0x04001CCE RID: 7374
		private XmlNode node;
	}
}
