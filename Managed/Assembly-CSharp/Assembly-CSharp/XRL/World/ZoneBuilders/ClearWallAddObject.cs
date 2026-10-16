using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A61 RID: 2657
	public class ClearWallAddObject
	{
		// Token: 0x06006E38 RID: 28216 RVA: 0x0025A2E3 File Offset: 0x002584E3
		public bool BuildZone(Zone Z)
		{
			Cell cell = Z.GetCell(this.x.RollCached(), this.y.RollCached());
			cell.ClearWalls();
			cell.AddObject(this.obj, null, null, null, null);
			return true;
		}

		// Token: 0x0400326F RID: 12911
		public string x = "-1";

		// Token: 0x04003270 RID: 12912
		public string y = "-1";

		// Token: 0x04003271 RID: 12913
		public string obj;
	}
}
