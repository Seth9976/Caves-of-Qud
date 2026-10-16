using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>A data-bound list control that allows custom layout by repeating a specified template for each item displayed in the list.</summary>
	// Token: 0x020003F9 RID: 1017
	[DefaultEvent("ItemCommand")]
	[DefaultProperty("DataSource")]
	[Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Repeater : Control, INamingContainer
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x000758D7 File Offset: 0x00073AD7
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ViewState["Items"] != null)
			{
				this.CreateControlHierarchy(false);
			}
		}

		/// <summary>Raises the DataBinding event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000758FD File Offset: 0x00073AFD
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(EventArgs.Empty);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.TrackViewState();
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x00075930 File Offset: 0x00073B30
		private void DoItem(int i, ListItemType t, object d, bool databind)
		{
			RepeaterItem repeaterItem = this.CreateItem(i, t);
			if (t == ListItemType.Item || t == ListItemType.AlternatingItem)
			{
				this.items.Add(repeaterItem);
			}
			repeaterItem.DataItem = d;
			RepeaterItemEventArgs e = new RepeaterItemEventArgs(repeaterItem);
			this.InitializeItem(repeaterItem);
			this.Controls.Add(repeaterItem);
			this.OnItemCreated(e);
			if (databind)
			{
				repeaterItem.DataBind();
				this.OnItemDataBound(e);
			}
		}

		/// <summary>Creates a control hierarchy, with or without the specified data source.</summary>
		/// <param name="useDataSource">Indicates whether to use the specified data source. </param>
		// Token: 0x06002CF5 RID: 11509 RVA: 0x00075994 File Offset: 0x00073B94
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			this.items = new ArrayList();
			this.itemscol = null;
			IEnumerable enumerable;
			if (useDataSource)
			{
				enumerable = this.GetData();
			}
			else
			{
				enumerable = new object[(int)this.ViewState["Items"]];
			}
			if (enumerable == null)
			{
				return;
			}
			if (this.HeaderTemplate != null)
			{
				this.DoItem(-1, ListItemType.Header, null, useDataSource);
			}
			int num = 0;
			foreach (object obj in enumerable)
			{
				if (num != 0 && this.SeparatorTemplate != null)
				{
					this.DoItem(num - 1, ListItemType.Separator, null, useDataSource);
				}
				this.DoItem(num, (num % 2 == 0) ? ListItemType.Item : ListItemType.AlternatingItem, obj, useDataSource);
				num++;
			}
			if (this.FooterTemplate != null)
			{
				this.DoItem(-1, ListItemType.Footer, null, useDataSource);
			}
			this.ViewState["Items"] = num;
		}

		/// <summary>Binds the <see cref="T:System.Web.UI.WebControls.Repeater" /> control and all its child controls to the specified data source.</summary>
		// Token: 0x06002CF6 RID: 11510 RVA: 0x00075A88 File Offset: 0x00073C88
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.RequiresDataBinding = false;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object with the specified item type and location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>The new <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object.</returns>
		/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control to place the created item. </param>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the <see cref="T:System.Web.UI.WebControls.Repeater" /> item to create. </param>
		// Token: 0x06002CF7 RID: 11511 RVA: 0x00075A9C File Offset: 0x00073C9C
		protected virtual RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new RepeaterItem(itemIndex, itemType);
		}

		/// <summary>Populates iteratively the specified <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> with a sub-hierarchy of child controls.</summary>
		/// <param name="item">The control to be initialized from an inline template. </param>
		// Token: 0x06002CF8 RID: 11512 RVA: 0x00075AA8 File Offset: 0x00073CA8
		protected virtual void InitializeItem(RepeaterItem item)
		{
			ITemplate template = null;
			switch (item.ItemType)
			{
			case ListItemType.Header:
				template = this.HeaderTemplate;
				break;
			case ListItemType.Footer:
				template = this.FooterTemplate;
				break;
			case ListItemType.Item:
				template = this.ItemTemplate;
				break;
			case ListItemType.AlternatingItem:
				template = this.AlternatingItemTemplate;
				if (template == null)
				{
					template = this.ItemTemplate;
				}
				break;
			case ListItemType.Separator:
				template = this.SeparatorTemplate;
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(item);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event if the <paramref name="EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
		/// <returns>true if the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> was raised, otherwise false.</returns>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002CF9 RID: 11513 RVA: 0x00075B24 File Offset: 0x00073D24
		protected override bool OnBubbleEvent(object sender, EventArgs e)
		{
			RepeaterCommandEventArgs e2 = e as RepeaterCommandEventArgs;
			if (e2 != null)
			{
				this.OnItemCommand(e2);
				return true;
			}
			return false;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0004505A File Offset: 0x0004325A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects. The default is an empty <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</returns>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x00075B45 File Offset: 0x00073D45
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[Browsable(false)]
		public virtual RepeaterItemCollection Items
		{
			get
			{
				if (this.itemscol == null)
				{
					if (this.items == null)
					{
						this.items = new ArrayList();
					}
					this.itemscol = new RepeaterItemCollection(this.items);
				}
				return this.itemscol;
			}
		}

		/// <summary>Gets or sets the specific table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> to bind to the control.</summary>
		/// <returns>A string that specifies a table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" />.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x00075B79 File Offset: 0x00073D79
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x00075B90 File Offset: 0x00073D90
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", "");
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DataMember");
				}
				else
				{
					this.ViewState["DataMember"] = value;
				}
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
				}
			}
		}

		/// <summary>Gets or sets the data source that provides data for populating the list.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> object that contains a collection of values used to supply data to this control. The default value is null.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> object specified is not a supported source of data for the <see cref="T:System.Web.UI.WebControls.Repeater" /> control. </exception>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property. </exception>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x00075BC6 File Offset: 0x00073DC6
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x00075BD0 File Offset: 0x00073DD0
		[Bindable(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IListSource) && !(value is IEnumerable))
				{
					throw new ArgumentException(string.Format("An invalid data source is being used for {0}. A valid data source must implement either IListSource or IEnumerable", this.ID));
				}
				this.dataSource = value;
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
					return;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control should use to retrieve its data source.</summary>
		/// <returns>The ID property of the data source control.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved for one of the following reasons:A value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> and <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> properties.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property cannot be found on the page.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property does not implement <see cref="T:System.Web.UI.IDataSource" />.</exception>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x00041835 File Offset: 0x0003FA35
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x00075C1C File Offset: 0x00073E1C
		[IDReferenceProperty(typeof(DataSourceControl))]
		[DefaultValue("")]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", "");
			}
			set
			{
				if (this.dataSource != null)
				{
					throw new HttpException("Only one of DataSource and DataSourceID can be specified.");
				}
				this.ViewState["DataSourceID"] = value;
				if (!this.Initialized)
				{
					this.OnDataPropertyChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether themes are applied to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false. </returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0006E38C File Offset: 0x0006C58C
		// (set) Token: 0x06002D03 RID: 11523 RVA: 0x0006E394 File Offset: 0x0006C594
		[Browsable(true)]
		public override bool EnableTheming
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

		/// <summary>Gets or sets the object implementing <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items in the control are displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items are displayed. The default value is null.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x00075C50 File Offset: 0x00073E50
		// (set) Token: 0x06002D05 RID: 11525 RVA: 0x00075C58 File Offset: 0x00073E58
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("")]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alt_itm_tmpl;
			}
			set
			{
				this.alt_itm_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is null.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x00075C61 File Offset: 0x00073E61
		// (set) Token: 0x06002D07 RID: 11527 RVA: 0x00075C69 File Offset: 0x00073E69
		[WebSysDescription("")]
		[TemplateContainer(typeof(RepeaterItem))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footer_tmpl;
			}
			set
			{
				this.footer_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is null.</returns>
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x00075C72 File Offset: 0x00073E72
		// (set) Token: 0x06002D09 RID: 11529 RVA: 0x00075C7A File Offset: 0x00073E7A
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.header_tmpl;
			}
			set
			{
				this.header_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed. The default value is null.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x00075C83 File Offset: 0x00073E83
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x00075C8B File Offset: 0x00073E8B
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.item_tmpl;
			}
			set
			{
				this.item_tmpl = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> interface that defines how the separator between items is displayed.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that defines how the separator between items is displayed. The default is null.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x00075C94 File Offset: 0x00073E94
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x00075C9C File Offset: 0x00073E9C
		[WebSysDescription("")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separator_tmpl;
			}
			set
			{
				this.separator_tmpl = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D0E RID: 11534 RVA: 0x00075CA8 File Offset: 0x00073EA8
		protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
		{
			RepeaterCommandEventHandler repeaterCommandEventHandler = (RepeaterCommandEventHandler)base.Events[Repeater.ItemCommandEvent];
			if (repeaterCommandEventHandler != null)
			{
				repeaterCommandEventHandler(this, e);
			}
		}

		/// <summary>Occurs when a button is clicked in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06002D0F RID: 11535 RVA: 0x00075CD6 File Offset: 0x00073ED6
		// (remove) Token: 0x06002D10 RID: 11536 RVA: 0x00075CE9 File Offset: 0x00073EE9
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event RepeaterCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemCommandEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCreated" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D11 RID: 11537 RVA: 0x00075CFC File Offset: 0x00073EFC
		protected virtual void OnItemCreated(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.ItemCreatedEvent];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		/// <summary>Occurs when an item is created in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06002D12 RID: 11538 RVA: 0x00075D2A File Offset: 0x00073F2A
		// (remove) Token: 0x06002D13 RID: 11539 RVA: 0x00075D3D File Offset: 0x00073F3D
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event RepeaterItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemCreatedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemDataBound" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D14 RID: 11540 RVA: 0x00075D50 File Offset: 0x00073F50
		protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.ItemDataBoundEvent];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		/// <summary>Occurs after an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is data-bound but before it is rendered on the page.</summary>
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06002D15 RID: 11541 RVA: 0x00075D7E File Offset: 0x00073F7E
		// (remove) Token: 0x06002D16 RID: 11542 RVA: 0x00075D91 File Offset: 0x00073F91
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public event RepeaterItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(Repeater.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.ItemDataBoundEvent, value);
			}
		}

		/// <summary>Returns a value indicating whether the control has been initialized.</summary>
		/// <returns>true, if the control has been initialized, otherwise, false.</returns>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x00075DA4 File Offset: 0x00073FA4
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set. </summary>
		/// <returns>true if the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set to a value other than an empty string (""); otherwise, false. </returns>
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x00075DAC File Offset: 0x00073FAC
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length != 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to its specified data source.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to a data source; otherwise, false.</returns>
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x00075DBC File Offset: 0x00073FBC
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x00075DC4 File Offset: 0x00073FC4
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				this.requiresDataBinding = value;
				if (value && this.preRendered && this.IsBoundUsingDataSourceID && this.Page != null && !this.Page.IsCallback)
				{
					this.EnsureDataBound();
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control uses when retrieving data from a data source control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used to retrieve data. The default is the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </returns>
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x00075DFB File Offset: 0x00073FFB
		protected DataSourceSelectArguments SelectArguments
		{
			get
			{
				if (this.selectArguments == null)
				{
					this.selectArguments = this.CreateDataSourceSelectArguments();
				}
				return this.selectArguments;
			}
		}

		/// <summary>Returns the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </summary>
		/// <returns>The <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value.</returns>
		// Token: 0x06002D1C RID: 11548 RVA: 0x00045337 File Offset: 0x00043537
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Verifies that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.Repeater.DataBind" /> method.</summary>
		// Token: 0x06002D1D RID: 11549 RVA: 0x00075E17 File Offset: 0x00074017
		protected void EnsureDataBound()
		{
			if (this.IsBoundUsingDataSourceID && this.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x00075E2F File Offset: 0x0007402F
		private void SelectCallback(IEnumerable data)
		{
			this.data = data;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" /> interface from the data source.</summary>
		/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> that represents the data from the data source.</returns>
		// Token: 0x06002D1F RID: 11551 RVA: 0x00075E38 File Offset: 0x00074038
		protected virtual IEnumerable GetData()
		{
			IEnumerable enumerable;
			if (this.IsBoundUsingDataSourceID)
			{
				if (this.DataSourceID.Length == 0)
				{
					return null;
				}
				if (this.boundDataSource == null)
				{
					return null;
				}
				this.boundDataSource.GetView(string.Empty).Select(this.SelectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
				enumerable = this.data;
				this.data = null;
			}
			else
			{
				enumerable = DataSourceResolver.ResolveDataSource(this.DataSource, this.DataMember);
			}
			return enumerable;
		}

		/// <summary>Determines whether data binding is required.</summary>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="M:System.Web.UI.WebControls.Repeater.OnDataPropertyChanged" /> is called during the data-binding phase of the control.</exception>
		// Token: 0x06002D20 RID: 11552 RVA: 0x00075EB0 File Offset: 0x000740B0
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.Repeater.RequiresDataBinding" /> property to true.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D21 RID: 11553 RVA: 0x00075EC1 File Offset: 0x000740C1
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D22 RID: 11554 RVA: 0x00075ECC File Offset: 0x000740CC
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null)
			{
				page.PreLoad += this.OnPagePreLoad;
				if (!base.IsViewStateEnabled && page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x00075F13 File Offset: 0x00074113
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			this.Initialize();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event and performs other initialization. </summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
		// Token: 0x06002D24 RID: 11556 RVA: 0x00075F1B File Offset: 0x0007411B
		protected internal override void OnLoad(EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
			base.OnLoad(e);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x00075F34 File Offset: 0x00074134
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && this.ViewState["Items"] == null)))
			{
				this.RequiresDataBinding = true;
			}
			if (this.IsBoundUsingDataSourceID)
			{
				this.ConnectToDataSource();
			}
			this.initialized = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object contains the event data. </param>
		// Token: 0x06002D26 RID: 11558 RVA: 0x00075F89 File Offset: 0x00074189
		protected internal override void OnPreRender(EventArgs e)
		{
			this.preRendered = true;
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x00075FA0 File Offset: 0x000741A0
		private void ConnectToDataSource()
		{
			object obj = null;
			if (this.Parent != null)
			{
				obj = this.Parent.FindControl(this.DataSourceID);
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
			this.boundDataSource = (IDataSource)obj;
			this.boundDataSource.GetView(string.Empty).DataSourceViewChanged += this.OnDataSourceViewChanged;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0007602B File Offset: 0x0007422B
		// Note: this type is marked as 'beforefieldinit'.
		static Repeater()
		{
			Repeater.ItemCommandEvent = new object();
			Repeater.ItemCreatedEvent = new object();
			Repeater.ItemDataBoundEvent = new object();
		}

		/// <summary>Gets a value that indicates whether data binding is automatic.</summary>
		/// <returns>true if data binding is automatic; otherwise, false.</returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x0007604C File Offset: 0x0007424C
		protected bool IsDataBindingAutomatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>The name of the model type for strongly typed data binding.</summary>
		/// <returns>The name of the model type.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ItemType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>The name of the method to call in order to read data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string SelectMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when data methods are being called.</summary>
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06002D2F RID: 11567 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06002D30 RID: 11568 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object is being created.</summary>
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06002D31 RID: 11569 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06002D32 RID: 11570 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event CreatingModelDataSourceEventHandler CreatingModelDataSource
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.CreatingModelDataSource" /> event.</summary>
		/// <param name="e">An object that provides access to the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object that is being created. </param>
		// Token: 0x06002D33 RID: 11571 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B61 RID: 7009
		private object dataSource;

		// Token: 0x04001B62 RID: 7010
		private IDataSource boundDataSource;

		// Token: 0x04001B63 RID: 7011
		private bool initialized;

		// Token: 0x04001B64 RID: 7012
		private bool preRendered;

		// Token: 0x04001B65 RID: 7013
		private bool requiresDataBinding;

		// Token: 0x04001B66 RID: 7014
		private DataSourceSelectArguments selectArguments;

		// Token: 0x04001B67 RID: 7015
		private IEnumerable data;

		// Token: 0x04001B68 RID: 7016
		private RepeaterItemCollection itemscol;

		// Token: 0x04001B69 RID: 7017
		private ArrayList items;

		// Token: 0x04001B6A RID: 7018
		private ITemplate alt_itm_tmpl;

		// Token: 0x04001B6B RID: 7019
		private ITemplate footer_tmpl;

		// Token: 0x04001B6C RID: 7020
		private ITemplate header_tmpl;

		// Token: 0x04001B6D RID: 7021
		private ITemplate item_tmpl;

		// Token: 0x04001B6E RID: 7022
		private ITemplate separator_tmpl;
	}
}
