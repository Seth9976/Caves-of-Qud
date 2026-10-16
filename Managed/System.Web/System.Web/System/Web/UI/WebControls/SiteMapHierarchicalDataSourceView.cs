using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a data view on a site map node or collection of nodes for a <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control.</summary>
	// Token: 0x02000405 RID: 1029
	public class SiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, adding the specified node to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
		/// <param name="node">A <see cref="T:System.Web.SiteMapNode" /> that the data source view represents.</param>
		// Token: 0x06002D9E RID: 11678 RVA: 0x00076ADC File Offset: 0x00074CDC
		public SiteMapHierarchicalDataSourceView(SiteMapNode node)
			: this(new SiteMapNodeCollection(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, setting the specified collection to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
		/// <param name="collection">A <see cref="T:System.Web.SiteMapNodeCollection" /> that the data source view represents.</param>
		// Token: 0x06002D9F RID: 11679 RVA: 0x00076AEA File Offset: 0x00074CEA
		public SiteMapHierarchicalDataSourceView(SiteMapNodeCollection collection)
		{
			this.collection = collection;
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of site map nodes.</returns>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x00076AF9 File Offset: 0x00074CF9
		public override IHierarchicalEnumerable Select()
		{
			return this.collection;
		}

		// Token: 0x04001B7F RID: 7039
		private SiteMapNodeCollection collection;
	}
}
