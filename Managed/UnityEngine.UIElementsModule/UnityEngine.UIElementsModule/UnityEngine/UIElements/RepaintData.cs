using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000304 RID: 772
	internal class RepaintData
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00055F84 File Offset: 0x00054184
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x00055F8C File Offset: 0x0005418C
		public Matrix4x4 currentOffset { get; set; } = Matrix4x4.identity;

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00055F95 File Offset: 0x00054195
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x00055F9D File Offset: 0x0005419D
		public Vector2 mousePosition { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00055FA6 File Offset: 0x000541A6
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00055FAE File Offset: 0x000541AE
		public Rect currentWorldClip { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00055FB7 File Offset: 0x000541B7
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00055FBF File Offset: 0x000541BF
		public Event repaintEvent { get; set; }
	}
}
