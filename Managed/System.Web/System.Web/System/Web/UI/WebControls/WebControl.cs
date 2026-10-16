using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class that defines the methods, properties and events common to all controls in the <see cref="N:System.Web.UI.WebControls" /> namespace.</summary>
	// Token: 0x02000439 RID: 1081
	[ParseChildren(true)]
	[Themeable(true)]
	[PersistChildren(false, false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebControl : Control, IAttributeAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
		/// <param name="tag">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values. </param>
		// Token: 0x060031C9 RID: 12745 RVA: 0x000830E3 File Offset: 0x000812E3
		public WebControl(HtmlTextWriterTag tag)
		{
			this.tag = tag;
			this.enabled = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class that represents a Span HTML tag.</summary>
		// Token: 0x060031CA RID: 12746 RVA: 0x000830F9 File Offset: 0x000812F9
		protected WebControl()
			: this(HtmlTextWriterTag.Span)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
		/// <param name="tag">An HTML tag. </param>
		// Token: 0x060031CB RID: 12747 RVA: 0x00083103 File Offset: 0x00081303
		protected WebControl(string tag)
		{
			this.tag = HtmlTextWriterTag.Unknown;
			this.tag_name = tag;
			this.enabled = true;
		}

		/// <summary>Gets or sets the access key that allows you to quickly navigate to the Web server control.</summary>
		/// <returns>The access key for quick navigation to the Web server control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither null, <see cref="F:System.String.Empty" /> nor a single character string. </exception>
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x00083120 File Offset: 0x00081320
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x00083137 File Offset: 0x00081337
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string AccessKey
		{
			get
			{
				return this.ViewState.GetString("AccessKey", string.Empty);
			}
			set
			{
				if (value == null || value.Length < 2)
				{
					this.ViewState["AccessKey"] = value;
					return;
				}
				throw new ArgumentException("AccessKey can only be null, empty or a single character", "value");
			}
		}

		/// <summary>Gets the collection of arbitrary attributes (for rendering only) that do not correspond to properties on the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> of name and value pairs.</returns>
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x00083166 File Offset: 0x00081366
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[Browsable(false)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attribute_state = new StateBag(true);
					if (base.IsTrackingViewState)
					{
						this.attribute_state.TrackViewState();
					}
					this.attributes = new AttributeCollection(this.attribute_state);
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the background color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000831A6 File Offset: 0x000813A6
		// (set) Token: 0x060031D0 RID: 12752 RVA: 0x000831C1 File Offset: 0x000813C1
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual Color BackColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.BackColor;
			}
			set
			{
				this.ControlStyle.BackColor = value;
			}
		}

		/// <summary>Gets or sets the border color of the Web control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000831CF File Offset: 0x000813CF
		// (set) Token: 0x060031D2 RID: 12754 RVA: 0x000831EA File Offset: 0x000813EA
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual Color BorderColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.BorderColor;
			}
			set
			{
				this.ControlStyle.BorderColor = value;
			}
		}

		/// <summary>Gets or sets the border style of the Web server control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000831F8 File Offset: 0x000813F8
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x0008320F File Offset: 0x0008140F
		[DefaultValue(BorderStyle.NotSet)]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				if (this.style == null)
				{
					return BorderStyle.NotSet;
				}
				return this.style.BorderStyle;
			}
			set
			{
				if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlStyle.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the border width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentException">The specified border width is a negative value. </exception>
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x00083231 File Offset: 0x00081431
		// (set) Token: 0x060031D6 RID: 12758 RVA: 0x0008324C File Offset: 0x0008144C
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit BorderWidth
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.BorderWidth;
			}
			set
			{
				this.ControlStyle.BorderWidth = value;
			}
		}

		/// <summary>Gets the style of the Web server control. This property is used primarily by control developers.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that encapsulates the appearance properties of the Web server control.</returns>
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x0008325A File Offset: 0x0008145A
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Style ControlStyle
		{
			get
			{
				if (this.style == null)
				{
					this.style = this.CreateControlStyle();
					if (base.IsTrackingViewState)
					{
						this.style.TrackViewState();
					}
				}
				return this.style;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property. This property is primarily used by control developers.</summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property; otherwise, false.</returns>
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x00083289 File Offset: 0x00081489
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool ControlStyleCreated
		{
			get
			{
				return this.style != null;
			}
		}

		/// <summary>Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.</summary>
		/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x00083294 File Offset: 0x00081494
		// (set) Token: 0x060031DA RID: 12762 RVA: 0x000832AF File Offset: 0x000814AF
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[CssClassProperty]
		public virtual string CssClass
		{
			get
			{
				if (this.style == null)
				{
					return string.Empty;
				}
				return this.style.CssClass;
			}
			set
			{
				this.ControlStyle.CssClass = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Web server control is enabled.</summary>
		/// <returns>true if control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000832BD File Offset: 0x000814BD
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000832C5 File Offset: 0x000814C5
		[Themeable(false)]
		[Bindable(true)]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					if (base.IsTrackingViewState)
					{
						this.track_enabled_state = true;
					}
					this.enabled = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is true.</returns>
		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x0006E38C File Offset: 0x0006C58C
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x0006E394 File Offset: 0x0006C594
		[Browsable(true)]
		public new virtual bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets the font properties associated with the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000832E6 File Offset: 0x000814E6
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		public virtual FontInfo Font
		{
			get
			{
				return this.ControlStyle.Font;
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000832F3 File Offset: 0x000814F3
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x0008330E File Offset: 0x0008150E
		[WebCategory("Appearance")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebSysDescription("")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color ForeColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.ForeColor;
			}
			set
			{
				this.ControlStyle.ForeColor = value;
			}
		}

		/// <summary>Gets a value indicating whether the control has attributes set.</summary>
		/// <returns>true if the control has attributes set; otherwise, false.</returns>
		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x0008331C File Offset: 0x0008151C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Gets or sets the height of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The height was set to a negative value.</exception>
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x00083336 File Offset: 0x00081536
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x00083351 File Offset: 0x00081551
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual Unit Height
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.Height;
			}
			set
			{
				this.ControlStyle.Height = value;
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.WebControl.SkinID" /> property does not exist in the theme.</exception>
		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x0003003F File Offset: 0x0002E23F
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x00030047 File Offset: 0x0002E247
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		/// <summary>Gets a collection of text attributes that will be rendered as a style attribute on the outer tag of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> that contains the HTML style attributes to render on the outer tag of the Web server control.</returns>
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x0008335F File Offset: 0x0008155F
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		/// <summary>Gets or sets the tab index of the Web server control.</summary>
		/// <returns>The tab index of the Web server control. The default is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x0008336C File Offset: 0x0008156C
		// (set) Token: 0x060031E9 RID: 12777 RVA: 0x0008337F File Offset: 0x0008157F
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(0)]
		public virtual short TabIndex
		{
			get
			{
				return this.ViewState.GetShort("TabIndex", 0);
			}
			set
			{
				this.ViewState["TabIndex"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed when the mouse pointer hovers over the Web server control.</summary>
		/// <returns>The text displayed when the mouse pointer hovers over the Web server control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x00083397 File Offset: 0x00081597
		// (set) Token: 0x060031EB RID: 12779 RVA: 0x000833AE File Offset: 0x000815AE
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual string ToolTip
		{
			get
			{
				return this.ViewState.GetString("ToolTip", string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The width of the Web server control was set to a negative value. </exception>
		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000833C1 File Offset: 0x000815C1
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x000833DC File Offset: 0x000815DC
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("")]
		public virtual Unit Width
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.Width;
			}
			set
			{
				this.ControlStyle.Width = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000833EA File Offset: 0x000815EA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return this.tag;
			}
		}

		/// <summary>Gets the name of the control tag. This property is used primarily by control developers.</summary>
		/// <returns>The name of the control tag.</returns>
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000833F2 File Offset: 0x000815F2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual string TagName
		{
			get
			{
				if (this.tag_name == null)
				{
					this.tag_name = HtmlTextWriter.StaticGetTagName(this.TagKey);
				}
				return this.tag_name;
			}
		}

		/// <summary>Gets a value indicating whether the control is enabled.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebControl" /> object is enabled; otherwise, false.</returns>
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x00083414 File Offset: 0x00081614
		protected internal bool IsEnabled
		{
			get
			{
				for (WebControl webControl = this; webControl != null; webControl = webControl.Parent as WebControl)
				{
					if (!webControl.Enabled)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Gets or sets the CSS class to apply to the rendered HTML element when the control is disabled.</summary>
		/// <returns>The CSS class that should be applied to the rendered HTML element when the control is disabled. The default value is "aspNetDisabled".</returns>
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0008343F File Offset: 0x0008163F
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x00083446 File Offset: 0x00081646
		public static string DisabledCssClass { get; set; } = "aspNetDisabled";

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x00008B4A File Offset: 0x00006D4A
		[Browsable(false)]
		public virtual bool SupportsDisabledAttribute
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies any nonblank elements of the specified style to the Web control, overwriting any existing style elements of the control. This method is primarily used by control developers.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
		// Token: 0x060031F4 RID: 12788 RVA: 0x0008344E File Offset: 0x0008164E
		public void ApplyStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.CopyFrom(s);
			}
		}

		/// <summary>Copies the properties not encapsulated by the <see cref="P:System.Web.UI.WebControls.WebControl.Style" /> object from the specified Web server control to the Web server control that this method is called from. This method is used primarily by control developers.</summary>
		/// <param name="controlSrc">A <see cref="T:System.Web.UI.WebControls.WebControl" /> that represents the source control with properties to be copied to the Web server control that this method is called from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="controlSrc" /> is null.</exception>
		// Token: 0x060031F5 RID: 12789 RVA: 0x00083468 File Offset: 0x00081668
		public void CopyBaseAttributes(WebControl controlSrc)
		{
			if (controlSrc == null)
			{
				return;
			}
			this.Enabled = controlSrc.Enabled;
			object obj = controlSrc.ViewState["AccessKey"];
			if (obj != null)
			{
				this.ViewState["AccessKey"] = obj;
			}
			obj = controlSrc.ViewState["TabIndex"];
			if (obj != null)
			{
				this.ViewState["TabIndex"] = obj;
			}
			obj = controlSrc.ViewState["ToolTip"];
			if (obj != null)
			{
				this.ViewState["ToolTip"] = obj;
			}
			if (controlSrc.attributes != null)
			{
				AttributeCollection attributeCollection = this.Attributes;
				foreach (object obj2 in controlSrc.attributes.Keys)
				{
					string text = (string)obj2;
					attributeCollection[text] = controlSrc.attributes[text];
				}
			}
		}

		/// <summary>Copies any nonblank elements of the specified style to the Web control, but will not overwrite any existing style elements of the control. This method is used primarily by control developers.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
		// Token: 0x060031F6 RID: 12790 RVA: 0x00083564 File Offset: 0x00081764
		public void MergeStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.MergeWith(s);
			}
		}

		/// <summary>Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x060031F7 RID: 12791 RVA: 0x0008357D File Offset: 0x0008177D
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			if (this.TagKey == HtmlTextWriterTag.Unknown)
			{
				writer.RenderBeginTag(this.TagName);
				return;
			}
			writer.RenderBeginTag(this.TagKey);
		}

		/// <summary>Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x060031F8 RID: 12792 RVA: 0x000431E9 File Offset: 0x000413E9
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000835A8 File Offset: 0x000817A8
		internal string BuildScriptAttribute(string name, string tail)
		{
			AttributeCollection attributeCollection = this.Attributes;
			string text = attributeCollection[name];
			if (text == null || text.Length == 0)
			{
				return tail;
			}
			if (text[text.Length - 1] == ';')
			{
				text = text.TrimEnd(WebControl._script_trim_chars);
			}
			text = text + ";" + tail;
			attributeCollection.Remove(name);
			return text;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00083604 File Offset: 0x00081804
		internal void AddDisplayStyleAttribute(HtmlTextWriter writer)
		{
			if (!this.ControlStyleCreated)
			{
				return;
			}
			if (!this.ControlStyle.BorderWidth.IsEmpty || (this.ControlStyle.BorderStyle != BorderStyle.None && this.ControlStyle.BorderStyle != BorderStyle.NotSet) || !this.ControlStyle.Height.IsEmpty || !this.ControlStyle.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00083681 File Offset: 0x00081881
		private void RenderDisabled(HtmlTextWriter writer)
		{
			if (!this.IsEnabled)
			{
				if (!this.SupportsDisabledAttribute)
				{
					this.ControlStyle.PrependCssClass(WebControl.DisabledCssClass);
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" />. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x060031FC RID: 12796 RVA: 0x000836B4 File Offset: 0x000818B4
		protected virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.RenderDisabled(writer);
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (this.AccessKey != string.Empty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.ToolTip != string.Empty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			if (this.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			if (this.style != null && !this.style.IsEmpty)
			{
				if (this.TagKey == HtmlTextWriterTag.Span)
				{
					this.AddDisplayStyleAttribute(writer);
				}
				this.style.AddAttributesToRender(writer, this);
			}
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes.Keys)
				{
					string text = (string)obj;
					writer.AddAttribute(text, this.attributes[text]);
				}
			}
		}

		/// <summary>Creates the style object that is used internally by the <see cref="T:System.Web.UI.WebControls.WebControl" /> class to implement all style related properties. This method is used primarily by control developers.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that is used to implement all style-related properties of the control.</returns>
		// Token: 0x060031FD RID: 12797 RVA: 0x000837D4 File Offset: 0x000819D4
		protected virtual Style CreateControlStyle()
		{
			return new Style(this.ViewState);
		}

		/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method. </summary>
		/// <param name="savedState">An object that represents the control state to restore. </param>
		// Token: 0x060031FE RID: 12798 RVA: 0x000837E4 File Offset: 0x000819E4
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null || !(savedState is Pair))
			{
				base.LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (this.ViewState["_!SB"] != null)
			{
				this.ControlStyle.LoadBitState();
			}
			if (pair.Second != null)
			{
				if (this.attribute_state == null)
				{
					this.attribute_state = new StateBag();
					if (base.IsTrackingViewState)
					{
						this.attribute_state.TrackViewState();
					}
				}
				this.attribute_state.LoadViewState(pair.Second);
				this.attributes = new AttributeCollection(this.attribute_state);
			}
			this.enabled = this.ViewState.GetBool("Enabled", this.enabled);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000838A0 File Offset: 0x00081AA0
		internal virtual string InlinePropertiesSet()
		{
			List<string> list = new List<string>();
			if (this.BackColor != Color.Empty)
			{
				list.Add("BackColor");
			}
			if (this.BorderColor != Color.Empty)
			{
				list.Add("BorderColor");
			}
			if (this.BorderStyle != BorderStyle.NotSet)
			{
				list.Add("BorderStyle");
			}
			if (this.BorderWidth != Unit.Empty)
			{
				list.Add("BorderWidth");
			}
			if (this.CssClass != string.Empty)
			{
				list.Add("CssClass");
			}
			if (this.ForeColor != Color.Empty)
			{
				list.Add("ForeColor");
			}
			if (this.Height != Unit.Empty)
			{
				list.Add("Height");
			}
			if (this.Width != Unit.Empty)
			{
				list.Add("Width");
			}
			if (list.Count == 0)
			{
				return null;
			}
			return string.Join(", ", list);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000839A8 File Offset: 0x00081BA8
		internal void VerifyInlinePropertiesNotSet()
		{
			IRenderOuterTable renderOuterTable = this as IRenderOuterTable;
			if (renderOuterTable == null || renderOuterTable.RenderOuterTable)
			{
				return;
			}
			string text = this.InlinePropertiesSet();
			if (!string.IsNullOrEmpty(text))
			{
				bool flag = text.IndexOf(',') > -1;
				throw new InvalidOperationException(string.Format("The style propert{0} '{1}' cannot be used while RenderOuterTable is disabled on the {2} control with ID '{3}'", new object[]
				{
					flag ? "ies" : "y",
					text,
					base.GetType().Name,
					this.ID
				}));
			}
		}

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x06003201 RID: 12801 RVA: 0x00083A26 File Offset: 0x00081C26
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.Adapter != null)
			{
				base.Adapter.Render(writer);
				return;
			}
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003202 RID: 12802 RVA: 0x0006C6AD File Offset: 0x0006A8AD
		protected internal virtual void RenderContents(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
		/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.</returns>
		// Token: 0x06003203 RID: 12803 RVA: 0x00083A54 File Offset: 0x00081C54
		protected override object SaveViewState()
		{
			if (this.track_enabled_state)
			{
				this.ViewState["Enabled"] = this.enabled;
			}
			object obj = null;
			if (this.style != null)
			{
				this.style.SaveBitState();
			}
			object obj2 = base.SaveViewState();
			if (this.attribute_state != null)
			{
				obj = this.attribute_state.SaveViewState();
			}
			if (obj2 == null && obj == null)
			{
				return null;
			}
			return new Pair(obj2, obj);
		}

		/// <summary>Causes the control to track changes to its view state so they can be stored in the object's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x06003204 RID: 12804 RVA: 0x00083AC3 File Offset: 0x00081CC3
		protected override void TrackViewState()
		{
			if (this.style != null)
			{
				this.style.TrackViewState();
			}
			if (this.attribute_state != null)
			{
				this.attribute_state.TrackViewState();
				this.attribute_state.SetDirty(true);
			}
			base.TrackViewState();
		}

		/// <summary>Gets an attribute of the Web control with the specified name.</summary>
		/// <returns>The value of the attribute.</returns>
		/// <param name="name">The name of the attribute.</param>
		// Token: 0x06003205 RID: 12805 RVA: 0x00083AFD File Offset: 0x00081CFD
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this.attributes != null)
			{
				return this.attributes[key];
			}
			return null;
		}

		/// <summary>Sets an attribute of the Web control to the specified name and value.</summary>
		/// <param name="name">The name component of the attribute's name/value pair.</param>
		/// <param name="value">The value component of the attribute's name/value pair.</param>
		// Token: 0x06003206 RID: 12806 RVA: 0x00083B15 File Offset: 0x00081D15
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x04001C5C RID: 7260
		private const string DEFAULT_DISABLED_CSS_CLASS = "aspNetDisabled";

		// Token: 0x04001C5D RID: 7261
		private Style style;

		// Token: 0x04001C5E RID: 7262
		private HtmlTextWriterTag tag;

		// Token: 0x04001C5F RID: 7263
		private string tag_name;

		// Token: 0x04001C60 RID: 7264
		private AttributeCollection attributes;

		// Token: 0x04001C61 RID: 7265
		private StateBag attribute_state;

		// Token: 0x04001C62 RID: 7266
		private bool enabled;

		// Token: 0x04001C63 RID: 7267
		private bool track_enabled_state;

		// Token: 0x04001C65 RID: 7269
		private static char[] _script_trim_chars = new char[] { ';' };
	}
}
