using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAD RID: 2733
	public class SnapjawFortMaker
	{
		// Token: 0x06006F55 RID: 28501 RVA: 0x0026CD9F File Offset: 0x0026AF9F
		public bool BuildZone(Zone Z)
		{
			return new FortMaker().BuildZone(Z, true, "BrinestalkWall", "SnapjawFortGlobals", null);
		}
	}
}
