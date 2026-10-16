using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7A RID: 2682
	public class RoadSouthMouth : IConnectionBuilder
	{
		// Token: 0x06006E75 RID: 28277 RVA: 0x0025C7E4 File Offset: 0x0025A9E4
		public bool BuildZone(Zone Z)
		{
			return base.ConnectionMouth(Z, "Road", "South", "Mouth");
		}
	}
}
