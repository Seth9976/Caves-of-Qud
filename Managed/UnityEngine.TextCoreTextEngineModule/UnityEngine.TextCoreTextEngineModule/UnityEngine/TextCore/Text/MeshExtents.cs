using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	internal struct MeshExtents
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0001FB36 File Offset: 0x0001DD36
		public MeshExtents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001FB48 File Offset: 0x0001DD48
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

		// Token: 0x040002F5 RID: 757
		public Vector2 min;

		// Token: 0x040002F6 RID: 758
		public Vector2 max;
	}
}
