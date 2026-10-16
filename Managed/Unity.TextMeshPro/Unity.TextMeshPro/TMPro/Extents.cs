using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007B RID: 123
	public struct Extents
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
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

		// Token: 0x0400052B RID: 1323
		internal static Extents zero = new Extents(Vector2.zero, Vector2.zero);

		// Token: 0x0400052C RID: 1324
		internal static Extents uninitialized = new Extents(new Vector2(32767f, 32767f), new Vector2(-32767f, -32767f));

		// Token: 0x0400052D RID: 1325
		public Vector2 min;

		// Token: 0x0400052E RID: 1326
		public Vector2 max;
	}
}
