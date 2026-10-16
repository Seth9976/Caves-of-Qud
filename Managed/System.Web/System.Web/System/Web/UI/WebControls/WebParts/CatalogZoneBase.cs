using System;
using System.Collections.Specialized;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all zone controls that act as catalogs. Catalogs contain lists of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls that users can add to a Web page. </summary>
	// Token: 0x0200079D RID: 1949
	public abstract class CatalogZoneBase : ToolZone, IPostBackDataHandler
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06004E81 RID: 20097 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected CatalogZoneBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to add controls from a catalog to a Web Parts page.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to add controls from the catalog to a Web page.</returns>
		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb AddVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> class associated with the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> that is associated with the zone. </returns>
		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public CatalogPartChrome CatalogPartChrome
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of all the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls contained in a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> that contains all the individual <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a zone.</returns>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> being added to the collection did not have a value assigned to its ID property.</exception>
		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public CatalogPartCollection CatalogParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to close the catalog user interface (UI) and return the page to normal browse mode.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to close the catalog UI on the Web page.</returns>
		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a message that appears when a zone contains no controls.</summary>
		/// <returns>A string containing the message that appears in an empty zone. A default culture-specific string is supplied by the .NET Framework.</returns>
		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E87 RID: 20103 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string EmptyZoneText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text for the header area of a zone.</summary>
		/// <returns>A string that contains the header text for the zone. A default culture-specific string is supplied by the .NET Framework.</returns>
		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E89 RID: 20105 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string HeaderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text in a zone that provides directions for end users.</summary>
		/// <returns>A string that contains the directions for end users. A default, culture-specific string is provided by the Web Parts control set.</returns>
		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E8B RID: 20107 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string InstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets an object that contains style attributes for the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls that are not currently selected in the zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the controls that are not currently selected.</returns>
		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style PartLinkStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a string as an identifier for the currently selected <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control in a zone.</summary>
		/// <returns>A string that serves as the identifier for the currently selected control. The default is the value of the control's <see cref="P:System.Web.UI.Control.ID" /> property.</returns>
		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E8E RID: 20110 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string SelectedCatalogPartID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets an object that contains style attributes for the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control that is currently selected in the zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the control that is currently selected.</returns>
		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style SelectedPartLinkStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the text alongside the control in the catalog user interface (UI) that allows users to choose which zone to add their selected controls to.</summary>
		/// <returns>A string that contains the text to display alongside the zone selection control. A default culture-specific string is supplied by the .NET Framework. </returns>
		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E91 RID: 20113 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string SelectTargetZoneText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether server controls in the catalog display their associated icons in the catalog.</summary>
		/// <returns>true if the icons associated with server controls in the catalog should be displayed; otherwise, false. The default is true.</returns>
		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x000C8B94 File Offset: 0x000C6D94
		// (set) Token: 0x06004E93 RID: 20115 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool ShowCatalogIcons
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Switches the Web page from catalog display mode to normal browse mode.</summary>
		// Token: 0x06004E94 RID: 20116 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void Close()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates an instance of a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> object used to render the peripheral user interface (UI) elements for <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartChrome" /> that renders the peripheral UI elements for the zone.</returns>
		// Token: 0x06004E95 RID: 20117 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual CatalogPartChrome CreateCatalogPartChrome()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides a base method declaration that derived zones can override to handle the details of creating the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls contained in a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> that contains the collection of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls associated with the zone.</returns>
		// Token: 0x06004E96 RID: 20118
		protected abstract CatalogPartCollection CreateCatalogParts();

		/// <summary>Destroys the collection of <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls associated with a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		// Token: 0x06004E97 RID: 20119 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected void InvalidateCatalogParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Processes the state of the check boxes that correspond to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in the catalog, when the page is posted back to the server.</summary>
		/// <returns>This method, in contrast to the overridden base method, always returns false, because the class does not expose any change event.</returns>
		/// <param name="postDataKey">The key identifier for the control. </param>
		/// <param name="postCollection">The collection of name/value pairs posted to the server. </param>
		// Token: 0x06004E98 RID: 20120 RVA: 0x000C8BB0 File Offset: 0x000C6DB0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Carries out the actions associated with one of the zone verbs, or raises an event that posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument for the event. </param>
		// Token: 0x06004E99 RID: 20121 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render the body area of a zone derived from the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> class.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x06004E9A RID: 20122 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the links to the individual <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the contents of the links to the zone's controls. </param>
		// Token: 0x06004E9B RID: 20123 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RenderCatalogPartLinks(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the footer area for a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the footer area for the zone. </param>
		// Token: 0x06004E9C RID: 20124 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderFooter(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the verbs in the footer area of a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the verbs for the zone. </param>
		// Token: 0x06004E9D RID: 20125 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.WebControls.WebParts.CatalogZoneBase.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> class.</summary>
		/// <returns>true if an event should be raised to indicate that data has changed; otherwise false. </returns>
		/// <param name="postDataKey">The key identifier for the control. </param>
		/// <param name="postCollection">The collection of name/value pairs posted to the server. </param>
		// Token: 0x06004E9E RID: 20126 RVA: 0x000C8BCC File Offset: 0x000C6DCC
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x06004E9F RID: 20127 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
