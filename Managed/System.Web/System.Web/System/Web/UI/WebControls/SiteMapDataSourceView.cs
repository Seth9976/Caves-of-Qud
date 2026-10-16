using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a strongly typed <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object for the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control.</summary>
	// Token: 0x02000404 RID: 1028
	public class SiteMapDataSourceView : DataSourceView
	{
		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> class, adding the single specified node to the internal collection of nodes.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> is associated with.</param>
		/// <param name="name">The name of the view.</param>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the internal <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
		// Token: 0x06002D99 RID: 11673 RVA: 0x00076AA3 File Offset: 0x00074CA3
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNode node)
			: this(owner, name, new SiteMapNodeCollection(node))
		{
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> class, setting the internal collection of nodes to the specified node collection.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> is associated with.</param>
		/// <param name="name">The name of the view.</param>
		/// <param name="collection">A <see cref="T:System.Web.SiteMapNodeCollection" /> of nodes that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> provides a view of. </param>
		// Token: 0x06002D9A RID: 11674 RVA: 0x00076AB3 File Offset: 0x00074CB3
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNodeCollection collection)
			: base(owner, name)
		{
			this.collection = collection;
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNodeCollection" /> that represents the site navigation structure for the current user. </returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SiteMapDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		// Token: 0x06002D9B RID: 11675 RVA: 0x0006F666 File Offset: 0x0006D866
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNodeCollection" /> that represents the site navigation structure for the current user.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SiteMapDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		// Token: 0x06002D9C RID: 11676 RVA: 0x00076AC4 File Offset: 0x00074CC4
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			return this.collection;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
		// Token: 0x06002D9D RID: 11677 RVA: 0x00076AD3 File Offset: 0x00074CD3
		protected override void OnDataSourceViewChanged(EventArgs e)
		{
			base.OnDataSourceViewChanged(e);
		}

		// Token: 0x04001B7E RID: 7038
		private SiteMapNodeCollection collection;
	}
}
