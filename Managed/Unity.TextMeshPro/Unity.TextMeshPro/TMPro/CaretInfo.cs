using System;

namespace TMPro
{
	// Token: 0x0200006C RID: 108
	public struct CaretInfo
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0002BC2C File Offset: 0x00029E2C
		public CaretInfo(int index, CaretPosition position)
		{
			this.index = index;
			this.position = position;
		}

		// Token: 0x040004DA RID: 1242
		public int index;

		// Token: 0x040004DB RID: 1243
		public CaretPosition position;
	}
}
