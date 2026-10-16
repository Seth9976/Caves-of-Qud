using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a menu item displayed in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003CC RID: 972
	[ParseChildren(true, "ChildItems")]
	public sealed class MenuItem : IStateManager, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class without menu text or a value.</summary>
		// Token: 0x06002966 RID: 10598 RVA: 0x0006AAD5 File Offset: 0x00068CD5
		public MenuItem()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		// Token: 0x06002967 RID: 10599 RVA: 0x0006AAEF File Offset: 0x00068CEF
		public MenuItem(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text and value. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		// Token: 0x06002968 RID: 10600 RVA: 0x0006AB10 File Offset: 0x00068D10
		public MenuItem(string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, and URL to an image. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
		// Token: 0x06002969 RID: 10601 RVA: 0x0006AB38 File Offset: 0x00068D38
		public MenuItem(string text, string value, string imageUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, and navigation URL. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
		/// <param name="navigateUrl">The URL to link to when the menu item is clicked.</param>
		// Token: 0x0600296A RID: 10602 RVA: 0x0006AB67 File Offset: 0x00068D67
		public MenuItem(string text, string value, string imageUrl, string navigateUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, navigation URL, and target. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. </param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item. </param>
		/// <param name="navigateUrl">The URL to link to when the menu item is clicked. </param>
		/// <param name="target">The target window or frame in which to display the Web page content linked to a menu item when the menu item is clicked. </param>
		// Token: 0x0600296B RID: 10603 RVA: 0x0006AB9E File Offset: 0x00068D9E
		public MenuItem(string text, string value, string imageUrl, string navigateUrl, string target)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
			this.Target = target;
		}

		/// <summary>Gets the level at which a menu item is displayed.</summary>
		/// <returns>The level at which a menu item is displayed.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x0006ABDD File Offset: 0x00068DDD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Depth
		{
			get
			{
				if (this.depth != -1)
				{
					return this.depth;
				}
				if (this.Parent == null)
				{
					this.depth = 0;
				}
				else
				{
					this.depth = this.Parent.Depth + 1;
				}
				return this.depth;
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0006AC19 File Offset: 0x00068E19
		private void ResetPathData()
		{
			this.path = null;
			this.depth = -1;
			this.gotBinding = false;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0006AC30 File Offset: 0x00068E30
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x0006AC38 File Offset: 0x00068E38
		internal Menu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
				if (this.items != null)
				{
					this.items.SetMenu(this.menu);
				}
				this.ResetPathData();
			}
		}

		/// <summary>Gets a value indicating whether the menu item was created through data binding.</summary>
		/// <returns>true if the menu item was created through data binding; otherwise, false.</returns>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x0006AC60 File Offset: 0x00068E60
		// (set) Token: 0x06002971 RID: 10609 RVA: 0x0006AC8B File Offset: 0x00068E8B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool DataBound
		{
			get
			{
				return this.ViewState["DataBound"] != null && (bool)this.ViewState["DataBound"];
			}
			private set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Gets the data item that is bound to the menu item.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data item that is bound to the menu item. The default value is null, which indicates that the menu item is not bound to any data item.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x0006ACA3 File Offset: 0x00068EA3
		[DefaultValue(null)]
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				if (!this.DataBound)
				{
					throw new InvalidOperationException("MenuItem is not data bound.");
				}
				return this.dataItem;
			}
		}

		/// <summary>Gets the path to the data that is bound to the menu item.</summary>
		/// <returns>The path to the data that is bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.Menu" /> control is bound. The default value is an empty string ("").</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x0006ACBE File Offset: 0x00068EBE
		// (set) Token: 0x06002974 RID: 10612 RVA: 0x0006ACED File Offset: 0x00068EED
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataPath
		{
			get
			{
				if (this.ViewState["DataPath"] != null)
				{
					return (string)this.ViewState["DataPath"];
				}
				return string.Empty;
			}
			private set
			{
				this.ViewState["DataPath"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object that contains the submenu items of the current menu item.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> that contains the submenu items of the current menu item. The default is null, which indicates that this menu item does not contain any submenu items.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x0006AD00 File Offset: 0x00068F00
		[MergableProperty(false)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public MenuItemCollection ChildItems
		{
			get
			{
				if (this.items == null)
				{
					this.items = new MenuItemCollection(this);
					if (((IStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the text in a menu item.</summary>
		/// <returns>The URL to a custom image that is displayed next to the text of a menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x0006AD2F File Offset: 0x00068F2F
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x0006AD5E File Offset: 0x00068F5E
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				if (this.ViewState["ImageUrl"] != null)
				{
					return (string)this.ViewState["ImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when the menu item is clicked.</summary>
		/// <returns>The URL to navigate to when the menu item is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x0006AD71 File Offset: 0x00068F71
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x0006ADA0 File Offset: 0x00068FA0
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				if (this.ViewState["NavigateUrl"] != null)
				{
					return (string)this.ViewState["NavigateUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu.</summary>
		/// <returns>The URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x0006ADB3 File Offset: 0x00068FB3
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x0006ADE2 File Offset: 0x00068FE2
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string PopOutImageUrl
		{
			get
			{
				if (this.ViewState["PopOutImageUrl"] != null)
				{
					return (string)this.ViewState["PopOutImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x0006ADF5 File Offset: 0x00068FF5
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x0006AE24 File Offset: 0x00069024
		[DefaultValue("")]
		public string Target
		{
			get
			{
				if (this.ViewState["Target"] != null)
				{
					return (string)this.ViewState["Target"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>The text displayed for the menu item in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. The default is an empty string ("").</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x0006AE38 File Offset: 0x00069038
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x0006AE79 File Offset: 0x00069079
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the menu item.</summary>
		/// <returns>The ToolTip text for the menu item. The default is an empty string ("").</returns>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x0006AE8C File Offset: 0x0006908C
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x0006AEBB File Offset: 0x000690BB
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTip
		{
			get
			{
				if (this.ViewState["ToolTip"] != null)
				{
					return (string)this.ViewState["ToolTip"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets a non-displayed value used to store any additional data about the menu item, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about the menu item that is not displayed. The default value is an empty string ("").</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x0006AED0 File Offset: 0x000690D0
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x0006AF11 File Offset: 0x00069111
		[Localizable(true)]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed at the bottom of a menu item to separate it from other menu items.</summary>
		/// <returns>The URL to an image used to separate the current menu item from other menu items.</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x0006AF24 File Offset: 0x00069124
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x0006AF53 File Offset: 0x00069153
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty]
		public string SeparatorImageUrl
		{
			get
			{
				if (this.ViewState["SeparatorImageUrl"] != null)
				{
					return (string)this.ViewState["SeparatorImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object can be selected, or is "clickable."</summary>
		/// <returns>true if the menu item can be selected; otherwise, false.</returns>
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x0006AF66 File Offset: 0x00069166
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x0006AF91 File Offset: 0x00069191
		[Browsable(true)]
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				return this.ViewState["Selectable"] == null || (bool)this.ViewState["Selectable"];
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
		/// <returns>true if the menu item is enabled; otherwise, false.</returns>
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x0006AFA9 File Offset: 0x000691A9
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x0006AFD4 File Offset: 0x000691D4
		[DefaultValue(true)]
		[Browsable(true)]
		public bool Enabled
		{
			get
			{
				return this.ViewState["Enabled"] == null || (bool)this.ViewState["Enabled"];
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x0006AFEC File Offset: 0x000691EC
		internal bool BranchEnabled
		{
			get
			{
				return this.Enabled && (this.parent == null || this.parent.BranchEnabled);
			}
		}

		/// <summary>Gets or sets a value indicating whether the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>true to indicate that the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control; otherwise, false. The default is false.</returns>
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x0006B00D File Offset: 0x0006920D
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x0006B027 File Offset: 0x00069227
		[Browsable(true)]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return this.menu != null && this.menu.SelectedItem == this;
			}
			set
			{
				if (this.menu != null)
				{
					if (!value && this.menu.SelectedItem == this)
					{
						this.menu.SetSelectedItem(null);
						return;
					}
					if (value)
					{
						this.menu.SetSelectedItem(this);
					}
				}
			}
		}

		/// <summary>Gets the parent menu item of the current menu item.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the parent menu item of the current menu item.</returns>
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x0006B05E File Offset: 0x0006925E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MenuItem Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the path from the root menu item to the current menu item.</summary>
		/// <returns>A delimiter-separated list of menu item values that form a path from the root menu item to the current menu item.</returns>
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x0006B068 File Offset: 0x00069268
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValuePath
		{
			get
			{
				if (this.menu == null)
				{
					return this.Value;
				}
				StringBuilder stringBuilder = new StringBuilder(this.Value);
				for (MenuItem menuItem = this.parent; menuItem != null; menuItem = menuItem.Parent)
				{
					stringBuilder.Insert(0, this.menu.PathSeparator);
					stringBuilder.Insert(0, menuItem.Value);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x0006B0CA File Offset: 0x000692CA
		// (set) Token: 0x06002990 RID: 10640 RVA: 0x0006B0D2 File Offset: 0x000692D2
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
				this.ResetPathData();
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0006B0E1 File Offset: 0x000692E1
		internal void SetParent(MenuItem item)
		{
			this.parent = item;
			this.ResetPathData();
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x0006B0F0 File Offset: 0x000692F0
		internal string Path
		{
			get
			{
				if (this.path != null)
				{
					return this.path;
				}
				StringBuilder stringBuilder = new StringBuilder(this.index.ToString());
				for (MenuItem menuItem = this.parent; menuItem != null; menuItem = menuItem.Parent)
				{
					stringBuilder.Insert(0, '_');
					stringBuilder.Insert(0, menuItem.Index.ToString());
				}
				this.path = stringBuilder.ToString();
				return this.path;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x0006B162 File Offset: 0x00069362
		internal bool HasChildData
		{
			get
			{
				return this.items != null;
			}
		}

		/// <summary>Loads the menu item's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
		// Token: 0x06002994 RID: 10644 RVA: 0x0006B170 File Offset: 0x00069370
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			this.ViewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ChildItems).LoadViewState(array[1]);
			}
		}

		/// <summary>Saves the view-state changes to an <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view-state changes.</returns>
		// Token: 0x06002995 RID: 10645 RVA: 0x0006B1AC File Offset: 0x000693AC
		object IStateManager.SaveViewState()
		{
			object[] array = new object[]
			{
				this.ViewState.SaveViewState(),
				(this.items == null) ? null : ((IStateManager)this.items).SaveViewState()
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to track changes to its view state.</summary>
		// Token: 0x06002996 RID: 10646 RVA: 0x0006B1FD File Offset: 0x000693FD
		void IStateManager.TrackViewState()
		{
			if (this.marked)
			{
				return;
			}
			this.marked = true;
			this.ViewState.TrackViewState();
			if (this.items != null)
			{
				((IStateManager)this.items).TrackViewState();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x0006B22D File Offset: 0x0006942D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0006B235 File Offset: 0x00069435
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.items != null)
			{
				this.items.SetDirty();
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Web.UI.WebControls.MenuItem" /> object. </summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</returns>
		// Token: 0x06002999 RID: 10649 RVA: 0x0006B258 File Offset: 0x00069458
		object ICloneable.Clone()
		{
			MenuItem menuItem = new MenuItem();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				menuItem.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			foreach (object obj2 in this.ChildItems)
			{
				ICloneable cloneable = (ICloneable)obj2;
				menuItem.ChildItems.Add((MenuItem)cloneable.Clone());
			}
			return menuItem;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x0006B32C File Offset: 0x0006952C
		internal void Bind(IHierarchyData hierarchyData)
		{
			this.hierarchyData = hierarchyData;
			this.DataBound = true;
			this.DataPath = hierarchyData.Path;
			this.dataItem = hierarchyData.Item;
			MenuItemBinding menuItemBinding = this.GetBinding();
			if (menuItemBinding != null)
			{
				if (menuItemBinding.EnabledField != "")
				{
					try
					{
						this.Enabled = Convert.ToBoolean(this.GetBoundPropertyValue(menuItemBinding.EnabledField));
						goto IL_0079;
					}
					catch
					{
						this.Enabled = menuItemBinding.Enabled;
						goto IL_0079;
					}
				}
				this.Enabled = menuItemBinding.Enabled;
				IL_0079:
				if (menuItemBinding.ImageUrlField.Length > 0)
				{
					this.ImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ImageUrlField));
					if (this.ImageUrl.Length == 0)
					{
						this.ImageUrl = menuItemBinding.ImageUrl;
					}
				}
				else if (menuItemBinding.ImageUrl.Length > 0)
				{
					this.ImageUrl = menuItemBinding.ImageUrl;
				}
				if (menuItemBinding.NavigateUrlField.Length > 0)
				{
					this.NavigateUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.NavigateUrlField));
					if (this.NavigateUrl.Length == 0)
					{
						this.NavigateUrl = menuItemBinding.NavigateUrl;
					}
				}
				else if (menuItemBinding.NavigateUrl.Length > 0)
				{
					this.NavigateUrl = menuItemBinding.NavigateUrl;
				}
				if (menuItemBinding.PopOutImageUrlField.Length > 0)
				{
					this.PopOutImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.PopOutImageUrlField));
					if (this.PopOutImageUrl.Length == 0)
					{
						this.PopOutImageUrl = menuItemBinding.PopOutImageUrl;
					}
				}
				else if (menuItemBinding.PopOutImageUrl.Length > 0)
				{
					this.PopOutImageUrl = menuItemBinding.PopOutImageUrl;
				}
				if (menuItemBinding.SelectableField != "")
				{
					try
					{
						this.Selectable = Convert.ToBoolean(this.GetBoundPropertyValue(menuItemBinding.SelectableField));
						goto IL_01CD;
					}
					catch
					{
						this.Selectable = menuItemBinding.Selectable;
						goto IL_01CD;
					}
				}
				this.Selectable = menuItemBinding.Selectable;
				IL_01CD:
				if (menuItemBinding.SeparatorImageUrlField.Length > 0)
				{
					this.SeparatorImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.SeparatorImageUrlField));
					if (this.SeparatorImageUrl.Length == 0)
					{
						this.SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
					}
				}
				else if (menuItemBinding.SeparatorImageUrl.Length > 0)
				{
					this.SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
				}
				if (menuItemBinding.TargetField.Length > 0)
				{
					this.Target = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.TargetField));
					if (this.Target.Length == 0)
					{
						this.Target = menuItemBinding.Target;
					}
				}
				else if (menuItemBinding.Target.Length > 0)
				{
					this.Target = menuItemBinding.Target;
				}
				if (menuItemBinding.ToolTipField.Length > 0)
				{
					this.ToolTip = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ToolTipField));
					if (this.ToolTip.Length == 0)
					{
						this.ToolTip = menuItemBinding.ToolTip;
					}
				}
				else if (menuItemBinding.ToolTip.Length > 0)
				{
					this.ToolTip = menuItemBinding.ToolTip;
				}
				string text = null;
				if (menuItemBinding.ValueField.Length > 0)
				{
					text = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ValueField));
				}
				if (string.IsNullOrEmpty(text))
				{
					if (menuItemBinding.Value.Length > 0)
					{
						text = menuItemBinding.Value;
					}
					else if (menuItemBinding.Text.Length > 0)
					{
						text = menuItemBinding.Text;
					}
					else
					{
						text = string.Empty;
					}
				}
				this.Value = text;
				string text2 = null;
				if (menuItemBinding.TextField.Length > 0)
				{
					text2 = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.TextField));
					if (menuItemBinding.FormatString.Length > 0)
					{
						text2 = string.Format(menuItemBinding.FormatString, text2);
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (menuItemBinding.Text.Length > 0)
					{
						text2 = menuItemBinding.Text;
					}
					else if (menuItemBinding.Value.Length > 0)
					{
						text2 = menuItemBinding.Value;
					}
					else
					{
						text2 = string.Empty;
					}
				}
				this.Text = text2;
			}
			else
			{
				this.Text = (this.Value = this.GetDefaultBoundText());
			}
			INavigateUIData navigateUIData = hierarchyData as INavigateUIData;
			if (navigateUIData != null)
			{
				this.ToolTip = navigateUIData.Description;
				this.Text = navigateUIData.ToString();
				this.NavigateUrl = navigateUIData.NavigateUrl;
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x0006B75C File Offset: 0x0006995C
		internal void SetDataItem(object item)
		{
			this.dataItem = item;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x0006B765 File Offset: 0x00069965
		internal void SetDataPath(string path)
		{
			this.DataPath = path;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0006B76E File Offset: 0x0006996E
		internal void SetDataBound(bool bound)
		{
			this.DataBound = bound;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0006B777 File Offset: 0x00069977
		private string GetDefaultBoundText()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.ToString();
			}
			if (this.dataItem != null)
			{
				return this.dataItem.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0006B7A6 File Offset: 0x000699A6
		private string GetDataItemType()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.Type;
			}
			if (this.dataItem != null)
			{
				return this.dataItem.GetType().ToString();
			}
			return string.Empty;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0006B7DC File Offset: 0x000699DC
		private MenuItemBinding GetBinding()
		{
			if (this.menu == null)
			{
				return null;
			}
			if (this.gotBinding)
			{
				return this.binding;
			}
			this.binding = this.menu.FindBindingForItem(this.GetDataItemType(), this.Depth);
			this.gotBinding = true;
			return this.binding;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0006B82C File Offset: 0x00069A2C
		private object GetBoundPropertyValue(string name)
		{
			if (this.boundProperties == null)
			{
				if (this.hierarchyData != null)
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.hierarchyData);
				}
				else
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.dataItem);
				}
			}
			PropertyDescriptor propertyDescriptor = this.boundProperties.Find(name, true);
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
			}
			if (this.hierarchyData != null)
			{
				return propertyDescriptor.GetValue(this.hierarchyData);
			}
			return propertyDescriptor.GetValue(this.dataItem);
		}

		// Token: 0x04001AB9 RID: 6841
		private StateBag ViewState = new StateBag();

		// Token: 0x04001ABA RID: 6842
		private MenuItemCollection items;

		// Token: 0x04001ABB RID: 6843
		private bool marked;

		// Token: 0x04001ABC RID: 6844
		private Menu menu;

		// Token: 0x04001ABD RID: 6845
		private MenuItem parent;

		// Token: 0x04001ABE RID: 6846
		private int index;

		// Token: 0x04001ABF RID: 6847
		private string path;

		// Token: 0x04001AC0 RID: 6848
		private int depth = -1;

		// Token: 0x04001AC1 RID: 6849
		private object dataItem;

		// Token: 0x04001AC2 RID: 6850
		private IHierarchyData hierarchyData;

		// Token: 0x04001AC3 RID: 6851
		private bool gotBinding;

		// Token: 0x04001AC4 RID: 6852
		private MenuItemBinding binding;

		// Token: 0x04001AC5 RID: 6853
		private PropertyDescriptorCollection boundProperties;
	}
}
