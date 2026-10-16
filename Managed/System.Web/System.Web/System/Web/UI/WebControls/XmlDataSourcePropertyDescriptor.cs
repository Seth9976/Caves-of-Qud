using System;
using System.ComponentModel;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000450 RID: 1104
	internal class XmlDataSourcePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600334D RID: 13133 RVA: 0x00087659 File Offset: 0x00085859
		public XmlDataSourcePropertyDescriptor(string name, bool readOnly)
			: base(name, null)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool CanResetValue(object o)
		{
			return false;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ResetValue(object o)
		{
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0008766C File Offset: 0x0008586C
		public override object GetValue(object o)
		{
			XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor = o as XmlDataSourceNodeDescriptor;
			if (xmlDataSourceNodeDescriptor != null && xmlDataSourceNodeDescriptor.Node.Attributes != null)
			{
				XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[this.Name];
				if (xmlAttribute != null)
				{
					return xmlAttribute.Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000876B8 File Offset: 0x000858B8
		public override void SetValue(object o, object value)
		{
			XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor = o as XmlDataSourceNodeDescriptor;
			if (xmlDataSourceNodeDescriptor != null && xmlDataSourceNodeDescriptor.Node.Attributes != null)
			{
				XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[this.Name];
				if (xmlAttribute != null)
				{
					xmlAttribute.Value = value.ToString();
				}
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x00087702 File Offset: 0x00085902
		public override bool ShouldSerializeValue(object o)
		{
			return o is XmlNode;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x0008770D File Offset: 0x0008590D
		public override Type ComponentType
		{
			get
			{
				return typeof(XmlDataSourceNodeDescriptor);
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x00087719 File Offset: 0x00085919
		public override bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x00033922 File Offset: 0x00031B22
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x04001CCF RID: 7375
		private bool readOnly;
	}
}
