using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000325 RID: 805
	internal sealed class WizardSideBarListControlItem
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00043D10 File Offset: 0x00041F10
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00043D18 File Offset: 0x00041F18
		public object DataItem { get; private set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00043D21 File Offset: 0x00041F21
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x00043D29 File Offset: 0x00041F29
		public ListItemType ItemType { get; private set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00043D32 File Offset: 0x00041F32
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00043D3A File Offset: 0x00041F3A
		public int ItemIndex { get; private set; }

		// Token: 0x06001BEA RID: 7146 RVA: 0x00043D43 File Offset: 0x00041F43
		public WizardSideBarListControlItem(object dataItem, ListItemType itemType, int itemIndex, Control container)
		{
			this.DataItem = dataItem;
			this.ItemType = itemType;
			this.ItemIndex = itemIndex;
			this._container = container;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00043D68 File Offset: 0x00041F68
		internal Control FindControl(string id)
		{
			return this._container.FindControl(id);
		}

		// Token: 0x040017D0 RID: 6096
		private Control _container;
	}
}
