using System;
using Qud.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014F5 RID: 5365
	public class FilterBarCategoryButtonData : FrameworkDataElement
	{
		// Token: 0x0600D19F RID: 53663 RVA: 0x004DE131 File Offset: 0x004DC331
		public FilterBarCategoryButtonData()
		{
		}

		// Token: 0x0600D1A0 RID: 53664 RVA: 0x004DE139 File Offset: 0x004DC339
		public FilterBarCategoryButtonData(string category, Action<string> onSelect)
		{
			this.category = category;
			this.onSelect = onSelect;
		}

		// Token: 0x040052D5 RID: 21205
		public FilterBarCategoryButton button;

		// Token: 0x040052D6 RID: 21206
		public string category;

		// Token: 0x040052D7 RID: 21207
		public string tooltip;

		// Token: 0x040052D8 RID: 21208
		public Action<string> onSelect;
	}
}
