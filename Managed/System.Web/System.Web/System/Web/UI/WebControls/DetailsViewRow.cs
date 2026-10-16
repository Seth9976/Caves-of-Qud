using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x02000388 RID: 904
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewRow : TableRow
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> class.</summary>
		/// <param name="rowIndex">The index of the row in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x06002372 RID: 9074 RVA: 0x0005A3FF File Offset: 0x000585FF
		public DetailsViewRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = rowIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0005A41C File Offset: 0x0005861C
		public virtual int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x0005A424 File Offset: 0x00058624
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0005A42C File Offset: 0x0005862C
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x0005A434 File Offset: 0x00058634
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x0005A43C File Offset: 0x0005863C
		internal DataControlField ContainingField
		{
			get
			{
				return this.containingField;
			}
			set
			{
				this.containingField = value;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002378 RID: 9080 RVA: 0x0005A448 File Offset: 0x00058648
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				DetailsViewCommandEventArgs e2 = new DetailsViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, e2);
				return true;
			}
			return false;
		}

		// Token: 0x0400197C RID: 6524
		private int rowIndex;

		// Token: 0x0400197D RID: 6525
		private DataControlRowState rowState;

		// Token: 0x0400197E RID: 6526
		private DataControlRowType rowType;

		// Token: 0x0400197F RID: 6527
		private DataControlField containingField;
	}
}
