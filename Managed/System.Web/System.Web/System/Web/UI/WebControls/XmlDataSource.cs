using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Caching;
using System.Xml;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an XML data source to data-bound controls.</summary>
	// Token: 0x0200044E RID: 1102
	[DefaultProperty("DataFile")]
	[DefaultEvent("Transforming")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[WebSysDescription("Connect to an XML file.")]
	[ToolboxBitmap("")]
	[Designer("System.Web.UI.Design.WebControls.XmlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class XmlDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
	{
		/// <summary>For a description of this member, see <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" />.</summary>
		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06003312 RID: 13074 RVA: 0x000766EC File Offset: 0x000748EC
		// (remove) Token: 0x06003313 RID: 13075 RVA: 0x000766F5 File Offset: 0x000748F5
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

		/// <summary>Occurs before the style sheet that is defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> property or identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> property is applied to XML data.</summary>
		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06003314 RID: 13076 RVA: 0x00086FA0 File Offset: 0x000851A0
		// (remove) Token: 0x06003315 RID: 13077 RVA: 0x00086FB3 File Offset: 0x000851B3
		public event EventHandler Transforming
		{
			add
			{
				base.Events.AddHandler(XmlDataSource.EventTransforming, value);
			}
			remove
			{
				base.Events.RemoveHandler(XmlDataSource.EventTransforming, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.XmlDataSource.Transforming" /> event before the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control performs an XSLT transformation on its XML data.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003316 RID: 13078 RVA: 0x00086FC8 File Offset: 0x000851C8
		protected virtual void OnTransforming(EventArgs e)
		{
			EventHandler eventHandler = base.Events[XmlDataSource.EventTransforming] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Loads the XML data into memory, either directly from the underlying data storage or from the cache, and returns it in the form of an <see cref="T:System.Xml.XmlDataDocument" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDataDocument" /> object that represents the XML specified in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or in the file identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property, with any transformations and <see cref="P:System.Web.UI.WebControls.XmlDataSource.XPath" /> queries applied.</returns>
		/// <exception cref="T:System.InvalidOperationException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
		/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not support caching when client impersonation is enabled.</exception>
		/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
		// Token: 0x06003317 RID: 13079 RVA: 0x00086FF8 File Offset: 0x000851F8
		public XmlDocument GetXmlDocument()
		{
			if (this._documentNeedsUpdate)
			{
				this.UpdateXml();
			}
			if (this.xmlDocument == null && this.EnableCaching)
			{
				this.xmlDocument = this.GetXmlDocumentFromCache();
			}
			if (this.xmlDocument == null)
			{
				this.xmlDocument = this.LoadXmlDocument();
				this.UpdateCache();
			}
			return this.xmlDocument;
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x00087050 File Offset: 0x00085250
		[global::System.MonoTODO("schema")]
		private XmlDocument LoadXmlDocument()
		{
			XmlDocument xmlDocument = this.LoadFileOrData(this.DataFile, this.Data);
			if (string.IsNullOrEmpty(this.TransformFile) && string.IsNullOrEmpty(this.Transform))
			{
				return xmlDocument;
			}
			XslTransform xslTransform = new XslTransform();
			XmlDocument xmlDocument2 = this.LoadFileOrData(this.TransformFile, this.Transform);
			xslTransform.Load(xmlDocument2);
			this.OnTransforming(EventArgs.Empty);
			XmlDocument xmlDocument3 = new XmlDocument();
			xmlDocument3.Load(xslTransform.Transform(xmlDocument, this.TransformArgumentList));
			return xmlDocument3;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000870D0 File Offset: 0x000852D0
		private XmlDocument LoadFileOrData(string filename, string data)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (!string.IsNullOrEmpty(filename))
			{
				Uri uri;
				if (Uri.TryCreate(filename, UriKind.Absolute, out uri))
				{
					xmlDocument.Load(filename);
				}
				else
				{
					xmlDocument.Load(base.MapPathSecure(filename));
				}
			}
			else if (!string.IsNullOrEmpty(data))
			{
				xmlDocument.LoadXml(data);
			}
			return xmlDocument;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x0008711E File Offset: 0x0008531E
		private XmlDocument GetXmlDocumentFromCache()
		{
			if (this.DataCache != null)
			{
				return (XmlDocument)this.DataCache[this.GetDataKey()];
			}
			return null;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x00087140 File Offset: 0x00085340
		private string GetDataKey()
		{
			if (string.IsNullOrEmpty(this.DataFile) && !string.IsNullOrEmpty(this.Data))
			{
				string cacheKeyContext = this.CacheKeyContext;
				if (!string.IsNullOrEmpty(cacheKeyContext))
				{
					return cacheKeyContext;
				}
			}
			Page page = this.Page;
			string text = ((page != null) ? page.ToString() : "NullPage");
			return string.Concat(new string[] { this.TemplateSourceDirectory, "_", text, "_", this.ID });
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000871C1 File Offset: 0x000853C1
		private Cache DataCache
		{
			get
			{
				if (HttpContext.Current != null)
				{
					return HttpContext.Current.InternalCache;
				}
				return null;
			}
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000871D8 File Offset: 0x000853D8
		private void UpdateCache()
		{
			if (!this.EnableCaching)
			{
				return;
			}
			if (this.DataCache == null)
			{
				return;
			}
			string dataKey = this.GetDataKey();
			if (this.DataCache[dataKey] != null)
			{
				this.DataCache.Remove(dataKey);
			}
			DateTime dateTime = Cache.NoAbsoluteExpiration;
			TimeSpan noSlidingExpiration = Cache.NoSlidingExpiration;
			if (this.CacheDuration > 0)
			{
				if (this.CacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
				{
					dateTime = DateTime.Now.AddSeconds((double)this.CacheDuration);
				}
				else
				{
					noSlidingExpiration = new TimeSpan((long)this.CacheDuration * 10000L);
				}
			}
			CacheDependency cacheDependency;
			if (this.CacheKeyDependency.Length > 0)
			{
				cacheDependency = new CacheDependency(new string[0], new string[] { this.CacheKeyDependency });
			}
			else
			{
				cacheDependency = new CacheDependency(new string[0], new string[0]);
			}
			this.DataCache.Add(dataKey, this.xmlDocument, cacheDependency, dateTime, noSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000872BA File Offset: 0x000854BA
		private void UpdateXml()
		{
			this.xmlDocument = this.LoadXmlDocument();
			this.UpdateCache();
			this._documentNeedsUpdate = false;
		}

		/// <summary>Saves the XML data currently held in memory by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control to disk if the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">XML data was loaded using the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property instead of the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. - or -A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
		/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</exception>
		/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
		// Token: 0x0600331F RID: 13087 RVA: 0x000872D5 File Offset: 0x000854D5
		public void Save()
		{
			if (!this.CanBeSaved)
			{
				throw new InvalidOperationException();
			}
			if (this.xmlDocument != null)
			{
				this.xmlDocument.Save(base.MapPathSecure(this.DataFile));
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x00087304 File Offset: 0x00085504
		private bool CanBeSaved
		{
			get
			{
				return this.Transform == string.Empty && this.TransformFile == string.Empty && this.DataFile != string.Empty;
			}
		}

		/// <summary>Gets the data source view object for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The <paramref name="viewPath" /> parameter can be an XPath expression.</summary>
		/// <returns>Returns an <see cref="T:System.Web.UI.WebControls.XmlHierarchicalDataSourceView" /> object that represents a single view of the data starting with the data node identified by <paramref name="viewPath" />.</returns>
		/// <param name="viewPath">An XPath expression that identifies a node from which the current hierarchical view is built. </param>
		// Token: 0x06003321 RID: 13089 RVA: 0x0008733C File Offset: 0x0008553C
		protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
		{
			XmlNode xmlNode = this.GetXmlDocument();
			XmlNodeList xmlNodeList = null;
			if (!string.IsNullOrEmpty(viewPath))
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode(viewPath);
				if (xmlNode2 != null)
				{
					xmlNodeList = xmlNode2.ChildNodes;
				}
			}
			else if (!string.IsNullOrEmpty(this.XPath))
			{
				xmlNodeList = xmlNode.SelectNodes(this.XPath);
			}
			else
			{
				xmlNodeList = xmlNode.ChildNodes;
			}
			return new XmlHierarchicalDataSourceView(xmlNodeList);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IList" /> that can be bound to a data source.</returns>
		// Token: 0x06003322 RID: 13090 RVA: 0x0002FFE6 File Offset: 0x0002E1E6
		IList IListSource.GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>true if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, false.</returns>
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x00030050 File Offset: 0x0002E250
		bool IListSource.ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
		/// <param name="viewName">The name of the view to retrieve. If <see cref="F:System.String.Empty" /> is specified, the default view for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control is retrieved. </param>
		// Token: 0x06003324 RID: 13092 RVA: 0x00087397 File Offset: 0x00085597
		DataSourceView IDataSource.GetView(string viewName)
		{
			if (string.IsNullOrEmpty(viewName))
			{
				viewName = "DefaultView";
			}
			return new XmlDataSourceView(this, viewName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.ICollection" /> containing names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> object.</returns>
		// Token: 0x06003325 RID: 13093 RVA: 0x000873AF File Offset: 0x000855AF
		ICollection IDataSource.GetViewNames()
		{
			return XmlDataSource.emptyNames;
		}

		/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data it has retrieved.</summary>
		/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control caches the results of a data retrieval operation. The default value is 0.</returns>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000873B6 File Offset: 0x000855B6
		// (set) Token: 0x06003327 RID: 13095 RVA: 0x000873BE File Offset: 0x000855BE
		[TypeConverter(typeof(DataSourceCacheDurationConverter))]
		[DefaultValue(0)]
		public virtual int CacheDuration
		{
			get
			{
				return this._cacheDuration;
			}
			set
			{
				this._cacheDuration = value;
			}
		}

		/// <summary>Gets or sets the cache expiration policy that is combined with the cache duration to describe the caching behavior of the cache that the data source control uses.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default cache expiration policy setting is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000873C7 File Offset: 0x000855C7
		// (set) Token: 0x06003329 RID: 13097 RVA: 0x000873CF File Offset: 0x000855CF
		[DefaultValue(DataSourceCacheExpiry.Absolute)]
		public virtual DataSourceCacheExpiry CacheExpirationPolicy
		{
			get
			{
				return this._cacheExpirationPolicy;
			}
			set
			{
				this._cacheExpirationPolicy = value;
			}
		}

		/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects created by the data source control. All cache objects explicitly expire when the key expires.</summary>
		/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000873D8 File Offset: 0x000855D8
		// (set) Token: 0x0600332B RID: 13099 RVA: 0x000873E0 File Offset: 0x000855E0
		[DefaultValue("")]
		public virtual string CacheKeyDependency
		{
			get
			{
				return this._cacheKeyDependency;
			}
			set
			{
				this._cacheKeyDependency = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control has data caching enabled.</summary>
		/// <returns>true if data caching is enabled for the data source control; otherwise, false. The default value is true.</returns>
		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000873E9 File Offset: 0x000855E9
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x000873F1 File Offset: 0x000855F1
		[DefaultValue(true)]
		public virtual bool EnableCaching
		{
			get
			{
				return this._enableCaching;
			}
			set
			{
				this._enableCaching = value;
			}
		}

		/// <summary>Gets or sets a block of XML data that the data source control binds to.</summary>
		/// <returns>A string of inline XML data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control binds to. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000873FA File Offset: 0x000855FA
		// (set) Token: 0x0600332F RID: 13103 RVA: 0x00087402 File Offset: 0x00085602
		[WebCategory("Data")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("Inline XML data.")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter(typeof(MultilineStringConverter))]
		public virtual string Data
		{
			get
			{
				return this._data;
			}
			set
			{
				if (this._data != value)
				{
					this._data = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies the file name of an XML file that the data source binds to.</summary>
		/// <returns>The absolute physical path or relative path of the XML file that contains data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control represents. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x0008742B File Offset: 0x0008562B
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x00087433 File Offset: 0x00085633
		[DefaultValue("")]
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[Editor("System.Web.UI.Design.XmlDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DataFile
		{
			get
			{
				return this._dataFile;
			}
			set
			{
				if (this._dataFile != value)
				{
					this._dataFile = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Provides a list of XSLT arguments that are used with the style sheet defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> properties to perform a transformation on the XML data.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> object that contains XSLT parameters and objects to be applied to XML data when it is loaded by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The default value is null.</returns>
		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x0008745C File Offset: 0x0008565C
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x00087464 File Offset: 0x00085664
		[Browsable(false)]
		public virtual XsltArgumentList TransformArgumentList
		{
			get
			{
				return this.transformArgumentList;
			}
			set
			{
				this.transformArgumentList = value;
			}
		}

		/// <summary>Gets or sets a block of Extensible Stylesheet Language (XSL) data that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
		/// <returns>A string of inline XSL that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x0008746D File Offset: 0x0008566D
		// (set) Token: 0x06003335 RID: 13109 RVA: 0x00087475 File Offset: 0x00085675
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(MultilineStringConverter))]
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string Transform
		{
			get
			{
				return this._transform;
			}
			set
			{
				if (this._transform != value)
				{
					this._transform = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies the file name of an Extensible Stylesheet Language (XSL) file (.xsl) that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
		/// <returns>The absolute physical path or relative path of the XSL style sheet file that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x0008749E File Offset: 0x0008569E
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000874A6 File Offset: 0x000856A6
		[DefaultValue("")]
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[Editor("System.Web.UI.Design.XslTransformFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string TransformFile
		{
			get
			{
				return this._transformFile;
			}
			set
			{
				if (this._transformFile != value)
				{
					this._transformFile = value;
					this._documentNeedsUpdate = true;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Specifies an XPath expression to be applied to the XML data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</summary>
		/// <returns>A string that represents an XPath expression that can be used to filter the data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000874CF File Offset: 0x000856CF
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x000874D7 File Offset: 0x000856D7
		[DefaultValue("")]
		public virtual string XPath
		{
			get
			{
				return this._xpath;
			}
			set
			{
				if (this._xpath != value)
				{
					this._xpath = value;
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the value of the cache key for the data source control from view state, or adds the cache key to view state.</summary>
		/// <returns>The value of the cache key, or an empty string if the cache key is not in view state.</returns>
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000874F9 File Offset: 0x000856F9
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x00087510 File Offset: 0x00085710
		[DefaultValue("")]
		public virtual string CacheKeyContext
		{
			get
			{
				return this.ViewState.GetString("CacheKeyContext", string.Empty);
			}
			set
			{
				this.ViewState["CacheKeyContext"] = value;
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private string _data = string.Empty;

		// Token: 0x04001CC1 RID: 7361
		private string _transform = string.Empty;

		// Token: 0x04001CC2 RID: 7362
		private string _xpath = string.Empty;

		// Token: 0x04001CC3 RID: 7363
		private string _dataFile = string.Empty;

		// Token: 0x04001CC4 RID: 7364
		private string _transformFile = string.Empty;

		// Token: 0x04001CC5 RID: 7365
		private string _cacheKeyDependency = string.Empty;

		// Token: 0x04001CC6 RID: 7366
		private bool _enableCaching = true;

		// Token: 0x04001CC7 RID: 7367
		private int _cacheDuration;

		// Token: 0x04001CC8 RID: 7368
		private bool _documentNeedsUpdate;

		// Token: 0x04001CC9 RID: 7369
		private DataSourceCacheExpiry _cacheExpirationPolicy;

		// Token: 0x04001CCA RID: 7370
		private static readonly string[] emptyNames = new string[] { "DefaultView" };

		// Token: 0x04001CCB RID: 7371
		private static object EventTransforming = new object();

		// Token: 0x04001CCC RID: 7372
		private XmlDocument xmlDocument;

		// Token: 0x04001CCD RID: 7373
		private XsltArgumentList transformArgumentList;
	}
}
