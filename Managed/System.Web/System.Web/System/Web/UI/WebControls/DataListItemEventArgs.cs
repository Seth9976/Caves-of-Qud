using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataList.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.DataList.ItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000296 RID: 662
	public class DataListItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> class.</summary>
		/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object that represents an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. </param>
		// Token: 0x06001A69 RID: 6761 RVA: 0x0004343A File Offset: 0x0004163A
		public DataListItemEventArgs(DataListItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the referenced item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the event is raised.</summary>
		/// <returns>The referenced item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the event is raised.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00043449 File Offset: 0x00041649
		public DataListItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040016A8 RID: 5800
		private DataListItem item;
	}
}
