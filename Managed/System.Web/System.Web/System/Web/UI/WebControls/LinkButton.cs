using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a hyperlink-style button control on a Web page.</summary>
	// Token: 0x020003B8 RID: 952
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlBuilder(typeof(LinkButtonControlBuilder))]
	[ToolboxData("<{0}:LinkButton runat=\"server\">LinkButton</{0}:LinkButton>")]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.LinkButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(false)]
	[DefaultEvent("Click")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LinkButton : WebControl, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> class.</summary>
		// Token: 0x06002731 RID: 10033 RVA: 0x00062561 File Offset: 0x00060761
		public LinkButton()
			: base(HtmlTextWriterTag.A)
		{
		}

		/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06002732 RID: 10034 RVA: 0x00064154 File Offset: 0x00062354
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			base.AddAttributesToRender(writer);
			bool isEnabled = base.IsEnabled;
			string text = this.OnClientClick;
			text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			if (base.HasAttributes && base.Attributes["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text + base.Attributes["onclick"]);
				base.Attributes.Remove("onclick");
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			if (isEnabled && page != null)
			{
				PostBackOptions postBackOptions = this.GetPostBackOptions();
				string postBackEventReference = page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
			}
			base.AddDisplayStyleAttribute(writer);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002733 RID: 10035 RVA: 0x00064210 File Offset: 0x00062410
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnClick(EventArgs.Empty);
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06002734 RID: 10036 RVA: 0x0006426A File Offset: 0x0006246A
		void IPostBackEventHandler.RaisePostBackEvent(string ea)
		{
			this.RaisePostBackEvent(ea);
		}

		/// <summary>Notifies the control that an element, either XML or HTML, was parsed, and adds the element to the control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06002735 RID: 10037 RVA: 0x00064274 File Offset: 0x00062474
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

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</returns>
		// Token: 0x06002736 RID: 10038 RVA: 0x000642D8 File Offset: 0x000624D8
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			Page page = this.Page;
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? ((page != null) ? page.ResolveClientUrl(this.PostBackUrl) : this.PostBackUrl) : null);
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.ClientSubmit = true;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <param name="savedState">The saved state to retrieve.</param>
		// Token: 0x06002737 RID: 10039 RVA: 0x00064376 File Offset: 0x00062576
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Text"] != null)
			{
				this.Text = (string)this.ViewState["Text"];
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002738 RID: 10040 RVA: 0x0003EF60 File Offset: 0x0003D160
		[global::System.MonoTODO("Why override?")]
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the contents of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002739 RID: 10041 RVA: 0x000643AC File Offset: 0x000625AC
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("")]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets an optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property.</summary>
		/// <returns>An optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x00047521 File Offset: 0x00045721
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x00047538 File Offset: 0x00045738
		[Bindable(true)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		public string CommandArgument
		{
			get
			{
				return this.ViewState.GetString("CommandArgument", string.Empty);
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. This value is passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the <see cref="P:System.Web.UI.WebControls.LinkButton.CommandArgument" /> property.</summary>
		/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0004754B File Offset: 0x0004574B
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x00047562 File Offset: 0x00045762
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		public string CommandName
		{
			get
			{
				return this.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised</summary>
		/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised.</returns>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x00047575 File Offset: 0x00045775
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x0004758C File Offset: 0x0004578C
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public virtual string OnClientClick
		{
			get
			{
				return this.ViewState.GetString("OnClientClick", string.Empty);
			}
			set
			{
				this.ViewState["OnClientClick"] = value;
			}
		}

		/// <summary>Gets or sets the text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <returns>The text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x0006270D File Offset: 0x0006090D
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Bindable(true)]
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

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002744 RID: 10052 RVA: 0x000643D4 File Offset: 0x000625D4
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LinkButton.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06002745 RID: 10053 RVA: 0x00064402 File Offset: 0x00062602
		// (remove) Token: 0x06002746 RID: 10054 RVA: 0x00064415 File Offset: 0x00062615
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(LinkButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.ClickEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x06002747 RID: 10055 RVA: 0x00064428 File Offset: 0x00062628
		protected virtual void OnCommand(CommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[LinkButton.CommandEvent];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06002748 RID: 10056 RVA: 0x0006445E File Offset: 0x0006265E
		// (remove) Token: 0x06002749 RID: 10057 RVA: 0x00064471 File Offset: 0x00062671
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(LinkButton.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkButton.CommandEvent, value);
			}
		}

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x000478C6 File Offset: 0x00045AC6
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x000478DD File Offset: 0x00045ADD
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty("*.aspx")]
		public virtual string PostBackUrl
		{
			get
			{
				return this.ViewState.GetString("PostBackUrl", string.Empty);
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x00064484 File Offset: 0x00062684
		// Note: this type is marked as 'beforefieldinit'.
		static LinkButton()
		{
			LinkButton.ClickEvent = new object();
			LinkButton.CommandEvent = new object();
		}
	}
}
