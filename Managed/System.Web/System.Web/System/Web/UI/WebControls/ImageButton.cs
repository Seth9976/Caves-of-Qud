using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>A control that displays an image and responds to mouse clicks on the image.</summary>
	// Token: 0x020003B3 RID: 947
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageButton : Image, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
	{
		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked; otherwise, false. The default value is true.</returns>
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0003EF0B File Offset: 0x0003D10B
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets an optional argument that provides additional information about the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</summary>
		/// <returns>An optional argument that supplements the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</returns>
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00047521 File Offset: 0x00045721
		// (set) Token: 0x060026CA RID: 9930 RVA: 0x00047538 File Offset: 0x00045738
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

		/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>The command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0004754B File Offset: 0x0004574B
		// (set) Token: 0x060026CC RID: 9932 RVA: 0x00047562 File Offset: 0x00045762
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ImageButton" /> can be clicked to perform a post back to the server.</summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x00063161 File Offset: 0x00061361
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x00063169 File Offset: 0x00061369
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(true)]
		[Bindable(true)]
		public new virtual bool Enabled
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

		/// <summary>Gets or sets a value indicating whether the control generates an alternate-text attribute for an empty string value. </summary>
		/// <returns>false, indicating that the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control does not generate an alternate-text attribute when the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property is empty.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x00003A0D File Offset: 0x00001C0D
		[Themeable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool GenerateEmptyAlternateText
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</summary>
		/// <returns>The client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</returns>
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x00047575 File Offset: 0x00045775
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x0004758C File Offset: 0x0004578C
		[Themeable(false)]
		[DefaultValue("")]
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

		/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000478C6 File Offset: 0x00045AC6
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000478DD File Offset: 0x00045ADD
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Themeable(false)]
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

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00063172 File Offset: 0x00061372
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</summary>
		/// <returns>The value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x00063176 File Offset: 0x00061376
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0006317E File Offset: 0x0006137E
		protected virtual string Text
		{
			get
			{
				return this.AlternateText;
			}
			set
			{
				this.AlternateText = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.ImageButton" /> to the output stream for rendering on the client.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x060026DB RID: 9947 RVA: 0x00063188 File Offset: 0x00061388
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "image", false);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			base.AddAttributesToRender(writer);
			string text = this.OnClientClick;
			if (!string.IsNullOrEmpty(text))
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(text);
			}
			else
			{
				text = string.Empty;
			}
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
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00063250 File Offset: 0x00061450
		internal virtual string GetClientScriptEventReference()
		{
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			Page page = this.Page;
			if (!postBackOptions.PerformValidation && string.IsNullOrEmpty(postBackOptions.ActionUrl))
			{
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(postBackOptions);
				}
				return string.Empty;
			}
			if (page == null)
			{
				return string.Empty;
			}
			return page.ClientScript.GetPostBackEventReference(postBackOptions, true);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</returns>
		// Token: 0x060026DD RID: 9949 RVA: 0x000632AC File Offset: 0x000614AC
		protected virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			Page page = this.Page;
			postBackOptions.ActionUrl = ((this.PostBackUrl.Length > 0) ? ((page != null) ? page.ResolveClientUrl(this.PostBackUrl) : null) : null);
			postBackOptions.Argument = string.Empty;
			postBackOptions.ClientSubmit = true;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			else
			{
				postBackOptions.ValidationGroup = null;
			}
			return postBackOptions;
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
		/// <returns>Returns false for all cases.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.</param>
		// Token: 0x060026DE RID: 9950 RVA: 0x00063348 File Offset: 0x00061548
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string uniqueID = this.UniqueID;
			string text = postCollection[uniqueID + ".x"];
			string text2 = postCollection[uniqueID + ".y"];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				this.pos_x = int.Parse(text);
				this.pos_y = int.Parse(text2);
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			text = postCollection[uniqueID];
			if (!string.IsNullOrEmpty(text))
			{
				this.pos_x = int.Parse(text);
				this.pos_y = 0;
				this.Page.RegisterRequiresRaiseEvent(this);
				return true;
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control has changed.</summary>
		// Token: 0x060026DF RID: 9951 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x060026E0 RID: 9952 RVA: 0x000633E8 File Offset: 0x000615E8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnClick(new ImageClickEventArgs(this.pos_x, this.pos_y));
			this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection that contains value information indexed by control identifiers. </param>
		// Token: 0x060026E1 RID: 9953 RVA: 0x00063452 File Offset: 0x00061652
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x060026E2 RID: 9954 RVA: 0x0006345C File Offset: 0x0006165C
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">The argument for the event</param>
		// Token: 0x060026E3 RID: 9955 RVA: 0x00063464 File Offset: 0x00061664
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event directly.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains the event data. </param>
		// Token: 0x060026E4 RID: 9956 RVA: 0x00063470 File Offset: 0x00061670
		protected virtual void OnClick(ImageClickEventArgs e)
		{
			if (base.Events != null)
			{
				ImageClickEventHandler imageClickEventHandler = (ImageClickEventHandler)base.Events[ImageButton.ClickEvent];
				if (imageClickEventHandler != null)
				{
					imageClickEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event directly.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
		// Token: 0x060026E5 RID: 9957 RVA: 0x000634A8 File Offset: 0x000616A8
		protected virtual void OnCommand(CommandEventArgs e)
		{
			if (base.Events != null)
			{
				CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[ImageButton.CommandEvent];
				if (commandEventHandler != null)
				{
					commandEventHandler(this, e);
				}
			}
			base.RaiseBubbleEvent(this, e);
		}

		/// <summary>Determines whether the image has been clicked prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060026E6 RID: 9958 RVA: 0x000634E8 File Offset: 0x000616E8
		protected internal override void OnPreRender(EventArgs e)
		{
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x060026E7 RID: 9959 RVA: 0x0006350E File Offset: 0x0006170E
		// (remove) Token: 0x060026E8 RID: 9960 RVA: 0x00063521 File Offset: 0x00061721
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event ImageClickEventHandler Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x060026E9 RID: 9961 RVA: 0x00063534 File Offset: 0x00061734
		// (remove) Token: 0x060026EA RID: 9962 RVA: 0x00063547 File Offset: 0x00061747
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(ImageButton.CommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.CommandEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IButtonControl.Text" />.</summary>
		/// <returns>The text caption that is displayed for the button.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x0006355A File Offset: 0x0006175A
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x00063562 File Offset: 0x00061762
		string IButtonControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		/// <summary>For a description of this member, see the <see cref="E:System.Web.UI.WebControls.IButtonControl.Click" /> event.</summary>
		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x060026ED RID: 9965 RVA: 0x0006350E File Offset: 0x0006170E
		// (remove) Token: 0x060026EE RID: 9966 RVA: 0x00063521 File Offset: 0x00061721
		event EventHandler IButtonControl.Click
		{
			add
			{
				base.Events.AddHandler(ImageButton.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageButton.ClickEvent, value);
			}
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0006356B File Offset: 0x0006176B
		// Note: this type is marked as 'beforefieldinit'.
		static ImageButton()
		{
			ImageButton.ClickEvent = new object();
			ImageButton.CommandEvent = new object();
		}

		// Token: 0x04001A4E RID: 6734
		private int pos_x;

		// Token: 0x04001A4F RID: 6735
		private int pos_y;
	}
}
