using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.Adapters;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in list or tabular form.</summary>
	// Token: 0x02000366 RID: 870
	[Designer("System.Web.UI.Design.WebControls.DataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataBoundControl : BaseDataBoundControl
	{
		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06002094 RID: 8340 RVA: 0x00051C22 File Offset: 0x0004FE22
		protected DataBoundControl()
		{
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00051C2A File Offset: 0x0004FE2A
		internal DataBoundControl(HtmlTextWriterTag tag)
			: base(tag)
		{
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.IDataSource" /> interface that the data-bound control is associated with, if any.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IDataSource" /> that represents the data source identified by <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" />. </returns>
		/// <exception cref="T:System.Web.HttpException">The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IDataSource" /> interface.</exception>
		// Token: 0x06002096 RID: 8342 RVA: 0x00051C34 File Offset: 0x0004FE34
		protected virtual IDataSource GetDataSource()
		{
			if (base.IsBoundUsingDataSourceID)
			{
				Control control = base.FindDataSource();
				if (control == null)
				{
					throw new HttpException(string.Format("A control with ID '{0}' could not be found.", this.DataSourceID));
				}
				if (!(control is IDataSource))
				{
					throw new HttpException(string.Format("The control with ID '{0}' is not a control of type IDataSource.", this.DataSourceID));
				}
				return (IDataSource)control;
			}
			else
			{
				IDataSource dataSource = this.DataSource as IDataSource;
				if (dataSource != null)
				{
					return dataSource;
				}
				return new CollectionDataSource(DataSourceResolver.ResolveDataSource(this.DataSource, this.DataMember));
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.DataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
		/// <returns>The <see cref="T:System.Web.UI.DataSourceView" /> that the data-bound control uses to perform data operations. If the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set, a specific, named <see cref="T:System.Web.UI.DataSourceView" /> is returned; otherwise, the default <see cref="T:System.Web.UI.DataSourceView" /> is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Both the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> and <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> properties are set.- or -The <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set but a <see cref="T:System.Web.UI.DataSourceView" /> object by that name does not exist.</exception>
		// Token: 0x06002097 RID: 8343 RVA: 0x00051CB3 File Offset: 0x0004FEB3
		protected virtual DataSourceView GetData()
		{
			if (this.currentView == null)
			{
				this.UpdateViewData();
			}
			return this.currentView;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00051CCC File Offset: 0x0004FECC
		private DataSourceView InternalGetData()
		{
			if (this.currentView != null)
			{
				return this.currentView;
			}
			if (this.DataSource != null && base.IsBoundUsingDataSourceID)
			{
				throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
			}
			IDataSource dataSource = this.GetDataSource();
			if (dataSource != null)
			{
				return dataSource.GetView(this.DataMember);
			}
			return null;
		}

		/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
		// Token: 0x06002099 RID: 8345 RVA: 0x00051D1B File Offset: 0x0004FF1B
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this.currentView = null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.DataSourceView" />.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600209A RID: 8346 RVA: 0x00051D2A File Offset: 0x0004FF2A
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600209B RID: 8347 RVA: 0x00051D33 File Offset: 0x0004FF33
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			this.Initialize();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00051D44 File Offset: 0x0004FF44
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && !this.IsDataBound)
			{
				if (!page.IsPostBack)
				{
					base.RequiresDataBinding = true;
					return;
				}
				if (base.IsViewStateEnabled)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00051D84 File Offset: 0x0004FF84
		private void UpdateViewData()
		{
			if (this.currentView != null)
			{
				this.currentView.DataSourceViewChanged -= this.OnDataSourceViewChanged;
			}
			DataSourceView dataSourceView = this.InternalGetData();
			if (dataSourceView != this.currentView)
			{
				this.currentView = dataSourceView;
			}
			if (this.currentView != null)
			{
				this.currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x0600209E RID: 8350 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		protected internal override void OnLoad(EventArgs e)
		{
			this.UpdateViewData();
			if (!base.Initialized)
			{
				this.Initialize();
				base.ConfirmInitState();
			}
			base.OnLoad(e);
		}

		/// <summary>When overridden in a derived class, binds data from the data source to the control. </summary>
		/// <param name="data">The <see cref="T:System.Collections.IEnumerable" /> list of data returned from a <see cref="M:System.Web.UI.WebControls.DataBoundControl.PerformSelect" /> method call.</param>
		// Token: 0x0600209F RID: 8351 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
		}

		/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">The object passed by the <paramref name="dataSource" /> parameter is not null or a recognized type.</exception>
		// Token: 0x060020A0 RID: 8352 RVA: 0x00051E0B File Offset: 0x0005000B
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
			{
				return;
			}
			throw new ArgumentException("Invalid data source source type. The data source must be of type IListSource, IEnumerable or IDataSource.");
		}

		/// <summary>Gets or sets the name of the list of data that the data-bound control binds to, in cases where the data source contains more than one distinct list of data items.</summary>
		/// <returns>The name of the specific list of data that the data-bound control binds to, if more than one list is supplied by a data source control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00045106 File Offset: 0x00043306
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x00051E33 File Offset: 0x00050033
		[WebCategory("Data")]
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string DataMember
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

		/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x00044D9A File Offset: 0x00042F9A
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x00051E46 File Offset: 0x00050046
		[IDReferenceProperty(typeof(DataSourceControl))]
		public override string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", string.Empty);
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				base.DataSourceID = value;
			}
		}

		/// <summary>Gets an object that implements the <see cref="T:System.Web.UI.IDataSource" /> interface, which provides access to the object's data content.</summary>
		/// <returns>An object with access to its data content.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x00051E60 File Offset: 0x00050060
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IDataSource DataSourceObject
		{
			get
			{
				return this.GetDataSource();
			}
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x060020A6 RID: 8358 RVA: 0x00051E68 File Offset: 0x00050068
		protected override void PerformSelect()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			base.RequiresDataBinding = false;
			this.SelectArguments = this.CreateDataSourceSelectArguments();
			this.GetData().Select(this.SelectArguments, new DataSourceViewSelectCallback(this.OnSelect));
			this.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00051EC9 File Offset: 0x000500C9
		private void OnSelect(IEnumerable data)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			this.InternalPerformDataBinding(data);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00051EE8 File Offset: 0x000500E8
		internal void InternalPerformDataBinding(IEnumerable data)
		{
			DataBoundControlAdapter dataBoundControlAdapter = base.Adapter as DataBoundControlAdapter;
			if (dataBoundControlAdapter != null)
			{
				dataBoundControlAdapter.PerformDataBinding(data);
				return;
			}
			this.PerformDataBinding(data);
		}

		/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
		// Token: 0x060020A9 RID: 8361 RVA: 0x00045337 File Offset: 0x00043537
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> value used by the data-bound control to retrieve data. The default is <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00051F13 File Offset: 0x00050113
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x00051F2F File Offset: 0x0005012F
		private protected DataSourceSelectArguments SelectArguments
		{
			protected get
			{
				if (this.selectArguments == null)
				{
					this.selectArguments = this.CreateDataSourceSelectArguments();
				}
				return this.selectArguments;
			}
			private set
			{
				this.selectArguments = value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00051F38 File Offset: 0x00050138
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x00051F61 File Offset: 0x00050161
		private bool IsDataBound
		{
			get
			{
				object obj = this.ViewState["DataBound"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
		// Token: 0x060020AE RID: 8366 RVA: 0x00051F79 File Offset: 0x00050179
		protected void MarkAsDataBound()
		{
			this.IsDataBound = true;
		}

		/// <summary>Gets a value that indicates whether model binding is in use.</summary>
		/// <returns>true if model binding is in use; otherwise, false.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x00051F84 File Offset: 0x00050184
		protected override bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the name of the data item type for strongly typed data binding.</summary>
		/// <returns>The name of the model type.</returns>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x0000B3D8 File Offset: 0x000095D8
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
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x0000B3D8 File Offset: 0x000095D8
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
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060020B4 RID: 8372 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x060020B5 RID: 8373 RVA: 0x0000B3D8 File Offset: 0x000095D8
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
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060020B6 RID: 8374 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x060020B7 RID: 8375 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataBoundControl.CreatingModelDataSource" /> event.</summary>
		/// <param name="e">An object that provides access to the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> object that is being created.</param>
		// Token: 0x060020B8 RID: 8376 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018A6 RID: 6310
		private DataSourceSelectArguments selectArguments;

		// Token: 0x040018A7 RID: 6311
		private DataSourceView currentView;
	}
}
