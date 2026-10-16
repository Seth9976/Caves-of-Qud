using System;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200002F RID: 47
	public readonly struct TerrainTileCoord
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00006783 File Offset: 0x00004983
		public TerrainTileCoord(int tileX, int tileZ)
		{
			this.tileX = tileX;
			this.tileZ = tileZ;
		}

		// Token: 0x040000BB RID: 187
		public readonly int tileX;

		// Token: 0x040000BC RID: 188
		public readonly int tileZ;
	}
}
