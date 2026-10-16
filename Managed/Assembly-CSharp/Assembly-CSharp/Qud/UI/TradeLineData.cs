using System;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000483 RID: 1155
	public class TradeLineData : PooledFrameworkDataElement<TradeLineData>
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x000ED770 File Offset: 0x000EB970
		public TradeLineData set(int side = 0, TradeLineDataType type = TradeLineDataType.Item, TradeLineDataStyle style = TradeLineDataStyle.Interact, GameObject go = null, TradeEntry entry = null, bool traderInventory = false, int numberSelected = 0, string category = null, bool collapsed = false, bool indent = false, char quickKey = '\0', string hotkeyDescription = null)
		{
			this.side = side;
			this.type = type;
			this.style = style;
			this.go = go;
			this.category = category;
			this.collapsed = collapsed;
			this.indent = indent;
			this.quickKey = quickKey;
			this.hotkeyDescription = hotkeyDescription;
			this.traderInventory = traderInventory;
			this.numberSelected = numberSelected;
			this.entry = entry;
			return this;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000ED7DB File Offset: 0x000EB9DB
		public override void free()
		{
			this.update = null;
			this.entry = null;
			this.go = null;
			base.free();
		}

		// Token: 0x0400185A RID: 6234
		public TradeLineDataType type;

		// Token: 0x0400185B RID: 6235
		public TradeLineDataStyle style;

		// Token: 0x0400185C RID: 6236
		public GameObject go;

		// Token: 0x0400185D RID: 6237
		public bool traderInventory;

		// Token: 0x0400185E RID: 6238
		public TradeEntry entry;

		// Token: 0x0400185F RID: 6239
		public int numberSelected;

		// Token: 0x04001860 RID: 6240
		public string category;

		// Token: 0x04001861 RID: 6241
		public int numberInCategory;

		// Token: 0x04001862 RID: 6242
		public bool collapsed;

		// Token: 0x04001863 RID: 6243
		public bool indent;

		// Token: 0x04001864 RID: 6244
		public char quickKey;

		// Token: 0x04001865 RID: 6245
		public string hotkeyDescription;

		// Token: 0x04001866 RID: 6246
		public int side;

		// Token: 0x04001867 RID: 6247
		public Action update;
	}
}
