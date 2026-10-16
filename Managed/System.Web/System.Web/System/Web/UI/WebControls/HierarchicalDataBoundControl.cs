using System;
using System.ComponentModel;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in hierarchical form.</summary>
	// Token: 0x020003A8 RID: 936
	[Designer("System.Web.UI.Design.WebControls.HierarchicalDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class HierarchicalDataBoundControl : BaseDataBoundControl
	{
		/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00061FC0 File Offset: 0x000601C0
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x00061FED File Offset: 0x000601ED
		[IDReferenceProperty(typeof(HierarchicalDataSourceControl))]
		public override string DataSourceID
		{
			get
			{
				object obj = this.ViewState["DataSourceID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
				this.ViewState["DataSourceID"] = value;
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations. </returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> could not be retrieved for the specified <paramref name="viewPath" />.</exception>
		// Token: 0x0600262B RID: 9771 RVA: 0x00062010 File Offset: 0x00060210
		protected virtual HierarchicalDataSourceView GetData(string viewPath)
		{
			if (this.DataSource != null && !string.IsNullOrEmpty(this.DataSourceID))
			{
				throw new HttpException();
			}
			IHierarchicalDataSource dataSource = this.GetDataSource();
			if (dataSource != null)
			{
				return dataSource.GetHierarchicalView(viewPath);
			}
			if (this.DataSource is IHierarchicalEnumerable)
			{
				return new ReadOnlyDataSourceView((IHierarchicalEnumerable)this.DataSource);
			}
			return null;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> that the data-bound control is associated with, if any.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that represents the data source identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property. </returns>
		/// <exception cref="T:System.Web.HttpException">The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
		// Token: 0x0600262C RID: 9772 RVA: 0x0006206C File Offset: 0x0006026C
		protected virtual IHierarchicalDataSource GetDataSource()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				return this.DataSource as IHierarchicalDataSource;
			}
			Control control = base.FindDataSource();
			if (control == null)
			{
				throw new HttpException(string.Format("A control with ID '{0}' could not be found.", this.DataSourceID));
			}
			if (!(control is IHierarchicalDataSource))
			{
				throw new HttpException(string.Format("The control with ID '{0}' is not a control of type IHierarchicalDataSource.", this.DataSourceID));
			}
			return (IHierarchicalDataSource)control;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000620CF File Offset: 0x000602CF
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x00051F61 File Offset: 0x00050161
		private bool IsDataBound
		{
			get
			{
				return this.ViewState.GetBool("DataBound", false);
			}
			set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
		// Token: 0x0600262F RID: 9775 RVA: 0x000620E2 File Offset: 0x000602E2
		protected void MarkAsDataBound()
		{
			this.IsDataBound = true;
		}

		/// <summary>Called when one of the base data source identification properties is changed, to re-bind the data-bound control to its data.</summary>
		// Token: 0x06002630 RID: 9776 RVA: 0x00051D2A File Offset: 0x0004FF2A
		protected override void OnDataPropertyChanged()
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Called when the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that the data-bound control works with raises the <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> object. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06002631 RID: 9777 RVA: 0x00051D2A File Offset: 0x0004FF2A
		protected virtual void OnDataSourceChanged(object sender, EventArgs e)
		{
			base.RequiresDataBinding = true;
		}

		/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
		// Token: 0x06002632 RID: 9778 RVA: 0x000620EB File Offset: 0x000602EB
		protected internal override void OnLoad(EventArgs e)
		{
			if (!base.Initialized)
			{
				this.Initialize();
				base.ConfirmInitState();
			}
			base.OnLoad(e);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00062108 File Offset: 0x00060308
		private void Initialize()
		{
			if (!this.Page.IsPostBack || (base.IsViewStateEnabled && !this.IsDataBound))
			{
				base.RequiresDataBinding = true;
			}
			IHierarchicalDataSource dataSource = this.GetDataSource();
			if (dataSource != null && this.DataSourceID != "")
			{
				dataSource.DataSourceChanged += this.OnDataSourceChanged;
			}
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002634 RID: 9780 RVA: 0x00062168 File Offset: 0x00060368
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			this.Initialize();
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00062178 File Offset: 0x00060378
		protected void InternalPerformDataBinding()
		{
			HierarchicalDataBoundControlAdapter hierarchicalDataBoundControlAdapter = base.Adapter as HierarchicalDataBoundControlAdapter;
			if (hierarchicalDataBoundControlAdapter != null)
			{
				hierarchicalDataBoundControlAdapter.PerformDataBinding();
				return;
			}
			this.PerformDataBinding();
		}

		/// <summary>When overridden in a derived class, binds data from the data source to the control.</summary>
		// Token: 0x06002636 RID: 9782 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void PerformDataBinding()
		{
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x06002637 RID: 9783 RVA: 0x000621A1 File Offset: 0x000603A1
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.InternalPerformDataBinding();
			base.RequiresDataBinding = false;
			this.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
		/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="dataSource" /> is not null and implements neither the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> nor the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
		// Token: 0x06002638 RID: 9784 RVA: 0x000621CC File Offset: 0x000603CC
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IHierarchicalDataSource || dataSource is IHierarchicalEnumerable)
			{
				return;
			}
			throw new InvalidOperationException("Invalid data source");
		}
	}
}
