using System;

namespace XRL.World.ZoneBuilders.Utility
{
	// Token: 0x02000B17 RID: 2839
	public class FloatNoiseMapNode
	{
		// Token: 0x0600720A RID: 29194 RVA: 0x002AFFFC File Offset: 0x002AE1FC
		public FloatNoiseMapNode(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.depth = -1f;
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x002B0028 File Offset: 0x002AE228
		public FloatNoiseMapNode(int x, int y, float depth)
		{
			this.x = x;
			this.y = y;
			this.depth = depth;
		}

		// Token: 0x040034E0 RID: 13536
		public int x;

		// Token: 0x040034E1 RID: 13537
		public int y;

		// Token: 0x040034E2 RID: 13538
		public float depth = -1f;
	}
}
