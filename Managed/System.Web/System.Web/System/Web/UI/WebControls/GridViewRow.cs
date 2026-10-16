using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an individual row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	// Token: 0x020003A3 RID: 931
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class GridViewRow : TableRow, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> class.</summary>
		/// <param name="rowIndex">The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</param>
		/// <param name="dataItemIndex">The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x060025EF RID: 9711 RVA: 0x00061C5A File Offset: 0x0005FE5A
		public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = rowIndex;
			this.dataItemIndex = dataItemIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound. </returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x00061C7F File Offset: 0x0005FE7F
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x00061C87 File Offset: 0x0005FE87
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		/// <summary>Gets the index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying data source.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x00061C90 File Offset: 0x0005FE90
		public virtual int DataItemIndex
		{
			get
			{
				return this.dataItemIndex;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x00061C98 File Offset: 0x0005FE98
		public virtual int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x00061CA0 File Offset: 0x0005FEA0
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x00061CA8 File Offset: 0x0005FEA8
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
			set
			{
				this.rowState = value;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x00061CB1 File Offset: 0x0005FEB1
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x00061CB9 File Offset: 0x0005FEB9
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
			set
			{
				this.rowType = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" />.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x00061CC2 File Offset: 0x0005FEC2
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItemIndex" />.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x00061CCA File Offset: 0x0005FECA
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.RowIndex" />.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00061CD2 File Offset: 0x0005FED2
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.RowIndex;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060025FB RID: 9723 RVA: 0x00061CDC File Offset: 0x0005FEDC
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				GridViewCommandEventArgs e2 = new GridViewCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, e2);
				return true;
			}
			return false;
		}

		// Token: 0x04001A35 RID: 6709
		private object dataItem;

		// Token: 0x04001A36 RID: 6710
		private int rowIndex;

		// Token: 0x04001A37 RID: 6711
		private int dataItemIndex;

		// Token: 0x04001A38 RID: 6712
		private DataControlRowState rowState;

		// Token: 0x04001A39 RID: 6713
		private DataControlRowType rowType;
	}
}
