using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a check box that allows the user to select a true or false condition.</summary>
	// Token: 0x02000347 RID: 839
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("CheckedChanged")]
	[DefaultProperty("Text")]
	[ControlValueProperty("Checked", null)]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBox : WebControl, IPostBackDataHandler, ICheckBoxControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> class.</summary>
		// Token: 0x06001EBF RID: 7871 RVA: 0x0004BA82 File Offset: 0x00049C82
		public CheckBox()
			: base(HtmlTextWriterTag.Input)
		{
			this.render_type = "checkbox";
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0004BA97 File Offset: 0x00049C97
		internal CheckBox(string render_type)
			: base(HtmlTextWriterTag.Input)
		{
			this.render_type = render_type;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> state automatically posts back to the server when clicked.</summary>
		/// <returns>true to automatically post the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to the server when it is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x0004BABB File Offset: 0x00049CBB
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is selected.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0004BAD3 File Offset: 0x00049CD3
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(false)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is checked.</summary>
		/// <returns>true to indicate a checked state; otherwise, false. The default is false.</returns>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0004BAE6 File Offset: 0x00049CE6
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0004BAF9 File Offset: 0x00049CF9
		[DefaultValue(false)]
		[Bindable(true, BindingDirection.TwoWay)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool Checked
		{
			get
			{
				return this.ViewState.GetBool("Checked", false);
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		/// <summary>Gets a reference to the collection of attributes for the rendered input element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>The collection of attribute names and values that are added to the rendered INPUT element for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0004BB14 File Offset: 0x00049D14
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection InputAttributes
		{
			get
			{
				if (this.inputAttributes == null)
				{
					if (this.inputAttributesState == null)
					{
						this.inputAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.inputAttributesState.TrackViewState();
						}
					}
					this.inputAttributes = new AttributeCollection(this.inputAttributesState);
				}
				return this.inputAttributes;
			}
		}

		/// <summary>Gets a reference to the collection of attributes for the rendered LABEL element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>The collection of attribute names and values that are added to the rendered LABEL element for the <see cref="T:System.Web.UI.WebControls.CheckBox" />. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0004BB68 File Offset: 0x00049D68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AttributeCollection LabelAttributes
		{
			get
			{
				if (this.labelAttributes == null)
				{
					if (this.labelAttributesState == null)
					{
						this.labelAttributesState = new StateBag(true);
						if (base.IsTrackingViewState)
						{
							this.labelAttributesState.TrackViewState();
						}
					}
					this.labelAttributes = new AttributeCollection(this.labelAttributesState);
				}
				return this.labelAttributes;
			}
		}

		/// <summary>Gets or sets the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" />.</summary>
		/// <returns>The text label associated with the CheckBox. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x000475B6 File Offset: 0x000457B6
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Localizable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public virtual string Text
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

		/// <summary>Gets or sets the alignment of the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is Right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0004BBBB File Offset: 0x00049DBB
		// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0004BBCE File Offset: 0x00049DCE
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(TextAlign.Right)]
		public virtual TextAlign TextAlign
		{
			get
			{
				return (TextAlign)this.ViewState.GetInt("TextAlign", 2);
			}
			set
			{
				if (value != TextAlign.Left && value != TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("")]
		[DefaultValue("")]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.CheckBox.Checked" /> property changes between posts to the server.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06001ECF RID: 7887 RVA: 0x0004BBF9 File Offset: 0x00049DF9
		// (remove) Token: 0x06001ED0 RID: 7888 RVA: 0x0004BC0C File Offset: 0x00049E0C
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EventCheckedChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CheckBox.CheckedChanged" /> event of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. This allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x0004BC20 File Offset: 0x00049E20
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		internal virtual string NameAttribute
		{
			get
			{
				return this.UniqueID;
			}
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <param name="savedState">An object that contains the saved view state values for the control. </param>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0004BC50 File Offset: 0x00049E50
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			Triplet triplet = (Triplet)savedState;
			base.LoadViewState(triplet.First);
			if (triplet.Second != null)
			{
				if (this.inputAttributesState == null)
				{
					this.inputAttributesState = new StateBag(true);
					this.inputAttributesState.TrackViewState();
				}
				this.inputAttributesState.LoadViewState(triplet.Second);
			}
			if (triplet.Third != null)
			{
				if (this.labelAttributesState == null)
				{
					this.labelAttributesState = new StateBag(true);
					this.labelAttributesState.TrackViewState();
				}
				this.labelAttributesState.LoadViewState(triplet.Third);
			}
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state; otherwise, if no view state is associated with the object, null.</returns>
		// Token: 0x06001ED4 RID: 7892 RVA: 0x0004BCEC File Offset: 0x00049EEC
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			object obj3 = null;
			if (this.inputAttributesState != null)
			{
				obj2 = this.inputAttributesState.SaveViewState();
			}
			if (this.labelAttributesState != null)
			{
				obj3 = this.labelAttributesState.SaveViewState();
			}
			if (obj == null && obj2 == null && obj3 == null)
			{
				return null;
			}
			return new Triplet(obj, obj2, obj3);
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control so that they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property. </summary>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0004BD3F File Offset: 0x00049F3F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.inputAttributesState != null)
			{
				this.inputAttributesState.TrackViewState();
			}
			if (this.labelAttributesState != null)
			{
				this.labelAttributesState.TrackViewState();
			}
		}

		/// <summary>Registers client script for generating postback prior to rendering on the client if <see cref="P:System.Web.UI.WebControls.CheckBox.AutoPostBack" /> is true.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0004BD70 File Offset: 0x00049F70
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0004BDA4 File Offset: 0x00049FA4
		private static bool IsInputOrCommonAttr(string attname)
		{
			attname = attname.ToUpper(Helpers.InvariantCulture);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(attname);
			if (num <= 1828175992U)
			{
				if (num <= 622060074U)
				{
					if (num <= 370604265U)
					{
						if (num != 223059961U)
						{
							if (num != 304993157U)
							{
								if (num != 370604265U)
								{
									return false;
								}
								if (!(attname == "SRC"))
								{
									return false;
								}
							}
							else if (!(attname == "DISABLED"))
							{
								return false;
							}
						}
						else if (!(attname == "ONMOUSEOVER"))
						{
							return false;
						}
					}
					else if (num != 527474642U)
					{
						if (num != 547231721U)
						{
							if (num != 622060074U)
							{
								return false;
							}
							if (!(attname == "VALUE"))
							{
								return false;
							}
						}
						else if (!(attname == "ACCEPT"))
						{
							return false;
						}
					}
					else if (!(attname == "ACCESSKEY"))
					{
						return false;
					}
				}
				else if (num <= 1394915399U)
				{
					if (num != 904560638U)
					{
						if (num != 990604873U)
						{
							if (num != 1394915399U)
							{
								return false;
							}
							if (!(attname == "ONMOUSEDOWN"))
							{
								return false;
							}
						}
						else if (!(attname == "ONBLUR"))
						{
							return false;
						}
					}
					else if (!(attname == "ONMOUSEUP"))
					{
						return false;
					}
				}
				else if (num <= 1636987420U)
				{
					if (num != 1566678716U)
					{
						if (num != 1636987420U)
						{
							return false;
						}
						if (!(attname == "SIZE"))
						{
							return false;
						}
					}
					else if (!(attname == "ALT"))
					{
						return false;
					}
				}
				else if (num != 1656862888U)
				{
					if (num != 1828175992U)
					{
						return false;
					}
					if (!(attname == "ONSELECT"))
					{
						return false;
					}
				}
				else if (!(attname == "TABINDEX"))
				{
					return false;
				}
			}
			else if (num <= 2741377153U)
			{
				if (num <= 1994583590U)
				{
					if (num != 1883566248U)
					{
						if (num != 1925226952U)
						{
							if (num != 1994583590U)
							{
								return false;
							}
							if (!(attname == "CHECKED"))
							{
								return false;
							}
						}
						else if (!(attname == "ONFOCUS"))
						{
							return false;
						}
					}
					else if (!(attname == "ONCHANGE"))
					{
						return false;
					}
				}
				else if (num != 2097196413U)
				{
					if (num != 2140897288U)
					{
						if (num != 2741377153U)
						{
							return false;
						}
						if (!(attname == "MAXLENGTH"))
						{
							return false;
						}
					}
					else if (!(attname == "ONCLICK"))
					{
						return false;
					}
				}
				else if (!(attname == "ONMOUSEOUT"))
				{
					return false;
				}
			}
			else if (num <= 3145768986U)
			{
				if (num != 2937075063U)
				{
					if (num != 3044347291U)
					{
						if (num != 3145768986U)
						{
							return false;
						}
						if (!(attname == "ONKEYUP"))
						{
							return false;
						}
					}
					else if (!(attname == "ONKEYDOWN"))
					{
						return false;
					}
				}
				else if (!(attname == "READONLY"))
				{
					return false;
				}
			}
			else if (num <= 3644745434U)
			{
				if (num != 3538573158U)
				{
					if (num != 3644745434U)
					{
						return false;
					}
					if (!(attname == "USEMAP"))
					{
						return false;
					}
				}
				else if (!(attname == "ONKEYPRESS"))
				{
					return false;
				}
			}
			else if (num != 3798112898U)
			{
				if (num != 4239779666U)
				{
					return false;
				}
				if (!(attname == "ONMOUSEMOVE"))
				{
					return false;
				}
			}
			else if (!(attname == "ONDBLCLICK"))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0004C150 File Offset: 0x0004A350
		private bool AddAttributesForSpan(HtmlTextWriter writer)
		{
			if (base.HasAttributes)
			{
				AttributeCollection attributes = base.Attributes;
				ICollection keys = attributes.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (CheckBox.IsInputOrCommonAttr(text))
					{
						if (this.common_attrs == null)
						{
							this.common_attrs = new AttributeCollection(new StateBag());
						}
						this.common_attrs[text] = base.Attributes[text];
						attributes.Remove(text);
					}
				}
				if (attributes.Count > 0)
				{
					attributes.AddAttributes(writer);
					return true;
				}
			}
			return false;
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBox" /> on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001ED9 RID: 7897 RVA: 0x0004C1F4 File Offset: 0x0004A3F4
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			bool flag = base.ControlStyleCreated && !base.ControlStyle.IsEmpty;
			bool isEnabled = base.IsEnabled;
			if (!isEnabled)
			{
				if (!base.RenderingCompatibilityLessThan40)
				{
					base.ControlStyle.PrependCssClass(WebControl.DisabledCssClass);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				flag = true;
			}
			if (flag)
			{
				base.AddDisplayStyleAttribute(writer);
				base.ControlStyle.AddAttributesToRender(writer, this);
			}
			string toolTip = this.ToolTip;
			if (toolTip != null && toolTip.Length > 0)
			{
				writer.AddAttribute("title", toolTip);
				flag = true;
			}
			if (base.HasAttributes && this.AddAttributesForSpan(writer))
			{
				flag = true;
			}
			if (flag)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			if (this.TextAlign == TextAlign.Right)
			{
				this.RenderInput(writer, isEnabled);
				this.RenderLabel(writer);
			}
			else
			{
				this.RenderLabel(writer);
				this.RenderInput(writer, isEnabled);
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0004C2FC File Offset: 0x0004A4FC
		private void RenderInput(HtmlTextWriter w, bool enabled)
		{
			if (this.ClientID != null && this.ClientID.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			w.AddAttribute(HtmlTextWriterAttribute.Type, this.render_type);
			string nameAttribute = this.NameAttribute;
			if (nameAttribute != null && nameAttribute.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Name, nameAttribute);
			}
			this.InternalAddAttributesToRender(w, enabled);
			this.AddAttributesToRender(w);
			if (this.Checked)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Checked, "checked", false);
			}
			if (this.AutoPostBack)
			{
				Page page = this.Page;
				string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true) : string.Empty);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				if (this.common_attrs != null && this.common_attrs["onclick"] != null)
				{
					text = ClientScriptManager.EnsureEndsWithSemicolon(this.common_attrs["onclick"]) + text;
					this.common_attrs.Remove("onclick");
				}
				w.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
			}
			if (this.AccessKey.Length > 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.TabIndex != 0)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.common_attrs != null)
			{
				this.common_attrs.AddAttributes(w);
			}
			if (this.inputAttributes != null)
			{
				this.inputAttributes.AddAttributes(w);
			}
			w.RenderBeginTag(HtmlTextWriterTag.Input);
			w.RenderEndTag();
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0004C4A0 File Offset: 0x0004A6A0
		private void RenderLabel(HtmlTextWriter w)
		{
			string text = this.Text;
			if (text.Length > 0)
			{
				if (this.labelAttributes != null)
				{
					this.labelAttributes.AddAttributes(w);
				}
				w.AddAttribute(HtmlTextWriterAttribute.For, this.ClientID);
				w.RenderBeginTag(HtmlTextWriterTag.Label);
				w.Write(text);
				w.RenderEndTag();
			}
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server.</param>
		// Token: 0x06001EDC RID: 7900 RVA: 0x0004C4F4 File Offset: 0x0004A6F4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			string text = postCollection[postDataKey];
			bool flag = text != null && text.Length > 0;
			if (this.Checked != flag)
			{
				this.Checked = flag;
				return true;
			}
			return false;
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.CheckBox.OnCheckedChanged(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control has changed.</summary>
		// Token: 0x06001EDD RID: 7901 RVA: 0x0004C538 File Offset: 0x0004A738
		protected virtual void RaisePostDataChangedEvent()
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
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
		/// <returns>true if the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> has changed; otherwise false.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information. </param>
		// Token: 0x06001EDE RID: 7902 RVA: 0x0004C57F File Offset: 0x0004A77F
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raises when posted data for a control has changed.</summary>
		// Token: 0x06001EDF RID: 7903 RVA: 0x0004C589 File Offset: 0x0004A789
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0004C594 File Offset: 0x0004A794
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

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to be rendered to the specified output stream.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001EE1 RID: 7905 RVA: 0x00003946 File Offset: 0x00001B46
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0004C60B File Offset: 0x0004A80B
		internal virtual void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
		{
			if (!enabled)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
			}
		}

		// Token: 0x04001876 RID: 6262
		private string render_type;

		// Token: 0x04001877 RID: 6263
		private AttributeCollection common_attrs;

		// Token: 0x04001878 RID: 6264
		private AttributeCollection inputAttributes;

		// Token: 0x04001879 RID: 6265
		private StateBag inputAttributesState;

		// Token: 0x0400187A RID: 6266
		private AttributeCollection labelAttributes;

		// Token: 0x0400187B RID: 6267
		private StateBag labelAttributesState;

		// Token: 0x0400187C RID: 6268
		private static readonly object EventCheckedChanged = new object();
	}
}
