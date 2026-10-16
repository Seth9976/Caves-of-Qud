using System;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for controls that bind to data using an ASP.NET data source control.</summary>
	// Token: 0x02000331 RID: 817
	[Designer("System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("DataSourceID")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BaseDataBoundControl : WebControl
	{
		/// <summary>Occurs after the server control binds to a data source.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001C54 RID: 7252 RVA: 0x00044D2C File Offset: 0x00042F2C
		// (remove) Token: 0x06001C55 RID: 7253 RVA: 0x00044D3F File Offset: 0x00042F3F
		public event EventHandler DataBound
		{
			add
			{
				this.events.AddHandler(BaseDataBoundControl.dataBoundEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(BaseDataBoundControl.dataBoundEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
		// Token: 0x06001C56 RID: 7254 RVA: 0x00044D52 File Offset: 0x00042F52
		protected BaseDataBoundControl()
		{
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00044D65 File Offset: 0x00042F65
		internal BaseDataBoundControl(HtmlTextWriterTag tag)
			: base(tag)
		{
		}

		/// <summary>Gets or sets the object from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>An object that represents the data source from which the data-bound control retrieves its data. The default is null.</returns>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00044D79 File Offset: 0x00042F79
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x00044D81 File Offset: 0x00042F81
		[Themeable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Bindable(true)]
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null)
				{
					this.ValidateDataSource(value);
				}
				this.dataSource = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00044D9A File Offset: 0x00042F9A
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00044DB1 File Offset: 0x00042FB1
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
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		/// <summary>Gets a value indicating whether the data-bound control has been initialized.</summary>
		/// <returns>true if the data-bound control has been initialized; otherwise, false.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00044DCA File Offset: 0x00042FCA
		protected bool Initialized
		{
			get
			{
				return this.initialized;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set.</summary>
		/// <returns>The value true is returned if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set to value other than <see cref="F:System.String.Empty" />; otherwise, the value is false.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00044DD2 File Offset: 0x00042FD2
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called. </summary>
		/// <returns>The returned value is true if the data-bound control's <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called before the control is rendered; otherwise, the value is false.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00044DE2 File Offset: 0x00042FE2
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x00044DEA File Offset: 0x00042FEA
		protected bool RequiresDataBinding
		{
			get
			{
				return this.requiresDataBinding;
			}
			set
			{
				if (value && this.preRendered && this.IsBoundUsingDataSourceID && this.Page != null && !this.Page.IsCallback)
				{
					this.requiresDataBinding = true;
					this.EnsureDataBound();
					return;
				}
				this.requiresDataBinding = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control.</summary>
		// Token: 0x06001C61 RID: 7265 RVA: 0x00044E31 File Offset: 0x00043031
		protected void ConfirmInitState()
		{
			this.initialized = true;
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
		// Token: 0x06001C62 RID: 7266 RVA: 0x00044E3A File Offset: 0x0004303A
		public override void DataBind()
		{
			this.PerformSelect();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set and the data-bound control is marked to require binding.</summary>
		// Token: 0x06001C63 RID: 7267 RVA: 0x00044E42 File Offset: 0x00043042
		protected virtual void EnsureDataBound()
		{
			if (this.RequiresDataBinding && this.IsBoundUsingDataSourceID)
			{
				this.DataBind();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataBoundControl.DataBound" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001C64 RID: 7268 RVA: 0x00044E5C File Offset: 0x0004305C
		protected virtual void OnDataBound(EventArgs e)
		{
			EventHandler eventHandler = this.events[BaseDataBoundControl.dataBoundEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
		// Token: 0x06001C65 RID: 7269 RVA: 0x00044E8A File Offset: 0x0004308A
		protected virtual void OnDataPropertyChanged()
		{
			if (this.Initialized)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001C66 RID: 7270 RVA: 0x00044E9C File Offset: 0x0004309C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.PreLoad += this.OnPagePreLoad;
			if (!base.IsViewStateEnabled && this.Page != null && this.Page.IsPostBack)
			{
				this.RequiresDataBinding = true;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001C67 RID: 7271 RVA: 0x00044EEC File Offset: 0x000430EC
		protected virtual void OnPagePreLoad(object sender, EventArgs e)
		{
			this.ConfirmInitState();
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06001C68 RID: 7272 RVA: 0x00044EF4 File Offset: 0x000430F4
		protected internal override void OnPreRender(EventArgs e)
		{
			this.preRendered = true;
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00044F0C File Offset: 0x0004310C
		internal Control FindDataSource()
		{
			Control control = this.NamingContainer;
			string dataSourceID = this.DataSourceID;
			while (control != null)
			{
				Control control2 = control.FindControl(dataSourceID);
				if (control2 != null)
				{
					return control2;
				}
				control = control.NamingContainer;
			}
			return null;
		}

		/// <summary>When overridden in a derived class, controls how data is retrieved and bound to the control.</summary>
		// Token: 0x06001C6A RID: 7274
		protected abstract void PerformSelect();

		/// <summary>When overridden in a derived class, verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">The object to verify. Typically an instance of <see cref="T:System.Collections.IEnumerable" />, <see cref="T:System.ComponentModel.IListSource" />, <see cref="T:System.Web.UI.IDataSource" />, or <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</param>
		// Token: 0x06001C6B RID: 7275
		protected abstract void ValidateDataSource(object dataSource);

		/// <summary>Gets a value that indicates whether data binding is automatic.</summary>
		/// <returns>true if data binding is automatic; otherwise, false.</returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00044F50 File Offset: 0x00043150
		protected internal bool IsDataBindingAutomatic
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the control is using model binders.</summary>
		/// <returns>true if the control is using model binders; otherwise, false. The default is false.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00044F6C File Offset: 0x0004316C
		protected virtual bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x040017EB RID: 6123
		private static readonly object dataBoundEvent = new object();

		// Token: 0x040017EC RID: 6124
		private EventHandlerList events = new EventHandlerList();

		// Token: 0x040017ED RID: 6125
		private object dataSource;

		// Token: 0x040017EE RID: 6126
		private bool initialized;

		// Token: 0x040017EF RID: 6127
		private bool preRendered;

		// Token: 0x040017F0 RID: 6128
		private bool requiresDataBinding;
	}
}
