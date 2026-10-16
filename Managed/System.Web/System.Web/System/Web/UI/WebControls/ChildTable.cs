using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200034A RID: 842
	internal class ChildTable : Table
	{
		// Token: 0x06001F22 RID: 7970 RVA: 0x0004CD8B File Offset: 0x0004AF8B
		public ChildTable(Control parent)
		{
			this.parent = parent;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0004CD9A File Offset: 0x0004AF9A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.ID == null)
			{
				writer.AddAttribute("id", this.parent.ClientID);
			}
		}

		// Token: 0x0400187E RID: 6270
		private Control parent;
	}
}
