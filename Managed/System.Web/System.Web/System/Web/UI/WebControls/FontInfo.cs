using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the font properties of text. This class cannot be inherited.</summary>
	// Token: 0x02000391 RID: 913
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FontInfo
	{
		// Token: 0x060023D2 RID: 9170 RVA: 0x0005AC57 File Offset: 0x00058E57
		internal FontInfo(Style owner)
		{
			this._owner = owner;
			this.bag = owner.ViewState;
		}

		/// <summary>Gets or sets a value that indicates whether the font is bold.</summary>
		/// <returns>true if the font is bold; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x0005AC72 File Offset: 0x00058E72
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x0005AC99 File Offset: 0x00058E99
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Bold
		{
			get
			{
				return this._owner.CheckBit(2048) && this.bag.GetBool("Font_Bold", false);
			}
			set
			{
				this.bag["Font_Bold"] = value;
				this._owner.SetBit(2048);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is italic.</summary>
		/// <returns>true if the font is italic; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x0005ACC1 File Offset: 0x00058EC1
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0005ACE8 File Offset: 0x00058EE8
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Italic
		{
			get
			{
				return this._owner.CheckBit(4096) && this.bag.GetBool("Font_Italic", false);
			}
			set
			{
				this.bag["Font_Italic"] = value;
				this._owner.SetBit(4096);
			}
		}

		/// <summary>Gets or sets the primary font name.</summary>
		/// <returns>The primary font name. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified font name is null. </exception>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0005AD10 File Offset: 0x00058F10
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0005AD31 File Offset: 0x00058F31
		[WebCategory("Font")]
		[WebSysDescription("")]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[NotifyParentProperty(true)]
		public string Name
		{
			get
			{
				string[] names = this.Names;
				if (names.Length == 0)
				{
					return string.Empty;
				}
				return names[0];
			}
			set
			{
				if (value == string.Empty)
				{
					this.Names = null;
					return;
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Font name cannot be null");
				}
				this.Names = new string[] { value };
			}
		}

		/// <summary>Gets or sets an ordered array of font names.</summary>
		/// <returns>An ordered array of font names.</returns>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x0005AD6C File Offset: 0x00058F6C
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x0005ADB4 File Offset: 0x00058FB4
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(FontNamesConverter))]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public string[] Names
		{
			get
			{
				if (!this._owner.CheckBit(512))
				{
					return FontInfo.empty_names;
				}
				string[] array = (string[])this.bag["Font_Names"];
				if (array != null)
				{
					return array;
				}
				return FontInfo.empty_names;
			}
			set
			{
				if (value == null)
				{
					this.bag.Remove("Font_Names");
					this._owner.RemoveBit(512);
					return;
				}
				this.bag["Font_Names"] = value;
				this._owner.SetBit(512);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is overlined.</summary>
		/// <returns>true if the font is overlined; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x0005AE06 File Offset: 0x00059006
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x0005AE2D File Offset: 0x0005902D
		[WebCategory("Font")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool Overline
		{
			get
			{
				return this._owner.CheckBit(16384) && this.bag.GetBool("Font_Overline", false);
			}
			set
			{
				this.bag["Font_Overline"] = value;
				this._owner.SetBit(16384);
			}
		}

		/// <summary>Gets or sets the font size.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified font size is negative. </exception>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x0005AE55 File Offset: 0x00059055
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x0005AE84 File Offset: 0x00059084
		[WebCategory("Font")]
		[WebSysDescription("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(typeof(FontUnit), "")]
		[NotifyParentProperty(true)]
		public FontUnit Size
		{
			get
			{
				if (!this._owner.CheckBit(1024))
				{
					return FontUnit.Empty;
				}
				return (FontUnit)this.bag["Font_Size"];
			}
			set
			{
				if (value.Unit.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Unit.Value, "Font size cannot be negative");
				}
				this.bag["Font_Size"] = value;
				this._owner.SetBit(1024);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is strikethrough.</summary>
		/// <returns>true if the font is struck through; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0005AEF5 File Offset: 0x000590F5
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x0005AF1C File Offset: 0x0005911C
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Strikeout
		{
			get
			{
				return this._owner.CheckBit(32768) && this.bag.GetBool("Font_Strikeout", false);
			}
			set
			{
				this.bag["Font_Strikeout"] = value;
				this._owner.SetBit(32768);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is underlined.</summary>
		/// <returns>true if the font is underlined; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x0005AF44 File Offset: 0x00059144
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x0005AF6B File Offset: 0x0005916B
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Underline
		{
			get
			{
				return this._owner.CheckBit(8192) && this.bag.GetBool("Font_Underline", false);
			}
			set
			{
				this.bag["Font_Underline"] = value;
				this._owner.SetBit(8192);
			}
		}

		/// <summary>Duplicates the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> into the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
		/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to duplicate. </param>
		// Token: 0x060023E3 RID: 9187 RVA: 0x0005AF94 File Offset: 0x00059194
		public void CopyFrom(FontInfo f)
		{
			if (f == null || f.IsEmpty)
			{
				return;
			}
			if (f == this)
			{
				return;
			}
			if (f._owner.CheckBit(2048))
			{
				this.Bold = f.Bold;
			}
			if (f._owner.CheckBit(4096))
			{
				this.Italic = f.Italic;
			}
			this.Names = f.Names;
			if (f._owner.CheckBit(16384))
			{
				this.Overline = f.Overline;
			}
			if (f._owner.CheckBit(1024))
			{
				this.Size = f.Size;
			}
			if (f._owner.CheckBit(32768))
			{
				this.Strikeout = f.Strikeout;
			}
			if (f._owner.CheckBit(8192))
			{
				this.Underline = f.Underline;
			}
		}

		/// <summary>Combines the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> with the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
		/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to combine. </param>
		// Token: 0x060023E4 RID: 9188 RVA: 0x0005B074 File Offset: 0x00059274
		public void MergeWith(FontInfo f)
		{
			if (!this._owner.CheckBit(2048) && f._owner.CheckBit(2048))
			{
				this.Bold = f.Bold;
			}
			if (!this._owner.CheckBit(4096) && f._owner.CheckBit(4096))
			{
				this.Italic = f.Italic;
			}
			if (!this._owner.CheckBit(512) && f._owner.CheckBit(512))
			{
				this.Names = f.Names;
			}
			if (!this._owner.CheckBit(16384) && f._owner.CheckBit(16384))
			{
				this.Overline = f.Overline;
			}
			if (!this._owner.CheckBit(1024) && f._owner.CheckBit(1024))
			{
				this.Size = f.Size;
			}
			if (!this._owner.CheckBit(32768) && f._owner.CheckBit(32768))
			{
				this.Strikeout = f.Strikeout;
			}
			if (!this._owner.CheckBit(8192) && f._owner.CheckBit(8192))
			{
				this.Underline = f.Underline;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property has changed from its default value; otherwise, false.</returns>
		// Token: 0x060023E5 RID: 9189 RVA: 0x0005B1D1 File Offset: 0x000593D1
		public bool ShouldSerializeNames()
		{
			return this.Names.Length != 0;
		}

		/// <summary>Returns a string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</summary>
		/// <returns>A string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</returns>
		// Token: 0x060023E6 RID: 9190 RVA: 0x0005B1E0 File Offset: 0x000593E0
		public override string ToString()
		{
			if (this.Names.Length == 0)
			{
				return this.Size.ToString();
			}
			return this.Name + ", " + this.Size.ToString();
		}

		/// <summary>Resets all <see cref="T:System.Web.UI.WebControls.FontInfo" /> properties to the unset state and clears the view state.</summary>
		// Token: 0x060023E7 RID: 9191 RVA: 0x0005B22F File Offset: 0x0005942F
		public void ClearDefaults()
		{
			this.Reset();
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0005B238 File Offset: 0x00059438
		internal void Reset()
		{
			this.bag.Remove("Font_Bold");
			this.bag.Remove("Font_Italic");
			this.bag.Remove("Font_Names");
			this.bag.Remove("Font_Overline");
			this.bag.Remove("Font_Size");
			this.bag.Remove("Font_Strikeout");
			this.bag.Remove("Font_Underline");
			this._owner.RemoveBit(65024);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0005B2C8 File Offset: 0x000594C8
		internal void FillStyleAttributes(CssStyleCollection attributes, bool alwaysRenderTextDecoration)
		{
			if (this.IsEmpty)
			{
				if (alwaysRenderTextDecoration)
				{
					attributes.Add(HtmlTextWriterStyle.TextDecoration, "none");
				}
				return;
			}
			string text = string.Join(",", this.Names);
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.FontFamily, text);
			}
			if (this._owner.CheckBit(2048))
			{
				attributes.Add(HtmlTextWriterStyle.FontWeight, this.Bold ? "bold" : "normal");
			}
			if (this._owner.CheckBit(4096))
			{
				attributes.Add(HtmlTextWriterStyle.FontStyle, this.Italic ? "italic" : "normal");
			}
			if (!this.Size.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.FontSize, this.Size.ToString());
			}
			text = string.Empty;
			bool flag = false;
			if (this._owner.CheckBit(16384))
			{
				if (this.Overline)
				{
					text += "overline ";
				}
				flag = true;
			}
			if (this._owner.CheckBit(32768))
			{
				if (this.Strikeout)
				{
					text += "line-through ";
				}
				flag = true;
			}
			if (this._owner.CheckBit(8192))
			{
				if (this.Underline)
				{
					text += "underline ";
				}
				flag = true;
			}
			text = ((text.Length > 0) ? text.Trim() : ((alwaysRenderTextDecoration || flag) ? "none" : string.Empty));
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, text);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x0005B44C File Offset: 0x0005964C
		private bool IsEmpty
		{
			get
			{
				return !this._owner.CheckBit(65024);
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal FontInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400198F RID: 6543
		private static string[] empty_names = new string[0];

		// Token: 0x04001990 RID: 6544
		private StateBag bag;

		// Token: 0x04001991 RID: 6545
		private Style _owner;
	}
}
