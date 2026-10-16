using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>A control that displays a link to another Web page.</summary>
	// Token: 0x020003AC RID: 940
	[DefaultProperty("Text")]
	[ParseChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.HyperLinkDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DataBindingHandler("System.Web.UI.Design.HyperLinkDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:HyperLink runat=\"server\">HyperLink</{0}:HyperLink>")]
	[ControlBuilder(typeof(HyperLinkControlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLink : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLink" /> class.</summary>
		// Token: 0x06002666 RID: 9830 RVA: 0x00062561 File Offset: 0x00060761
		public HyperLink()
			: base(HtmlTextWriterTag.A)
		{
		}

		/// <summary>Adds the attributes of a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control to the output stream for rendering.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x06002667 RID: 9831 RVA: 0x0006256C File Offset: 0x0006076C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			base.AddDisplayStyleAttribute(writer);
			if (!base.IsEnabled)
			{
				return;
			}
			string target = this.Target;
			string navigateUrl = this.NavigateUrl;
			if (navigateUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.ResolveClientUrl(navigateUrl));
			}
			if (target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
			}
		}

		/// <summary>Notifies the control that an element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. </summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06002668 RID: 9832 RVA: 0x000625C8 File Offset: 0x000607C8
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = this.Text;
				if (text.Length != 0)
				{
					this.Text = null;
					this.Controls.Add(new LiteralControl(text));
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <param name="savedState">The <see cref="T:System.Object" /> that contains the previously saved state.</param>
		// Token: 0x06002669 RID: 9833 RVA: 0x0006262A File Offset: 0x0006082A
		[global::System.MonoTODO("Why override?")]
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control on a page.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x0600266A RID: 9834 RVA: 0x00062634 File Offset: 0x00060834
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			string imageUrl = this.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				string text = this.ToolTip;
				if (!string.IsNullOrEmpty(text))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(imageUrl));
				text = this.Text;
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets or sets the path to an image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
		/// <returns>The path to the image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000626B9 File Offset: 0x000608B9
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x000626D0 File Offset: 0x000608D0
		[WebCategory("Appearance")]
		[UrlProperty]
		[WebSysDescription("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Bindable(true)]
		[DefaultValue("")]
		public virtual string ImageUrl
		{
			get
			{
				return this.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
		/// <returns>The URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. The default value is an empty string ('').</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000626E3 File Offset: 0x000608E3
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x000626FA File Offset: 0x000608FA
		[Bindable(true)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("")]
		[WebCategory("Navigation")]
		public string NavigateUrl
		{
			get
			{
				return this.ViewState.GetString("NavigateUrl", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
		/// <returns>The target window or frame to load the Web page linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. Values must begin with a letter in the range of a through z (case-insensitive), except for the special values listed in the following table, which begin with an underscore._blank Renders the content in a new window without frames. _parent Renders the content in the immediate frameset parent. _searchRenders the content in the search pane._self Renders the content in the frame with focus. _top Renders the content in the full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string ("").</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x00047499 File Offset: 0x00045699
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x000444A2 File Offset: 0x000426A2
		[WebSysDescription("")]
		[WebCategory("Navigation")]
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
		/// <returns>The text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x0006270D File Offset: 0x0006090D
		[WebSysDescription("")]
		[Bindable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Gets or sets the height of the hyperlink when the hyperlink is an image.</summary>
		/// <returns>The height of the hyperlink image.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x00062734 File Offset: 0x00060934
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual Unit ImageHeight
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Unit);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the width of the hyperlink when the hyperlink is an image.</summary>
		/// <returns>The width of the hyperlink image.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00062750 File Offset: 0x00060950
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual Unit ImageWidth
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Unit);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
