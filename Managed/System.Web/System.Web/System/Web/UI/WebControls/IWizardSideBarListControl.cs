using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020002D4 RID: 724
	internal interface IWizardSideBarListControl
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001B41 RID: 6977
		// (set) Token: 0x06001B42 RID: 6978
		object DataSource { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001B43 RID: 6979
		IEnumerable Items { get; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001B44 RID: 6980
		// (set) Token: 0x06001B45 RID: 6981
		ITemplate ItemTemplate { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001B46 RID: 6982
		// (set) Token: 0x06001B47 RID: 6983
		int SelectedIndex { get; set; }

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001B48 RID: 6984
		// (remove) Token: 0x06001B49 RID: 6985
		event CommandEventHandler ItemCommand;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001B4A RID: 6986
		// (remove) Token: 0x06001B4B RID: 6987
		event EventHandler<WizardSideBarListControlItemEventArgs> ItemDataBound;

		// Token: 0x06001B4C RID: 6988
		void DataBind();
	}
}
