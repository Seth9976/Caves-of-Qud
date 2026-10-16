using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public struct Mesh_Extents
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0002E5A7 File Offset: 0x0002C7A7
		public Mesh_Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Min (",
				this.min.x.ToString("f2"),
				", ",
				this.min.y.ToString("f2"),
				")   Max (",
				this.max.x.ToString("f2"),
				", ",
				this.max.y.ToString("f2"),
				")"
			});
		}

		// Token: 0x0400052F RID: 1327
		public Vector2 min;

		// Token: 0x04000530 RID: 1328
		public Vector2 max;
	}
}
