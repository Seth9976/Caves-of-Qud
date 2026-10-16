using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> events of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000294 RID: 660
	public class DataListCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> class.</summary>
		/// <param name="item">The selected item from the <see cref="T:System.Web.UI.WebControls.DataList" />. </param>
		/// <param name="commandSource">The source of the command. </param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the original event data. </param>
		// Token: 0x06001A62 RID: 6754 RVA: 0x00043413 File Offset: 0x00041613
		public DataListCommandEventArgs(DataListItem item, object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		/// <summary>Gets the item containing the command source in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0004342A File Offset: 0x0004162A
		public DataListItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the source of the command.</summary>
		/// <returns>The source of the command.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00043432 File Offset: 0x00041632
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x040016A6 RID: 5798
		private DataListItem item;

		// Token: 0x040016A7 RID: 5799
		private object commandSource;
	}
}
