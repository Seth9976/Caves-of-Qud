using System;
using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000453 RID: 1107
	internal class XmlHierarchicalEnumerable : IHierarchicalEnumerable, IEnumerable
	{
		// Token: 0x0600335D RID: 13149 RVA: 0x000878A7 File Offset: 0x00085AA7
		internal XmlHierarchicalEnumerable(XmlNodeList nodeList)
		{
			this.nodeList = nodeList;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000878B8 File Offset: 0x00085AB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			ArrayList arrayList = new ArrayList(this.nodeList.Count);
			foreach (object obj in this.nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					arrayList.Add(new XmlHierarchyData(xmlNode));
				}
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x00087938 File Offset: 0x00085B38
		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return (IHierarchyData)enumeratedItem;
		}

		// Token: 0x04001CD3 RID: 7379
		private XmlNodeList nodeList;
	}
}
