using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public struct VertexGradient
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0002E348 File Offset: 0x0002C548
		public VertexGradient(Color color)
		{
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002E366 File Offset: 0x0002C566
		public VertexGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x04000514 RID: 1300
		public Color topLeft;

		// Token: 0x04000515 RID: 1301
		public Color topRight;

		// Token: 0x04000516 RID: 1302
		public Color bottomLeft;

		// Token: 0x04000517 RID: 1303
		public Color bottomRight;
	}
}
