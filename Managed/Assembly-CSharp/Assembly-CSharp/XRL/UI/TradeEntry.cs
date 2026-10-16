using System;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x0200149B RID: 5275
	public class TradeEntry
	{
		// Token: 0x0600CF34 RID: 53044 RVA: 0x004B6ED3 File Offset: 0x004B50D3
		public TradeEntry(string CategoryName)
		{
			this.CategoryName = CategoryName;
		}

		// Token: 0x0600CF35 RID: 53045 RVA: 0x004B6EED File Offset: 0x004B50ED
		public TradeEntry(GameObject GO)
		{
			this.GO = GO;
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600CF36 RID: 53046 RVA: 0x004B6F07 File Offset: 0x004B5107
		public string NameForSort
		{
			get
			{
				if (this.GO != null)
				{
					return this.GO.GetCachedDisplayNameForSort();
				}
				return this.CategoryName;
			}
		}

		// Token: 0x0400506F RID: 20591
		public GameObject GO;

		// Token: 0x04005070 RID: 20592
		public string CategoryName = "";
	}
}
