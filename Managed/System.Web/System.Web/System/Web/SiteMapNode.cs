using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Resources;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web
{
	/// <summary>Represents a node in the hierarchical site map structure such as that described by the <see cref="T:System.Web.SiteMap" /> class and classes that implement the abstract <see cref="T:System.Web.SiteMapProvider" /> class.</summary>
	// Token: 0x020000D1 RID: 209
	public class SiteMapNode : IHierarchyData, INavigateUIData, ICloneable
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00002050 File Offset: 0x00000250
		private SiteMapNode()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class, using the specified <paramref name="key" /> to identify the page that the node represents and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B13 RID: 2835 RVA: 0x0001D84C File Offset: 0x0001BA4C
		public SiteMapNode(SiteMapProvider provider, string key)
			: this(provider, key, null, null, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B14 RID: 2836 RVA: 0x0001D868 File Offset: 0x0001BA68
		public SiteMapNode(SiteMapProvider provider, string key, string url)
			: this(provider, key, url, null, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B15 RID: 2837 RVA: 0x0001D884 File Offset: 0x0001BA84
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title)
			: this(provider, key, url, title, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title and description, and the site map provider that manages the node.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key.</param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <param name="description">A description of the page that the node represents. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B16 RID: 2838 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description)
			: this(provider, key, url, title, description, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified site map provider that manages the node, URL, title, description, roles, additional attributes, and explicit and implicit resource keys for localization.</summary>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
		/// <param name="key">A provider-specific lookup key. </param>
		/// <param name="url">The URL of the page that the node represents within the site. </param>
		/// <param name="title">A label for the node, often displayed by navigation controls. </param>
		/// <param name="description">A description of the page that the node represents. </param>
		/// <param name="roles">An <see cref="T:System.Collections.IList" /> of roles that are allowed to view the page represented by the <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes used to initialize the <see cref="T:System.Web.SiteMapNode" />. </param>
		/// <param name="explicitResourceKeys">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of explicit resource keys used for localization. </param>
		/// <param name="implicitResourceKey">An implicit resource key used for localization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.Web.SiteMapProvider" /> is null. - or -<paramref name="key" /> is null.</exception>
		// Token: 0x06000B17 RID: 2839 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.provider = provider;
			this.key = key;
			this.url = url;
			this.title = title;
			this.description = description;
			this.roles = roles;
			this.attributes = attributes;
			this.resourceKeys = explicitResourceKeys;
			this.resourceKey = implicitResourceKey;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> object that is associated with the current node.</summary>
		/// <returns>A named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> for the current node.</returns>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control that the view is associated with.</param>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x06000B18 RID: 2840 RVA: 0x0001D938 File Offset: 0x0001BB38
		public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
		{
			return new SiteMapDataSourceView(owner, viewName, this);
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> object that is associated with the current node.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> for the current node.</returns>
		// Token: 0x06000B19 RID: 2841 RVA: 0x0001D942 File Offset: 0x0001BB42
		public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
		{
			return new SiteMapHierarchicalDataSourceView(this);
		}

		/// <summary>Gets a value indicating whether the specified site map node can be viewed by the user in the specified context.</summary>
		/// <returns>true if any one of the following conditions is met: the security trimming is enabled and the current user is a member of at least one of the roles allowing access to view the site map node; the current user is authorized specifically for the requested node's URL in the authorization element for the current application and the URL is located within the directory structure for the application; the current thread has an associated <see cref="T:System.Security.Principal.WindowsIdentity" /> that has file access to the requested node's URL and the URL is located within the directory structure for the application; or security trimming is not enabled and therefore any user is allowed to view the site map node; otherwise, false.</returns>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified context is null.</exception>
		// Token: 0x06000B1A RID: 2842 RVA: 0x0001D94A File Offset: 0x0001BB4A
		public virtual bool IsAccessibleToUser(HttpContext context)
		{
			return this.provider.IsAccessibleToUser(context, this);
		}

		/// <summary>Converts the value of this instance of the <see cref="T:System.Web.SiteMapNode" /> class to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this <see cref="T:System.Web.SiteMapNode" />.</returns>
		// Token: 0x06000B1B RID: 2843 RVA: 0x0001D959 File Offset: 0x0001BB59
		public override string ToString()
		{
			return this.Title;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> has any child nodes.</summary>
		/// <returns>true if the node has children; otherwise, false.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0001D964 File Offset: 0x0001BB64
		public virtual bool HasChildNodes
		{
			get
			{
				SiteMapNodeCollection siteMapNodeCollection = this.ChildNodes;
				return siteMapNodeCollection != null && siteMapNodeCollection.Count > 0;
			}
		}

		/// <summary>Retrieves a read-only collection of all <see cref="T:System.Web.SiteMapNode" /> objects that are descendants of the calling node, regardless of the degree of separation.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that represents all the descendants of a <see cref="T:System.Web.SiteMapNode" /> within the scope of the current provider.</returns>
		// Token: 0x06000B1D RID: 2845 RVA: 0x0001D988 File Offset: 0x0001BB88
		public SiteMapNodeCollection GetAllNodes()
		{
			SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
			this.GetAllNodesRecursive(siteMapNodeCollection);
			return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		private void GetAllNodesRecursive(SiteMapNodeCollection c)
		{
			SiteMapNodeCollection siteMapNodeCollection = this.ChildNodes;
			if (siteMapNodeCollection != null && siteMapNodeCollection.Count > 0)
			{
				c.AddRange(siteMapNodeCollection);
				foreach (object obj in siteMapNodeCollection)
				{
					((SiteMapNode)obj).GetAllNodesRecursive(c);
				}
			}
		}

		/// <summary>Gets a value indicating whether the current site map node is a child or a direct descendant of the specified node.</summary>
		/// <returns>true if the current node is a child or descendant of the specified node; otherwise, false.</returns>
		/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to check if the current node is a child or descendant of.</param>
		// Token: 0x06000B1F RID: 2847 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public virtual bool IsDescendantOf(SiteMapNode node)
		{
			for (SiteMapNode siteMapNode = this.ParentNode; siteMapNode != null; siteMapNode = siteMapNode.ParentNode)
			{
				if (siteMapNode == node)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the next <see cref="T:System.Web.SiteMapNode" /> node on the same hierarchical level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property (if one exists).</summary>
		/// <returns>The next <see cref="T:System.Web.SiteMapNode" />, serially, after the current one, under the parent node; otherwise, null, if no parent exists, there is no node that follows this one, or security trimming is enabled and the user cannot view the parent or next sibling nodes.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public virtual SiteMapNode NextSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num >= 0 && num < siblingNodes.Count - 1)
				{
					return (SiteMapNode)siblingNodes[num + 1];
				}
				return null;
			}
		}

		/// <summary>Gets the previous <see cref="T:System.Web.SiteMapNode" /> object on the same level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> object (if one exists).</summary>
		/// <returns>The previous <see cref="T:System.Web.SiteMapNode" />, serially, before the current one, under the parent node; otherwise, null, if no parent exists, there is no node before this one, or security trimming is enabled and the user cannot view the parent or previous sibling nodes.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		public virtual SiteMapNode PreviousSibling
		{
			get
			{
				IList siblingNodes = this.SiblingNodes;
				if (siblingNodes == null)
				{
					return null;
				}
				int num = siblingNodes.IndexOf(this);
				if (num > 0 && num < siblingNodes.Count)
				{
					return (SiteMapNode)siblingNodes[num - 1];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that is the parent of the current node.</summary>
		/// <returns>The parent <see cref="T:System.Web.SiteMapNode" />; otherwise, null, if security trimming is enabled and the user cannot view the parent node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001DABC File Offset: 0x0001BCBC
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0001DB06 File Offset: 0x0001BD06
		public virtual SiteMapNode ParentNode
		{
			get
			{
				if (this.parent != null)
				{
					return this.parent;
				}
				SiteMapProvider parentProvider = this.provider;
				for (;;)
				{
					this.parent = parentProvider.GetParentNode(this);
					if (this.parent != null)
					{
						break;
					}
					parentProvider = parentProvider.ParentProvider;
					if (parentProvider == null)
					{
						goto Block_3;
					}
				}
				return this.parent;
				Block_3:
				return null;
			}
			set
			{
				this.CheckWritable();
				this.parent = value;
			}
		}

		/// <summary>Gets or sets all the child nodes of the current <see cref="T:System.Web.SiteMapNode" /> object from the associated <see cref="T:System.Web.SiteMapProvider" /> provider.</summary>
		/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> of child nodes, if any exist for the current node; otherwise, null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001DB18 File Offset: 0x0001BD18
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001DB9A File Offset: 0x0001BD9A
		public virtual SiteMapNodeCollection ChildNodes
		{
			get
			{
				if (this.provider.SecurityTrimmingEnabled)
				{
					IPrincipal principal = HttpContext.Current.User;
					if ((this.user == null && this.user != principal) || (this.user != null && this.user != principal))
					{
						this.user = principal;
						this.childNodes = this.provider.GetChildNodes(this);
					}
				}
				else if (this.childNodes == null)
				{
					this.childNodes = this.provider.GetChildNodes(this);
				}
				return this.childNodes;
			}
			set
			{
				this.CheckWritable();
				this.user = null;
				this.childNodes = value;
			}
		}

		/// <summary>Gets the root node of the root provider in a site map provider hierarchy. If no provider hierarchy exists, the <see cref="P:System.Web.SiteMapNode.RootNode" /> property gets the root node of the current provider. </summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the site navigation structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The root node cannot be retrieved from the root provider.</exception>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		public virtual SiteMapNode RootNode
		{
			get
			{
				return this.provider.RootProvider.RootNode;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001DBC2 File Offset: 0x0001BDC2
		private SiteMapNodeCollection SiblingNodes
		{
			get
			{
				if (this.ParentNode != null)
				{
					return this.ParentNode.ChildNodes;
				}
				return null;
			}
		}

		/// <summary>Retrieves a localized string based on a <see cref="T:System.Web.SiteMapNode" /> attribute to localize, a default string to return if no resource is found, and a Boolean value indicating whether to throw an exception if no resource is found. </summary>
		/// <returns>A string representing the localized attribute.</returns>
		/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize. </param>
		/// <param name="defaultValue">The default value to return if a matching resource is not found.</param>
		/// <param name="throwIfNotFound">true to throw an <see cref="T:System.InvalidOperationException" />, if an explicit resource is defined for <paramref name="attributeName" />, <paramref name="defaultValue" /> is null, and a localized value is not found; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">A matching resource object was not found and <paramref name="throwIfNotFound" /> is true. </exception>
		// Token: 0x06000B28 RID: 2856 RVA: 0x0001DBDC File Offset: 0x0001BDDC
		protected string GetExplicitResourceString(string attributeName, string defaultValue, bool throwIfNotFound)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			if (this.resourceKeys != null)
			{
				string[] values = this.resourceKeys.GetValues(attributeName);
				if (values != null && values.Length == 2)
				{
					try
					{
						object globalResourceObject = HttpContext.GetGlobalResourceObject(values[0], values[1]);
						if (globalResourceObject is string)
						{
							return (string)globalResourceObject;
						}
					}
					catch (MissingManifestResourceException)
					{
					}
					if (throwIfNotFound && defaultValue == null)
					{
						throw new InvalidOperationException(string.Format("The resource object with classname '{0}' and key '{1}' was not found.", values[0], values[1]));
					}
					return defaultValue;
				}
			}
			return defaultValue;
		}

		/// <summary>Gets a localized string based on the attribute name and <see cref="P:System.Web.SiteMapProvider.ResourceKey" /> property that is specified by the <see cref="T:System.Web.SiteMapProvider" /> by which the <see cref="T:System.Web.SiteMapNode" /> is tracked.</summary>
		/// <returns>A string representing the localized attribute. The default is null.</returns>
		/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeName" /> is null. </exception>
		// Token: 0x06000B29 RID: 2857 RVA: 0x0001DC68 File Offset: 0x0001BE68
		protected string GetImplicitResourceString(string attributeName)
		{
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			string text = this.ResourceKey;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			try
			{
				object globalResourceObject = HttpContext.GetGlobalResourceObject(this.provider.ResourceKey, text + "." + attributeName);
				if (globalResourceObject is string)
				{
					return (string)globalResourceObject;
				}
			}
			catch (MissingManifestResourceException)
			{
			}
			return null;
		}

		/// <summary>Gets or sets a custom attribute from the <see cref="P:System.Web.SiteMapNode.Attributes" /> collection or a resource string based on the specified key.</summary>
		/// <returns>A custom attribute or resource string identified by <paramref name="key" />; otherwise, null.</returns>
		/// <param name="key">A string that identifies the attribute or resource string to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FA RID: 1018
		public virtual string this[string key]
		{
			get
			{
				if (this.provider.EnableLocalization)
				{
					string text = this.GetImplicitResourceString(key);
					if (text == null)
					{
						text = this.GetExplicitResourceString(key, null, true);
					}
					if (text != null)
					{
						return text;
					}
				}
				if (this.attributes != null)
				{
					return this.attributes[key];
				}
				return null;
			}
			set
			{
				this.CheckWritable();
				if (this.attributes == null)
				{
					this.attributes = new NameValueCollection();
				}
				this.attributes[key] = value;
			}
		}

		/// <summary>Creates a new node that is a copy of the current node. For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		// Token: 0x06000B2C RID: 2860 RVA: 0x0001DD4E File Offset: 0x0001BF4E
		object ICloneable.Clone()
		{
			return this.Clone(false);
		}

		/// <summary>Creates a new node that is a copy of the current node.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		// Token: 0x06000B2D RID: 2861 RVA: 0x0001DD4E File Offset: 0x0001BF4E
		public virtual SiteMapNode Clone()
		{
			return this.Clone(false);
		}

		/// <summary>Creates a new copy that is a copy of the current node, optionally cloning all parent and ancestor nodes of the current node.</summary>
		/// <returns>A new node that is a copy of the current node.</returns>
		/// <param name="cloneParentNodes">true to clone all parent and ancestor nodes of the current node; otherwise, false.</param>
		// Token: 0x06000B2E RID: 2862 RVA: 0x0001DD58 File Offset: 0x0001BF58
		public virtual SiteMapNode Clone(bool cloneParentNodes)
		{
			SiteMapNode siteMapNode = new SiteMapNode();
			siteMapNode.provider = this.provider;
			siteMapNode.key = this.key;
			siteMapNode.url = this.url;
			siteMapNode.title = this.title;
			siteMapNode.description = this.description;
			if (this.roles != null)
			{
				siteMapNode.roles = new ArrayList(this.roles);
			}
			if (this.attributes != null)
			{
				siteMapNode.attributes = new NameValueCollection(this.attributes);
			}
			if (cloneParentNodes && this.ParentNode != null)
			{
				siteMapNode.parent = this.ParentNode.Clone(true);
			}
			return siteMapNode;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> is identical to the specified object. </summary>
		/// <returns>true if <paramref name="obj" /> is both a <see cref="T:System.Web.SiteMapNode" /> and identical to the current <see cref="T:System.Web.SiteMapNode" />; otherwise, false. </returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.Web.SiteMapNode" />. </param>
		// Token: 0x06000B2F RID: 2863 RVA: 0x0001DDF8 File Offset: 0x0001BFF8
		public override bool Equals(object obj)
		{
			SiteMapNode siteMapNode = obj as SiteMapNode;
			if (siteMapNode == null)
			{
				return false;
			}
			if (siteMapNode.key != this.key || siteMapNode.url != this.url || siteMapNode.title != this.title || siteMapNode.description != this.description)
			{
				return false;
			}
			if (this.roles == null || siteMapNode.roles == null)
			{
				if (this.roles != siteMapNode.roles)
				{
					return false;
				}
			}
			else
			{
				if (this.roles.Count != siteMapNode.roles.Count)
				{
					return false;
				}
				foreach (object obj2 in this.roles)
				{
					if (!siteMapNode.roles.Contains(obj2))
					{
						return false;
					}
				}
			}
			if (this.attributes == null || siteMapNode.attributes == null)
			{
				if (this.attributes != siteMapNode.attributes)
				{
					return false;
				}
			}
			else
			{
				if (this.attributes.Count != siteMapNode.attributes.Count)
				{
					return false;
				}
				foreach (object obj3 in this.attributes)
				{
					string text = (string)obj3;
					if (this.attributes[text] != siteMapNode.attributes[text])
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <returns>A 32-bit signed integer representing the hash code.</returns>
		// Token: 0x06000B30 RID: 2864 RVA: 0x0001DF98 File Offset: 0x0001C198
		public override int GetHashCode()
		{
			return (this.key + this.url + this.title + this.description).GetHashCode();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		private void CheckWritable()
		{
			if (this.readOnly)
			{
				throw new InvalidOperationException("Can't modify read-only node");
			}
		}

		/// <summary>Gets or sets a collection of additional attributes beyond the strongly typed properties that are defined for the <see cref="T:System.Web.SiteMapNode" /> class.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes for the <see cref="T:System.Web.SiteMapNode" /> beyond <see cref="P:System.Web.SiteMapNode.Title" />, <see cref="P:System.Web.SiteMapNode.Description" />, <see cref="P:System.Web.SiteMapNode.Url" />, and <see cref="P:System.Web.SiteMapNode.Roles" />; otherwise, null, if no attributes exist.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001DFD1 File Offset: 0x0001C1D1
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0001DFD9 File Offset: 0x0001C1D9
		protected NameValueCollection Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.CheckWritable();
				this.attributes = value;
			}
		}

		/// <summary>Gets or sets a description for the <see cref="T:System.Web.SiteMapNode" />. </summary>
		/// <returns>A string that represents a description of the node; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001DFE8 File Offset: 0x0001C1E8
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0001E039 File Offset: 0x0001C239
		[Localizable(true)]
		public virtual string Description
		{
			get
			{
				string text;
				if (this.provider.EnableLocalization)
				{
					text = this.GetImplicitResourceString("description");
					if (text == null)
					{
						text = this.GetExplicitResourceString("description", this.description, true);
					}
				}
				else
				{
					text = this.description;
				}
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.CheckWritable();
				this.description = value;
			}
		}

		/// <summary>Gets or sets the title of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
		/// <returns>A string that represents the title of the node. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001E048 File Offset: 0x0001C248
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001E099 File Offset: 0x0001C299
		[Localizable(true)]
		public virtual string Title
		{
			get
			{
				string text;
				if (this.provider.EnableLocalization)
				{
					text = this.GetImplicitResourceString("title");
					if (text == null)
					{
						text = this.GetExplicitResourceString("title", this.title, true);
					}
				}
				else
				{
					text = this.title;
				}
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.CheckWritable();
				this.title = value;
			}
		}

		/// <summary>Gets or sets the URL of the page that the <see cref="T:System.Web.SiteMapNode" /> object represents.</summary>
		/// <returns>The URL of the page that the node represents. The default is <see cref="F:System.String.Empty" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0001E0BE File Offset: 0x0001C2BE
		public virtual string Url
		{
			get
			{
				if (this.url == null)
				{
					return "";
				}
				return this.url;
			}
			set
			{
				this.CheckWritable();
				this.url = value;
			}
		}

		/// <summary>Gets or sets a collection of roles that are associated with the <see cref="T:System.Web.SiteMapNode" /> object, used during security trimming. </summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of roles.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001E0CD File Offset: 0x0001C2CD
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0001E0D5 File Offset: 0x0001C2D5
		public IList Roles
		{
			get
			{
				return this.roles;
			}
			set
			{
				this.CheckWritable();
				this.roles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the site map node can be modified.</summary>
		/// <returns>true if the site map node can be modified; otherwise, false.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		/// <summary>Gets or sets the resource key that is used to localize the <see cref="T:System.Web.SiteMapNode" />.</summary>
		/// <returns>A string containing the resource key name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001E0F5 File Offset: 0x0001C2F5
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0001E0FD File Offset: 0x0001C2FD
		public string ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
			set
			{
				if (this.ReadOnly)
				{
					throw new InvalidOperationException("The node is read-only.");
				}
				this.resourceKey = value;
			}
		}

		/// <summary>Gets a string representing a lookup key for a site map node.</summary>
		/// <returns>A string representing a lookup key.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001E119 File Offset: 0x0001C319
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SiteMapProvider" /> provider that the <see cref="T:System.Web.SiteMapNode" /> object is tracked by.</summary>
		/// <returns>The <see cref="T:System.Web.SiteMapProvider" /> that the <see cref="T:System.Web.SiteMapNode" /> is tracked by. </returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001E121 File Offset: 0x0001C321
		public SiteMapProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		/// <summary>Retrieves the hierarchical children data items of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetChildren" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the immediate children of the current item in the hierarchy.</returns>
		// Token: 0x06000B42 RID: 2882 RVA: 0x0001E129 File Offset: 0x0001C329
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return this.ChildNodes;
		}

		/// <summary>Retrieves the hierarchical parent of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetParent" />.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the parent of the current item in the hierarchy.</returns>
		// Token: 0x06000B43 RID: 2883 RVA: 0x0001E131 File Offset: 0x0001C331
		IHierarchyData IHierarchyData.GetParent()
		{
			return this.ParentNode;
		}

		/// <summary>Gets a value that indicates whether the current <see cref="T:System.Web.SiteMapNode" /> object has any child nodes. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.HasChildren" />.</summary>
		/// <returns>true if the node has child nodes; otherwise, false.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001E139 File Offset: 0x0001C339
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this.HasChildNodes;
			}
		}

		/// <summary>Gets the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Item" />.</summary>
		/// <returns>An hierarchical data node object.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00002058 File Offset: 0x00000258
		object IHierarchyData.Item
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the path of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Path" />.</summary>
		/// <returns>The path of the data item.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0001E141 File Offset: 0x0001C341
		string IHierarchyData.Path
		{
			get
			{
				return this.Url;
			}
		}

		/// <summary>Gets a string that represents the type name of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Type" />.</summary>
		/// <returns>The string named "SiteMapNode".</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0001E149 File Offset: 0x0001C349
		string IHierarchyData.Type
		{
			get
			{
				return "SiteMapNode";
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Name" />.</summary>
		/// <returns>Text that is displayed for a node of a navigation control; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Title" /> is set for the node.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001D959 File Offset: 0x0001BB59
		string INavigateUIData.Name
		{
			get
			{
				return this.Title;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Url" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.NavigateUrl" />.</summary>
		/// <returns>The URL to navigate to when the node is clicked; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Url" /> is set for the node.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0001E141 File Offset: 0x0001C341
		string INavigateUIData.NavigateUrl
		{
			get
			{
				return this.Url;
			}
		}

		/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Value" />.</summary>
		/// <returns>A value that is not displayed; otherwise, <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001D959 File Offset: 0x0001BB59
		string INavigateUIData.Value
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x04001095 RID: 4245
		private SiteMapProvider provider;

		// Token: 0x04001096 RID: 4246
		private string key;

		// Token: 0x04001097 RID: 4247
		private string url;

		// Token: 0x04001098 RID: 4248
		private string title;

		// Token: 0x04001099 RID: 4249
		private string description;

		// Token: 0x0400109A RID: 4250
		private IList roles;

		// Token: 0x0400109B RID: 4251
		private NameValueCollection attributes;

		// Token: 0x0400109C RID: 4252
		private NameValueCollection resourceKeys;

		// Token: 0x0400109D RID: 4253
		private bool readOnly;

		// Token: 0x0400109E RID: 4254
		private string resourceKey;

		// Token: 0x0400109F RID: 4255
		private SiteMapNode parent;

		// Token: 0x040010A0 RID: 4256
		private SiteMapNodeCollection childNodes;

		// Token: 0x040010A1 RID: 4257
		private IPrincipal user;
	}
}
