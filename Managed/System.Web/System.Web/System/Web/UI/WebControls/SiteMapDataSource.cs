using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a data source control that Web server controls and other controls can use to bind to hierarchical site map data.</summary>
	// Token: 0x02000403 RID: 1027
	[PersistChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true)]
	[ToolboxBitmap("")]
	public class SiteMapDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		/// <summary>Retrieves a collection of named views for the data source control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> method returns an <see cref="T:System.Collections.ICollection" /> with one <see cref="F:System.String.Empty" /> element.</returns>
		// Token: 0x06002D7E RID: 11646 RVA: 0x000766C4 File Offset: 0x000748C4
		public virtual ICollection GetViewNames()
		{
			return SiteMapDataSource.emptyNames;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x06002D7F RID: 11647 RVA: 0x000766CB File Offset: 0x000748CB
		IList IListSource.GetList()
		{
			return this.GetList();
		}

		/// <summary>Gets a value that indicates whether the collection is a collection of <see cref="T:System.Collections.IList" /> objects.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is associated with one or more <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects; otherwise, false.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000766D3 File Offset: 0x000748D3
		bool IListSource.ContainsListCollection
		{
			get
			{
				return this.ContainsListCollection;
			}
		}

		/// <summary>Retrieves a list of data source controls that can be used as sources of lists of data.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x06002D81 RID: 11649 RVA: 0x0002FFE6 File Offset: 0x0002E1E6
		public virtual IList GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>Gets a value indicating whether the data source control contains a collection of data source view objects.</summary>
		/// <returns>true if the data source control contains a collection of data source view objects; otherwise, false.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x00030050 File Offset: 0x0002E250
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetView(System.String)" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewName">The URL of the root node of the view. </param>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
		// Token: 0x06002D83 RID: 11651 RVA: 0x000766DB File Offset: 0x000748DB
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> returns a collection containing one element set to <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000766E4 File Offset: 0x000748E4
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls.</summary>
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06002D85 RID: 11653 RVA: 0x000766EC File Offset: 0x000748EC
		// (remove) Token: 0x06002D86 RID: 11654 RVA: 0x000766F5 File Offset: 0x000748F5
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				((IHierarchicalDataSource)this).DataSourceChanged += value;
			}
			remove
			{
				((IHierarchicalDataSource)this).DataSourceChanged -= value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.SiteMapProvider" /> object that is associated with the data source control.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> that is associated with the data source control; otherwise, if no provider is explicitly set, the default site map provider.</returns>
		/// <exception cref="T:System.Web.HttpException">The provider named by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.SiteMapProvider" /> is not available.- or -No default provider is configured for the site.</exception>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x00076700 File Offset: 0x00074900
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x0007677F File Offset: 0x0007497F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public SiteMapProvider Provider
		{
			get
			{
				if (this.provider == null)
				{
					if (this.SiteMapProvider.Length == 0)
					{
						this.provider = SiteMap.Provider;
						if (this.provider == null)
						{
							throw new HttpException("There is no default provider configured for the site.");
						}
					}
					else
					{
						this.provider = SiteMap.Providers[this.SiteMapProvider];
						if (this.provider == null)
						{
							throw new HttpException("SiteMap provider '" + this.SiteMapProvider + "' not found.");
						}
					}
				}
				return this.provider;
			}
			set
			{
				this.provider = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the site map provider that the data source binds to.</summary>
		/// <returns>The name of the site map provider that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> binds to. By default, the value is <see cref="F:System.String.Empty" />, and the default site map provider for the site is used.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00076793 File Offset: 0x00074993
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x000767AA File Offset: 0x000749AA
		[DefaultValue("")]
		public virtual string SiteMapProvider
		{
			get
			{
				return this.ViewState.GetString("SiteMapProvider", "");
			}
			set
			{
				this.ViewState["SiteMapProvider"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a node in the site map that the data source then uses as a reference point to retrieve nodes from a hierarchical site map.</summary>
		/// <returns>The URL of a node in the site map. The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> retrieves the identified <see cref="T:System.Web.SiteMapNode" /> and any child nodes from the site map. The default is an <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000767C8 File Offset: 0x000749C8
		// (set) Token: 0x06002D8C RID: 11660 RVA: 0x000767DF File Offset: 0x000749DF
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string StartingNodeUrl
		{
			get
			{
				return this.ViewState.GetString("StartingNodeUrl", "");
			}
			set
			{
				this.ViewState["StartingNodeUrl"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a positive or negative integer offset from the starting node that determines the root hierarchy that is exposed by the data source control.</summary>
		/// <returns>The default is 0, which indicates that the root hierarchy exposed by the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is the same as the starting node.</returns>
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000767FD File Offset: 0x000749FD
		// (set) Token: 0x06002D8E RID: 11662 RVA: 0x00076810 File Offset: 0x00074A10
		[DefaultValue(0)]
		public virtual int StartingNodeOffset
		{
			get
			{
				return this.ViewState.GetInt("StartingNodeOffset", 0);
			}
			set
			{
				this.ViewState["StartingNodeOffset"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the site map node tree is retrieved using the node that represents the current page.</summary>
		/// <returns>true if the node tree is retrieved relative to the current page; otherwise, false. The default is false. </returns>
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x00076833 File Offset: 0x00074A33
		// (set) Token: 0x06002D90 RID: 11664 RVA: 0x00076846 File Offset: 0x00074A46
		[DefaultValue(false)]
		public virtual bool StartFromCurrentNode
		{
			get
			{
				return this.ViewState.GetBool("StartFromCurrentNode", false);
			}
			set
			{
				this.ViewState["StartFromCurrentNode"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the starting node is retrieved and displayed. </summary>
		/// <returns>true if the starting node is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x00076869 File Offset: 0x00074A69
		// (set) Token: 0x06002D92 RID: 11666 RVA: 0x0007687C File Offset: 0x00074A7C
		[DefaultValue(true)]
		public virtual bool ShowStartingNode
		{
			get
			{
				return this.ViewState.GetBool("ShowStartingNode", true);
			}
			set
			{
				this.ViewState["ShowStartingNode"] = value;
				this.OnDataSourceChanged(EventArgs.Empty);
			}
		}

		/// <summary>Retrieves a named view on the site map data of the site map provider according to the starting node and other properties of the data source.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewName">The name of the data source view to retrieve.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.Provider" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000768A0 File Offset: 0x00074AA0
		public virtual DataSourceView GetView(string viewName)
		{
			SiteMapNode startNode = this.GetStartNode(viewName);
			if (startNode == null)
			{
				return new SiteMapDataSourceView(this, viewName, SiteMapNodeCollection.EmptyList);
			}
			if (this.ShowStartingNode)
			{
				return new SiteMapDataSourceView(this, viewName, startNode);
			}
			return new SiteMapDataSourceView(this, viewName, startNode.ChildNodes);
		}

		/// <summary>Retrieves a single view on the site map data for the <see cref="T:System.Web.SiteMapProvider" /> object according to the starting node and other properties of the data source.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, starting with the node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is false.</returns>
		/// <param name="viewPath">The URL of the starting node, specified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />. </param>
		/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
		// Token: 0x06002D94 RID: 11668 RVA: 0x000768E4 File Offset: 0x00074AE4
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			SiteMapNode startNode = this.GetStartNode(viewPath);
			if (startNode == null)
			{
				return new SiteMapHierarchicalDataSourceView(SiteMapNodeCollection.EmptyList);
			}
			if (this.ShowStartingNode || startNode == null)
			{
				return new SiteMapHierarchicalDataSourceView(startNode);
			}
			return new SiteMapHierarchicalDataSourceView(startNode.ChildNodes);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00076924 File Offset: 0x00074B24
		[global::System.MonoTODO("handle StartNodeOffsets > 0")]
		private SiteMapNode GetStartNode(string viewPath)
		{
			if (viewPath != null && viewPath.Length != 0)
			{
				string text = this.MapUrl(this.StartingNodeUrl);
				return this.Provider.FindSiteMapNode(text);
			}
			SiteMapNode siteMapNode;
			if (this.StartFromCurrentNode)
			{
				if (this.StartingNodeUrl.Length != 0)
				{
					throw new InvalidOperationException("StartingNodeUrl can't be set if StartFromCurrentNode is set to true.");
				}
				siteMapNode = SiteMap.CurrentNode;
			}
			else if (this.StartingNodeUrl.Length != 0)
			{
				string text2 = this.MapUrl(this.StartingNodeUrl);
				SiteMapNode siteMapNode2 = this.Provider.FindSiteMapNode(text2);
				if (siteMapNode2 == null)
				{
					throw new ArgumentException("Can't find a site map node for the url: " + this.StartingNodeUrl);
				}
				siteMapNode = siteMapNode2;
			}
			else
			{
				siteMapNode = this.Provider.RootNode;
			}
			if (siteMapNode == null)
			{
				return null;
			}
			if (this.StartingNodeOffset < 0)
			{
				for (int i = this.StartingNodeOffset; i < 0; i++)
				{
					if (siteMapNode.ParentNode == null)
					{
						break;
					}
					siteMapNode = siteMapNode.ParentNode;
				}
			}
			else if (this.StartingNodeOffset > 0)
			{
				List<SiteMapNode> list = new List<SiteMapNode>();
				SiteMapNode siteMapNode3 = this.Provider.CurrentNode;
				while (siteMapNode3 != null && siteMapNode3 != siteMapNode)
				{
					list.Insert(0, siteMapNode3);
					siteMapNode3 = siteMapNode3.ParentNode;
				}
				if (siteMapNode3 == siteMapNode && this.StartingNodeOffset <= list.Count)
				{
					siteMapNode = list[this.StartingNodeOffset - 1];
				}
			}
			return siteMapNode;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00076A5C File Offset: 0x00074C5C
		private string MapUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return string.Empty;
			}
			if (UrlUtils.IsRelativeUrl(url))
			{
				return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
			}
			return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
		}

		// Token: 0x04001B7C RID: 7036
		private static string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001B7D RID: 7037
		private SiteMapProvider provider;
	}
}
