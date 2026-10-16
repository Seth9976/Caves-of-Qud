using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	// Token: 0x0200039D RID: 925
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormViewRow : TableRow
	{
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x0005E0A2 File Offset: 0x0005C2A2
		// (set) Token: 0x060024E4 RID: 9444 RVA: 0x0005E0AA File Offset: 0x0005C2AA
		internal bool RenderJustCellContents { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> class.</summary>
		/// <param name="itemIndex">The index of the data item in the data source.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x060024E5 RID: 9445 RVA: 0x0005E0B3 File Offset: 0x0005C2B3
		public FormViewRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = itemIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the index of the data item displayed from the data source.</summary>
		/// <returns>The index of the data item displayed from the data source.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
		public virtual int ItemIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0005E0D8 File Offset: 0x0005C2D8
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x0005E0E0 File Offset: 0x0005C2E0
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">The event data.</param>
		// Token: 0x060024E9 RID: 9449 RVA: 0x0005E0E8 File Offset: 0x0005C2E8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				FormViewCommandEventArgs e2 = new FormViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, e2);
				return true;
			}
			return false;
		}

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">The HTML text writer object that receives the control content.</param>
		// Token: 0x060024EA RID: 9450 RVA: 0x0005E124 File Offset: 0x0005C324
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!this.RenderJustCellContents)
			{
				base.Render(writer);
				return;
			}
			foreach (object obj in this.Cells)
			{
				((TableCell)obj).RenderContents(writer);
			}
		}

		// Token: 0x040019E2 RID: 6626
		private int rowIndex;

		// Token: 0x040019E3 RID: 6627
		private DataControlRowState rowState;

		// Token: 0x040019E4 RID: 6628
		private DataControlRowType rowType;
	}
}
