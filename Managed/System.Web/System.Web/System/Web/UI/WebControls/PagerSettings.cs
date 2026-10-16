using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the properties of the paging controls in a control that supports pagination. This class cannot be inherited.</summary>
	// Token: 0x020003DE RID: 990
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagerSettings : IStateManager
	{
		/// <summary>Occurs when a property of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object changes values.</summary>
		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06002B59 RID: 11097 RVA: 0x00070FEB File Offset: 0x0006F1EB
		// (remove) Token: 0x06002B5A RID: 11098 RVA: 0x00070FFE File Offset: 0x0006F1FE
		[Browsable(false)]
		public event EventHandler PropertyChanged
		{
			add
			{
				this.events.AddHandler(PagerSettings.propertyChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PagerSettings.propertyChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> class.</summary>
		// Token: 0x06002B5B RID: 11099 RVA: 0x00071011 File Offset: 0x0006F211
		public PagerSettings()
		{
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0007102F File Offset: 0x0006F22F
		internal PagerSettings(Control ctrl)
		{
			this.ctrl = ctrl;
		}

		/// <summary>Gets or sets the URL to an image to display for the first-page button.</summary>
		/// <returns>The URL to an image to display for the first-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.FirstPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x00071054 File Offset: 0x0006F254
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x00071081 File Offset: 0x0006F281
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string FirstPageImageUrl
		{
			get
			{
				object obj = this.ViewState["FirstPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FirstPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the first-page button.</summary>
		/// <returns>The text to display for the first-page button. The default is "&amp;lt;&amp;lt;", which renders as "&lt;&lt;".</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x0007109C File Offset: 0x0006F29C
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x000710C9 File Offset: 0x0006F2C9
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;&lt;")]
		[WebCategory("Appearance")]
		public string FirstPageText
		{
			get
			{
				object obj = this.ViewState["FirstPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;&lt;";
			}
			set
			{
				this.ViewState["FirstPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the last-page button.</summary>
		/// <returns>The URL to an image to display for the last-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.LastPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000710E4 File Offset: 0x0006F2E4
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x00071111 File Offset: 0x0006F311
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		public string LastPageImageUrl
		{
			get
			{
				object obj = this.ViewState["LastPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LastPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the last-page button.</summary>
		/// <returns>The text to display for the last-page button. The default is "&amp;gt;&amp;gt;", which renders as "&gt;&gt;".</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0007112C File Offset: 0x0006F32C
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x00071159 File Offset: 0x0006F359
		[DefaultValue("&gt;&gt;")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public string LastPageText
		{
			get
			{
				object obj = this.ViewState["LastPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;&gt;";
			}
			set
			{
				this.ViewState["LastPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the mode in which to display the pager controls in a control that supports pagination.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values. The default is PagerButtons.Numeric.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values.</exception>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x00071174 File Offset: 0x0006F374
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x0007119D File Offset: 0x0006F39D
		[DefaultValue(PagerButtons.Numeric)]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		public PagerButtons Mode
		{
			get
			{
				object obj = this.ViewState["Mode"];
				if (obj != null)
				{
					return (PagerButtons)obj;
				}
				return PagerButtons.Numeric;
			}
			set
			{
				this.ViewState["Mode"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the next-page button.</summary>
		/// <returns>The URL to an image to display for the next-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.NextPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000711BC File Offset: 0x0006F3BC
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x000711E9 File Offset: 0x0006F3E9
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string NextPageImageUrl
		{
			get
			{
				object obj = this.ViewState["NextPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NextPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the next-page button.</summary>
		/// <returns>The text to display for the next-page button. The default is "&amp;gt;", which renders as "&gt;".</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x00071204 File Offset: 0x0006F404
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x00071231 File Offset: 0x0006F431
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("&gt;")]
		public string NextPageText
		{
			get
			{
				object obj = this.ViewState["NextPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;";
			}
			set
			{
				this.ViewState["NextPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the number of page buttons to display in the pager when the <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> property is set to the <see cref="F:System.Web.UI.WebControls.PagerButtons.Numeric" /> or <see cref="F:System.Web.UI.WebControls.PagerButtons.NumericFirstLast" /> value.</summary>
		/// <returns>The number of page buttons to display in the pager. The default is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.PageButtonCount" /> is set to a value that is less than 1.</exception>
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x0007124C File Offset: 0x0006F44C
		// (set) Token: 0x06002B6C RID: 11116 RVA: 0x00071276 File Offset: 0x0006F476
		[WebCategory("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(10)]
		public int PageButtonCount
		{
			get
			{
				object obj = this.ViewState["PageButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				this.ViewState["PageButtonCount"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets a value that specifies the location where the pager is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default is <see cref="F:System.Web.UI.WebControls.PagerPosition.Bottom" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Position" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values.</exception>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x00071294 File Offset: 0x0006F494
		// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000712BD File Offset: 0x0006F4BD
		[NotifyParentProperty(true)]
		[DefaultValue(PagerPosition.Bottom)]
		[WebCategory("Layout")]
		public PagerPosition Position
		{
			get
			{
				object obj = this.ViewState["Position"];
				if (obj != null)
				{
					return (PagerPosition)obj;
				}
				return PagerPosition.Bottom;
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the previous-page button.</summary>
		/// <returns>The URL to an image to display for the previous-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.PreviousPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000712D8 File Offset: 0x0006F4D8
		// (set) Token: 0x06002B70 RID: 11120 RVA: 0x00071305 File Offset: 0x0006F505
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public string PreviousPageImageUrl
		{
			get
			{
				object obj = this.ViewState["PreviousPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PreviousPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the previous page button.</summary>
		/// <returns>The text to display for the previous page button. The default is "&amp;lt;", which renders as "&lt;".</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x00071320 File Offset: 0x0006F520
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x0007134D File Offset: 0x0006F54D
		[WebCategory("Appearance")]
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		public string PreviousPageText
		{
			get
			{
				object obj = this.ViewState["PreviousPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;";
			}
			set
			{
				this.ViewState["PreviousPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the paging controls are displayed in a control that supports pagination.</summary>
		/// <returns>true to display the pager; otherwise, false. The default is true.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x00071368 File Offset: 0x0006F568
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x00071391 File Offset: 0x0006F591
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Visible"] = value;
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000713AC File Offset: 0x0006F5AC
		private void RaisePropertyChanged()
		{
			EventHandler eventHandler = this.events[PagerSettings.propertyChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Retrieves the string representation of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <returns>An empty string ("").</returns>
		// Token: 0x06002B76 RID: 11126 RVA: 0x0000EE63 File Offset: 0x0000D063
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <param name="state">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</param>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000713DE File Offset: 0x0006F5DE
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</returns>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000713EC File Offset: 0x0006F5EC
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		// Token: 0x06002B79 RID: 11129 RVA: 0x000713F9 File Offset: 0x0006F5F9
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the server control is tracking its view state changes.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00071406 File Offset: 0x0006F606
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x00071414 File Offset: 0x0006F614
		internal Table CreatePagerControl(int currentPage, int pageCount)
		{
			Table table = new Table();
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			int num = ((this.Mode == PagerButtons.Numeric || this.Mode == PagerButtons.NumericFirstLast) ? this.PageButtonCount : 1);
			int num2 = num * (currentPage / num);
			int num3 = num2 + num;
			if (num3 > pageCount)
			{
				num3 = pageCount;
				if (num3 - num2 < num)
				{
					num2 = num3 - num;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
			}
			if ((this.Mode == PagerButtons.NumericFirstLast || this.Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
			{
				tableRow.Cells.Add(this.CreateCell(this.FirstPageText, this.FirstPageImageUrl, "Page", "First"));
			}
			if ((this.Mode == PagerButtons.NextPrevious || this.Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
			{
				tableRow.Cells.Add(this.CreateCell(this.PreviousPageText, this.PreviousPageImageUrl, "Page", "Prev"));
			}
			if (this.Mode == PagerButtons.Numeric || this.Mode == PagerButtons.NumericFirstLast)
			{
				if (num2 > 0)
				{
					tableRow.Cells.Add(this.CreateCell("...", string.Empty, "Page", num2.ToString()));
				}
				for (int i = num2; i < num3; i++)
				{
					tableRow.Cells.Add(this.CreateCell((i + 1).ToString(), string.Empty, (i != currentPage) ? "Page" : "", (i != currentPage) ? (i + 1).ToString() : ""));
				}
				if (num3 < pageCount)
				{
					tableRow.Cells.Add(this.CreateCell("...", string.Empty, "Page", (num3 + 1).ToString()));
				}
			}
			if ((this.Mode == PagerButtons.NextPrevious || this.Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
			{
				tableRow.Cells.Add(this.CreateCell(this.NextPageText, this.NextPageImageUrl, "Page", "Next"));
			}
			if ((this.Mode == PagerButtons.NumericFirstLast || this.Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
			{
				tableRow.Cells.Add(this.CreateCell(this.LastPageText, this.LastPageImageUrl, "Page", "Last"));
			}
			return table;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x00071648 File Offset: 0x0006F848
		private TableCell CreateCell(string text, string image, string command, string argument)
		{
			TableCell tableCell = new TableCell();
			Control control;
			if (string.IsNullOrEmpty(command))
			{
				control = new Label
				{
					Text = text
				};
			}
			else
			{
				control = (Control)DataControlButton.CreateButton(string.IsNullOrEmpty(image) ? ButtonType.Link : ButtonType.Image, this.ctrl, text, image, command, argument, true);
			}
			tableCell.Controls.Add(control);
			return tableCell;
		}

		// Token: 0x04001B21 RID: 6945
		private static readonly object propertyChangedEvent = new object();

		// Token: 0x04001B22 RID: 6946
		private StateBag ViewState = new StateBag();

		// Token: 0x04001B23 RID: 6947
		private Control ctrl;

		// Token: 0x04001B24 RID: 6948
		private EventHandlerList events = new EventHandlerList();
	}
}
