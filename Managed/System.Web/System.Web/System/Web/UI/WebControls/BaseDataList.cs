using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for data listing controls, such as <see cref="T:System.Web.UI.WebControls.DataList" /> and <see cref="T:System.Web.UI.WebControls.DataGrid" />. This class provides the methods and properties common to all data listing controls.</summary>
	// Token: 0x02000332 RID: 818
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("DataSource")]
	[Designer("System.Web.UI.Design.WebControls.BaseDataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseDataList : WebControl
	{
		/// <summary>Gets or sets the text to render in an HTML caption element in the control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in the control. The default value is an empty string ("").</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00044F8F File Offset: 0x0004318F
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x00044FA6 File Offset: 0x000431A6
		[DefaultValue("")]
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				return this.ViewState.GetString("Caption", string.Empty);
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("Caption");
					return;
				}
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00044FCD File Offset: 0x000431CD
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x00044FE0 File Offset: 0x000431E0
		[DefaultValue(TableCaptionAlign.NotSet)]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				return (TableCaptionAlign)this.ViewState.GetInt("CaptionAlign", 0);
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid TableCaptionAlign value."));
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00045010 File Offset: 0x00043210
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x00045027 File Offset: 0x00043227
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space (in pixels) between cells. The default value is 0.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00045035 File Offset: 0x00043235
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x0004504C File Offset: 0x0004324C
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains a collection of child controls in a data listing control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains a collection of child controls in a data listing control.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0004505A File Offset: 0x0004325A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets or sets the key field in the data source specified by the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property.</summary>
		/// <returns>The name of the key field in the data source specified by <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" />.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00045068 File Offset: 0x00043268
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0004507F File Offset: 0x0004327F
		[DefaultValue("")]
		[Themeable(false)]
		[global::System.MonoTODO("incomplete")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataKeyField
		{
			get
			{
				return this.ViewState.GetString("DataKeyField", string.Empty);
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DataKeyField");
					return;
				}
				this.ViewState["DataKeyField"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> object that stores the key values of each record in a data listing control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> that stores the key values of each record in a data listing control.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000450A6 File Offset: 0x000432A6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Data")]
		[WebSysDescription("")]
		public DataKeyCollection DataKeys
		{
			get
			{
				if (this.keycoll == null)
				{
					this.keycoll = new DataKeyCollection(this.DataKeysArray);
				}
				return this.keycoll;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> object that contains the key values of each record in a data listing control.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the key values of each record in a data listing control.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000450C8 File Offset: 0x000432C8
		protected ArrayList DataKeysArray
		{
			get
			{
				ArrayList arrayList = (ArrayList)this.ViewState["DataKeys"];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					this.ViewState["DataKeys"] = arrayList;
				}
				return arrayList;
			}
		}

		/// <summary>Gets or sets the specific data member in a multimember data source to bind to a data listing control.</summary>
		/// <returns>A data member from a multimember data source. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00045106 File Offset: 0x00043306
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0004511D File Offset: 0x0004331D
		[DefaultValue("")]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", string.Empty);
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
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets or sets the source containing a list of values used to populate the items within the control.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to this control. The default value is null.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">The data source is of an invalid type. The data source must be null or implement either the <see cref="T:System.Collections.IEnumerable" /> or the <see cref="T:System.ComponentModel.IListSource" /> interface.</exception>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0004514B File Offset: 0x0004334B
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x00045154 File Offset: 0x00043354
		[Bindable(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual object DataSource
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null || value is IEnumerable || value is IListSource)
				{
					this.source = value;
					this.OnDataPropertyChanged();
					return;
				}
				throw new ArgumentException(global::Locale.GetText("Invalid data source. This requires an object implementing {0} or {1}.", new object[] { "IEnumerable", "IListSource" }));
			}
		}

		/// <summary>Gets or sets a value that specifies whether the border between the cells of a data listing control is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default value is Both.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000451A7 File Offset: 0x000433A7
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x000451BE File Offset: 0x000433BE
		[DefaultValue(GridLines.Both)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.Both;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of a data listing control within its container.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is NotSet.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000451CC File Offset: 0x000433CC
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x000451E3 File Offset: 0x000433E3
		[DefaultValue(HorizontalAlign.NotSet)]
		[Category("Layout")]
		[WebSysDescription("")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return this.TableStyle.HorizontalAlign;
			}
			set
			{
				this.TableStyle.HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data listing control renders its header in an accessible format. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>true if the control renders its header in an accessible format; otherwise, false. The default is false.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000451F1 File Offset: 0x000433F1
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x00045204 File Offset: 0x00043404
		[DefaultValue(false)]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				return this.ViewState.GetBool("UseAccessibleHeader", false);
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the data listing control should use to retrieve its data source.</summary>
		/// <returns>The programmatic identifier assigned to the data source control.</returns>
		/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00044D9A File Offset: 0x00042F9A
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0004521C File Offset: 0x0004341C
		[IDReferenceProperty(typeof(DataSourceControl))]
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string DataSourceID
		{
			get
			{
				return this.ViewState.GetString("DataSourceID", string.Empty);
			}
			set
			{
				if (this.source != null)
				{
					throw new InvalidOperationException(global::Locale.GetText("DataSource is already set."));
				}
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets a value indicating whether the control has been initialized.</summary>
		/// <returns>true if the control has been initialized; otherwise, false.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0004524D File Offset: 0x0004344D
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property is set.</summary>
		/// <returns>true if <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> is set to a value other than <see cref="F:System.String.Empty" />; otherwise, false.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00045255 File Offset: 0x00043455
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length != 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data listing control needs to bind to its specified data source.</summary>
		/// <returns>true if the control needs to bind to a data source; otherwise, false.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x00045265 File Offset: 0x00043465
		// (set) Token: 0x06001C8C RID: 7308 RVA: 0x0004526D File Offset: 0x0004346D
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

		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used by the data-bound control to retrieve data. The default is to return the value from <see cref="M:System.Web.UI.WebControls.BaseDataList.CreateDataSourceSelectArguments" />.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00045276 File Offset: 0x00043476
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

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x00045292 File Offset: 0x00043492
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x06001C90 RID: 7312 RVA: 0x00003946 File Offset: 0x00001B46
		protected override void AddParsedSubObject(object obj)
		{
		}

		/// <summary>Creates a child control using the view state.</summary>
		// Token: 0x06001C91 RID: 7313 RVA: 0x0004529F File Offset: 0x0004349F
		protected internal override void CreateChildControls()
		{
			if (this.HasControls())
			{
				base.Controls.Clear();
			}
			if (this.IsDataBound)
			{
				this.CreateControlHierarchy(false);
				return;
			}
			if (this.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
		}

		/// <summary>When overridden in a derived class, creates the control hierarchy for the data listing control with or without the specified data source.</summary>
		/// <param name="useDataSource">true to use the control's data source; otherwise, false.</param>
		// Token: 0x06001C92 RID: 7314
		protected abstract void CreateControlHierarchy(bool useDataSource);

		/// <summary>Binds the control and all its child controls to the specified data source.</summary>
		// Token: 0x06001C93 RID: 7315 RVA: 0x000452D4 File Offset: 0x000434D4
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			if (this.HasControls())
			{
				this.Controls.Clear();
			}
			if (base.HasChildViewState)
			{
				base.ClearChildViewState();
			}
			if (!base.IsTrackingViewState)
			{
				this.TrackViewState();
			}
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
			this.RequiresDataBinding = false;
			this.IsDataBound = true;
		}

		/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />.</returns>
		// Token: 0x06001C94 RID: 7316 RVA: 0x00045337 File Offset: 0x00043537
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		/// <summary>Verifies that the data listing control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.BaseDataList.DataBind" /> method.</summary>
		// Token: 0x06001C95 RID: 7317 RVA: 0x0004533E File Offset: 0x0004353E
		protected void EnsureDataBound()
		{
			if (this.IsBoundUsingDataSourceID && this.RequiresDataBinding)
			{
				this.DataBind();
			}
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00045356 File Offset: 0x00043556
		private void SelectCallback(IEnumerable data)
		{
			this.data = data;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</returns>
		// Token: 0x06001C97 RID: 7319 RVA: 0x00045360 File Offset: 0x00043560
		protected virtual IEnumerable GetData()
		{
			if (this.DataSourceID.Length == 0)
			{
				return null;
			}
			if (this.boundDataSource == null)
			{
				this.ConnectToDataSource();
			}
			this.boundDataSource.GetView(string.Empty).Select(this.SelectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
			return this.data;
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00041EA1 File Offset: 0x000400A1
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x00041EB4 File Offset: 0x000400B4
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

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001C9A RID: 7322 RVA: 0x000453B7 File Offset: 0x000435B7
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
		}

		/// <summary>Called when one of the base data source identification properties is changed, to rebind the data-bound control to its data.</summary>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
		// Token: 0x06001C9B RID: 7323 RVA: 0x000453C0 File Offset: 0x000435C0
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001C9C RID: 7324 RVA: 0x000453D1 File Offset: 0x000435D1
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001C9D RID: 7325 RVA: 0x000453DC File Offset: 0x000435DC
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

		// Token: 0x06001C9E RID: 7326 RVA: 0x00045423 File Offset: 0x00043623
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001C9F RID: 7327 RVA: 0x00045433 File Offset: 0x00043633
		protected internal override void OnLoad(EventArgs e)
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
			base.OnLoad(e);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0004544C File Offset: 0x0004364C
		private void Initialize()
		{
			Page page = this.Page;
			if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && !this.IsDataBound)))
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
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x00045497 File Offset: 0x00043697
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataList.SelectedIndexChanged" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x000454A8 File Offset: 0x000436A8
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BaseDataList.selectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Sets up the control hierarchy for the data-bound control.</summary>
		// Token: 0x06001CA3 RID: 7331
		protected abstract void PrepareControlHierarchy();

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000454D6 File Offset: 0x000436D6
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			this.RenderContents(writer);
		}

		/// <summary>Occurs when a different item is selected in a data listing control between posts to the server.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001CA5 RID: 7333 RVA: 0x000454E5 File Offset: 0x000436E5
		// (remove) Token: 0x06001CA6 RID: 7334 RVA: 0x000454F8 File Offset: 0x000436F8
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(BaseDataList.selectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BaseDataList.selectedIndexChangedEvent, value);
			}
		}

		/// <summary>Determines whether the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
		/// <returns>true if the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that contains the data type to test. </param>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x0004550C File Offset: 0x0004370C
		public static bool IsBindableType(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			return typeCode - TypeCode.Boolean <= 13 || typeCode == TypeCode.String;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00045540 File Offset: 0x00043740
		private void ConnectToDataSource()
		{
			if (this.NamingContainer != null)
			{
				this.boundDataSource = this.NamingContainer.FindControl(this.DataSourceID) as IDataSource;
			}
			if (this.boundDataSource == null)
			{
				if (this.Parent != null)
				{
					this.boundDataSource = this.Parent.FindControl(this.DataSourceID) as IDataSource;
				}
				if (this.boundDataSource == null)
				{
					throw new HttpException(global::Locale.GetText("Coulnd't find a DataSource named '{0}'.", new object[] { this.DataSourceID }));
				}
			}
			this.boundDataSource.GetView(string.Empty).DataSourceViewChanged += this.OnDataSourceViewChanged;
		}

		// Token: 0x040017F1 RID: 6129
		private static readonly object selectedIndexChangedEvent = new object();

		// Token: 0x040017F2 RID: 6130
		private DataKeyCollection keycoll;

		// Token: 0x040017F3 RID: 6131
		private object source;

		// Token: 0x040017F4 RID: 6132
		private IDataSource boundDataSource;

		// Token: 0x040017F5 RID: 6133
		private bool initialized;

		// Token: 0x040017F6 RID: 6134
		private bool requiresDataBinding;

		// Token: 0x040017F7 RID: 6135
		private DataSourceSelectArguments selectArguments;

		// Token: 0x040017F8 RID: 6136
		private IEnumerable data;
	}
}
