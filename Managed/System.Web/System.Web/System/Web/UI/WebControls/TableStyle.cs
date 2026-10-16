using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style for the <see cref="T:System.Web.UI.WebControls.Table" /> control and some Web Parts.</summary>
	// Token: 0x0200041F RID: 1055
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableStyle : Style
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> class using default values.</summary>
		// Token: 0x06002F96 RID: 12182 RVA: 0x0006C32C File Offset: 0x0006A52C
		public TableStyle()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> class with the specified state bag information.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
		// Token: 0x06002F97 RID: 12183 RVA: 0x0006C334 File Offset: 0x0006A534
		public TableStyle(StateBag bag)
			: base(bag)
		{
		}

		/// <summary>Gets or sets the URL of an image to display in the background of a table control.</summary>
		/// <returns>The URL of an image to display in the background of a table control. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The URL of the background image was set to null. </exception>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x0007B665 File Offset: 0x00079865
		// (set) Token: 0x06002F99 RID: 12185 RVA: 0x0007B68F File Offset: 0x0007988F
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.CheckBit(65536))
				{
					return string.Empty;
				}
				return (string)base.ViewState["BackImageUrl"];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("BackImageUrl");
				}
				base.ViewState["BackImageUrl"] = value;
				this.SetBit(65536);
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of the cell and the cell's border.</summary>
		/// <returns>The distance (in pixels) between the contents of a cell and the cell's border. The default is -1, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified distance is set to a value less than -1. </exception>
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x0007B6BB File Offset: 0x000798BB
		// (set) Token: 0x06002F9B RID: 12187 RVA: 0x0007B6E1 File Offset: 0x000798E1
		[NotifyParentProperty(true)]
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.CheckBit(131072))
				{
					return -1;
				}
				return (int)base.ViewState["CellPadding"];
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("< -1");
				}
				base.ViewState["CellPadding"] = value;
				this.SetBit(131072);
			}
		}

		/// <summary>Gets or sets the distance between table cells.</summary>
		/// <returns>The distance (in pixels) between table cells. The default is -1, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified distance is set to a value less than -1. </exception>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x0007B713 File Offset: 0x00079913
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x0007B739 File Offset: 0x00079939
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.CheckBit(262144))
				{
					return -1;
				}
				return (int)base.ViewState["CellSpacing"];
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("< -1");
				}
				base.ViewState["CellSpacing"] = value;
				this.SetBit(262144);
			}
		}

		/// <summary>Gets or sets a value that specifies whether the border between the cells of the table control is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default is Both.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. </exception>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x0007B76B File Offset: 0x0007996B
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x0007B791 File Offset: 0x00079991
		[NotifyParentProperty(true)]
		[DefaultValue(GridLines.None)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.CheckBit(524288))
				{
					return GridLines.None;
				}
				return (GridLines)base.ViewState["GridLines"];
			}
			set
			{
				if (value < GridLines.None || value > GridLines.Both)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid GridLines value."));
				}
				base.ViewState["GridLines"] = value;
				this.SetBit(524288);
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the table within its container.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. </exception>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x0007B7CC File Offset: 0x000799CC
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x0007B7F2 File Offset: 0x000799F2
		[NotifyParentProperty(true)]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.CheckBit(1048576))
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)base.ViewState["HorizontalAlign"];
			}
			set
			{
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid HorizontalAlign value."));
				}
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(1048576);
			}
		}

		/// <summary>Adds information about the background image, cell spacing, cell padding, gridlines, and alignment to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		/// <param name="owner">The control associated with the style. </param>
		// Token: 0x06002FA2 RID: 12194 RVA: 0x0007B830 File Offset: 0x00079A30
		[global::System.MonoTODO("collapse style should be rendered only for browsers which support that.")]
		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			if (writer == null)
			{
				return;
			}
			int num = this.CellSpacing;
			if (num != -1)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, num.ToString(Helpers.InvariantCulture), false);
				if (num == 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
				}
			}
			num = this.CellPadding;
			if (num != -1)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, num.ToString(Helpers.InvariantCulture), false);
			}
			GridLines gridLines = this.GridLines;
			switch (gridLines)
			{
			case GridLines.Horizontal:
				writer.AddAttribute(HtmlTextWriterAttribute.Rules, "rows", false);
				break;
			case GridLines.Vertical:
				writer.AddAttribute(HtmlTextWriterAttribute.Rules, "cols", false);
				break;
			case GridLines.Both:
				writer.AddAttribute(HtmlTextWriterAttribute.Rules, "all", false);
				break;
			}
			switch (this.HorizontalAlign)
			{
			case HorizontalAlign.Left:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", false);
				break;
			case HorizontalAlign.Center:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "center", false);
				break;
			case HorizontalAlign.Right:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", false);
				break;
			case HorizontalAlign.Justify:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "justify", false);
				break;
			}
			if (gridLines != GridLines.None && base.BorderWidth.IsEmpty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Border, "1", false);
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x0007B95C File Offset: 0x00079B5C
		private void Copy(string name, TableStyle.TableStyles s, Style source)
		{
			if (source.CheckBit((int)s))
			{
				object obj = source.ViewState[name];
				if (obj != null)
				{
					base.ViewState[name] = obj;
					this.SetBit((int)s);
				}
			}
		}

		/// <summary>Copies non-blank elements from the specified style, overwriting existing style elements if necessary.</summary>
		/// <param name="s">The style to copy. </param>
		// Token: 0x06002FA4 RID: 12196 RVA: 0x0007B998 File Offset: 0x00079B98
		public override void CopyFrom(Style s)
		{
			base.CopyFrom(s);
			if (s != null && !s.IsEmpty)
			{
				this.Copy("BackImageUrl", TableStyle.TableStyles.BackImageUrl, s);
				this.Copy("CellPadding", TableStyle.TableStyles.CellPadding, s);
				this.Copy("CellSpacing", TableStyle.TableStyles.CellSpacing, s);
				this.Copy("GridLines", TableStyle.TableStyles.GridLines, s);
				this.Copy("HorizontalAlign", TableStyle.TableStyles.HorizontalAlign, s);
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0007BA0C File Offset: 0x00079C0C
		private void Merge(string name, TableStyle.TableStyles s, Style source)
		{
			if (!base.CheckBit((int)s) && source.CheckBit((int)s))
			{
				object obj = source.ViewState[name];
				if (obj != null)
				{
					base.ViewState[name] = obj;
					this.SetBit((int)s);
				}
			}
		}

		/// <summary>Copies non-blank elements from the specified style, but will not overwrite any existing style elements.</summary>
		/// <param name="s">The style to copy. </param>
		// Token: 0x06002FA6 RID: 12198 RVA: 0x0007BA50 File Offset: 0x00079C50
		public override void MergeWith(Style s)
		{
			if (this.IsEmpty)
			{
				this.CopyFrom(s);
				return;
			}
			base.MergeWith(s);
			if (s != null && !s.IsEmpty)
			{
				this.Merge("BackImageUrl", TableStyle.TableStyles.BackImageUrl, s);
				this.Merge("CellPadding", TableStyle.TableStyles.CellPadding, s);
				this.Merge("CellSpacing", TableStyle.TableStyles.CellSpacing, s);
				this.Merge("GridLines", TableStyle.TableStyles.GridLines, s);
				this.Merge("HorizontalAlign", TableStyle.TableStyles.HorizontalAlign, s);
			}
		}

		/// <summary>Clears any defined style elements of the style.</summary>
		// Token: 0x06002FA7 RID: 12199 RVA: 0x0007BAD4 File Offset: 0x00079CD4
		public override void Reset()
		{
			if (base.CheckBit(65536))
			{
				base.ViewState.Remove("BackImageUrl");
			}
			if (base.CheckBit(131072))
			{
				base.ViewState.Remove("CellPadding");
			}
			if (base.CheckBit(262144))
			{
				base.ViewState.Remove("CellSpacing");
			}
			if (base.CheckBit(524288))
			{
				base.ViewState.Remove("GridLines");
			}
			if (base.CheckBit(1048576))
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			base.Reset();
		}

		/// <summary>Adds the style properties of the <see cref="T:System.Web.UI.WebControls.TableStyle" /> object to the specified <see cref="T:System.Web.UI.CssStyleCollection" /> collection.</summary>
		/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> to which to add the style properties. </param>
		/// <param name="urlResolver">An object implemented by the <see cref="T:System.Web.UI.IUrlResolutionService" /> that contains the context information for the current location (URL). </param>
		// Token: 0x06002FA8 RID: 12200 RVA: 0x0007BB78 File Offset: 0x00079D78
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			if (attributes != null)
			{
				string text = this.BackImageUrl;
				if (text.Length > 0)
				{
					if (urlResolver != null)
					{
						text = urlResolver.ResolveClientUrl(text);
					}
					attributes.Add(HtmlTextWriterStyle.BackgroundImage, text);
				}
			}
			base.FillStyleAttributes(attributes, urlResolver);
		}

		// Token: 0x02000420 RID: 1056
		[Flags]
		private enum TableStyles
		{
			// Token: 0x04001BEC RID: 7148
			BackImageUrl = 65536,
			// Token: 0x04001BED RID: 7149
			CellPadding = 131072,
			// Token: 0x04001BEE RID: 7150
			CellSpacing = 262144,
			// Token: 0x04001BEF RID: 7151
			GridLines = 524288,
			// Token: 0x04001BF0 RID: 7152
			HorizontalAlign = 1048576
		}
	}
}
