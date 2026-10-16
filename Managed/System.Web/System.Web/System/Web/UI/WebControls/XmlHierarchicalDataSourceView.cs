using System;
using System.Xml;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data view on an XML node or collection of XML nodes for an <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	// Token: 0x02000452 RID: 1106
	public class XmlHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x0008788B File Offset: 0x00085A8B
		internal XmlHierarchicalDataSourceView(XmlNodeList nodeList)
		{
			this.nodeList = nodeList;
		}

		/// <summary>Gets a list of the data items from the underlying data source.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of data items based on the hierarchical level of the current view.</returns>
		// Token: 0x0600335B RID: 13147 RVA: 0x0008789A File Offset: 0x00085A9A
		public override IHierarchicalEnumerable Select()
		{
			return new XmlHierarchicalEnumerable(this.nodeList);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal XmlHierarchicalDataSourceView()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001CD2 RID: 7378
		private XmlNodeList nodeList;
	}
}
