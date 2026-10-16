using System;

namespace QupKit
{
	// Token: 0x020003B0 RID: 944
	public class LayoutSides
	{
		// Token: 0x0600231D RID: 8989 RVA: 0x000B25D5 File Offset: 0x000B07D5
		public LayoutSides()
		{
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000B25DD File Offset: 0x000B07DD
		public LayoutSides(float Top, float Right, float Bottom, float Left)
		{
			this.Top = Top;
			this.Right = Right;
			this.Bottom = Bottom;
			this.Left = Left;
		}

		// Token: 0x040011BE RID: 4542
		public float Top;

		// Token: 0x040011BF RID: 4543
		public float Bottom;

		// Token: 0x040011C0 RID: 4544
		public float Left;

		// Token: 0x040011C1 RID: 4545
		public float Right;
	}
}
