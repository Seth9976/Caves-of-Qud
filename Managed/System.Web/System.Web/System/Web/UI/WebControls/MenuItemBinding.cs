using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the relationship between a data item and the menu item it is binding to in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited. </summary>
	// Token: 0x020003CD RID: 973
	[DefaultProperty("TextField")]
	public sealed class MenuItemBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		/// <summary>Gets or sets the data member to bind to a menu item.</summary>
		/// <returns>The data member to bind to a menu item. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x0006B8B5 File Offset: 0x00069AB5
		// (set) Token: 0x060029A3 RID: 10659 RVA: 0x0006B8CC File Offset: 0x00069ACC
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", string.Empty);
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		/// <summary>Gets or sets the menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x0006B8DF File Offset: 0x00069ADF
		// (set) Token: 0x060029A5 RID: 10661 RVA: 0x0006B8F2 File Offset: 0x00069AF2
		[DefaultValue(-1)]
		public int Depth
		{
			get
			{
				return this.ViewState.GetInt("Depth", -1);
			}
			set
			{
				this.ViewState["Depth"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
		/// <returns>true if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is enabled; otherwise, false.</returns>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x0006B90A File Offset: 0x00069B0A
		// (set) Token: 0x060029A7 RID: 10663 RVA: 0x0006B91D File Offset: 0x00069B1D
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.ViewState.GetBool("Enabled", true);
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. </summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x0006B935 File Offset: 0x00069B35
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x0006B94C File Offset: 0x00069B4C
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string EnabledField
		{
			get
			{
				return this.ViewState.GetString("EnabledField", string.Empty);
			}
			set
			{
				this.ViewState["EnabledField"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>A formatting string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x0006B95F File Offset: 0x00069B5F
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x0006B976 File Offset: 0x00069B76
		[Localizable(true)]
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				return this.ViewState.GetString("FormatString", string.Empty);
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x0006B989 File Offset: 0x00069B89
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x0006B9A0 File Offset: 0x00069BA0
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				return this.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x0006B9B3 File Offset: 0x00069BB3
		// (set) Token: 0x060029AF RID: 10671 RVA: 0x0006B9CA File Offset: 0x00069BCA
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageUrlField
		{
			get
			{
				return this.ViewState.GetString("ImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is clicked.</summary>
		/// <returns>The URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x0006B9DD File Offset: 0x00069BDD
		// (set) Token: 0x060029B1 RID: 10673 RVA: 0x0006B9F4 File Offset: 0x00069BF4
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				return this.ViewState.GetString("NavigateUrl", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x0006BA07 File Offset: 0x00069C07
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x0006BA1E File Offset: 0x00069C1E
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string NavigateUrlField
		{
			get
			{
				return this.ViewState.GetString("NavigateUrlField", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied can be selected, or is "clickable."</summary>
		/// <returns>true if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is selectable; otherwise, false.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x0006BA31 File Offset: 0x00069C31
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x0006BA44 File Offset: 0x00069C44
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				return this.ViewState.GetBool("Selectable", true);
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x0006BA5C File Offset: 0x00069C5C
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x0006BA73 File Offset: 0x00069C73
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SelectableField
		{
			get
			{
				return this.ViewState.GetString("SelectableField", string.Empty);
			}
			set
			{
				this.ViewState["SelectableField"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x0006BA86 File Offset: 0x00069C86
		// (set) Token: 0x060029B9 RID: 10681 RVA: 0x0006BA9D File Offset: 0x00069C9D
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x0006BAB0 File Offset: 0x00069CB0
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x0006BAC7 File Offset: 0x00069CC7
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TargetField
		{
			get
			{
				return this.ViewState.GetString("TargetField", string.Empty);
			}
			set
			{
				this.ViewState["TargetField"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x0006BADA File Offset: 0x00069CDA
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x0006BAF1 File Offset: 0x00069CF1
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("The display text of the menu item.")]
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x0006BB04 File Offset: 0x00069D04
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x0006BB1B File Offset: 0x00069D1B
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				return this.ViewState.GetString("TextField", string.Empty);
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x0006BB2E File Offset: 0x00069D2E
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x0006BB45 File Offset: 0x00069D45
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTip
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x0006BB58 File Offset: 0x00069D58
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x0006BB6F File Offset: 0x00069D6F
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipField
		{
			get
			{
				return this.ViewState.GetString("ToolTipField", string.Empty);
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets a nondisplayed value used to store any additional data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied; this data is not displayed. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x0006BB82 File Offset: 0x00069D82
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x0006BB99 File Offset: 0x00069D99
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				return this.ViewState.GetString("Value", string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x0006BBAC File Offset: 0x00069DAC
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x0006BBC3 File Offset: 0x00069DC3
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ValueField
		{
			get
			{
				return this.ViewState.GetString("ValueField", string.Empty);
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x0006BBD6 File Offset: 0x00069DD6
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x0006BBED File Offset: 0x00069DED
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string PopOutImageUrl
		{
			get
			{
				return this.ViewState.GetString("PopOutImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x0006BC00 File Offset: 0x00069E00
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x0006BC17 File Offset: 0x00069E17
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string PopOutImageUrlField
		{
			get
			{
				return this.ViewState.GetString("PopOutImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["PopOutImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed below the text of a menu item (to separate it from other menu items) for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image displayed below the text of a menu item for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x0006BC2A File Offset: 0x00069E2A
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x0006BC41 File Offset: 0x00069E41
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string SeparatorImageUrl
		{
			get
			{
				return this.ViewState.GetString("SeparatorImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x0006BC54 File Offset: 0x00069E54
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x0006BC6B File Offset: 0x00069E6B
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SeparatorImageUrlField
		{
			get
			{
				return this.ViewState.GetString("SeparatorImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["SeparatorImageUrlField"] = value;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object.</summary>
		// Token: 0x060029D0 RID: 10704 RVA: 0x0006BC80 File Offset: 0x00069E80
		public override string ToString()
		{
			string dataMember = this.DataMember;
			if (string.IsNullOrEmpty(dataMember))
			{
				return "(Empty)";
			}
			return dataMember;
		}

		/// <summary>Loads the node's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
		// Token: 0x060029D1 RID: 10705 RVA: 0x0006BCA3 File Offset: 0x00069EA3
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the view state changes to an <see cref="T:System.Object" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x060029D2 RID: 10706 RVA: 0x0006BCB1 File Offset: 0x00069EB1
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to track changes to its view state.</summary>
		// Token: 0x060029D3 RID: 10707 RVA: 0x0006BCBE File Offset: 0x00069EBE
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x0006BCCB File Offset: 0x00069ECB
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x060029D6 RID: 10710 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</returns>
		// Token: 0x060029D7 RID: 10711 RVA: 0x0006BCD8 File Offset: 0x00069ED8
		object ICloneable.Clone()
		{
			MenuItemBinding menuItemBinding = new MenuItemBinding();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				menuItemBinding.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return menuItemBinding;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0006BD50 File Offset: 0x00069F50
		internal void SetDirty()
		{
			StateBag viewState = this.ViewState;
			foreach (object obj in viewState.Keys)
			{
				string text = (string)obj;
				viewState.SetItemDirty(text, true);
			}
		}

		// Token: 0x04001AC6 RID: 6854
		private StateBag ViewState = new StateBag();
	}
}
