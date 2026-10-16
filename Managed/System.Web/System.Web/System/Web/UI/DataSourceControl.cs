using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for controls that represent data sources to data-bound controls.</summary>
	// Token: 0x020001BF RID: 447
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.DataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[NonVisualControl]
	[Bindable(false)]
	public abstract class DataSourceControl : Control, IDataSource, IListSource
	{
		/// <summary>Applies the style properties that are defined in the page style sheet to the control. </summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> containing the control.</param>
		// Token: 0x06001219 RID: 4633 RVA: 0x00003A2B File Offset: 0x00001C2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::System.MonoTODO("Not implemented")]
		public override void ApplyStyleSheetSkin(Page page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x0600121A RID: 4634 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter. </summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x0600121B RID: 4635 RVA: 0x0002FFCC File Offset: 0x0002E1CC
		[global::System.MonoTODO("why override?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to call the <see cref="M:System.Web.UI.DataSourceControl.Focus" /> method.</exception>
		// Token: 0x0600121C RID: 4636 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the named data source view associated with the data source control.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource" />, this parameter is ignored. </param>
		// Token: 0x0600121D RID: 4637
		protected abstract DataSourceView GetView(string viewName);

		/// <summary>Gets the named <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control. Some data source controls support only one view, while others support more than one.</summary>
		/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource" />, this parameter is ignored.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="viewName" /> is null. </exception>
		// Token: 0x0600121E RID: 4638 RVA: 0x0002FFD5 File Offset: 0x0002E1D5
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		/// <summary>Gets a collection of names, representing the list of <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		// Token: 0x0600121F RID: 4639 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual ICollection GetViewNames()
		{
			return null;
		}

		/// <summary>Gets a collection of names, representing the list of <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the <see cref="T:System.Web.UI.DataSourceView" /> objects associated with the <see cref="T:System.Web.UI.DataSourceControl" />.</returns>
		// Token: 0x06001220 RID: 4640 RVA: 0x0002FFDE File Offset: 0x0002E1DE
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		/// <summary>Gets a list of data source controls that can be used as sources of lists of data.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
		// Token: 0x06001221 RID: 4641 RVA: 0x0002FFE6 File Offset: 0x0002E1E6
		IList IListSource.GetList()
		{
			return ListSourceHelper.GetList(this);
		}

		/// <summary>Determines if the server control contains any child controls. </summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x06001222 RID: 4642 RVA: 0x0002FFEE File Offset: 0x0002E1EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001223 RID: 4643 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		protected virtual void RaiseDataSourceChangedEvent(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataSourceControl.dataSourceChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled. </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x06001224 RID: 4644 RVA: 0x00030026 File Offset: 0x0002E226
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		/// <summary>Gets the server control identifier generated by ASP.NET.</summary>
		/// <returns>The server control identifier generated by ASP.NET.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0003002F File Offset: 0x0002E22F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls for a specified server control in the UI hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00030037 File Offset: 0x0002E237
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets a value indicating whether this control supports themes.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.EnableTheming" /> property. </exception>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x00003A0D File Offset: 0x00001C0D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
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

		/// <summary>Gets the skin to apply to the <see cref="T:System.Web.UI.DataSourceControl" /> control.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.SkinID" /> property. </exception>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0003003F File Offset: 0x0002E23F
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00030047 File Offset: 0x0002E247
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		/// <summary>Indicates whether the data source control is associated with one or more lists of data.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.DataSourceControl" /> is associated with one or more <see cref="T:System.Web.UI.DataSourceView" /> objects; otherwise, false.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00030050 File Offset: 0x0002E250
		bool IListSource.ContainsListCollection
		{
			get
			{
				return ListSourceHelper.ContainsListCollection(this);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visually displayed.</summary>
		/// <returns>Always false.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the value of the <see cref="P:System.Web.UI.DataSourceControl.Visible" /> property. </exception>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00003A0D File Offset: 0x00001C0D
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when a data source control has changed in a way that affects data-bound controls. </summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600122E RID: 4654 RVA: 0x00030058 File Offset: 0x0002E258
		// (remove) Token: 0x0600122F RID: 4655 RVA: 0x0003006B File Offset: 0x0002E26B
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataSourceControl.dataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataSourceControl.dataSourceChanged, value);
			}
		}

		// Token: 0x04001416 RID: 5142
		private static object dataSourceChanged = new object();
	}
}
