using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a cell in a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
	// Token: 0x02000415 RID: 1045
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[DefaultProperty("Text")]
	[ParseChildren(false)]
	[ControlBuilder(typeof(TableCellControlBuilder))]
	[ToolboxItem("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableCell : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableCell" /> class.</summary>
		// Token: 0x06002F22 RID: 12066 RVA: 0x0007A7F8 File Offset: 0x000789F8
		public TableCell()
			: base(HtmlTextWriterTag.Td)
		{
			base.AutoID = false;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0007A809 File Offset: 0x00078A09
		internal TableCell(HtmlTextWriterTag tag)
			: base(tag)
		{
			base.AutoID = false;
		}

		/// <summary>Gets or sets a space-separated list of table header cells associated with the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <returns>An array of strings containing the identifiers of the associated table header cells.</returns>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x0007A81C File Offset: 0x00078A1C
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x0007A84A File Offset: 0x00078A4A
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		public virtual string[] AssociatedHeaderCellID
		{
			get
			{
				object obj = this.ViewState["AssociatedHeaderCellID"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AssociatedHeaderCellID");
					return;
				}
				this.ViewState["AssociatedHeaderCellID"] = value;
			}
		}

		/// <summary>Gets or sets the number of columns in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
		/// <returns>The number of columns in the rendered table that the cell spans. The default value is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x0007A874 File Offset: 0x00078A74
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x0007A89D File Offset: 0x00078A9D
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual int ColumnSpan
		{
			get
			{
				object obj = this.ViewState["ColumnSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("< 0");
				}
				this.ViewState["ColumnSpan"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents in the cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0007A8C4 File Offset: 0x00078AC4
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x0007A8DB File Offset: 0x00078ADB
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableItemStyle.HorizontalAlign;
			}
			set
			{
				this.TableItemStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets the number of rows in the <see cref="T:System.Web.UI.WebControls.Table" /> control that the cell spans.</summary>
		/// <returns>The number of rows in the rendered table that the cell spans. The default value is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x0007A8EC File Offset: 0x00078AEC
		// (set) Token: 0x06002F2B RID: 12075 RVA: 0x0007A915 File Offset: 0x00078B15
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int RowSpan
		{
			get
			{
				object obj = this.ViewState["RowSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("< 0");
				}
				this.ViewState["RowSpan"] = value;
			}
		}

		/// <summary>Gets or sets the text contents of the cell.</summary>
		/// <returns>The text contents of the cell. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0007A93C File Offset: 0x00078B3C
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x0007A969 File Offset: 0x00078B69
		[Localizable(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("Text");
					return;
				}
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Gets or sets the vertical alignment of the contents in the cell.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.VerticalAlign" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0007A9A3 File Offset: 0x00078BA3
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x0007A9BA File Offset: 0x00078BBA
		[DefaultValue(VerticalAlign.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return VerticalAlign.NotSet;
				}
				return this.TableItemStyle.VerticalAlign;
			}
			set
			{
				this.TableItemStyle.VerticalAlign = value;
			}
		}

		/// <summary>Gets or sets a value that indicating whether the contents of the cell wrap.</summary>
		/// <returns>true if the contents of the cell wrap in the cell; otherwise, false. The default is true.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0007A9C8 File Offset: 0x00078BC8
		// (set) Token: 0x06002F31 RID: 12081 RVA: 0x0007A9DF File Offset: 0x00078BDF
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(true)]
		public virtual bool Wrap
		{
			get
			{
				return !base.ControlStyleCreated || this.TableItemStyle.Wrap;
			}
			set
			{
				this.TableItemStyle.Wrap = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0007A9ED File Offset: 0x00078BED
		private TableItemStyle TableItemStyle
		{
			get
			{
				return base.ControlStyle as TableItemStyle;
			}
		}

		/// <summary>Adds properties specific to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		/// <exception cref="T:System.Web.HttpException">A cell listed as an associated header cell was not found.</exception>
		// Token: 0x06002F34 RID: 12084 RVA: 0x0007A9FC File Offset: 0x00078BFC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (writer == null)
			{
				return;
			}
			int i = this.ColumnSpan;
			if (i > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Colspan, i.ToString(Helpers.InvariantCulture), false);
			}
			i = this.RowSpan;
			if (i > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, i.ToString(Helpers.InvariantCulture), false);
			}
			string[] associatedHeaderCellID = this.AssociatedHeaderCellID;
			if (associatedHeaderCellID.Length > 1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (i = 0; i < associatedHeaderCellID.Length - 1; i++)
				{
					stringBuilder.Append(associatedHeaderCellID[i]);
					stringBuilder.Append(",");
				}
				stringBuilder.Append(associatedHeaderCellID.Length - 1);
				writer.AddAttribute(HtmlTextWriterAttribute.Headers, stringBuilder.ToString());
				return;
			}
			if (associatedHeaderCellID.Length == 1)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Headers, associatedHeaderCellID[0]);
			}
		}

		/// <summary>Adds a parsed child control to the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element</param>
		// Token: 0x06002F35 RID: 12085 RVA: 0x0007AABC File Offset: 0x00078CBC
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
				if (text.Length > 0)
				{
					this.Controls.Add(new LiteralControl(text));
					this.Text = null;
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> for the <see cref="T:System.Web.UI.WebControls.TableCell" /> control. </returns>
		// Token: 0x06002F36 RID: 12086 RVA: 0x00056FF6 File Offset: 0x000551F6
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.TableCell" /> contents to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06002F37 RID: 12087 RVA: 0x0007AB1F File Offset: 0x00078D1F
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}
	}
}
