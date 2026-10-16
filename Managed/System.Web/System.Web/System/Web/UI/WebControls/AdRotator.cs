using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an advertisement banner on a Web page.</summary>
	// Token: 0x0200032C RID: 812
	[Designer("System.Web.UI.Design.WebControls.AdRotatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("AdvertisementFile")]
	[DefaultEvent("AdCreated")]
	[ToolboxData("<{0}:AdRotator runat=\"server\"></{0}:AdRotator>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AdRotator : DataBoundControl
	{
		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">The event arguments.</param>
		// Token: 0x06001C07 RID: 7175 RVA: 0x00044036 File Offset: 0x00042236
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		/// <summary>Gets the advertisement information for rendering by looking up the file data or calling the user event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06001C08 RID: 7176 RVA: 0x00044040 File Offset: 0x00042240
		protected internal override void OnPreRender(EventArgs e)
		{
			Hashtable hashtable = null;
			if (!string.IsNullOrEmpty(this.ad_file))
			{
				this.ReadAdsFromFile(base.GetPhysicalFilePath(this.ad_file));
				hashtable = this.ChooseAd();
			}
			AdCreatedEventArgs e2 = new AdCreatedEventArgs(hashtable);
			this.OnAdCreated(e2);
			this.createdargs = e2;
		}

		/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <param name="data">An object that represents the data source; the object must implement the <see cref="T:System.Collections.IEnumerable" /> interface.</param>
		// Token: 0x06001C09 RID: 7177 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the advertisement data from the associated data source.</summary>
		// Token: 0x06001C0A RID: 7178 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected override void PerformSelect()
		{
			throw new NotImplementedException();
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001C0B RID: 7179 RVA: 0x0004408C File Offset: 0x0004228C
		protected internal override void Render(HtmlTextWriter writer)
		{
			AdCreatedEventArgs e = this.createdargs;
			base.AddAttributesToRender(writer);
			if (e.NavigateUrl != null && e.NavigateUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, this.ResolveAdUrl(e.NavigateUrl));
			}
			if (this.Target != null && this.Target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (e.ImageUrl != null && e.ImageUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ResolveAdUrl(e.ImageUrl));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, (e.AlternateText == null) ? string.Empty : e.AlternateText);
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0", false);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00044164 File Offset: 0x00042364
		private string ResolveAdUrl(string url)
		{
			if (this.AdvertisementFile != null && this.AdvertisementFile.Length > 0 && url[0] != '/' && url[0] != '~')
			{
				try
				{
					new Uri(url);
				}
				catch
				{
					return UrlUtils.Combine(UrlUtils.GetDirectory(base.ResolveUrl(this.AdvertisementFile)), url);
				}
			}
			return base.ResolveUrl(url);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000441DC File Offset: 0x000423DC
		private Hashtable ChooseAd()
		{
			string keywordFilter = this.KeywordFilter;
			int num = 0;
			int num2 = 0;
			bool flag = keywordFilter.Length == 0;
			foreach (object obj in this.ads)
			{
				Hashtable hashtable = (Hashtable)obj;
				if (flag || keywordFilter == (string)hashtable["Keyword"])
				{
					num += ((hashtable["Impressions"] != null) ? int.Parse((string)hashtable["Impressions"]) : 1);
				}
			}
			int num3 = new Random().Next(num);
			foreach (object obj2 in this.ads)
			{
				Hashtable hashtable2 = (Hashtable)obj2;
				if (flag || !(keywordFilter != (string)hashtable2["Keyword"]))
				{
					num2 += ((hashtable2["Impressions"] != null) ? int.Parse((string)hashtable2["Impressions"]) : 1);
					if (num2 > num3)
					{
						return hashtable2;
					}
				}
			}
			if (num != 0)
			{
				throw new Exception("I should only get here if no ads matched");
			}
			return null;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0004434C File Offset: 0x0004254C
		private void ReadAdsFromFile(string s)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(s);
			}
			catch (Exception ex)
			{
				throw new HttpException("AdRotator could not parse the xml file", ex);
			}
			this.ads.Clear();
			foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Hashtable hashtable = new Hashtable();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					hashtable.Add(xmlNode2.Name, xmlNode2.InnerText);
				}
				this.ads.Add(hashtable);
			}
		}

		/// <summary>Gets or sets the path to an XML file that contains advertisement information.</summary>
		/// <returns>The location of an XML file containing advertisement information. The default value is an empty string ("").</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00044448 File Offset: 0x00042648
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x00044450 File Offset: 0x00042650
		[UrlProperty]
		[Bindable(true)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string AdvertisementFile
		{
			get
			{
				return this.ad_file;
			}
			set
			{
				this.ad_file = value;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the AlternateText attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field where the alternate text for an advertisement is stored. The default value is "AlternateText."</returns>
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00003A2B File Offset: 0x00001C2B
		[DefaultValue("AlternateText")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[global::System.MonoTODO("Not implemented")]
		public string AlternateTextField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the font properties associated with the advertisement banner control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> object that represents the font properties of the control.</returns>
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00044459 File Offset: 0x00042659
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the ImageUrl attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field where the URL for the image displayed for an advertisement is stored. The default value is "ImageUrl."</returns>
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x00003A2B File Offset: 0x00001C2B
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("ImageUrl")]
		[global::System.MonoTODO("Not implemented")]
		public string ImageUrlField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a category keyword to filter for specific types of advertisements in the XML advertisement file.</summary>
		/// <returns>The keyword to filter for specific types of advertisements in the XML advertisement file. The default value is an empty string ("").</returns>
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00044461 File Offset: 0x00042661
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x00044478 File Offset: 0x00042678
		[Bindable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string KeywordFilter
		{
			get
			{
				return this.ViewState.GetString("KeywordFilter", string.Empty);
			}
			set
			{
				this.ViewState["KeywordFilter"] = value;
			}
		}

		/// <summary>Gets or sets a custom data field to use in place of the NavigateUrl attribute for an advertisement.</summary>
		/// <returns>The name that identifies the field containing the URL for the page to navigate to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is "NavigateUrl."</returns>
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x00003A2B File Offset: 0x00001C2B
		[DefaultValue("NavigateUrl")]
		[global::System.MonoTODO("Not implemented")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public string NavigateUrlField
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
		/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0004448B File Offset: 0x0004268B
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x000444A2 File Offset: 0x000426A2
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("_top")]
		[Bindable(true)]
		[TypeConverter(typeof(TargetConverter))]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", "_top");
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets the unique, hierarchically qualified identifier for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <returns>The fully qualified identifier for the server control.</returns>
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000444B5 File Offset: 0x000426B5
		public override string UniqueID
		{
			get
			{
				return base.UniqueID;
			}
		}

		/// <summary>Gets the HTML tag for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value representing the HTML tag for an <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. </returns>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000444BD File Offset: 0x000426BD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return base.TagKey;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event for the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> that contains event data. </param>
		// Token: 0x06001C1E RID: 7198 RVA: 0x000444C8 File Offset: 0x000426C8
		protected virtual void OnAdCreated(AdCreatedEventArgs e)
		{
			AdCreatedEventHandler adCreatedEventHandler = (AdCreatedEventHandler)base.Events[AdRotator.AdCreatedEvent];
			if (adCreatedEventHandler != null)
			{
				adCreatedEventHandler(this, e);
			}
		}

		/// <summary>Occurs once per round trip to the server after the creation of the control, but before the page is rendered.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001C1F RID: 7199 RVA: 0x000444F6 File Offset: 0x000426F6
		// (remove) Token: 0x06001C20 RID: 7200 RVA: 0x00044509 File Offset: 0x00042709
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event AdCreatedEventHandler AdCreated
		{
			add
			{
				base.Events.AddHandler(AdRotator.AdCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(AdRotator.AdCreatedEvent, value);
			}
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0004453A File Offset: 0x0004273A
		// Note: this type is marked as 'beforefieldinit'.
		static AdRotator()
		{
			AdRotator.AdCreatedEvent = new object();
		}

		// Token: 0x040017E5 RID: 6117
		private AdCreatedEventArgs createdargs;

		// Token: 0x040017E6 RID: 6118
		private ArrayList ads = new ArrayList();

		// Token: 0x040017E7 RID: 6119
		private string ad_file = string.Empty;
	}
}
