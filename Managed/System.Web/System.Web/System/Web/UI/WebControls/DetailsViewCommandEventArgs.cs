using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemCommand" /> event.</summary>
	// Token: 0x0200029A RID: 666
	public class DetailsViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
		// Token: 0x06001A73 RID: 6771 RVA: 0x00043451 File Offset: 0x00041651
		public DetailsViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>An instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00043461 File Offset: 0x00041661
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished running; otherwise, false.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00043469 File Offset: 0x00041669
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00043471 File Offset: 0x00041671
		public bool Handled { get; set; }

		// Token: 0x040016AF RID: 5807
		private object _commandSource;
	}
}
