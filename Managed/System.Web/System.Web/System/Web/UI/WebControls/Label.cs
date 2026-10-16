using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a label control, which displays text on a Web page.</summary>
	// Token: 0x020003B6 RID: 950
	[Designer("System.Web.UI.Design.WebControls.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	[ControlValueProperty("Text", null)]
	[ControlBuilder(typeof(LabelControlBuilder))]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(false)]
	[ToolboxData("<{0}:Label runat=\"server\" Text=\"Label\"></{0}:Label>")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Label : WebControl, ITextControl
	{
		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <returns>The text content of the control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x0006270D File Offset: 0x0006090D
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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

		/// <summary>Gets or sets the identifier for a server control that the <see cref="T:System.Web.UI.WebControls.Label" /> control is associated with.</summary>
		/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a server control contained in the Web form. The default is an empty string (""), indicating that the <see cref="T:System.Web.UI.WebControls.Label" /> control is not associated with another server control.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x00064022 File Offset: 0x00062222
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x00064039 File Offset: 0x00062239
		[IDReferenceProperty(typeof(Control))]
		[TypeConverter(typeof(AssociatedControlConverter))]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual string AssociatedControlID
		{
			get
			{
				return this.ViewState.GetString("AssociatedControlID", string.Empty);
			}
			set
			{
				this.ViewState["AssociatedControlID"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Loads the previously saved state for the control. </summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06002729 RID: 10025 RVA: 0x0006404C File Offset: 0x0006224C
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Text"] != null)
			{
				this.Text = (string)this.ViewState["Text"];
			}
		}

		/// <summary>Notifies the control that an element was parsed and adds the element to the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <param name="obj">An object that represents the parsed element.</param>
		// Token: 0x0600272A RID: 10026 RVA: 0x00064084 File Offset: 0x00062284
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

		/// <summary>Renders the contents of the <see cref="T:System.Web.UI.WebControls.Label" /> into the specified writer.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x0600272B RID: 10027 RVA: 0x000640E6 File Offset: 0x000622E6
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value used to render the <see cref="T:System.Web.UI.WebControls.Label" />.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0006410C File Offset: 0x0006230C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.AssociatedControlID))
				{
					return HtmlTextWriterTag.Label;
				}
				return HtmlTextWriterTag.Span;
			}
		}

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.Label" /> control to render to the specified output stream. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		/// <exception cref="T:System.Web.HttpException">The control specified in the <see cref="P:System.Web.UI.WebControls.Label.AssociatedControlID" /> property cannot be found.</exception>
		// Token: 0x0600272D RID: 10029 RVA: 0x00064120 File Offset: 0x00062320
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(this.AssociatedControlID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.For, this.NamingContainer.FindControl(this.AssociatedControlID).ClientID);
			}
		}
	}
}
