using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000326 RID: 806
	internal sealed class WizardSideBarListControlItemEventArgs : EventArgs
	{
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00043D76 File Offset: 0x00041F76
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x00043D7E File Offset: 0x00041F7E
		public WizardSideBarListControlItem Item { get; private set; }

		// Token: 0x06001BEE RID: 7150 RVA: 0x00043D87 File Offset: 0x00041F87
		public WizardSideBarListControlItemEventArgs(WizardSideBarListControlItem item)
		{
			this.Item = item;
		}
	}
}
