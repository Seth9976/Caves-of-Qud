using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a text box control for user input.</summary>
	// Token: 0x02000425 RID: 1061
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[DefaultEvent("TextChanged")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlValueProperty("Text", null)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true, "Text")]
	[SupportsEventValidation]
	[ValidationProperty("Text")]
	[DefaultProperty("Text")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TextBox : WebControl, IPostBackDataHandler, IEditableTextControl, ITextControl
	{
		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> instance.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x0007C010 File Offset: 0x0007A210
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			switch (this.TextMode)
			{
			case TextBoxMode.SingleLine:
			case TextBoxMode.Password:
				if (this.TextMode == TextBoxMode.Password)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "password", false);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "text", false);
					if (this.Text.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Text);
					}
				}
				if (this.Columns != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Columns.ToString(), false);
				}
				if (this.MaxLength != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, this.MaxLength.ToString(), false);
				}
				if (this.AutoCompleteType != AutoCompleteType.None && this.TextMode == TextBoxMode.SingleLine)
				{
					if (this.AutoCompleteType != AutoCompleteType.Disabled)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.VCardName, TextBox.VCardValues[(int)this.AutoCompleteType]);
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off", false);
					}
				}
				break;
			case TextBoxMode.MultiLine:
				if (this.Columns != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Cols, this.Columns.ToString(), false);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Cols, "20", false);
				}
				if (this.Rows != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Rows, this.Rows.ToString(), false);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Rows, "2", false);
				}
				if (!this.Wrap)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Wrap, "off", false);
				}
				break;
			}
			if (this.AutoPostBack)
			{
				writer.AddAttribute("onkeypress", "if (WebForm_TextBoxKeyHandler(event) == false) return false;", false);
				if (page != null)
				{
					string text = page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true);
					text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
					writer.AddAttribute(HtmlTextWriterAttribute.Onchange, base.BuildScriptAttribute("onchange", text));
				}
			}
			else if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID, string.Empty);
			}
			if (this.ReadOnly)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "ReadOnly", false);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			base.AddAttributesToRender(writer);
		}

		/// <summary>Overridden to allow only literal controls to be added as the <see cref="P:System.Web.UI.WebControls.TextBox.Text" /> property.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
		// Token: 0x06002FD9 RID: 12249 RVA: 0x0007C240 File Offset: 0x0007A440
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.Text = literalControl.Text;
			}
		}

		/// <summary>Registers client script for generating postback events prior to rendering on the client, if <see cref="P:System.Web.UI.WebControls.TextBox.AutoPostBack" /> is true.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002FDA RID: 12250 RVA: 0x0007C264 File Offset: 0x0007A464
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.AutoPostBack)
			{
				this.RegisterKeyHandlerClientScript();
			}
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.TextBox" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x06002FDB RID: 12251 RVA: 0x0007C29F File Offset: 0x0007A49F
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			if (this.TextMode == TextBoxMode.MultiLine)
			{
				writer.WriteLine();
				HttpUtility.HtmlEncode(this.Text, writer);
			}
			this.RenderEndTag(writer);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false. </returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server. </param>
		// Token: 0x06002FDC RID: 12252 RVA: 0x0007C2CA File Offset: 0x0007A4CA
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.ValidateEvent(postDataKey, string.Empty);
			if (this.Text != postCollection[postDataKey])
			{
				this.Text = postCollection[postDataKey];
				return true;
			}
			return false;
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.TextBox.OnTextChanged(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control has changed.</summary>
		// Token: 0x06002FDD RID: 12253 RVA: 0x0007C2FC File Offset: 0x0007A4FC
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnTextChanged(EventArgs.Empty);
		}

		/// <summary>Loads the posted text box content if it is different from the last posting. </summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server. </param>
		// Token: 0x06002FDE RID: 12254 RVA: 0x0007C322 File Offset: 0x0007A522
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.TextBox.OnTextChanged(System.EventArgs)" /> method whenever posted data for the text box has changed. </summary>
		// Token: 0x06002FDF RID: 12255 RVA: 0x0007C32C File Offset: 0x0007A52C
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Saves the changes to the text box view state since the time the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.TextBox" /> view state. If no view state is associated with the object, this method returns null.</returns>
		// Token: 0x06002FE0 RID: 12256 RVA: 0x0007C334 File Offset: 0x0007A534
		protected override object SaveViewState()
		{
			if (this.TextMode == TextBoxMode.Password)
			{
				this.ViewState.SetItemDirty("Text", false);
			}
			return base.SaveViewState();
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0007C358 File Offset: 0x0007A558
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			Page page = this.Page;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0007C3D0 File Offset: 0x0007A5D0
		private void RegisterKeyHandlerClientScript()
		{
			if (!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(TextBox), "KeyHandler"))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("function WebForm_TextBoxKeyHandler(event) {");
				stringBuilder.AppendLine("\tvar target = event.target;");
				stringBuilder.AppendLine("\tif ((target == null) || (typeof(target) == \"undefined\")) target = event.srcElement;");
				stringBuilder.AppendLine("\tif (event.keyCode == 13) {");
				stringBuilder.AppendLine("\t\tif ((typeof(target) != \"undefined\") && (target != null)) {");
				stringBuilder.AppendLine("\t\t\tif (typeof(target.onchange) != \"undefined\") {");
				stringBuilder.AppendLine("\t\t\t\ttarget.onchange();");
				stringBuilder.AppendLine("\t\t\t\tevent.cancelBubble = true;");
				stringBuilder.AppendLine("\t\t\t\tif (event.stopPropagation) event.stopPropagation();");
				stringBuilder.AppendLine("\t\t\t\treturn false;");
				stringBuilder.AppendLine("\t\t\t}");
				stringBuilder.AppendLine("\t\t}");
				stringBuilder.AppendLine("\t}");
				stringBuilder.AppendLine("\treturn true;");
				stringBuilder.AppendLine("}");
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(TextBox), "KeyHandler", stringBuilder.ToString(), true);
			}
		}

		/// <summary>Gets or sets a value that indicates the AutoComplete behavior of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.AutoCompleteType" /> enumeration values, indicating the AutoComplete behavior for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control. The default value is <see cref="F:System.Web.UI.WebControls.AutoCompleteType.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.AutoCompleteType" /> enumeration values.</exception>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x0007C4E4 File Offset: 0x0007A6E4
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x0007C50D File Offset: 0x0007A70D
		[Themeable(false)]
		[DefaultValue(AutoCompleteType.None)]
		public virtual AutoCompleteType AutoCompleteType
		{
			get
			{
				object obj = this.ViewState["AutoCompleteType"];
				if (obj == null)
				{
					return AutoCompleteType.None;
				}
				return (AutoCompleteType)obj;
			}
			set
			{
				this.ViewState["AutoCompleteType"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an automatic postback to the server occurs when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control loses focus.</summary>
		/// <returns>true if an automatic postback occurs when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control loses focus; otherwise, false. The default is false.</returns>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x0004BABB File Offset: 0x00049CBB
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				return this.ViewState.GetBool("AutoPostBack", false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control is set to validate when a postback occurs.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control is set to validate when a postback occurs; otherwise, false. The default value is false.</returns>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x0004BAD3 File Offset: 0x00049CD3
		// (set) Token: 0x06002FE8 RID: 12264 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[Themeable(false)]
		[DefaultValue(false)]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the display width of the text box in characters.</summary>
		/// <returns>The display width, in characters, of the text box. The default is 0, which indicates that the property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified width is less than 0. </exception>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x0007C525 File Offset: 0x0007A725
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x0007C538 File Offset: 0x0007A738
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(0)]
		public virtual int Columns
		{
			get
			{
				return this.ViewState.GetInt("Columns", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Columns value has to be 0 for 'not set' or bigger than 0.");
				}
				this.ViewState["Columns"] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters allowed in the text box.</summary>
		/// <returns>The maximum number of characters allowed in the text box. The default is 0, which indicates that the property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified width is less than 0. </exception>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x0007C564 File Offset: 0x0007A764
		// (set) Token: 0x06002FEC RID: 12268 RVA: 0x0007C577 File Offset: 0x0007A777
		[Themeable(false)]
		[DefaultValue(0)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual int MaxLength
		{
			get
			{
				return this.ViewState.GetInt("MaxLength", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "MaxLength value has to be 0 for 'not set' or bigger than 0.");
				}
				this.ViewState["MaxLength"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control can be changed.</summary>
		/// <returns>true if the contents of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control cannot be changed; otherwise, false. The default value is false.</returns>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x0007C5A3 File Offset: 0x0007A7A3
		// (set) Token: 0x06002FEE RID: 12270 RVA: 0x0007C5B6 File Offset: 0x0007A7B6
		[Bindable(true)]
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool ReadOnly
		{
			get
			{
				return this.ViewState.GetBool("ReadOnly", false);
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		/// <summary>Gets or sets the number of rows displayed in a multiline text box.</summary>
		/// <returns>The number of rows in a multiline text box. The default is 0, which displays a two-line text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x0007C5CE File Offset: 0x0007A7CE
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x0007C5E1 File Offset: 0x0007A7E1
		[WebSysDescription("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DefaultValue(0)]
		public virtual int Rows
		{
			get
			{
				return this.ViewState.GetInt("Rows", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Rows value has to be 0 for 'not set' or bigger than 0.");
				}
				this.ViewState["Rows"] = value;
			}
		}

		/// <summary>Gets the HTML tag for the text box control. This property is protected.</summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.HtmlTextWriterTag.Textarea" /> if the text box is multiline; otherwise, <see cref="F:System.Web.UI.HtmlTextWriterTag.Input" />.</returns>
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x0007C60D File Offset: 0x0007A80D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.TextMode != TextBoxMode.MultiLine)
				{
					return HtmlTextWriterTag.Input;
				}
				return HtmlTextWriterTag.Textarea;
			}
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
		/// <returns>The text displayed in the <see cref="T:System.Web.UI.WebControls.TextBox" /> control. The default is an empty string ("").</returns>
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06002FF2 RID: 12274 RVA: 0x0007C61D File Offset: 0x0007A81D
		// (set) Token: 0x06002FF3 RID: 12275 RVA: 0x000475B6 File Offset: 0x000457B6
		[WebCategory("Appearance")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
		[DefaultValue("")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", "");
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the behavior mode (single-line, multiline, or password) of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextBoxMode" /> enumeration values. The default value is SingleLine.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified mode is not one of the <see cref="T:System.Web.UI.WebControls.TextBoxMode" /> enumeration values. </exception>
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x0007C634 File Offset: 0x0007A834
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x0007C647 File Offset: 0x0007A847
		[DefaultValue(TextBoxMode.SingleLine)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual TextBoxMode TextMode
		{
			get
			{
				return (TextBoxMode)this.ViewState.GetInt("TextMode", 0);
			}
			set
			{
				this.ViewState["TextMode"] = (int)value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.TextBox" /> control causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.TextBox" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x0003F11F File Offset: 0x0003D31F
		// (set) Token: 0x06002FF7 RID: 12279 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text content wraps within a multiline text box.</summary>
		/// <returns>true if the text content wraps within a multiline text box; otherwise, false. The default is true.</returns>
		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x0007C65F File Offset: 0x0007A85F
		// (set) Token: 0x06002FF9 RID: 12281 RVA: 0x0007C672 File Offset: 0x0007A872
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual bool Wrap
		{
			get
			{
				return this.ViewState.GetBool("Wrap", true);
			}
			set
			{
				this.ViewState["Wrap"] = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TextBox.TextChanged" /> event. This allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event information. </param>
		// Token: 0x06002FFA RID: 12282 RVA: 0x0007C68C File Offset: 0x0007A88C
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBox.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the content of the text box changes between posts to the server.</summary>
		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06002FFB RID: 12283 RVA: 0x0007C6BA File Offset: 0x0007A8BA
		// (remove) Token: 0x06002FFC RID: 12284 RVA: 0x0007C6CD File Offset: 0x0007A8CD
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(TextBox.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBox.TextChangedEvent, value);
			}
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0007C6E0 File Offset: 0x0007A8E0
		// Note: this type is marked as 'beforefieldinit'.
		static TextBox()
		{
			TextBox.TextChangedEvent = new object();
		}

		// Token: 0x04001BFF RID: 7167
		private static readonly string[] VCardValues = new string[]
		{
			null, null, "vCard.Cellular", "vCard.Company", "vCard.Department", "vCard.DisplayName", "vCard.Email", "vCard.FirstName", "vCard.Gender", "vCard.Home.City",
			"HomeCountry", "vCard.Home.Fax", "vCard.Home.Phone", "vCard.Home.State", "vCard.Home.StreetAddress", "vCard.Home.ZipCode", "vCard.Home.page", "vCard.JobTitle", "vCard.LastName", "vCard.MiddleName",
			"vCard.Notes", "vCard.Office", "vCard.Pager", "vCard.Business.City", "BusinessCountry", "vCard.Business.Fax", "vCard.Business.Phone", "vCard.Business.State", "vCard.Business.StreetAddress", "vCard.Business.Url",
			"vCard.Business.ZipCode", "search"
		};
	}
}
