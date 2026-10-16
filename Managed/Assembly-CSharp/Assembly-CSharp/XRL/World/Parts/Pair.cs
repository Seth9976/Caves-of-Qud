using System;

namespace XRL.World.Parts
{
	// Token: 0x0200111C RID: 4380
	[Serializable]
	public class Pair
	{
		// Token: 0x0600AD53 RID: 44371 RVA: 0x003FA807 File Offset: 0x003F8A07
		public Pair()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x0600AD54 RID: 44372 RVA: 0x003FA81D File Offset: 0x003F8A1D
		public Pair(int xp, int yp)
		{
			this.x = xp;
			this.y = yp;
		}

		// Token: 0x0400479F RID: 18335
		public int x;

		// Token: 0x040047A0 RID: 18336
		public int y;
	}
}
