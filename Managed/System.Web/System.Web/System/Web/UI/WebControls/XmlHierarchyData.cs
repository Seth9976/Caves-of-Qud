using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000454 RID: 1108
	internal class XmlHierarchyData : IHierarchyData, ICustomTypeDescriptor
	{
		// Token: 0x06003360 RID: 13152 RVA: 0x00087940 File Offset: 0x00085B40
		internal XmlHierarchyData(XmlNode item)
		{
			this.item = item;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x0008794F File Offset: 0x00085B4F
		public override string ToString()
		{
			return this.item.Name;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000875B6 File Offset: 0x000857B6
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0008795C File Offset: 0x00085B5C
		string ICustomTypeDescriptor.GetClassName()
		{
			return "XmlHierarchyData";
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x00003BF6 File Offset: 0x00001DF6
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x00003BF6 File Offset: 0x00001DF6
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x00003BF6 File Offset: 0x00001DF6
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x00087963 File Offset: 0x00085B63
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Name##");
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x00003BF6 File Offset: 0x00001DF6
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x00003BF6 File Offset: 0x00001DF6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x00003BF6 File Offset: 0x00001DF6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
		{
			return null;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x00087975 File Offset: 0x00085B75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x00087980 File Offset: 0x00085B80
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrFilter)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Name##"));
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##Value##"));
			arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, "##InnerText##"));
			if (this.item.Attributes != null)
			{
				foreach (object obj in this.item.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					arrayList.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(this.item, xmlAttribute.Name));
				}
			}
			return new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)));
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x00087A64 File Offset: 0x00085C64
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlHierarchyData.XmlHierarchyDataPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x00087A71 File Offset: 0x00085C71
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return new XmlHierarchicalEnumerable(this.item.ChildNodes);
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x00087A83 File Offset: 0x00085C83
		IHierarchyData IHierarchyData.GetParent()
		{
			return new XmlHierarchyData(this.item.ParentNode);
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x00087A95 File Offset: 0x00085C95
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this.item.HasChildNodes;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x00087AA2 File Offset: 0x00085CA2
		object IHierarchyData.Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x00087AAC File Offset: 0x00085CAC
		string IHierarchyData.Path
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				XmlNode parentNode = this.item;
				do
				{
					int num = 1;
					XmlNode xmlNode = parentNode.PreviousSibling;
					while (xmlNode != null)
					{
						xmlNode = xmlNode.PreviousSibling;
						num++;
					}
					stringBuilder.Insert(0, "/*[position()=" + num.ToString() + "]");
					parentNode = parentNode.ParentNode;
				}
				while (parentNode != null && !(parentNode is XmlDocument));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x0008794F File Offset: 0x00085B4F
		string IHierarchyData.Type
		{
			get
			{
				return this.item.Name;
			}
		}

		// Token: 0x04001CD4 RID: 7380
		private XmlNode item;

		// Token: 0x02000455 RID: 1109
		private class XmlHierarchyDataPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06003374 RID: 13172 RVA: 0x00087B15 File Offset: 0x00085D15
			public XmlHierarchyDataPropertyDescriptor(XmlNode xmlNode, string name)
				: base(name, null)
			{
				this.xmlNode = xmlNode;
				this.name = name;
			}

			// Token: 0x06003375 RID: 13173 RVA: 0x00008A4D File Offset: 0x00006C4D
			public override bool CanResetValue(object o)
			{
				return false;
			}

			// Token: 0x06003376 RID: 13174 RVA: 0x00003946 File Offset: 0x00001B46
			public override void ResetValue(object o)
			{
			}

			// Token: 0x06003377 RID: 13175 RVA: 0x00087B30 File Offset: 0x00085D30
			public override object GetValue(object o)
			{
				if (o is XmlHierarchyData)
				{
					string text = this.name;
					if (text == "##Name##")
					{
						return this.xmlNode.Name;
					}
					if (text == "##Value##")
					{
						return this.xmlNode.Value;
					}
					if (text == "##InnerText##")
					{
						return this.xmlNode.InnerText;
					}
					if (text == null)
					{
						return string.Empty;
					}
					if (this.xmlNode.Attributes != null)
					{
						XmlAttribute xmlAttribute = this.xmlNode.Attributes[this.name];
						if (xmlAttribute != null)
						{
							return xmlAttribute.Value;
						}
					}
				}
				return string.Empty;
			}

			// Token: 0x06003378 RID: 13176 RVA: 0x00087BD8 File Offset: 0x00085DD8
			public override void SetValue(object o, object value)
			{
				if (o is XmlHierarchyData)
				{
					string text = this.name;
					if (!(text == "##Name##"))
					{
						if (text == "##Value##")
						{
							this.xmlNode.Value = value.ToString();
							return;
						}
						if (text == "##InnerText##")
						{
							this.xmlNode.InnerText = value.ToString();
							return;
						}
						if (text != null && this.xmlNode.Attributes != null)
						{
							XmlAttribute xmlAttribute = this.xmlNode.Attributes[this.name];
							if (xmlAttribute != null)
							{
								xmlAttribute.Value = value.ToString();
							}
						}
					}
				}
			}

			// Token: 0x06003379 RID: 13177 RVA: 0x00087702 File Offset: 0x00085902
			public override bool ShouldSerializeValue(object o)
			{
				return o is XmlNode;
			}

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x0600337A RID: 13178 RVA: 0x00087C7A File Offset: 0x00085E7A
			public override Type ComponentType
			{
				get
				{
					return typeof(XmlHierarchyData);
				}
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x0600337B RID: 13179 RVA: 0x00087C86 File Offset: 0x00085E86
			public override bool IsReadOnly
			{
				get
				{
					return this.xmlNode.IsReadOnly;
				}
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x0600337C RID: 13180 RVA: 0x00033922 File Offset: 0x00031B22
			public override Type PropertyType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x04001CD5 RID: 7381
			private string name;

			// Token: 0x04001CD6 RID: 7382
			private XmlNode xmlNode;
		}
	}
}
