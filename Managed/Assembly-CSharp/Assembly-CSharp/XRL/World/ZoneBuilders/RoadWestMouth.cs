using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7C RID: 2684
	public class RoadWestMouth : IConnectionBuilder
	{
		// Token: 0x06006E79 RID: 28281 RVA: 0x0025C81F File Offset: 0x0025AA1F
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Road", "West", "Mouth");
		}
	}
}
