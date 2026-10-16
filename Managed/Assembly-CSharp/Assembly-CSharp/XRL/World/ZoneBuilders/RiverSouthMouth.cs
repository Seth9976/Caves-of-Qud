using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A74 RID: 2676
	public class RiverSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006E67 RID: 28263 RVA: 0x0025C178 File Offset: 0x0025A378
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "River", "South", "Mouth");
		}
	}
}
