using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class that defines the properties, methods, and events common for all list-type controls.</summary>
	// Token: 0x020003BB RID: 955
	[ControlValueProperty("SelectedValue", null)]
	[ParseChildren(true, "Items")]
	[Designer("System.Web.UI.Design.WebControls.ListControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("SelectedIndexChanged")]
	[DataBindingHandler("System.Web.UI.Design.WebControls.ListControlDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class ListControl : DataBoundControl, IEditableTextControl, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
		// Token: 0x06002768 RID: 10088 RVA: 0x00064828 File Offset: 0x00062A28
		public ListControl()
			: base(HtmlTextWriterTag.Select)
		{
		}

		/// <summary>Gets or sets a value that indicates whether list items are cleared before data binding.</summary>
		/// <returns>true if list items are not cleared before data binding; otherwise, false, if the items collection is cleared before data binding is performed. The default is false.</returns>
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x0006483A File Offset: 0x00062A3A
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x0006484D File Offset: 0x00062A4D
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AppendDataBoundItems
		{
			get
			{
				return this.ViewState.GetBool("AppendDataBoundItems", false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a postback to the server automatically occurs when the user changes the list selection.</summary>
		/// <returns>true if a postback to the server automatically occurs whenever the user changes the selection of the list; otherwise, false. The default is false.</returns>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x0004BABB File Offset: 0x00049CBB
		[Themeable(false)]
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the field of the data source that provides the text content of the list items.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the field of the data source that provides the text content of the list items. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00064874 File Offset: 0x00062A74
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x0006488B File Offset: 0x00062A8B
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				return this.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets the formatting string used to control how data bound to the list control is displayed.</summary>
		/// <returns>The formatting string for data bound to the control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000648AD File Offset: 0x00062AAD
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x000648C4 File Offset: 0x00062AC4
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataTextFormatString
		{
			get
			{
				return this.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets the field of the data source that provides the value of each list item.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the field of the data source that provides the value of each list item. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000648E6 File Offset: 0x00062AE6
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x000648FD File Offset: 0x00062AFD
		[Themeable(false)]
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("")]
		public virtual string DataValueField
		{
			get
			{
				return this.ViewState.GetString("DataValueField", string.Empty);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets the collection of items in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> that represents the items within the list. The default is an empty list.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x0006491F File Offset: 0x00062B1F
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual ListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the lowest ordinal index of the selected items in the list.</summary>
		/// <returns>The lowest ordinal index of the selected items in the list. The default is -1, which indicates that nothing is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered. </exception>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x00064950 File Offset: 0x00062B50
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x00064994 File Offset: 0x00062B94
		[WebCategory("Misc")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(0)]
		[Browsable(false)]
		[Bindable(true)]
		[Themeable(false)]
		public virtual int SelectedIndex
		{
			get
			{
				if (this.items == null)
				{
					return -1;
				}
				for (int i = 0; i < this.items.Count; i++)
				{
					if (this.items[i].Selected)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				this._selectedIndex = value;
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value >= this.Items.Count)
				{
					return;
				}
				this.ClearSelection();
				if (value == -1)
				{
					return;
				}
				this.items[value].Selected = true;
			}
		}

		/// <summary>Gets the selected item with the lowest index in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that represents the lowest indexed item selected from the list control. The default is null.</returns>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000649E4 File Offset: 0x00062BE4
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[Browsable(false)]
		public virtual ListItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex == -1)
				{
					return null;
				}
				return this.Items[selectedIndex];
			}
		}

		/// <summary>Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.</summary>
		/// <returns>The value of the selected item in the list control. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not in the list of available values and view state or other state has been loaded (a postback has been performed). For more information, see the Remarks section.</exception>
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00064A0C File Offset: 0x00062C0C
		// (set) Token: 0x06002778 RID: 10104 RVA: 0x00064A3B File Offset: 0x00062C3B
		[Bindable(true, BindingDirection.TwoWay)]
		[Themeable(false)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string SelectedValue
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex == -1)
				{
					return string.Empty;
				}
				return this.Items[selectedIndex].Value;
			}
			set
			{
				this._selectedValue = value;
				this.SetSelectedValue(value);
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00064A4C File Offset: 0x00062C4C
		private bool SetSelectedValue(string value)
		{
			if (this.items != null && this.items.Count > 0)
			{
				int count = this.items.Count;
				ListItemCollection listItemCollection = this.Items;
				for (int i = 0; i < count; i++)
				{
					if (listItemCollection[i].Value == value)
					{
						this.ClearSelection();
						listItemCollection[i].Selected = true;
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> property of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.ListControl" />.</returns>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x00064AB8 File Offset: 0x00062CB8
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x00064AC0 File Offset: 0x00062CC0
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string Text
		{
			get
			{
				return this.SelectedValue;
			}
			set
			{
				this.SelectedValue = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ListControl" /> control. </summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.HtmlTextWriterTag.Select" />.</returns>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x00064AC9 File Offset: 0x00062CC9
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Select;
			}
		}

		/// <summary>Applies HTML attributes and styles to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
		// Token: 0x0600277D RID: 10109 RVA: 0x00064ACD File Offset: 0x00062CCD
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		/// <summary>Clears out the list selection and sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of all items to false.</summary>
		// Token: 0x0600277E RID: 10110 RVA: 0x00064AD8 File Offset: 0x00062CD8
		public virtual void ClearSelection()
		{
			if (this.items == null)
			{
				return;
			}
			int count = this.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.items[i].Selected = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600277F RID: 10111 RVA: 0x00064B18 File Offset: 0x00062D18
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			IEnumerable enumerable = this.GetData().ExecuteSelect(DataSourceSelectArguments.Empty);
			base.InternalPerformDataBinding(enumerable);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002780 RID: 10112 RVA: 0x00064B44 File Offset: 0x00062D44
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x06002781 RID: 10113 RVA: 0x00064B74 File Offset: 0x00062D74
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Binds the specified data source to the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cached value of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> is out of range.</exception>
		/// <exception cref="T:System.ArgumentException">The cached values of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> do not match.</exception>
		// Token: 0x06002782 RID: 10114 RVA: 0x00064BA4 File Offset: 0x00062DA4
		protected internal override void PerformDataBinding(IEnumerable dataSource)
		{
			if (dataSource != null)
			{
				if (!this.AppendDataBoundItems)
				{
					this.Items.Clear();
				}
				string text = this.DataTextFormatString;
				if (text.Length == 0)
				{
					text = null;
				}
				string text2 = this.DataTextField;
				string text3 = this.DataValueField;
				if (text2.Length == 0)
				{
					text2 = null;
				}
				if (text3.Length == 0)
				{
					text3 = null;
				}
				ListItemCollection listItemCollection = this.Items;
				foreach (object obj in dataSource)
				{
					string text5;
					string text4 = (text5 = null);
					if (text2 != null)
					{
						text5 = DataBinder.GetPropertyValue(obj, text2, text);
					}
					if (text3 != null)
					{
						text4 = DataBinder.GetPropertyValue(obj, text3).ToString();
					}
					else if (text2 == null)
					{
						text4 = (text5 = obj.ToString());
						if (text != null)
						{
							text5 = string.Format(text, obj);
						}
					}
					else if (text5 != null)
					{
						text4 = text5;
					}
					if (text5 == null)
					{
						text5 = text4;
					}
					listItemCollection.Add(new ListItem(text5, text4));
				}
			}
			if (!string.IsNullOrEmpty(this._selectedValue))
			{
				if (!this.SetSelectedValue(this._selectedValue))
				{
					throw new ArgumentOutOfRangeException("value", string.Format("'{0}' has a SelectedValue which is invalid because it does not exist in the list of items.", this.ID));
				}
				if (this._selectedIndex >= 0 && this._selectedIndex != this.SelectedIndex)
				{
					throw new ArgumentException("SelectedIndex and SelectedValue are mutually exclusive.");
				}
			}
			else if (this._selectedIndex >= 0)
			{
				this.SelectedIndex = this._selectedIndex;
			}
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x06002783 RID: 10115 RVA: 0x00064D1C File Offset: 0x00062F1C
		[global::System.MonoTODO("why override?")]
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		/// <summary>Renders the items in the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
		// Token: 0x06002784 RID: 10116 RVA: 0x00064D44 File Offset: 0x00062F44
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			bool flag = false;
			Page page = this.Page;
			for (int i = 0; i < this.Items.Count; i++)
			{
				ListItem listItem = this.Items[i];
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(this.UniqueID, listItem.Value);
				}
				writer.WriteBeginTag("option");
				if (listItem.Selected)
				{
					if (flag)
					{
						this.VerifyMultiSelect();
					}
					writer.WriteAttribute("selected", "selected", false);
					flag = true;
				}
				writer.WriteAttribute("value", listItem.Value, true);
				if (listItem.HasAttributes)
				{
					listItem.Attributes.Render(writer);
				}
				writer.Write(">");
				string text = HttpUtility.HtmlEncode(listItem.Text);
				writer.Write(text);
				writer.WriteEndTag("option");
				writer.WriteLine();
			}
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00064E24 File Offset: 0x00063024
		internal ArrayList GetSelectedIndicesInternal()
		{
			ArrayList arrayList = null;
			int count;
			if (this.items != null && (count = this.items.Count) > 0)
			{
				arrayList = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					if (this.items[i].Selected)
					{
						arrayList.Add(i);
					}
				}
			}
			return arrayList;
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control and the items it contains.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</returns>
		// Token: 0x06002786 RID: 10118 RVA: 0x00064E80 File Offset: 0x00063080
		protected override object SaveViewState()
		{
			object obj = null;
			object obj2 = base.SaveViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				obj = stateManager.SaveViewState();
			}
			return new Pair(obj2, obj);
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</param>
		// Token: 0x06002787 RID: 10119 RVA: 0x00064EAC File Offset: 0x000630AC
		protected override void LoadViewState(object savedState)
		{
			object obj = null;
			object obj2 = null;
			Pair pair = savedState as Pair;
			if (pair != null)
			{
				obj = pair.First;
				obj2 = pair.Second;
			}
			base.LoadViewState(obj);
			if (obj2 != null)
			{
				((IStateManager)this.Items).LoadViewState(obj2);
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of a <see cref="T:System.Web.UI.WebControls.ListItem" /> control after a page is posted.</summary>
		/// <param name="selectedIndex">The index of the selected item in the <see cref="P:System.Web.UI.WebControls.ListControl.Items" /> collection.</param>
		// Token: 0x06002788 RID: 10120 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected void SetPostDataSelection(int selectedIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the starting point to begin tracking and saving view-state changes to a <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</summary>
		// Token: 0x06002789 RID: 10121 RVA: 0x00064EEC File Offset: 0x000630EC
		protected override void TrackViewState()
		{
			base.TrackViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				stateManager.TrackViewState();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.SelectedIndexChanged" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600278A RID: 10122 RVA: 0x00064F10 File Offset: 0x00063110
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Determines whether the list control supports multiselection mode.</summary>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.UI.WebControls.ListBox.SelectionMode" /> is set to <see cref="F:System.Web.UI.WebControls.ListSelectionMode.Single" />.</exception>
		// Token: 0x0600278B RID: 10123 RVA: 0x00064F3E File Offset: 0x0006313E
		protected internal virtual void VerifyMultiSelect()
		{
			if (!this.MultiSelectOk())
			{
				throw new HttpException("Multi select is not supported");
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00008A4D File Offset: 0x00006C4D
		internal virtual bool MultiSelectOk()
		{
			return false;
		}

		/// <summary>Occurs when the selection from the list control changes between posts to the server.</summary>
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600278D RID: 10125 RVA: 0x00064F53 File Offset: 0x00063153
		// (remove) Token: 0x0600278E RID: 10126 RVA: 0x00064F66 File Offset: 0x00063166
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.WebControls.ListControl.Text" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> properties change.</summary>
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x0600278F RID: 10127 RVA: 0x00064F79 File Offset: 0x00063179
		// (remove) Token: 0x06002790 RID: 10128 RVA: 0x00064F8C File Offset: 0x0006318C
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.TextChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.ListControl" /> control is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0004BAD3 File Offset: 0x00049CD3
		// (set) Token: 0x06002792 RID: 10130 RVA: 0x0003EF1E File Offset: 0x0003D11E
		[DefaultValue(false)]
		[WebSysDescription("")]
		[Themeable(false)]
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

		/// <summary>Gets or sets the group of controls for which the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the derived <see cref="T:System.Web.UI.WebControls.ListControl" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0003F11F File Offset: 0x0003D31F
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[Themeable(false)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		// Token: 0x06002795 RID: 10133 RVA: 0x00064F9F File Offset: 0x0006319F
		// Note: this type is marked as 'beforefieldinit'.
		static ListControl()
		{
			ListControl.SelectedIndexChangedEvent = new object();
			ListControl.TextChangedEvent = new object();
		}

		// Token: 0x04001A59 RID: 6745
		private ListItemCollection items;

		// Token: 0x04001A5A RID: 6746
		private int _selectedIndex = -2;

		// Token: 0x04001A5B RID: 6747
		private string _selectedValue;
	}
}
