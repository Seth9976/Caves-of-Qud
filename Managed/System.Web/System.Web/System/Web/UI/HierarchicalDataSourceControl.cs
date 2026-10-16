using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Provides a base class for data source controls that represent hierarchical data.</summary>
	// Token: 0x020001CE RID: 462
	[Bindable(false)]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.HierarchicalDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[NonVisualControl]
	public abstract class HierarchicalDataSourceControl : Control, IHierarchicalDataSource
	{
		/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
		// Token: 0x060012AD RID: 4781
		protected abstract HierarchicalDataSourceView GetHierarchicalView(string viewPath);

		/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
		/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
		// Token: 0x060012AE RID: 4782 RVA: 0x00030B72 File Offset: 0x0002ED72
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			return this.GetHierarchicalView(viewPath);
		}

		/// <summary>Gets a value indicating whether this control supports themes.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.EnableTheming" /> property. </exception>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.SkinID" /> property. </exception>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00003A0D File Offset: 0x00001C0D
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visually displayed.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.HierarchicalDataSourceControl.Visible" /> property. </exception>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.EmptyControlCollection" /> that prevents any child controls from being added.</returns>
		// Token: 0x060012B5 RID: 4789 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x060012B6 RID: 4790 RVA: 0x00030B7B File Offset: 0x0002ED7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			if (id == this.ID)
			{
				return this;
			}
			return null;
		}

		/// <summary>Determines if the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x060012B7 RID: 4791 RVA: 0x00008A4D File Offset: 0x00006C4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return false;
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to call the <see cref="M:System.Web.UI.HierarchicalDataSourceControl.Focus" /> method.</exception>
		// Token: 0x060012B8 RID: 4792 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> has changed in some way that affects data-bound controls.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060012B9 RID: 4793 RVA: 0x00030B8E File Offset: 0x0002ED8E
		// (remove) Token: 0x060012BA RID: 4794 RVA: 0x00030BA1 File Offset: 0x0002EDA1
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(HierarchicalDataSourceControl.dataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(HierarchicalDataSourceControl.dataSourceChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.IHierarchicalDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060012BB RID: 4795 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[HierarchicalDataSourceControl.dataSourceChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="writer">The HTML text writer.</param>
		// Token: 0x060012BC RID: 4796 RVA: 0x00003946 File Offset: 0x00001B46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
		}

		// Token: 0x04001434 RID: 5172
		private static object dataSourceChanged = new object();
	}
}
