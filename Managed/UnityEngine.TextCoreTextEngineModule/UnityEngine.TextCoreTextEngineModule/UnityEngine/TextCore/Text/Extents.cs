using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000020 RID: 32
	internal struct Extents
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000046D0 File Offset: 0x000028D0
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000046E4 File Offset: 0x000028E4
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

		// Token: 0x04000093 RID: 147
		public Vector2 min;

		// Token: 0x04000094 RID: 148
		public Vector2 max;
	}
}
