using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a push button control on the Web page.</summary>
	// Token: 0x0200033B RID: 827
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:Button runat=\"server\" Text=\"Button\"></{0}:Button>")]
	[Designer("System.Web.UI.Design.WebControls.ButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Button : WebControl, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Button" /> class.</summary>
		// Token: 0x06001D5A RID: 7514 RVA: 0x00047517 File Offset: 0x00045717
		public Button()
			: base(HtmlTextWriterTag.Input)
		{
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
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

		/// <summary>Gets or sets an optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />.</summary>
		/// <returns>An optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00047521 File Offset: 0x00045721
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x00047538 File Offset: 0x00045738
		[DefaultValue("")]
		[Bindable(true)]
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

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.Button" /> control that is passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event.</summary>
		/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0004754B File Offset: 0x0004574B
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x00047562 File Offset: 0x00045762
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</summary>
		/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00047575 File Offset: 0x00045775
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0004758C File Offset: 0x0004578C
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <returns>The text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x000475B6 File Offset: 0x000457B6
		[Localizable(true)]
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Button" /> control uses the client browser's submit mechanism or the ASP.NET postback mechanism.</summary>
		/// <returns>true if the control uses the client browser's submit mechanism; otherwise, false. The default is true.</returns>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x000475C9 File Offset: 0x000457C9
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x000475DC File Offset: 0x000457DC
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool UseSubmitBehavior
		{
			get
			{
				return this.ViewState.GetBool("UseSubmitBehavior", true);
			}
			set
			{
				this.ViewState["UseSubmitBehavior"] = value;
			}
		}

		/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.Button" /> control to the output stream for rendering on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001D67 RID: 7527 RVA: 0x000475F4 File Offset: 0x000457F4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, this.UseSubmitBehavior ? "submit" : "button", false);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
			string text = this.OnClientClick;
			text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			if (base.HasAttributes && base.Attributes["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text + base.Attributes["onclick"]);
				base.Attributes.Remove("onclick");
			}
			if (page != null)
			{
				text += this.GetClientScriptEventReference();
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000476C8 File Offset: 0x000458C8
		internal virtual string GetClientScriptEventReference()
		{
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			Page page = this.Page;
			if (page != null)
			{
				return page.ClientScript.GetPostBackEventReference(postBackOptions, true);
			}
			return string.Empty;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</returns>
		// Token: 0x06001D69 RID: 7529 RVA: 0x000476FC File Offset: 0x000458FC
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? this.Page.ResolveClientUrl(this.PostBackUrl) : null);
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = !this.UseSubmitBehavior;
			Page page = this.Page;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001D6A RID: 7530 RVA: 0x0004779C File Offset: 0x0004599C
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Click" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <param name="e">The event data. </param>
		// Token: 0x06001D6B RID: 7531 RVA: 0x000477A8 File Offset: 0x000459A8
		protected virtual void OnClick(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Button.ClickEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
		/// <param name="e">The event data. </param>
		// Token: 0x06001D6C RID: 7532 RVA: 0x000477E0 File Offset: 0x000459E0
		protected virtual void OnCommand(CommandEventArgs e)
		{
			if (base.Events != null)
			{
				CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[Button.CommandEvent];
				if (commandEventHandler != null)
				{
					commandEventHandler(this, e);
				}
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x06001D6D RID: 7533 RVA: 0x00047820 File Offset: 0x00045A20
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

		/// <summary>Determines whether the button has been clicked prior to rendering on the client.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06001D6E RID: 7534 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the contents of the control to the specified writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001D6F RID: 7535 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001D70 RID: 7536 RVA: 0x0004787A File Offset: 0x00045A7A
		// (remove) Token: 0x06001D71 RID: 7537 RVA: 0x0004788D File Offset: 0x00045A8D
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Button.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001D72 RID: 7538 RVA: 0x000478A0 File Offset: 0x00045AA0
		// (remove) Token: 0x06001D73 RID: 7539 RVA: 0x000478B3 File Offset: 0x00045AB3
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(Button.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Button.CommandEvent, value);
			}
		}

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000478C6 File Offset: 0x00045AC6
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000478DD File Offset: 0x00045ADD
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Themeable(false)]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0003EF4D File Offset: 0x0003D14D
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

		// Token: 0x06001D78 RID: 7544 RVA: 0x000478F0 File Offset: 0x00045AF0
		// Note: this type is marked as 'beforefieldinit'.
		static Button()
		{
			Button.ClickEvent = new object();
			Button.CommandEvent = new object();
		}
	}
}
