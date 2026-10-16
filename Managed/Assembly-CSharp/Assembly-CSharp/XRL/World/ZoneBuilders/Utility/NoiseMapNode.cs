using System;

namespace XRL.World.ZoneBuilders.Utility
{
	// Token: 0x02000B19 RID: 2841
	public class NoiseMapNode
	{
		// Token: 0x06007215 RID: 29205 RVA: 0x002B0E07 File Offset: 0x002AF007
		public NoiseMapNode(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.depth = -1;
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x002B0E2B File Offset: 0x002AF02B
		public NoiseMapNode(int x, int y, int depth)
		{
			this.x = x;
			this.y = y;
			this.depth = depth;
		}

		// Token: 0x040034EB RID: 13547
		public int x;

		// Token: 0x040034EC RID: 13548
		public int y;

		// Token: 0x040034ED RID: 13549
		public int depth = -1;
	}
}
