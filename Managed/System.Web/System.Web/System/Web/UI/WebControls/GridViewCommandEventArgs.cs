using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCommand" /> event.</summary>
	// Token: 0x020002B8 RID: 696
	public class GridViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified row, source of the command, and event arguments.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row containing the button.</param>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
		// Token: 0x06001AD3 RID: 6867 RVA: 0x00043531 File Offset: 0x00041731
		public GridViewCommandEventArgs(GridViewRow row, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._row = row;
			this._commandSource = commandSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified source of the command and event arguments.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
		// Token: 0x06001AD4 RID: 6868 RVA: 0x00043548 File Offset: 0x00041748
		public GridViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>A instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00043558 File Offset: 0x00041758
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished; otherwise, false.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00043560 File Offset: 0x00041760
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00043568 File Offset: 0x00041768
		public bool Handled { get; set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00043571 File Offset: 0x00041771
		internal GridViewRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x040016DA RID: 5850
		private GridViewRow _row;

		// Token: 0x040016DB RID: 5851
		private object _commandSource;
	}
}
