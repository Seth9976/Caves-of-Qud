using System;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x0200045B RID: 1115
	public class CharacterAttributeLineData : PooledFrameworkDataElement<CharacterAttributeLineData>
	{
		// Token: 0x06002A40 RID: 10816 RVA: 0x000DE059 File Offset: 0x000DC259
		public CharacterAttributeLineData set(Statistic data, GameObject go, string stat)
		{
			this.data = data;
			this.go = go;
			this.stat = stat;
			return this;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000DE071 File Offset: 0x000DC271
		public override void free()
		{
			this.data = null;
			this.go = null;
			this.stat = null;
			base.free();
		}

		// Token: 0x04001621 RID: 5665
		public CharacterAttributeLineData.Category category;

		// Token: 0x04001622 RID: 5666
		public Statistic data;

		// Token: 0x04001623 RID: 5667
		public GameObject go;

		// Token: 0x04001624 RID: 5668
		public string stat;

		// Token: 0x02001870 RID: 6256
		public enum Category
		{
			// Token: 0x0400626E RID: 25198
			primary,
			// Token: 0x0400626F RID: 25199
			secondary,
			// Token: 0x04006270 RID: 25200
			resistance
		}
	}
}
