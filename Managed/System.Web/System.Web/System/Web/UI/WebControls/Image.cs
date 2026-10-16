using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an image on a Web page.</summary>
	// Token: 0x020003B2 RID: 946
	[DefaultProperty("ImageUrl")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Image : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Image" /> class.</summary>
		// Token: 0x060026B5 RID: 9909 RVA: 0x00062E7D File Offset: 0x0006107D
		public Image()
			: base(HtmlTextWriterTag.Img)
		{
		}

		/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip.</summary>
		/// <returns>The alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00062E88 File Offset: 0x00061088
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00062EB5 File Offset: 0x000610B5
		[Bindable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string AlternateText
		{
			get
			{
				string text = (string)this.ViewState["AlternateText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AlternateText");
					return;
				}
				this.ViewState["AlternateText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is enabled.</summary>
		/// <returns>true if the control is enabled; otherwise false.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x00062EDC File Offset: 0x000610DC
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x00062EE4 File Offset: 0x000610E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets the font properties for the text associated with the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the properties for the text associated with the control.</returns>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00044459 File Offset: 0x00042659
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

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.WebControls.Image" /> control in relation to other elements on the Web page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. </exception>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00062EF0 File Offset: 0x000610F0
		// (set) Token: 0x060026BC RID: 9916 RVA: 0x00062F19 File Offset: 0x00061119
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(ImageAlign.NotSet)]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = this.ViewState["ImageAlign"];
				if (obj != null)
				{
					return (ImageAlign)obj;
				}
				return ImageAlign.NotSet;
			}
			set
			{
				if (value < ImageAlign.NotSet || value > ImageAlign.TextTop)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid ImageAlign value."));
				}
				this.ViewState["ImageAlign"] = value;
			}
		}

		/// <summary>Gets or sets the URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</summary>
		/// <returns>The URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</returns>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00062F4C File Offset: 0x0006114C
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x00062F79 File Offset: 0x00061179
		[Bindable(true)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string ImageUrl
		{
			get
			{
				string text = (string)this.ViewState["ImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ImageUrl");
					return;
				}
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the location to a detailed description for the image.</summary>
		/// <returns>The URL for the file that contains a detailed description for the image. The default is an empty string ("").</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00062FA0 File Offset: 0x000611A0
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x00062FCD File Offset: 0x000611CD
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual string DescriptionUrl
		{
			get
			{
				string text = (string)this.ViewState["DescriptionUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DescriptionUrl");
					return;
				}
				this.ViewState["DescriptionUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control generates an alternate text attribute for an empty string value.</summary>
		/// <returns>true if the control generates the alternate text attribute for an empty string value; otherwise, false. The default is false.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x00062FF4 File Offset: 0x000611F4
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x0006301D File Offset: 0x0006121D
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		[DefaultValue(false)]
		public virtual bool GenerateEmptyAlternateText
		{
			get
			{
				object obj = this.ViewState["GenerateEmptyAlternateText"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["GenerateEmptyAlternateText"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.Image" /> to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client browser. </param>
		// Token: 0x060026C4 RID: 9924 RVA: 0x00063038 File Offset: 0x00061238
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.ImageUrl));
			string text = this.AlternateText;
			if (text.Length > 0 || this.GenerateEmptyAlternateText)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text);
			}
			text = this.DescriptionUrl;
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, base.ResolveClientUrl(text));
			}
			switch (this.ImageAlign)
			{
			case ImageAlign.Left:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", false);
				return;
			case ImageAlign.Right:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", false);
				return;
			case ImageAlign.Baseline:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "baseline", false);
				return;
			case ImageAlign.Top:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "top", false);
				return;
			case ImageAlign.Middle:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "middle", false);
				return;
			case ImageAlign.Bottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "bottom", false);
				return;
			case ImageAlign.AbsBottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "absbottom", false);
				return;
			case ImageAlign.AbsMiddle:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "absmiddle", false);
				return;
			case ImageAlign.TextTop:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "texttop", false);
				return;
			default:
				return;
			}
		}

		/// <summary>Renders the image control contents to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060026C5 RID: 9925 RVA: 0x00063150 File Offset: 0x00061350
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}
	}
}
