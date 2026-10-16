using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A78 RID: 2680
	public class RoadEastMouth : IConnectionBuilder
	{
		// Token: 0x06006E71 RID: 28273 RVA: 0x0025C7A4 File Offset: 0x0025A9A4
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Road", "East", "Mouth");
		}
	}
}
