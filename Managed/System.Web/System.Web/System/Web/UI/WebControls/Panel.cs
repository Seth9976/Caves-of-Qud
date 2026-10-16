using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for other controls.</summary>
	// Token: 0x020003DF RID: 991
	[Designer("System.Web.UI.Design.WebControls.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Panel : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Panel" /> class.</summary>
		// Token: 0x06002B7E RID: 11134 RVA: 0x000716AC File Offset: 0x0006F8AC
		public Panel()
			: base(HtmlTextWriterTag.Div)
		{
		}

		/// <summary>Adds information about the background image, alignment, wrap, and direction to the list of attributes to render.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.Panel.DefaultButton" /> property of the <see cref="T:System.Web.UI.WebControls.Panel" /> control must be the ID of a control of type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x06002B7F RID: 11135 RVA: 0x000716B8 File Offset: 0x0006F8B8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string text = this.BackImageUrl;
			if (text != "")
			{
				text = base.ResolveClientUrl(text);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, text);
			}
			if (!string.IsNullOrEmpty(this.DefaultButton) && this.Page != null)
			{
				Control control = this.FindControl(this.DefaultButton);
				if (control == null || !(control is IButtonControl))
				{
					throw new InvalidOperationException(string.Format("The DefaultButton of '{0}' must be the ID of a control of type IButtonControl.", this.ID));
				}
				this.Page.ClientScript.RegisterWebFormClientScript();
				writer.AddAttribute("onkeypress", string.Concat(new string[]
				{
					"javascript:return ",
					this.Page.WebFormScriptReference,
					".WebForm_FireDefaultButton(event, '",
					control.ClientID,
					"')"
				}));
			}
			if (this.Direction != ContentDirection.NotSet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, (this.Direction == ContentDirection.RightToLeft) ? "rtl" : "ltr", false);
			}
			switch (this.ScrollBars)
			{
			case ScrollBars.Horizontal:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
				break;
			case ScrollBars.Vertical:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
				break;
			case ScrollBars.Both:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
				break;
			case ScrollBars.Auto:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
				break;
			}
			if (!this.Wrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			string text2 = "";
			switch (this.HorizontalAlign)
			{
			case HorizontalAlign.Left:
				text2 = "left";
				break;
			case HorizontalAlign.Center:
				text2 = "center";
				break;
			case HorizontalAlign.Right:
				text2 = "right";
				break;
			case HorizontalAlign.Justify:
				text2 = "justify";
				break;
			}
			if (text2 != "")
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, text2);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x00071881 File Offset: 0x0006FA81
		private PanelStyle PanelStyle
		{
			get
			{
				return base.ControlStyle as PanelStyle;
			}
		}

		/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
		/// <returns>The URL of the background image for the panel control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x0007188E File Offset: 0x0006FA8E
		// (set) Token: 0x06002B82 RID: 11138 RVA: 0x000718C7 File Offset: 0x0006FAC7
		[WebSysDescription("")]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.BackImageUrl;
				}
				return this.ViewState.GetString("BackImageUrl", string.Empty);
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.BackImageUrl = value;
					return;
				}
				this.ViewState["BackImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents within the panel.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. </exception>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000718F0 File Offset: 0x0006FAF0
		// (set) Token: 0x06002B84 RID: 11140 RVA: 0x00071944 File Offset: 0x0006FB44
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.HorizontalAlign;
				}
				if (this.ViewState["HorizontalAlign"] == null)
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)this.ViewState["HorizontalAlign"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.HorizontalAlign = value;
					return;
				}
				this.ViewState["HorizontalAlign"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
		/// <returns>true if the content wraps within the panel; otherwise, false. The default is true.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00071971 File Offset: 0x0006FB71
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x000719A2 File Offset: 0x0006FBA2
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual bool Wrap
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return true;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.Wrap;
				}
				return this.ViewState.GetBool("Wrap", true);
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.Wrap = value;
					return;
				}
				this.ViewState["Wrap"] = value;
			}
		}

		/// <summary>Gets or sets the identifier for the default button that is contained in the <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a button control contained in the <see cref="T:System.Web.UI.WebControls.Panel" />. The default is an empty string, indicating that the <see cref="T:System.Web.UI.WebControls.Panel" /> does not have a default button.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000719CF File Offset: 0x0006FBCF
		// (set) Token: 0x06002B88 RID: 11144 RVA: 0x000719E6 File Offset: 0x0006FBE6
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string DefaultButton
		{
			get
			{
				return this.ViewState.GetString("DefaultButton", string.Empty);
			}
			set
			{
				this.ViewState["DefaultButton"] = value;
			}
		}

		/// <summary>Gets or sets the direction in which to display controls that include text in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000719FC File Offset: 0x0006FBFC
		// (set) Token: 0x06002B8A RID: 11146 RVA: 0x00071A50 File Offset: 0x0006FC50
		[DefaultValue(ContentDirection.NotSet)]
		public virtual ContentDirection Direction
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ContentDirection.NotSet;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.Direction;
				}
				if (this.ViewState["Direction"] == null)
				{
					return ContentDirection.NotSet;
				}
				return (ContentDirection)this.ViewState["Direction"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.Direction = value;
					return;
				}
				this.ViewState["Direction"] = value;
			}
		}

		/// <summary>Gets or sets the caption for the group of controls that is contained in the panel control.</summary>
		/// <returns>The caption text for the child controls contained in the panel control. The default is an empty string ("").</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x00071A7D File Offset: 0x0006FC7D
		// (set) Token: 0x06002B8C RID: 11148 RVA: 0x00071A94 File Offset: 0x0006FC94
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string GroupingText
		{
			get
			{
				return this.ViewState.GetString("GroupingText", string.Empty);
			}
			set
			{
				this.ViewState["GroupingText"] = value;
			}
		}

		/// <summary>Gets or sets the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> enumeration values. The default is None.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x00071AA8 File Offset: 0x0006FCA8
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x00071AFC File Offset: 0x0006FCFC
		[DefaultValue(ScrollBars.None)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ScrollBars.None;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.ScrollBars;
				}
				if (this.ViewState["ScrollBars"] == null)
				{
					return ScrollBars.None;
				}
				return (ScrollBars)this.ViewState["Direction"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.ScrollBars = value;
					return;
				}
				this.ViewState["ScrollBars"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.Panel" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.PanelStyle" /> that contains the style properties of the control.</returns>
		// Token: 0x06002B90 RID: 11152 RVA: 0x00071B29 File Offset: 0x0006FD29
		protected override Style CreateControlStyle()
		{
			return new PanelStyle(this.ViewState);
		}

		/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002B91 RID: 11153 RVA: 0x00071B36 File Offset: 0x0006FD36
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			if (!string.IsNullOrEmpty(this.GroupingText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
				writer.RenderBeginTag(HtmlTextWriterTag.Legend);
				writer.Write(this.GroupingText);
				writer.RenderEndTag();
			}
		}

		/// <summary>Renders the HTML closing tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control into the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002B92 RID: 11154 RVA: 0x00071B6E File Offset: 0x0006FD6E
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.GroupingText))
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}
	}
}
