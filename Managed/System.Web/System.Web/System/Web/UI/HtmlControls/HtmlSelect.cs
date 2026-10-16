using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;select&gt; element on the server.</summary>
	// Token: 0x02000269 RID: 617
	[SupportsEventValidation]
	[ControlBuilder(typeof(HtmlSelectBuilder))]
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlSelect : HtmlContainerControl, IPostBackDataHandler, IParserAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> class.</summary>
		// Token: 0x06001937 RID: 6455 RVA: 0x000417AF File Offset: 0x0003F9AF
		public HtmlSelect()
			: base("select")
		{
		}

		/// <summary>Gets or sets the set of data to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control from a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property with multiple sets of data.</summary>
		/// <returns>The set of data to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control from a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> with multiple sets of data. The default value is an empty string (""), which indicates the property has not been set.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" /> property is set during the data-binding phase of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. </exception>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x000417BC File Offset: 0x0003F9BC
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x000417E4 File Offset: 0x0003F9E4
		[WebCategory("Data")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public virtual string DataMember
		{
			get
			{
				string text = base.Attributes["datamember"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("datamember");
					return;
				}
				base.Attributes["datamember"] = value;
			}
		}

		/// <summary>Gets or sets the source of information to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to this control. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The specified data source is not compatible with either <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" />, and it is not null. </exception>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. </exception>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0004180B File Offset: 0x0003FA0B
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x00041813 File Offset: 0x0003FA13
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual object DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				if (value != null && !(value is IEnumerable) && !(value is IListSource))
				{
					throw new ArgumentException();
				}
				this.datasource = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control should use to retrieve its data source.</summary>
		/// <returns>The programmatic identifier assigned to the data source control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. </exception>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00041835 File Offset: 0x0003FA35
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0004184C File Offset: 0x0003FA4C
		[DefaultValue("")]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", "");
			}
			set
			{
				if (this.DataSourceID == value)
				{
					return;
				}
				this.ViewState["DataSourceID"] = value;
				if (this._boundDataSourceView != null)
				{
					this._boundDataSourceView.DataSourceViewChanged -= this.OnDataSourceViewChanged;
				}
				this._boundDataSourceView = null;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets or sets the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>The field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x000418A8 File Offset: 0x0003FAA8
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x000418D0 File Offset: 0x0003FAD0
		[WebSysDescription("")]
		[WebCategory("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				string text = base.Attributes["datatextfield"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("datatextfield");
					return;
				}
				base.Attributes["datatextfield"] = value;
			}
		}

		/// <summary>Gets or sets the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>The field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x000418F8 File Offset: 0x0003FAF8
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x00041920 File Offset: 0x0003FB20
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataValueField
		{
			get
			{
				string text = base.Attributes["datavaluefield"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("datavaluefield");
					return;
				}
				base.Attributes["datavaluefield"] = value;
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read from or assign a value to this property. </exception>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override string InnerHtml
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the content between the opening and closing tags of the control with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
		/// <returns>The content between the opening and closing tags of the control.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read from or assign a value to this property. </exception>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00003A0D File Offset: 0x00001C0D
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x00003A0D File Offset: 0x00001C0D
		public override string InnerText
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. </summary>
		/// <returns>true if a data source control is defined; otherwise, false.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00041947 File Offset: 0x0003FB47
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length != 0;
			}
		}

		/// <summary>Gets a collection that contains the items listed in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> that contains the items listed in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00041957 File Offset: 0x0003FB57
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ListItemCollection Items
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

		/// <summary>Gets or sets a value indicating whether multiple items can be selected concurrently in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>true if multiple items can be concurrently selected in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control; otherwise, false. The default value is false.</returns>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00041985 File Offset: 0x0003FB85
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x0004199C File Offset: 0x0003FB9C
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public bool Multiple
		{
			get
			{
				return base.Attributes["multiple"] != null;
			}
			set
			{
				if (!value)
				{
					base.Attributes.Remove("multiple");
					return;
				}
				base.Attributes["multiple"] = "multiple";
			}
		}

		/// <summary>Gets or sets the unique identifier name associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>The unique identifier name associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x00003946 File Offset: 0x00001B46
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control needs to bind to its specified data source.</summary>
		/// <returns>true if the control needs to bind to a data source; otherwise, false.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000419C7 File Offset: 0x0003FBC7
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x000419CF File Offset: 0x0003FBCF
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				this.requiresDataBinding = value;
			}
		}

		/// <summary>Gets or sets the ordinal index of the selected item in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>The ordinal index of the selected item in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. A value of -1 indicates that no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than -1.</exception>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x000419D8 File Offset: 0x0003FBD8
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00041A38 File Offset: 0x0003FC38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int SelectedIndex
		{
			get
			{
				ListItemCollection listItemCollection = this.Items;
				for (int i = 0; i < listItemCollection.Count; i++)
				{
					if (listItemCollection[i].Selected)
					{
						return i;
					}
				}
				if (!this.Multiple && this.Size <= 1)
				{
					if (listItemCollection.Count > 0)
					{
						listItemCollection[0].Selected = true;
					}
					return 0;
				}
				return -1;
			}
			set
			{
				this.ClearSelection();
				if (value == -1 || this.items == null)
				{
					return;
				}
				if (value < 0 || value >= this.items.Count)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.items[value].Selected = true;
			}
		}

		/// <summary>Gets a collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>A collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00041A88 File Offset: 0x0003FC88
		protected virtual int[] SelectedIndices
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				int count = this.Items.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.Items[i].Selected)
					{
						arrayList.Add(i);
					}
				}
				return (int[])arrayList.ToArray(typeof(int));
			}
		}

		/// <summary>Gets or sets the height (in rows) of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>The height (in rows) of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00041AE8 File Offset: 0x0003FCE8
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00040ACD File Offset: 0x0003ECCD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, Helpers.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("size");
					return;
				}
				base.Attributes["size"] = value.ToString();
			}
		}

		/// <summary>Gets the value of the selected item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or sets the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property of the control to the index of the first item in the list with the specified value.</summary>
		/// <returns>The value of the selected item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. If no item is selected in the control, <see cref="F:System.String.Empty" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Value" /> property was set to an item greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than -1.</exception>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00041B18 File Offset: 0x0003FD18
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00041B58 File Offset: 0x0003FD58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Value
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0 && selectedIndex < this.Items.Count)
				{
					return this.Items[selectedIndex].Value;
				}
				return string.Empty;
			}
			set
			{
				int num = this.Items.IndexOf(value);
				if (num >= 0)
				{
					this.SelectedIndex = num;
				}
			}
		}

		/// <summary>Occurs when the selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control change between posts to the server.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001955 RID: 6485 RVA: 0x00041B7D File Offset: 0x0003FD7D
		// (remove) Token: 0x06001956 RID: 6486 RVA: 0x00041B90 File Offset: 0x0003FD90
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlSelect.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlSelect.EventServerChange, value);
			}
		}

		/// <summary>Adds a parsed child control to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <param name="obj">The parsed child control to add. </param>
		/// <exception cref="T:System.Web.HttpException">The child control specified by the <paramref name="obj" /> parameter must be of the type <see cref="T:System.Web.UI.WebControls.ListItem" />.</exception>
		// Token: 0x06001957 RID: 6487 RVA: 0x00041BA3 File Offset: 0x0003FDA3
		protected override void AddParsedSubObject(object obj)
		{
			if (!(obj is ListItem))
			{
				throw new HttpException("HtmlSelect can only contain ListItem");
			}
			this.Items.Add((ListItem)obj);
			base.AddParsedSubObject(obj);
		}

		/// <summary>Clears the list selection of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control and sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of all items to false.</summary>
		// Token: 0x06001958 RID: 6488 RVA: 0x00041BD0 File Offset: 0x0003FDD0
		protected virtual void ClearSelection()
		{
			if (this.items == null)
			{
				return;
			}
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				this.items[i].Selected = false;
			}
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> to contain the current server control's child server controls. </returns>
		// Token: 0x06001959 RID: 6489 RVA: 0x0003F777 File Offset: 0x0003D977
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Verifies that the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.Control.DataBind" /> method.</summary>
		// Token: 0x0600195A RID: 6490 RVA: 0x00041C10 File Offset: 0x0003FE10
		protected void EnsureDataBound()
		{
			if (this.IsBoundUsingDataSourceID && this.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerable" /> object that represents the data source that is bound to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> object. If no data source is specified, a default value of null is returned.</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IDataSource" />.- or - The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Both a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> and a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property are defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.- or -The requested data view cannot be found.</exception>
		// Token: 0x0600195B RID: 6491 RVA: 0x00041C28 File Offset: 0x0003FE28
		protected virtual IEnumerable GetData()
		{
			if (this.DataSource != null && this.IsBoundUsingDataSourceID)
			{
				throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
			}
			if (this.DataSource != null)
			{
				return DataSourceResolver.ResolveDataSource(this.DataSource, this.DataMember);
			}
			if (!this.IsBoundUsingDataSourceID)
			{
				return null;
			}
			IEnumerable result = null;
			this.ConnectToDataSource().Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
			{
				result = data;
			});
			return result;
		}

		/// <summary>Restores the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's view state information from a previous page request that was saved by the <see cref="M:System.Web.UI.HtmlControls.HtmlSelect.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x0600195C RID: 6492 RVA: 0x00041CA4 File Offset: 0x0003FEA4
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

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event of an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IDataSource" />.- or - The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Both a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> and a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property are defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.- or -The requested data view cannot be found.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than -1.</exception>
		// Token: 0x0600195D RID: 6493 RVA: 0x00041CE4 File Offset: 0x0003FEE4
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			ListItemCollection listItemCollection = this.Items;
			listItemCollection.Clear();
			IEnumerable data = this.GetData();
			if (data == null)
			{
				return;
			}
			foreach (object obj in data)
			{
				string text = null;
				string text2;
				if (this.DataTextField == string.Empty && this.DataValueField == string.Empty)
				{
					text = obj.ToString();
					text2 = text;
				}
				else
				{
					if (this.DataTextField != string.Empty)
					{
						text = DataBinder.Eval(obj, this.DataTextField).ToString();
					}
					if (this.DataValueField != string.Empty)
					{
						text2 = DataBinder.Eval(obj, this.DataValueField).ToString();
					}
					else
					{
						text2 = text;
					}
					if (text == null && text2 != null)
					{
						text = text2;
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				ListItem listItem = new ListItem(text, text2);
				listItemCollection.Add(listItem);
			}
			this.RequiresDataBinding = false;
			this.IsDataBound = true;
		}

		/// <summary>Invoked when the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" />, <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" />, or <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is changed.</summary>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
		// Token: 0x0600195E RID: 6494 RVA: 0x00041E20 File Offset: 0x00040020
		protected virtual void OnDataPropertyChanged()
		{
			if (this._initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Invoked when the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" />, <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" />, or <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is changed.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600195F RID: 6495 RVA: 0x00041E31 File Offset: 0x00040031
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001960 RID: 6496 RVA: 0x00041E3A File Offset: 0x0004003A
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.PreLoad += this.OnPagePreLoad;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00041E5B File Offset: 0x0004005B
		protected virtual void OnPagePreLoad(object sender, EventArgs e)
		{
			this.Initialize();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The ID specified in the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property cannot be found.- or -The control specified in the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of the type <see cref="T:System.Web.UI.IDataSource" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. - or -The requested <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" /> property could not be found.</exception>
		// Token: 0x06001962 RID: 6498 RVA: 0x00041E63 File Offset: 0x00040063
		protected internal override void OnLoad(EventArgs e)
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			base.OnLoad(e);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00041E7A File Offset: 0x0004007A
		private void Initialize()
		{
			this._initialized = true;
			if (!this.IsDataBound)
			{
				this.RequiresDataBinding = true;
			}
			if (this.IsBoundUsingDataSourceID)
			{
				this.ConnectToDataSource();
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00041EA1 File Offset: 0x000400A1
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x00041EB4 File Offset: 0x000400B4
		private bool IsDataBound
		{
			get
			{
				return this.ViewState.GetBool("_DataBound", false);
			}
			set
			{
				this.ViewState["_DataBound"] = value;
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00041ECC File Offset: 0x000400CC
		private DataSourceView ConnectToDataSource()
		{
			if (this._boundDataSourceView != null)
			{
				return this._boundDataSourceView;
			}
			object obj = null;
			Page page = this.Page;
			if (page != null)
			{
				obj = page.FindControl(this.DataSourceID);
			}
			if (obj == null || !(obj is IDataSource))
			{
				string text;
				if (obj == null)
				{
					text = "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.";
				}
				else
				{
					text = "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  '{1}' is not an IDataSource.";
				}
				throw new HttpException(string.Format(text, this.ID, this.DataSourceID));
			}
			this._boundDataSourceView = ((IDataSource)obj).GetView(string.Empty);
			this._boundDataSourceView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			return this._boundDataSourceView;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001967 RID: 6503 RVA: 0x00041F6C File Offset: 0x0004016C
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlSelect.ServerChange" /> event of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001968 RID: 6504 RVA: 0x00041FA8 File Offset: 0x000401A8
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlSelect.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x06001969 RID: 6505 RVA: 0x00041FD8 File Offset: 0x000401D8
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			writer.WriteAttribute("name", this.Name);
			base.Attributes.Remove("name");
			base.Attributes.Remove("datamember");
			base.Attributes.Remove("datatextfield");
			base.Attributes.Remove("datavaluefield");
			base.RenderAttributes(writer);
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		/// <exception cref="T:System.Web.HttpException">Multiple items were selected but the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Multiple" /> property is set to false.</exception>
		// Token: 0x0600196A RID: 6506 RVA: 0x00042058 File Offset: 0x00040258
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
			if (this.items == null)
			{
				return;
			}
			writer.WriteLine();
			bool flag = false;
			int count = this.items.Count;
			for (int i = 0; i < count; i++)
			{
				ListItem listItem = this.items[i];
				int num = writer.Indent;
				writer.Indent = num + 1;
				writer.WriteBeginTag("option");
				if (listItem.Selected && !flag)
				{
					writer.WriteAttribute("selected", "selected");
					if (!this.Multiple)
					{
						flag = true;
					}
				}
				writer.WriteAttribute("value", listItem.Value, true);
				if (listItem.HasAttributes)
				{
					AttributeCollection attributes = listItem.Attributes;
					foreach (object obj in attributes.Keys)
					{
						string text = (string)obj;
						writer.WriteAttribute(text, HttpUtility.HtmlAttributeEncode(attributes[text]));
					}
				}
				writer.Write('>');
				writer.Write(HttpUtility.HtmlEncode(listItem.Text));
				writer.WriteEndTag("option");
				writer.WriteLine();
				num = writer.Indent;
				writer.Indent = num - 1;
			}
		}

		/// <summary>Saves any <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control view state changes that have occurred since the page was posted back to the server.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> view state. If no view state is associated with the object, this method returns a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x0600196B RID: 6507 RVA: 0x000421AC File Offset: 0x000403AC
		protected override object SaveViewState()
		{
			object obj = null;
			object obj2 = base.SaveViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				obj = stateManager.SaveViewState();
			}
			if (obj2 == null && obj == null)
			{
				return null;
			}
			return new Pair(obj2, obj);
		}

		/// <summary>Selects multiple items of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Items" /> collection.</summary>
		/// <param name="selectedIndices">An <see cref="T:System.Array" /> of type <see cref="T:System.Int32" /> that contains the items to select.</param>
		// Token: 0x0600196C RID: 6508 RVA: 0x000421E4 File Offset: 0x000403E4
		protected virtual void Select(int[] selectedIndices)
		{
			if (this.items == null)
			{
				return;
			}
			this.ClearSelection();
			int count = this.items.Count;
			foreach (int num in selectedIndices)
			{
				if (num >= 0 && num < count)
				{
					this.items[num].Selected = true;
				}
			}
		}

		/// <summary>Tracks view state changes to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control so the changes can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600196D RID: 6509 RVA: 0x0004223C File Offset: 0x0004043C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				stateManager.TrackViewState();
			}
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlSelect.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control that the state of the control has changed.</summary>
		// Token: 0x0600196E RID: 6510 RVA: 0x0004225F File Offset: 0x0004045F
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's state has changed as a result of a postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than -1.</exception>
		// Token: 0x0600196F RID: 6511 RVA: 0x0004226C File Offset: 0x0004046C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string[] values = postCollection.GetValues(postDataKey);
			bool flag = false;
			if (values != null)
			{
				if (this.Multiple)
				{
					int num = values.Length;
					int[] selectedIndices = this.SelectedIndices;
					int[] array = new int[num];
					int num2 = selectedIndices.Length;
					for (int i = 0; i < num; i++)
					{
						array[i] = this.Items.IndexOf(values[i]);
						if (num2 != num || selectedIndices[i] != array[i])
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.Select(array);
					}
				}
				else
				{
					int num3 = this.Items.IndexOf(values[0]);
					if (num3 != this.SelectedIndex)
					{
						this.SelectedIndex = num3;
						flag = true;
					}
				}
			}
			if (flag)
			{
				base.ValidateEvent(postDataKey, string.Empty);
			}
			return flag;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's state has changed as a result of a postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than -1.</exception>
		// Token: 0x06001970 RID: 6512 RVA: 0x00042320 File Offset: 0x00040520
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />. </summary>
		// Token: 0x06001971 RID: 6513 RVA: 0x0004232A File Offset: 0x0004052A
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x04001637 RID: 5687
		private static readonly object EventServerChange = new object();

		// Token: 0x04001638 RID: 5688
		private DataSourceView _boundDataSourceView;

		// Token: 0x04001639 RID: 5689
		private bool requiresDataBinding;

		// Token: 0x0400163A RID: 5690
		private bool _initialized;

		// Token: 0x0400163B RID: 5691
		private object datasource;

		// Token: 0x0400163C RID: 5692
		private ListItemCollection items;
	}
}
