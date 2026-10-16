using System;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C8 RID: 1480
	public struct MapFileCellReference
	{
		// Token: 0x06003F05 RID: 16133 RVA: 0x00136D63 File Offset: 0x00134F63
		public MapFileCellReference(MapFileRegion region, int x, int y, MapFileCell cell)
		{
			this.region = region;
			this.x = x;
			this.y = y;
			this.cell = cell;
		}

		// Token: 0x04001F22 RID: 7970
		public readonly MapFileRegion region;

		// Token: 0x04001F23 RID: 7971
		public readonly int x;

		// Token: 0x04001F24 RID: 7972
		public readonly int y;

		// Token: 0x04001F25 RID: 7973
		public readonly MapFileCell cell;
	}
}
