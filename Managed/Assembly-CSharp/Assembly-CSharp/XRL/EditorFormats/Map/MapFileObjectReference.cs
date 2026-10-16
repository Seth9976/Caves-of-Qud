using System;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C9 RID: 1481
	public struct MapFileObjectReference
	{
		// Token: 0x06003F06 RID: 16134 RVA: 0x00136D82 File Offset: 0x00134F82
		public MapFileObjectReference(MapFileRegion region, int x, int y, MapFileCell cell, MapFileObjectBlueprint blueprint)
		{
			this.region = region;
			this.x = x;
			this.y = y;
			this.cell = cell;
			this.blueprint = blueprint;
		}

		// Token: 0x04001F26 RID: 7974
		public readonly MapFileRegion region;

		// Token: 0x04001F27 RID: 7975
		public readonly int x;

		// Token: 0x04001F28 RID: 7976
		public readonly int y;

		// Token: 0x04001F29 RID: 7977
		public readonly MapFileCell cell;

		// Token: 0x04001F2A RID: 7978
		public readonly MapFileObjectBlueprint blueprint;
	}
}
