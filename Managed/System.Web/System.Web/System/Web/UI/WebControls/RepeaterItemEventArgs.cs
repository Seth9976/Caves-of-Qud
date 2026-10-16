using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.Repeater.ItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.Repeater" />.</summary>
	// Token: 0x020002FD RID: 765
	public class RepeaterItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> class.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event. The <see cref="P:System.Web.UI.WebControls.RepeaterItemEventArgs.Item" /> property is set to this value. </param>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x0004380C File Offset: 0x00041A0C
		public RepeaterItemEventArgs(RepeaterItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</returns>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0004381B File Offset: 0x00041A1B
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001749 RID: 5961
		private RepeaterItem item;
	}
}
