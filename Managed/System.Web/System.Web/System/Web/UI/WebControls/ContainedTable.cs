using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000353 RID: 851
	internal class ContainedTable : Table
	{
		// Token: 0x06001F86 RID: 8070 RVA: 0x0004DA1B File Offset: 0x0004BC1B
		public ContainedTable(WebControl container)
		{
			this._container = container;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0004DA2A File Offset: 0x0004BC2A
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.ControlStyle.CopyFrom(this._container.ControlStyle);
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this._container.ClientID);
		}

		// Token: 0x04001882 RID: 6274
		private WebControl _container;
	}
}
