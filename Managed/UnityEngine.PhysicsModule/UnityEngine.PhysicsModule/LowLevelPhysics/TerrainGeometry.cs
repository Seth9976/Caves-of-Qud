using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x0200005E RID: 94
	public struct TerrainGeometry : IGeometry
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000C045 File Offset: 0x0000A245
		public GeometryType GeometryType
		{
			get
			{
				return GeometryType.Terrain;
			}
		}

		// Token: 0x04000183 RID: 387
		private IntPtr m_TerrainData;

		// Token: 0x04000184 RID: 388
		private float m_HeightScale;

		// Token: 0x04000185 RID: 389
		private float m_RowScale;

		// Token: 0x04000186 RID: 390
		private float m_ColumnScale;

		// Token: 0x04000187 RID: 391
		private byte m_TerrainFlags;

		// Token: 0x04000188 RID: 392
		private byte pad1;

		// Token: 0x04000189 RID: 393
		private short pad2;

		// Token: 0x0400018A RID: 394
		private uint pad3;
	}
}
