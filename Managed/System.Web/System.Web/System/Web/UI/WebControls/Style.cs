using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style of a Web server control.</summary>
	// Token: 0x0200040C RID: 1036
	[ToolboxItem("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Style : Component, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class using default values.</summary>
		// Token: 0x06002E92 RID: 11922 RVA: 0x000791DB File Offset: 0x000773DB
		public Style()
		{
			this.viewstate = new StateBag();
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class with the specified state bag information.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
		// Token: 0x06002E93 RID: 11923 RVA: 0x000791F4 File Offset: 0x000773F4
		public Style(StateBag bag)
		{
			this.viewstate = bag;
			if (this.viewstate == null)
			{
				this.viewstate = new StateBag();
			}
			this._isSharedViewState = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets the background color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x00079223 File Offset: 0x00077423
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x00079249 File Offset: 0x00077449
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(WebColorConverter))]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		public Color BackColor
		{
			get
			{
				if (!this.CheckBit(8))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["BackColor"];
			}
			set
			{
				this.viewstate["BackColor"] = value;
				this.SetBit(8);
			}
		}

		/// <summary>Gets or sets the border color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x00079268 File Offset: 0x00077468
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x0007928F File Offset: 0x0007748F
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(WebColorConverter))]
		public Color BorderColor
		{
			get
			{
				if (!this.CheckBit(16))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["BorderColor"];
			}
			set
			{
				this.viewstate["BorderColor"] = value;
				this.SetBit(16);
			}
		}

		/// <summary>Gets or sets the border style of the Web server control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values.</exception>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000792AF File Offset: 0x000774AF
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x000792D2 File Offset: 0x000774D2
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue(BorderStyle.NotSet)]
		[WebSysDescription("")]
		public BorderStyle BorderStyle
		{
			get
			{
				if (!this.CheckBit(64))
				{
					return BorderStyle.NotSet;
				}
				return (BorderStyle)this.viewstate["BorderStyle"];
			}
			set
			{
				if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
				{
					throw new ArgumentOutOfRangeException("value", "The selected value is not one of the BorderStyle enumeration values.");
				}
				this.viewstate["BorderStyle"] = value;
				this.SetBit(64);
			}
		}

		/// <summary>Gets or sets the border width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified border width is a negative value. </exception>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x0007930B File Offset: 0x0007750B
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x00079334 File Offset: 0x00077534
		[WebCategory("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		public Unit BorderWidth
		{
			get
			{
				if (!this.CheckBit(32))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["BorderWidth"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "BorderWidth must not be negative");
				}
				this.viewstate["BorderWidth"] = value;
				this.SetBit(32);
			}
		}

		/// <summary>Gets or sets the cascading style sheet (CSS) class rendered by the Web server control on the client.</summary>
		/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x00079390 File Offset: 0x00077590
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x000793CC File Offset: 0x000775CC
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[CssClassProperty]
		public string CssClass
		{
			get
			{
				if (!this.CheckBit(2))
				{
					return string.Empty;
				}
				string text = this.viewstate["CssClass"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.viewstate["CssClass"] = value;
				this.SetBit(2);
			}
		}

		/// <summary>Gets the font properties associated with the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000793E6 File Offset: 0x000775E6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public FontInfo Font
		{
			get
			{
				if (this.fontinfo == null)
				{
					this.fontinfo = new FontInfo(this);
				}
				return this.fontinfo;
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x00079402 File Offset: 0x00077602
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x00079428 File Offset: 0x00077628
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[TypeConverter(typeof(WebColorConverter))]
		public Color ForeColor
		{
			get
			{
				if (!this.CheckBit(4))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["ForeColor"];
			}
			set
			{
				this.viewstate["ForeColor"] = value;
				this.SetBit(4);
			}
		}

		/// <summary>Gets or sets the height of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x00079447 File Offset: 0x00077647
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x00079474 File Offset: 0x00077674
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public Unit Height
		{
			get
			{
				if (!this.CheckBit(128))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["Height"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "Height must not be negative");
				}
				this.viewstate["Height"] = value;
				this.SetBit(128);
			}
		}

		/// <summary>Gets or sets the width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000794D0 File Offset: 0x000776D0
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000794FC File Offset: 0x000776FC
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit Width
		{
			get
			{
				if (!this.CheckBit(256))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["Width"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "Width must not be negative");
				}
				this.viewstate["Width"] = value;
				this.SetBit(256);
			}
		}

		/// <summary>A protected property. Gets a value indicating whether any style elements have been defined in the state bag.</summary>
		/// <returns>true if the state bag has no style elements defined; otherwise, false.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x00079558 File Offset: 0x00077758
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsEmpty
		{
			get
			{
				return this.styles == 0 && this.RegisteredCssClass.Length == 0;
			}
		}

		/// <summary>Returns a value indicating whether any style elements have been defined in the state bag.</summary>
		/// <returns>true if there are style elements defined in the state bag; otherwise, false.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x00079572 File Offset: 0x00077772
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets the state bag that holds the style elements.</summary>
		/// <returns>A state bag that holds the style elements defined in it.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x0007957A File Offset: 0x0007777A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal StateBag ViewState
		{
			get
			{
				return this.viewstate;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x00079582 File Offset: 0x00077782
		// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x000795AD File Offset: 0x000777AD
		internal bool AlwaysRenderTextDecoration
		{
			get
			{
				return this.viewstate["AlwaysRenderTextDecoration"] != null && (bool)this.viewstate["AlwaysRenderTextDecoration"];
			}
			set
			{
				this.viewstate["AlwaysRenderTextDecoration"] = value;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />. This method is primarily used by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002EAA RID: 11946 RVA: 0x000795C5 File Offset: 0x000777C5
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer, null);
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> and Web server control. This method is primarily used by control developers.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.WebControl" /> or <see cref="T:System.Web.UI.WebControls.WebControl" /> derived object that represents the Web server control associated with the <see cref="T:System.Web.UI.WebControls.Style" />. </param>
		// Token: 0x06002EAB RID: 11947 RVA: 0x000795D0 File Offset: 0x000777D0
		public virtual void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			if (this.RegisteredCssClass.Length <= 0)
			{
				string cssClass = this.CssClass;
				if (cssClass != null && cssClass.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
				}
				CssStyleCollection cssStyleCollection = new CssStyleCollection();
				this.FillStyleAttributes(cssStyleCollection, owner);
				foreach (object obj in cssStyleCollection.Keys)
				{
					string text = (string)obj;
					writer.AddStyleAttribute(text, cssStyleCollection[text]);
				}
				return;
			}
			string cssClass2 = this.CssClass;
			if (!string.IsNullOrEmpty(cssClass2))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass2 + " " + this.RegisteredCssClass);
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.RegisteredCssClass);
		}

		/// <summary>Adds the specified object's style properties to a <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
		/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> object to which to add the style properties. </param>
		/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" /> -implemented object that contains the context information for the current location (URL). </param>
		// Token: 0x06002EAC RID: 11948 RVA: 0x000796A8 File Offset: 0x000778A8
		protected virtual void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			if (this.CheckBit(8))
			{
				Color color = (Color)this.viewstate["BackColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(color));
				}
			}
			if (this.CheckBit(16))
			{
				Color color = (Color)this.viewstate["BorderColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(color));
				}
			}
			bool flag = false;
			if (this.CheckBit(32))
			{
				Unit unit = (Unit)this.viewstate["BorderWidth"];
				if (!unit.IsEmpty)
				{
					if (unit.Value > 0.0)
					{
						flag = true;
					}
					attributes.Add(HtmlTextWriterStyle.BorderWidth, unit.ToString());
				}
			}
			if (this.CheckBit(64))
			{
				BorderStyle borderStyle = (BorderStyle)this.viewstate["BorderStyle"];
				if (borderStyle != BorderStyle.NotSet)
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, borderStyle.ToString());
				}
				else if (flag)
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
				}
			}
			else if (flag)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
			}
			if (this.CheckBit(4))
			{
				Color color = (Color)this.viewstate["ForeColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(color));
				}
			}
			if (this.CheckBit(128))
			{
				Unit unit = (Unit)this.viewstate["Height"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Height, unit.ToString());
				}
			}
			if (this.CheckBit(256))
			{
				Unit unit = (Unit)this.viewstate["Width"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Width, unit.ToString());
				}
			}
			this.Font.FillStyleAttributes(attributes, this.AlwaysRenderTextDecoration);
		}

		/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to copy. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
		// Token: 0x06002EAD RID: 11949 RVA: 0x00079898 File Offset: 0x00077A98
		public virtual void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.fontinfo != null)
			{
				this.Font.CopyFrom(s.fontinfo);
			}
			if (s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				this.BackColor = s.BackColor;
			}
			if (s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				this.BorderColor = s.BorderColor;
			}
			if (s.CheckBit(64) && s.BorderStyle != BorderStyle.NotSet)
			{
				this.BorderStyle = s.BorderStyle;
			}
			if (s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				this.BorderWidth = s.BorderWidth;
			}
			if (s.CheckBit(2) && s.CssClass != string.Empty)
			{
				this.CssClass = s.CssClass;
			}
			if (s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				this.ForeColor = s.ForeColor;
			}
			if (s.CheckBit(128) && !s.Height.IsEmpty)
			{
				this.Height = s.Height;
			}
			if (s.CheckBit(256) && !s.Width.IsEmpty)
			{
				this.Width = s.Width;
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> with the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to combine. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
		// Token: 0x06002EAE RID: 11950 RVA: 0x00079A00 File Offset: 0x00077C00
		public virtual void MergeWith(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.fontinfo != null)
			{
				this.Font.MergeWith(s.fontinfo);
			}
			if (!this.CheckBit(8) && s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				this.BackColor = s.BackColor;
			}
			if (!this.CheckBit(16) && s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				this.BorderColor = s.BorderColor;
			}
			if (!this.CheckBit(64) && s.CheckBit(64) && s.BorderStyle != BorderStyle.NotSet)
			{
				this.BorderStyle = s.BorderStyle;
			}
			if (!this.CheckBit(32) && s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				this.BorderWidth = s.BorderWidth;
			}
			if (!this.CheckBit(2) && s.CheckBit(2) && s.CssClass != string.Empty)
			{
				this.CssClass = s.CssClass;
			}
			if (!this.CheckBit(4) && s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				this.ForeColor = s.ForeColor;
			}
			if (!this.CheckBit(128) && s.CheckBit(128) && !s.Height.IsEmpty)
			{
				this.Height = s.Height;
			}
			if (!this.CheckBit(256) && s.CheckBit(256) && !s.Width.IsEmpty)
			{
				this.Width = s.Width;
			}
		}

		/// <summary>Removes any defined style elements from the state bag.</summary>
		// Token: 0x06002EAF RID: 11951 RVA: 0x00079BB8 File Offset: 0x00077DB8
		public virtual void Reset()
		{
			this.viewstate.Remove("BackColor");
			this.viewstate.Remove("BorderColor");
			this.viewstate.Remove("BorderStyle");
			this.viewstate.Remove("BorderWidth");
			this.viewstate.Remove("CssClass");
			this.viewstate.Remove("ForeColor");
			this.viewstate.Remove("Height");
			this.viewstate.Remove("Width");
			if (this.fontinfo != null)
			{
				this.fontinfo.Reset();
			}
			this.styles = 0;
			this.viewstate.Remove("_!SB");
			this.stylesTraked = 0;
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="state">The previously saved state. </param>
		// Token: 0x06002EB0 RID: 11952 RVA: 0x00079C76 File Offset: 0x00077E76
		protected internal void LoadViewState(object state)
		{
			this.viewstate.LoadViewState(state);
			this.LoadBitState();
		}

		/// <summary>A protected method. Saves any state that has been modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
		/// <returns>An object that represents the saved state. The default is null.</returns>
		// Token: 0x06002EB1 RID: 11953 RVA: 0x00079C8A File Offset: 0x00077E8A
		protected internal virtual object SaveViewState()
		{
			this.SaveBitState();
			if (this._isSharedViewState)
			{
				return null;
			}
			return this.viewstate.SaveViewState();
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x00079CA7 File Offset: 0x00077EA7
		internal void SaveBitState()
		{
			if (this.stylesTraked != 0)
			{
				this.viewstate["_!SB"] = this.stylesTraked;
			}
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00079CCC File Offset: 0x00077ECC
		internal void LoadBitState()
		{
			if (this.viewstate["_!SB"] == null)
			{
				return;
			}
			int num = (int)this.viewstate["_!SB"];
			this.styles |= num;
			this.stylesTraked |= num;
		}

		/// <summary>A protected internal method. Sets an internal bitmask field that indicates the style properties that are stored in the state bag.</summary>
		/// <param name="bit">A bitmask value.</param>
		// Token: 0x06002EB4 RID: 11956 RVA: 0x00079D1E File Offset: 0x00077F1E
		protected internal virtual void SetBit(int bit)
		{
			this.styles |= bit;
			if (this.tracking)
			{
				this.stylesTraked |= bit;
			}
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00079D44 File Offset: 0x00077F44
		internal void RemoveBit(int bit)
		{
			this.styles &= ~bit;
			if (this.tracking)
			{
				this.stylesTraked &= ~bit;
				if (this.stylesTraked == 0)
				{
					this.viewstate.Remove("_!SB");
				}
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00079D84 File Offset: 0x00077F84
		internal bool CheckBit(int bit)
		{
			return (this.styles & bit) != 0;
		}

		/// <summary>A protected method. Marks the beginning for tracking state changes on the control. Any changes made after tracking has begun will be tracked and saved as part of the control view state.</summary>
		// Token: 0x06002EB7 RID: 11959 RVA: 0x00079D91 File Offset: 0x00077F91
		protected internal virtual void TrackViewState()
		{
			this.tracking = true;
			this.viewstate.TrackViewState();
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="state">The previously saved state.</param>
		// Token: 0x06002EB8 RID: 11960 RVA: 0x00079DA5 File Offset: 0x00077FA5
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		/// <summary>Returns the object containing state changes.</summary>
		/// <returns>An object that represents the saved state. The default is null.</returns>
		// Token: 0x06002EB9 RID: 11961 RVA: 0x00079DAE File Offset: 0x00077FAE
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x06002EBA RID: 11962 RVA: 0x00079DB6 File Offset: 0x00077FB6
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether a server control is tracking its view state changes.</summary>
		/// <returns>true if there are style elements defined in the view state bag; otherwise, false.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x00079DBE File Offset: 0x00077FBE
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x00079DC6 File Offset: 0x00077FC6
		internal void SetRegisteredCssClass(string name)
		{
			this.registered_class = name;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.CssStyleCollection" /> object for the specified <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object.</returns>
		/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL). </param>
		// Token: 0x06002EBD RID: 11965 RVA: 0x00079DD0 File Offset: 0x00077FD0
		public CssStyleCollection GetStyleAttributes(IUrlResolutionService urlResolver)
		{
			CssStyleCollection cssStyleCollection = new CssStyleCollection();
			this.FillStyleAttributes(cssStyleCollection, urlResolver);
			return cssStyleCollection;
		}

		/// <summary>Gets the cascading style sheet (CSS) class that is registered with the control.</summary>
		/// <returns>The CSS class name with which the current instance was registered on the page.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x00079DEC File Offset: 0x00077FEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string RegisteredCssClass
		{
			get
			{
				if (this.registered_class == null)
				{
					this.registered_class = string.Empty;
				}
				return this.registered_class;
			}
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00079E07 File Offset: 0x00078007
		internal void CopyTextStylesFrom(Style source)
		{
			if (source.CheckBit(4))
			{
				this.ForeColor = source.ForeColor;
			}
			if (source.CheckBit(65024))
			{
				this.Font.CopyFrom(source.Font);
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00079E3C File Offset: 0x0007803C
		internal void RemoveTextStyles()
		{
			this.ForeColor = Color.Empty;
			this.fontinfo = null;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00079E50 File Offset: 0x00078050
		internal void AddCssClass(string cssClass)
		{
			if (string.IsNullOrEmpty(cssClass))
			{
				return;
			}
			string text = this.CssClass;
			if (text.Length > 0)
			{
				text += " ";
			}
			text += cssClass;
			this.CssClass = text;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00079E94 File Offset: 0x00078094
		internal void PrependCssClass(string cssClass)
		{
			if (string.IsNullOrEmpty(cssClass))
			{
				return;
			}
			string cssClass2 = this.CssClass;
			if (cssClass2.Length > 0)
			{
				cssClass += " ";
			}
			this.CssClass = cssClass + cssClass2;
		}

		/// <summary>Marks the <see cref="T:System.Web.UI.WebControls.Style" /> so that its state will be recorded in view state.</summary>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x00079ED4 File Offset: 0x000780D4
		public void SetDirty()
		{
			if (this.viewstate != null)
			{
				this.viewstate.SetDirty(true);
			}
			this.stylesTraked = this.styles;
		}

		// Token: 0x04001BBF RID: 7103
		internal const string BitStateKey = "_!SB";

		// Token: 0x04001BC0 RID: 7104
		private int styles;

		// Token: 0x04001BC1 RID: 7105
		private int stylesTraked;

		// Token: 0x04001BC2 RID: 7106
		internal StateBag viewstate;

		// Token: 0x04001BC3 RID: 7107
		private FontInfo fontinfo;

		// Token: 0x04001BC4 RID: 7108
		private bool tracking;

		// Token: 0x04001BC5 RID: 7109
		private bool _isSharedViewState;

		// Token: 0x04001BC6 RID: 7110
		private string registered_class;

		// Token: 0x0200040D RID: 1037
		[Flags]
		internal enum Styles
		{
			// Token: 0x04001BC8 RID: 7112
			BackColor = 8,
			// Token: 0x04001BC9 RID: 7113
			BorderColor = 16,
			// Token: 0x04001BCA RID: 7114
			BorderStyle = 64,
			// Token: 0x04001BCB RID: 7115
			BorderWidth = 32,
			// Token: 0x04001BCC RID: 7116
			CssClass = 2,
			// Token: 0x04001BCD RID: 7117
			Font = 1,
			// Token: 0x04001BCE RID: 7118
			ForeColor = 4,
			// Token: 0x04001BCF RID: 7119
			Height = 128,
			// Token: 0x04001BD0 RID: 7120
			Width = 256,
			// Token: 0x04001BD1 RID: 7121
			FontAll = 65024,
			// Token: 0x04001BD2 RID: 7122
			FontBold = 2048,
			// Token: 0x04001BD3 RID: 7123
			FontItalic = 4096,
			// Token: 0x04001BD4 RID: 7124
			FontNames = 512,
			// Token: 0x04001BD5 RID: 7125
			FontOverline = 16384,
			// Token: 0x04001BD6 RID: 7126
			FontSize = 1024,
			// Token: 0x04001BD7 RID: 7127
			FontStrikeout = 32768,
			// Token: 0x04001BD8 RID: 7128
			FontUnderline = 8192
		}
	}
}
