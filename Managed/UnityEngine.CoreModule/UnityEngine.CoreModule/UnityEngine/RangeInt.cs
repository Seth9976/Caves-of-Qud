using System;

namespace UnityEngine
{
	// Token: 0x020002A4 RID: 676
	public struct RangeInt
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x00041350 File Offset: 0x0003F550
		public int end
		{
			get
			{
				return this.start + this.length;
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0004136F File Offset: 0x0003F56F
		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x040009C9 RID: 2505
		public int start;

		// Token: 0x040009CA RID: 2506
		public int length;
	}
}
